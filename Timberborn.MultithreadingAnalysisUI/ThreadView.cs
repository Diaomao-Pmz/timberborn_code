using System;
using UnityEngine.UIElements;

namespace Timberborn.MultithreadingAnalysisUI
{
	// Token: 0x02000011 RID: 17
	public class ThreadView
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000382A File Offset: 0x00001A2A
		public VisualElement Root { get; }

		// Token: 0x06000056 RID: 86 RVA: 0x00003832 File Offset: 0x00001A32
		public ThreadView(VisualElement root, VisualElement taskContainer)
		{
			this.Root = root;
			this._taskContainer = taskContainer;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003848 File Offset: 0x00001A48
		public void AddTaskView(VisualElement taskViewRoot)
		{
			this._taskContainer.Add(taskViewRoot);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003856 File Offset: 0x00001A56
		public void SetScale(float pixelScale, long snapshotLength)
		{
			this._taskContainer.style.width = new StyleLength(new Length(pixelScale * (float)snapshotLength, 0));
		}

		// Token: 0x0400004D RID: 77
		public readonly VisualElement _taskContainer;
	}
}
