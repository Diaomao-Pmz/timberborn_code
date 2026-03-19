using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.MultithreadingAnalysis;
using UnityEngine.UIElements;

namespace Timberborn.MultithreadingAnalysisUI
{
	// Token: 0x02000007 RID: 7
	public class SnapshotTimeline : IInputProcessor
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002240 File Offset: 0x00000440
		public SnapshotTimeline(InputService inputService, ThreadViewFactory threadViewFactory, TaskViewFactory taskViewFactory, MarkerViewFactory markerViewFactory, TaskColorProvider taskColorProvider)
		{
			this._inputService = inputService;
			this._threadViewFactory = threadViewFactory;
			this._taskViewFactory = taskViewFactory;
			this._markerViewFactory = markerViewFactory;
			this._taskColorProvider = taskColorProvider;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002299 File Offset: 0x00000499
		public void Initialize(VisualElement root)
		{
			this._threadLabels = UQueryExtensions.Q<VisualElement>(root, "ThreadLabels", null);
			this._timeline = UQueryExtensions.Q<VisualElement>(root, "Timeline", null);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022BF File Offset: 0x000004BF
		public bool ProcessInput()
		{
			if (this._transparencyEnabled && this._inputService.Cancel)
			{
				this.ResetTransparency();
				return true;
			}
			return false;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022E0 File Offset: 0x000004E0
		public void Open(Snapshot snapshot)
		{
			this._taskColorProvider.InitializeFromSamples(snapshot.TaskSamples);
			foreach (IGrouping<Thread, TaskSample> grouping2 in from sample in snapshot.TaskSamples
			group sample by sample.Thread into grouping
			orderby grouping.Key.DisplayName()
			select grouping)
			{
				this.CreateTaskViews(grouping2.Key, grouping2);
			}
			this.CreateMarkerViews(snapshot.Markers);
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023B0 File Offset: 0x000005B0
		public void Close()
		{
			foreach (TaskView taskView in this._taskViews)
			{
				taskView.TaskViewClicked = (EventHandler)Delegate.Remove(taskView.TaskViewClicked, new EventHandler(this.OnTaskViewClicked));
			}
			this._threadViews.Clear();
			this._taskViews.Clear();
			this._markerViews.Clear();
			this._threadLabels.Clear();
			this._timeline.Clear();
			this._inputService.RemoveInputProcessor(this);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002460 File Offset: 0x00000660
		public void SetScale(float pixelScale, long referenceTimestamp, long snapshotLength)
		{
			foreach (ThreadView threadView in this._threadViews)
			{
				threadView.SetScale(pixelScale, snapshotLength);
			}
			foreach (TaskView taskView in this._taskViews)
			{
				taskView.SetScale(pixelScale, referenceTimestamp);
			}
			foreach (MarkerView markerView in this._markerViews)
			{
				markerView.SetScale(pixelScale, referenceTimestamp);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002534 File Offset: 0x00000734
		public void SetMarkerVisibility(bool isVisible)
		{
			foreach (MarkerView markerView in this._markerViews)
			{
				markerView.Root.ToggleDisplayStyle(isVisible);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000258C File Offset: 0x0000078C
		public void CreateTaskViews(Thread thread, IEnumerable<TaskSample> taskSamples)
		{
			ThreadView threadView = this._threadViewFactory.CreateThreadView();
			this._timeline.Add(threadView.Root);
			this._threadViews.Add(threadView);
			this._threadLabels.Add(SnapshotTimeline.CreateThreadLabel(thread));
			foreach (TaskSample task in taskSamples)
			{
				TaskView taskView = this._taskViewFactory.CreateTask(task);
				TaskView taskView2 = taskView;
				taskView2.TaskViewClicked = (EventHandler)Delegate.Combine(taskView2.TaskViewClicked, new EventHandler(this.OnTaskViewClicked));
				threadView.AddTaskView(taskView.Root);
				this._taskViews.Add(taskView);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002650 File Offset: 0x00000850
		public void CreateMarkerViews(IEnumerable<Marker> markers)
		{
			foreach (Marker marker in markers)
			{
				MarkerView markerView = this._markerViewFactory.CreateMarker(marker);
				markerView.Root.ToggleDisplayStyle(false);
				this._timeline.Add(markerView.Root);
				this._markerViews.Add(markerView);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026C8 File Offset: 0x000008C8
		public static VisualElement CreateThreadLabel(Thread thread)
		{
			Label label = new Label(thread.DisplayName());
			label.AddToClassList(SnapshotTimeline.ThreadLabelClass);
			return label;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000026E0 File Offset: 0x000008E0
		public void OnTaskViewClicked(object sender, EventArgs e)
		{
			Type genericType = ((TaskView)sender).TaskSample.GenericType;
			foreach (TaskView taskView in this._taskViews)
			{
				if (taskView.TaskSample.GenericType == genericType)
				{
					taskView.UnsetTransparent();
				}
				else
				{
					taskView.SetTransparent();
				}
			}
			this._transparencyEnabled = true;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000276C File Offset: 0x0000096C
		public void ResetTransparency()
		{
			foreach (TaskView taskView in this._taskViews)
			{
				taskView.UnsetTransparent();
			}
			this._transparencyEnabled = false;
		}

		// Token: 0x0400000B RID: 11
		public static readonly string ThreadLabelClass = "thread-label";

		// Token: 0x0400000C RID: 12
		public readonly InputService _inputService;

		// Token: 0x0400000D RID: 13
		public readonly ThreadViewFactory _threadViewFactory;

		// Token: 0x0400000E RID: 14
		public readonly TaskViewFactory _taskViewFactory;

		// Token: 0x0400000F RID: 15
		public readonly MarkerViewFactory _markerViewFactory;

		// Token: 0x04000010 RID: 16
		public readonly TaskColorProvider _taskColorProvider;

		// Token: 0x04000011 RID: 17
		public readonly List<ThreadView> _threadViews = new List<ThreadView>();

		// Token: 0x04000012 RID: 18
		public readonly List<TaskView> _taskViews = new List<TaskView>();

		// Token: 0x04000013 RID: 19
		public readonly List<MarkerView> _markerViews = new List<MarkerView>();

		// Token: 0x04000014 RID: 20
		public VisualElement _threadLabels;

		// Token: 0x04000015 RID: 21
		public VisualElement _timeline;

		// Token: 0x04000016 RID: 22
		public bool _transparencyEnabled;
	}
}
