using System;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.DwellingSystem;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.DwellingSystemUI
{
	// Token: 0x02000008 RID: 8
	public class DwellingBatchControlRowItemFactory
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000234D File Offset: 0x0000054D
		public DwellingBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._loc = loc;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000236C File Offset: 0x0000056C
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			Dwelling dwelling = entity.GetComponent<Dwelling>();
			if (dwelling != null)
			{
				string elementName = "Game/BatchControl/DwellingBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				Label info = UQueryExtensions.Q<Label>(visualElement, "Info", null);
				IBatchControlRowItem result = new DwellingBatchControlRowItem(visualElement, dwelling, info);
				this._tooltipRegistrar.Register(visualElement, () => this.GetTooltipText(dwelling));
				return result;
			}
			return null;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023E4 File Offset: 0x000005E4
		public string GetTooltipText(Dwelling dwelling)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("<b>" + this._loc.T(DwellingBatchControlRowItemFactory.DwellersLocKey) + "</b>");
			int numberOfAdultDwellers = dwelling.NumberOfAdultDwellers;
			int num = Math.Max(dwelling.AdultSlots, numberOfAdultDwellers);
			stringBuilder.AppendLine(string.Format("{0}: {1} / {2}", this._loc.T(DwellingBatchControlRowItemFactory.AdultsLocKey), numberOfAdultDwellers, num));
			int numberOfChildDwellers = dwelling.NumberOfChildDwellers;
			int num2 = dwelling.TotalSlots - num;
			stringBuilder.AppendLine(string.Format("{0}: {1} / {2}", this._loc.T(DwellingBatchControlRowItemFactory.ChildrenLocKey), numberOfChildDwellers, num2));
			return stringBuilder.ToStringWithoutNewLineEnd();
		}

		// Token: 0x04000014 RID: 20
		public static readonly string DwellersLocKey = "Dwelling.Dwellers";

		// Token: 0x04000015 RID: 21
		public static readonly string AdultsLocKey = "Beaver.Population.Adults";

		// Token: 0x04000016 RID: 22
		public static readonly string ChildrenLocKey = "Beaver.Population.Children";

		// Token: 0x04000017 RID: 23
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000018 RID: 24
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000019 RID: 25
		public readonly ILoc _loc;
	}
}
