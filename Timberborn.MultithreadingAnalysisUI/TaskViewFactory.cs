using System;
using Timberborn.CoreUI;
using Timberborn.MultithreadingAnalysis;
using Timberborn.TooltipSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.MultithreadingAnalysisUI
{
	// Token: 0x02000010 RID: 16
	public class TaskViewFactory
	{
		// Token: 0x06000053 RID: 83 RVA: 0x000037B4 File Offset: 0x000019B4
		public TaskViewFactory(VisualElementLoader visualElementLoader, TaskColorProvider taskColorProvider, ITooltipRegistrar tooltipRegistrar)
		{
			this._visualElementLoader = visualElementLoader;
			this._taskColorProvider = taskColorProvider;
			this._tooltipRegistrar = tooltipRegistrar;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000037D4 File Offset: 0x000019D4
		public TaskView CreateTask(TaskSample task)
		{
			VisualElement root = this._visualElementLoader.LoadVisualElement("Common/MultithreadingAnalysis/TaskView");
			Color color = this._taskColorProvider.GetColor(task.GenericType);
			TaskView taskView = new TaskView(root, task, color);
			taskView.Initialize();
			this._tooltipRegistrar.Register(taskView.Root, taskView.GetTooltipText());
			return taskView;
		}

		// Token: 0x04000049 RID: 73
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400004A RID: 74
		public readonly TaskColorProvider _taskColorProvider;

		// Token: 0x0400004B RID: 75
		public readonly ITooltipRegistrar _tooltipRegistrar;
	}
}
