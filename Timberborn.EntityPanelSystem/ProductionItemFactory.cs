using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.EntityPanelSystem
{
	// Token: 0x0200001B RID: 27
	public class ProductionItemFactory
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00003D52 File Offset: 0x00001F52
		public ProductionItemFactory(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003D64 File Offset: 0x00001F64
		public VisualElement CreateInputOutput(IEnumerable<VisualElement> inputs, IEnumerable<VisualElement> outputs, string craftingTime)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/EntityDescription/ProductionItem");
			UQueryExtensions.Q<VisualElement>(visualElement, "InputWrapper", null).Add(this.CreateInput(inputs));
			UQueryExtensions.Q<VisualElement>(visualElement, "OutputWrapper", null).Add(this.CreateOutput(outputs));
			Label label = UQueryExtensions.Q<Label>(visualElement, "CraftingTime", null);
			label.text = craftingTime;
			label.ToggleDisplayStyle(true);
			return visualElement;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003DCA File Offset: 0x00001FCA
		public VisualElement CreateInput(VisualElement input)
		{
			return this.CreateInput(Enumerables.One<VisualElement>(input));
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003DD8 File Offset: 0x00001FD8
		public VisualElement CreateOutput(VisualElement output)
		{
			return this.CreateOutput(Enumerables.One<VisualElement>(output));
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003DE8 File Offset: 0x00001FE8
		public VisualElement CreateInput(IEnumerable<VisualElement> inputs)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/EntityDescription/ProductionItemInput");
			bool visible = ProductionItemFactory.FillItems(visualElement, "Input", inputs);
			UQueryExtensions.Q<VisualElement>(visualElement, "InputArrow", null).ToggleDisplayStyle(visible);
			return visualElement;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003E24 File Offset: 0x00002024
		public VisualElement CreateOutput(IEnumerable<VisualElement> outputs)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/EntityDescription/ProductionItemOutput");
			bool visible = ProductionItemFactory.FillItems(visualElement, "Output", outputs);
			UQueryExtensions.Q<VisualElement>(visualElement, "OutputArrow", null).ToggleDisplayStyle(visible);
			return visualElement;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003E60 File Offset: 0x00002060
		public static bool FillItems(VisualElement element, string rootName, IEnumerable<VisualElement> items)
		{
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(element, rootName, null);
			foreach (VisualElement visualElement2 in items)
			{
				visualElement.Add(visualElement2);
			}
			return visualElement.childCount > 0;
		}

		// Token: 0x0400007E RID: 126
		public readonly VisualElementLoader _visualElementLoader;
	}
}
