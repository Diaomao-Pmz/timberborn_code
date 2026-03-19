using System;
using Timberborn.CoreUI;
using Timberborn.NeedSpecs;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x0200000E RID: 14
	public class NeedGroupViewFactory
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002998 File Offset: 0x00000B98
		public NeedGroupViewFactory(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000029A8 File Offset: 0x00000BA8
		public NeedGroupView Create(NeedGroupSpec needGroupSpec)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/NeedGroupView");
			UQueryExtensions.Q<Label>(visualElement, "Label", null).text = needGroupSpec.DisplayName.Value;
			VisualElement items = UQueryExtensions.Q<VisualElement>(visualElement, "Items", null);
			return new NeedGroupView(visualElement, items);
		}

		// Token: 0x04000039 RID: 57
		public readonly VisualElementLoader _visualElementLoader;
	}
}
