using System;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.MultithreadingAnalysisUI
{
	// Token: 0x02000012 RID: 18
	public class ThreadViewFactory
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00003877 File Offset: 0x00001A77
		public ThreadViewFactory(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003886 File Offset: 0x00001A86
		public ThreadView CreateThreadView()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/MultithreadingAnalysis/ThreadView");
			return new ThreadView(visualElement, UQueryExtensions.Q<VisualElement>(visualElement, "TaskContainer", null));
		}

		// Token: 0x0400004E RID: 78
		public readonly VisualElementLoader _visualElementLoader;
	}
}
