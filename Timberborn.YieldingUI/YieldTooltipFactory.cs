using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.Effects;
using Timberborn.EntitySystem;
using Timberborn.GoodsUI;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using Timberborn.Yielding;
using UnityEngine.UIElements;

namespace Timberborn.YieldingUI
{
	// Token: 0x02000007 RID: 7
	public class YieldTooltipFactory : ILoadableSingleton
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002307 File Offset: 0x00000507
		public YieldTooltipFactory(VisualElementLoader visualElementLoader, ILoc loc, GoodEffectDescriber goodEffectDescriber, TemplateService templateService, GoodDescriber goodDescriber)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._goodEffectDescriber = goodEffectDescriber;
			this._templateService = templateService;
			this._goodDescriber = goodDescriber;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002340 File Offset: 0x00000540
		public void Load()
		{
			foreach (LabeledEntitySpec labeledEntitySpec in this._templateService.GetAll<LabeledEntitySpec>())
			{
				YieldRemovingBuildingSpec spec = labeledEntitySpec.GetSpec<YieldRemovingBuildingSpec>();
				if (spec != null)
				{
					this._templates.GetOrAdd(spec.ResourceGroup).Add(labeledEntitySpec);
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023AC File Offset: 0x000005AC
		public VisualElement Create(YielderSpec yielderSpec, string growthTime, string growthAdditionalInfo = null)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/ResourceYieldTooltip");
			string id = yielderSpec.Yield.Id;
			UQueryExtensions.Q<Label>(visualElement, "ResourceName", null).text = this._goodDescriber.Describe(id);
			UQueryExtensions.Q<Label>(visualElement, "GrowthTime", null).text = (this._loc.T<string>(YieldTooltipFactory.GrowingTimeLocKey, growthTime) + "\n" + growthAdditionalInfo).TrimEnd();
			string text = this._goodEffectDescriber.DescribeEffects(id);
			Label label = UQueryExtensions.Q<Label>(visualElement, "Bonuses", null);
			label.text = text;
			bool visible = !string.IsNullOrEmpty(text);
			label.ToggleDisplayStyle(visible);
			UQueryExtensions.Q<Label>(visualElement, "EatableRaw", null).ToggleDisplayStyle(visible);
			this.AddBuildings(visualElement, yielderSpec);
			return visualElement;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002470 File Offset: 0x00000670
		public void AddBuildings(VisualElement parent, YielderSpec yielderSpec)
		{
			List<LabeledEntitySpec> list;
			if (this._templates.TryGetValue(yielderSpec.ResourceGroup, out list))
			{
				using (List<LabeledEntitySpec>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						LabeledEntitySpec labeledEntitySpec = enumerator.Current;
						VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/TooltipBuildingItem");
						UQueryExtensions.Q<Image>(visualElement, "Icon", null).sprite = labeledEntitySpec.Icon.Asset;
						UQueryExtensions.Q<Label>(visualElement, "Name", null).text = this._loc.T(labeledEntitySpec.DisplayNameLocKey);
						parent.Add(visualElement);
					}
					return;
				}
			}
			this.ThrowNonExistingYieldRemovingBuilding(yielderSpec);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000252C File Offset: 0x0000072C
		public void ThrowNonExistingYieldRemovingBuilding(YielderSpec yielderSpec)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("No yield removing building found for resource group \"" + yielderSpec.ResourceGroup + "\"");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Existing yield removing buildings:");
			stringBuilder.AppendLine();
			foreach (KeyValuePair<string, List<LabeledEntitySpec>> keyValuePair in this._templates)
			{
				string text;
				List<LabeledEntitySpec> list;
				keyValuePair.Deconstruct(ref text, ref list);
				string str = text;
				List<LabeledEntitySpec> source = list;
				stringBuilder.Append(str + " - ");
				stringBuilder.AppendJoin<string>(", ", from template in source
				select template.DisplayNameLocKey);
				stringBuilder.AppendLine();
			}
			throw new InvalidOperationException(stringBuilder.ToString());
		}

		// Token: 0x0400000E RID: 14
		public static readonly string GrowingTimeLocKey = "Growing.Time";

		// Token: 0x0400000F RID: 15
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000010 RID: 16
		public readonly ILoc _loc;

		// Token: 0x04000011 RID: 17
		public readonly GoodEffectDescriber _goodEffectDescriber;

		// Token: 0x04000012 RID: 18
		public readonly TemplateService _templateService;

		// Token: 0x04000013 RID: 19
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x04000014 RID: 20
		public readonly Dictionary<string, List<LabeledEntitySpec>> _templates = new Dictionary<string, List<LabeledEntitySpec>>();
	}
}
