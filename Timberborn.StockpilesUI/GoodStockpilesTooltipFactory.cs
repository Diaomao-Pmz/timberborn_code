using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.Effects;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.ResourceCountingSystem;
using Timberborn.ResourceCountingSystemUI;
using Timberborn.SingletonSystem;
using Timberborn.Stockpiles;
using Timberborn.TemplateSystem;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.StockpilesUI
{
	// Token: 0x0200000D RID: 13
	public class GoodStockpilesTooltipFactory : ILoadableSingleton
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000025BF File Offset: 0x000007BF
		public GoodStockpilesTooltipFactory(TemplateService templateService, VisualElementLoader visualElementLoader, GoodEffectDescriber goodEffectDescriber, ContextualResourceCountingService contextualResourceCountingService, IGoodService goodService, DevModeManager devModeManager)
		{
			this._templateService = templateService;
			this._visualElementLoader = visualElementLoader;
			this._goodEffectDescriber = goodEffectDescriber;
			this._contextualResourceCountingService = contextualResourceCountingService;
			this._goodService = goodService;
			this._devModeManager = devModeManager;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002600 File Offset: 0x00000800
		public void Load()
		{
			foreach (LabeledEntitySpec labeledEntitySpec in this._templateService.GetAll<LabeledEntitySpec>().OrderBy(new Func<LabeledEntitySpec, int>(GoodStockpilesTooltipFactory.GetCapacity)))
			{
				StockpileSpec spec = labeledEntitySpec.GetSpec<StockpileSpec>();
				if (spec != null && !labeledEntitySpec.HasSpec<FixedStockpileSpec>())
				{
					this._templates.GetOrAdd(spec.WhitelistedGoodType).Add(labeledEntitySpec);
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002688 File Offset: 0x00000888
		public VisualElement Create(string goodId)
		{
			ResourceCount contextualResourceCount = this._contextualResourceCountingService.GetContextualResourceCount(goodId);
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/GoodStockpilesTooltip");
			UQueryExtensions.Q<Label>(visualElement, "Name", null).text = this._goodService.GetGood(goodId).PluralDisplayName.Value;
			UQueryExtensions.Q<Label>(visualElement, "StockpilesValue", null).text = NumberFormatter.FormatFullNumber(contextualResourceCount.StockpiledStock + contextualResourceCount.CarriedToStockpilesStock) + " / " + NumberFormatter.FormatFullNumber(contextualResourceCount.InputOutputCapacity);
			UQueryExtensions.Q<Label>(visualElement, "OutputsValue", null).text = NumberFormatter.FormatFullNumber(contextualResourceCount.BufferedOutputStock);
			UQueryExtensions.Q<Label>(visualElement, "InputsValue", null).text = NumberFormatter.FormatFullNumber(contextualResourceCount.BufferedInput + contextualResourceCount.CarriedToProcessors + contextualResourceCount.StockUnderProcessing);
			Label label = UQueryExtensions.Q<Label>(visualElement, "DebugInfo", null);
			label.text = string.Concat(new string[]
			{
				SpecialStrings.RowStarter,
				"Buffered output: ",
				NumberFormatter.FormatFullNumber(contextualResourceCount.BufferedOutputStock),
				"\n",
				SpecialStrings.RowStarter,
				"Carried to stockpiles: ",
				NumberFormatter.FormatFullNumber(contextualResourceCount.CarriedToStockpilesStock),
				"\n",
				SpecialStrings.RowStarter,
				"Stockpiled: ",
				NumberFormatter.FormatFullNumber(contextualResourceCount.StockpiledStock),
				"\n",
				SpecialStrings.RowStarter,
				"Available: ",
				NumberFormatter.FormatFullNumber(contextualResourceCount.AvailableStock),
				"\n\n",
				SpecialStrings.RowStarter,
				"Carried to processors: ",
				NumberFormatter.FormatFullNumber(contextualResourceCount.CarriedToProcessors),
				"\n",
				SpecialStrings.RowStarter,
				"Buffered input: ",
				NumberFormatter.FormatFullNumber(contextualResourceCount.BufferedInput),
				"\n",
				SpecialStrings.RowStarter,
				"Under processing: ",
				NumberFormatter.FormatFullNumber(contextualResourceCount.StockUnderProcessing),
				"\n",
				SpecialStrings.RowStarter,
				"Total: ",
				NumberFormatter.FormatFullNumber(contextualResourceCount.AllStock)
			});
			label.ToggleDisplayStyle(this._devModeManager.Enabled);
			this.DescribeEffects(goodId, visualElement);
			this.AddIcons(UQueryExtensions.Q<VisualElement>(visualElement, "Icons", null), this._goodService.GetGood(goodId).GoodType);
			return visualElement;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002904 File Offset: 0x00000B04
		public void DescribeEffects(string goodId, VisualElement root)
		{
			string text = this._goodEffectDescriber.DescribeEffects(goodId);
			Label label = UQueryExtensions.Q<Label>(root, "Effects", null);
			label.text = text;
			label.ToggleDisplayStyle(!string.IsNullOrEmpty(text));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002940 File Offset: 0x00000B40
		public void AddIcons(VisualElement parent, string goodType)
		{
			foreach (LabeledEntitySpec labeledEntitySpec in this._templates[goodType])
			{
				Image image = new Image
				{
					name = labeledEntitySpec.DisplayNameLocKey,
					sprite = labeledEntitySpec.Icon.Asset
				};
				image.AddToClassList(GoodStockpilesTooltipFactory.IconClass);
				parent.Add(image);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000029C8 File Offset: 0x00000BC8
		public static int GetCapacity(LabeledEntitySpec entitySpec)
		{
			StockpileSpec spec = entitySpec.GetSpec<StockpileSpec>();
			if (spec == null)
			{
				return 0;
			}
			return spec.MaxCapacity;
		}

		// Token: 0x04000024 RID: 36
		public static readonly string IconClass = "good-stockpile-tooltip__building-icon";

		// Token: 0x04000025 RID: 37
		public readonly TemplateService _templateService;

		// Token: 0x04000026 RID: 38
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000027 RID: 39
		public readonly GoodEffectDescriber _goodEffectDescriber;

		// Token: 0x04000028 RID: 40
		public readonly ContextualResourceCountingService _contextualResourceCountingService;

		// Token: 0x04000029 RID: 41
		public readonly IGoodService _goodService;

		// Token: 0x0400002A RID: 42
		public readonly DevModeManager _devModeManager;

		// Token: 0x0400002B RID: 43
		public readonly Dictionary<string, List<LabeledEntitySpec>> _templates = new Dictionary<string, List<LabeledEntitySpec>>();
	}
}
