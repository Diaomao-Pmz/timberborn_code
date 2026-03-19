using System;
using System.Linq;
using System.Threading;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.MultithreadingAnalysis;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.MultithreadingAnalysisUI
{
	// Token: 0x0200000D RID: 13
	public class TaskSnapshotPanel : ILoadableSingleton, ILateUpdatableSingleton, IInputProcessor
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002C29 File Offset: 0x00000E29
		public TaskSnapshotPanel(VisualElementLoader visualElementLoader, RootVisualElementProvider rootVisualElementProvider, SnapshotCollector snapshotCollector, InputService inputService, SnapshotTimeline snapshotTimeline)
		{
			this._visualElementLoader = visualElementLoader;
			this._rootVisualElementProvider = rootVisualElementProvider;
			this._snapshotCollector = snapshotCollector;
			this._inputService = inputService;
			this._snapshotTimeline = snapshotTimeline;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002C58 File Offset: 0x00000E58
		public void Load()
		{
			this._root = this._rootVisualElementProvider.Create("TaskSnapshotPanel", "Common/MultithreadingAnalysis/TaskSnapshotContainer", 2);
			this._panel = this._visualElementLoader.LoadVisualElement("Common/MultithreadingAnalysis/TaskSnapshotPanel");
			UQueryExtensions.Q<VisualElement>(this._root, "TaskSnapshotContainer", null).Add(this._panel);
			this._minTime = UQueryExtensions.Q<Label>(this._panel, "MinTime", null);
			this._maxTime = UQueryExtensions.Q<Label>(this._panel, "MaxTime", null);
			this._scrollView = UQueryExtensions.Q<ScrollView>(this._panel, "ScrollView", null);
			this._scrollView.horizontalScrollerVisibility = 0;
			this._scrollView.verticalScrollerVisibility = 0;
			this._scrollView.mode = 2;
			this._scaleSlider = UQueryExtensions.Q<Slider>(this._panel, "ScaleSlider", null);
			this._scaleSlider.lowValue = (float)TaskSnapshotPanel.MinScale;
			this._scaleSlider.highValue = (float)TaskSnapshotPanel.MaxScale;
			INotifyValueChangedExtensions.RegisterValueChangedCallback<float>(this._scaleSlider, delegate(ChangeEvent<float> evt)
			{
				this.SetScale(evt.newValue);
			});
			this._scaleValue = UQueryExtensions.Q<TextField>(this._panel, "ScaleValue", null);
			this._scaleValue.RegisterCallback<FocusOutEvent>(delegate(FocusOutEvent _)
			{
				int num;
				if (int.TryParse(this._scaleValue.value, out num))
				{
					this.SetScale((float)num);
				}
			}, 0);
			this._showMarkers = UQueryExtensions.Q<Toggle>(this._panel, "ShowMarkers", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._showMarkers, new EventCallback<ChangeEvent<bool>>(this.OnShowMarkersChanged));
			this._taskCount = UQueryExtensions.Q<Label>(this._panel, "TaskCount", null);
			this._totalTaskTime = UQueryExtensions.Q<Label>(this._panel, "TotalTaskTime", null);
			this._totalIdleTime = UQueryExtensions.Q<Label>(this._panel, "TotalIdleTime", null);
			UQueryExtensions.Q<Button>(this._panel, "FitButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.SetScale((float)TaskSnapshotPanel.MinScale);
			}, 0);
			UQueryExtensions.Q<Button>(this._panel, "TakeSnapshot", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.TakeNextSnapshot();
			}, 0);
			UQueryExtensions.Q<Button>(this._panel, "CloseButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.Close();
			}, 0);
			this._root.ToggleDisplayStyle(false);
			this._snapshotTimeline.Initialize(this._panel);
			this._snapshotCollector.SnapshotCollected += this.OnSnapshotsCollected;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002EAD File Offset: 0x000010AD
		public void LateUpdateSingleton()
		{
			if (this._isOpened)
			{
				if (this._shouldResetScale)
				{
					this._shouldResetScale = false;
					this.SetScale((float)TaskSnapshotPanel.MinScale);
				}
				this.UpdateMinMaxLabels();
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002ED8 File Offset: 0x000010D8
		public bool ProcessInput()
		{
			if (this._inputService.UICancel)
			{
				this.Close();
				return true;
			}
			return false;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002EF0 File Offset: 0x000010F0
		public void Close()
		{
			if (this._isOpened)
			{
				this._snapshot = null;
				this._snapshotTimeline.Close();
				this._inputService.RemoveInputProcessor(this);
				this._root.ToggleDisplayStyle(false);
				this._isOpened = false;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002F2C File Offset: 0x0000112C
		public void Open(Snapshot snapshot)
		{
			this._snapshot = snapshot;
			this._inputService.AddInputProcessor(this);
			this._showMarkers.SetValueWithoutNotify(false);
			this._threadCount = (from s in this._snapshot.TaskSamples
			group s by s.Thread).Count<IGrouping<Thread, TaskSample>>();
			this.CalculateTime();
			this.UpdateStats();
			this._snapshotTimeline.Open(snapshot);
			this._root.ToggleDisplayStyle(true);
			this._shouldResetScale = true;
			this._isOpened = true;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002FCC File Offset: 0x000011CC
		public void CalculateTime()
		{
			long num = (from t in this._snapshot.TaskSamples
			select t.StartTime).Min();
			long num2 = (from t in this._snapshot.TaskSamples
			select t.EndTime).Max();
			if (this._showMarkers.value)
			{
				num = Math.Min((from t in this._snapshot.Markers
				select t.Timestamp).Min(), num);
				num2 = Math.Max((from t in this._snapshot.Markers
				select t.Timestamp).Max(), num2);
			}
			this._snapshotLength = num2 - num;
			this._referenceTimestamp = num;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000030F0 File Offset: 0x000012F0
		public void UpdateStats()
		{
			this._taskCount.text = string.Format("Tasks: {0}", this._snapshot.TaskSamples.Count);
			double num = (from sample in this._snapshot.TaskSamples
			select sample.EndTime - sample.StartTime).Sum(new Func<long, double>(TaskSampleCalculator.TicksToMs));
			this._totalTaskTime.text = string.Format("Total task time: {0:0.000}ms", num);
			double num2 = TaskSampleCalculator.TicksToMs(this._snapshotLength * (long)this._threadCount) - num;
			this._totalIdleTime.text = string.Format("Total idle time: {0:0.000}ms", num2);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000031BC File Offset: 0x000013BC
		public void SetScale(float scale)
		{
			Vector2 vector = this._scrollView.scrollOffset / (float)this._scale;
			this._scale = Mathf.Clamp((int)scale, TaskSnapshotPanel.MinScale, TaskSnapshotPanel.MaxScale);
			this._snapshotTimeline.SetScale(this.GetPixelScale(), this._referenceTimestamp, this._snapshotLength);
			this._scrollView.scrollOffset = vector * (float)this._scale;
			this._scaleSlider.SetValueWithoutNotify((float)this._scale);
			this._scaleValue.SetValueWithoutNotify(this._scale.ToString());
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003255 File Offset: 0x00001455
		public float GetPixelScale()
		{
			return this._scrollView.resolvedStyle.width / (float)this._snapshotLength * (float)this._scale / 100f;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003280 File Offset: 0x00001480
		public void UpdateMinMaxLabels()
		{
			float x = this._scrollView.scrollOffset.x;
			long ticks = (long)(Mathf.Max(0f, x) / this.GetPixelScale());
			float width = this._scrollView.contentContainer.resolvedStyle.width;
			float width2 = this._scrollView.contentViewport.layout.width;
			float num = Math.Max(1f, width - width2);
			float num2 = Mathf.Clamp01(x / num);
			long ticks2 = (long)Mathf.Lerp((float)this._snapshotLength / ((float)this._scale / 100f), (float)this._snapshotLength, num2);
			this._minTime.text = string.Format("{0:0.000}ms", TaskSampleCalculator.TicksToMs(ticks));
			this._maxTime.text = string.Format("{0:0.000}ms", TaskSampleCalculator.TicksToMs(ticks2));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003363 File Offset: 0x00001563
		public void OnSnapshotsCollected(object sender, Snapshot snapshot)
		{
			this.Close();
			this.Open(snapshot);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003372 File Offset: 0x00001572
		public void TakeNextSnapshot()
		{
			this._snapshotCollector.ScheduleCollection(this._snapshot.Ticks);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000338C File Offset: 0x0000158C
		public void OnShowMarkersChanged(ChangeEvent<bool> showMarkers)
		{
			this.CalculateTime();
			this.UpdateStats();
			this.UpdateMinMaxLabels();
			this._snapshotTimeline.SetScale(this.GetPixelScale(), this._referenceTimestamp, this._snapshotLength);
			this._snapshotTimeline.SetMarkerVisibility(showMarkers.newValue);
		}

		// Token: 0x04000021 RID: 33
		public static readonly int MinScale = 100;

		// Token: 0x04000022 RID: 34
		public static readonly int MaxScale = 50000;

		// Token: 0x04000023 RID: 35
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000024 RID: 36
		public readonly RootVisualElementProvider _rootVisualElementProvider;

		// Token: 0x04000025 RID: 37
		public readonly SnapshotCollector _snapshotCollector;

		// Token: 0x04000026 RID: 38
		public readonly InputService _inputService;

		// Token: 0x04000027 RID: 39
		public readonly SnapshotTimeline _snapshotTimeline;

		// Token: 0x04000028 RID: 40
		public VisualElement _root;

		// Token: 0x04000029 RID: 41
		public VisualElement _panel;

		// Token: 0x0400002A RID: 42
		public ScrollView _scrollView;

		// Token: 0x0400002B RID: 43
		public Slider _scaleSlider;

		// Token: 0x0400002C RID: 44
		public TextField _scaleValue;

		// Token: 0x0400002D RID: 45
		public Toggle _showMarkers;

		// Token: 0x0400002E RID: 46
		public Label _taskCount;

		// Token: 0x0400002F RID: 47
		public Label _totalTaskTime;

		// Token: 0x04000030 RID: 48
		public Label _totalIdleTime;

		// Token: 0x04000031 RID: 49
		public Label _minTime;

		// Token: 0x04000032 RID: 50
		public Label _maxTime;

		// Token: 0x04000033 RID: 51
		public Snapshot _snapshot;

		// Token: 0x04000034 RID: 52
		public bool _shouldResetScale;

		// Token: 0x04000035 RID: 53
		public int _scale;

		// Token: 0x04000036 RID: 54
		public long _referenceTimestamp;

		// Token: 0x04000037 RID: 55
		public long _snapshotLength;

		// Token: 0x04000038 RID: 56
		public int _threadCount;

		// Token: 0x04000039 RID: 57
		public bool _isOpened;
	}
}
