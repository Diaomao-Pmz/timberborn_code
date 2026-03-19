using System;
using Timberborn.CoreUI;
using Timberborn.MultithreadingAnalysis;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.MultithreadingAnalysisUI
{
	// Token: 0x0200000F RID: 15
	public class TaskView
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000347B File Offset: 0x0000167B
		public VisualElement Root { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003483 File Offset: 0x00001683
		public TaskSample TaskSample { get; }

		// Token: 0x0600004A RID: 74 RVA: 0x0000348B File Offset: 0x0000168B
		public TaskView(VisualElement root, TaskSample taskSample, Color color)
		{
			this.Root = root;
			this.TaskSample = taskSample;
			this._color = color;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000034A8 File Offset: 0x000016A8
		public void Initialize()
		{
			this.Root.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				EventHandler taskViewClicked = this.TaskViewClicked;
				if (taskViewClicked == null)
				{
					return;
				}
				taskViewClicked(this, EventArgs.Empty);
			}, 0);
			Type genericType = this.TaskSample.GenericType;
			this._name = UQueryExtensions.Q<Label>(this.Root, "Name", null);
			this._name.text = ((this.TaskSample.TotalRuns > 1) ? string.Format("{0} ({1})", genericType.Name, this.TaskSample.Run + 1) : (genericType.Name ?? ""));
			this.UpdateVisibility();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000354C File Offset: 0x0000174C
		public void SetScale(float pixelScale, long referenceTimestamp)
		{
			long num = this.TaskSample.EndTime - this.TaskSample.StartTime;
			this.Root.style.left = new StyleLength(new Length(pixelScale * (float)(this.TaskSample.StartTime - referenceTimestamp), 0));
			this.Root.style.width = new StyleLength(new Length(pixelScale * (float)num, 0));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000035C4 File Offset: 0x000017C4
		public string GetTooltipText()
		{
			Type genericType = this.TaskSample.GenericType;
			string @namespace = genericType.Namespace;
			string text;
			if (@namespace == null)
			{
				text = null;
			}
			else
			{
				string text2 = @namespace;
				int num = genericType.Namespace.IndexOf('.') + 1;
				text = text2.Substring(num, text2.Length - num);
			}
			string text3 = text ?? string.Empty;
			string text4 = (this.TaskSample.TotalRuns > 1) ? string.Format("{0} ({1}/{2})", genericType.Name, this.TaskSample.Run + 1, this.TaskSample.TotalRuns) : (genericType.Name ?? "");
			double num2 = TaskSampleCalculator.TicksToMs(this.TaskSample.EndTime - this.TaskSample.StartTime);
			return string.Concat(new string[]
			{
				"<b>",
				text4,
				"</b>\nScope: ",
				text3,
				"\n",
				string.Format("Duration: {0:0.000}ms\n", num2)
			});
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000036DF File Offset: 0x000018DF
		public void SetTransparent()
		{
			this._isTransparent = true;
			this.UpdateVisibility();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000036EE File Offset: 0x000018EE
		public void UnsetTransparent()
		{
			this._isTransparent = false;
			this.UpdateVisibility();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003700 File Offset: 0x00001900
		public void UpdateVisibility()
		{
			this.Root.style.backgroundColor = new Color(this._color.r, this._color.g, this._color.b, this._isTransparent ? TaskView.TransparentAlpha : 1f);
			this.Root.EnableInClassList(TaskView.TransparentClass, this._isTransparent);
			this._name.ToggleDisplayStyle(!this._isTransparent);
		}

		// Token: 0x04000041 RID: 65
		public static readonly string TransparentClass = "task-view--transparent";

		// Token: 0x04000042 RID: 66
		public static readonly float TransparentAlpha = 0.15f;

		// Token: 0x04000045 RID: 69
		public EventHandler TaskViewClicked;

		// Token: 0x04000046 RID: 70
		public Label _name;

		// Token: 0x04000047 RID: 71
		public readonly Color _color;

		// Token: 0x04000048 RID: 72
		public bool _isTransparent;
	}
}
