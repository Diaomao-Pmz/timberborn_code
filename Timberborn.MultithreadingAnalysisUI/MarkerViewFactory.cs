using System;
using Timberborn.CoreUI;
using Timberborn.MultithreadingAnalysis;
using Timberborn.TooltipSystem;

namespace Timberborn.MultithreadingAnalysisUI
{
	// Token: 0x02000005 RID: 5
	public class MarkerViewFactory
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000217C File Offset: 0x0000037C
		public MarkerViewFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002194 File Offset: 0x00000394
		public MarkerView CreateMarker(Marker marker)
		{
			MarkerView markerView = new MarkerView(this._visualElementLoader.LoadVisualElement("Common/MultithreadingAnalysis/MarkerView"), marker);
			this._tooltipRegistrar.Register(markerView.Root, markerView.GetTooltipText());
			return markerView;
		}

		// Token: 0x04000009 RID: 9
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000A RID: 10
		public readonly ITooltipRegistrar _tooltipRegistrar;
	}
}
