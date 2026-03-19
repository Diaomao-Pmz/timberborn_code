using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Effects;
using Timberborn.EntityPanelSystem;
using Timberborn.Goods;
using Timberborn.GoodsUI;
using Timberborn.Localization;
using Timberborn.UIFormatters;
using Timberborn.Workshops;
using Timberborn.WorkSystem;
using UnityEngine.UIElements;

namespace Timberborn.WorkshopsUI
{
	// Token: 0x02000007 RID: 7
	public class ManufactoryDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002248 File Offset: 0x00000448
		public ManufactoryDescriber(GoodEffectDescriber goodEffectDescriber, ILoc loc, DescribedAmountFactory describedAmountFactory, ProductionItemFactory productionItemFactory, GoodDescriber goodService)
		{
			this._goodEffectDescriber = goodEffectDescriber;
			this._loc = loc;
			this._describedAmountFactory = describedAmountFactory;
			this._productionItemFactory = productionItemFactory;
			this._goodDescriber = goodService;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000229C File Offset: 0x0000049C
		public void Awake()
		{
			this._manufactory = base.GetComponent<Manufactory>();
			this._workplace = base.GetComponent<Workplace>();
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022C2 File Offset: 0x000004C2
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			if (this._blockObject.IsPreview)
			{
				foreach (EntityDescription entityDescription in this.DescribeStatistics())
				{
					yield return entityDescription;
				}
				IEnumerator<EntityDescription> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022D2 File Offset: 0x000004D2
		[return: TupleElementNames(new string[]
		{
			"input",
			"output"
		})]
		public ValueTuple<VisualElement, VisualElement> DescribeRecipe(RecipeSpec productionRecipe)
		{
			return new ValueTuple<VisualElement, VisualElement>(this._productionItemFactory.CreateInput(this.GetInputs(productionRecipe)), this._productionItemFactory.CreateOutput(this.GetOutputs(productionRecipe)));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002300 File Offset: 0x00000500
		public string GetCraftingTime(RecipeSpec productionRecipe, float workers)
		{
			float num = productionRecipe.CycleDurationInHours / workers;
			string text;
			if (num >= 1f)
			{
				if (num >= 10f)
				{
					text = num.ToString("F0");
				}
				else
				{
					text = num.ToString("0.#");
				}
			}
			else
			{
				text = num.ToString("0.##");
			}
			string param = text;
			return this._loc.T<string>(this._craftingTimePhrase, param);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002367 File Offset: 0x00000567
		public IEnumerable<EntityDescription> DescribeStatistics()
		{
			int num2;
			for (int i = 0; i < this._manufactory.ProductionRecipes.Length; i = num2 + 1)
			{
				RecipeSpec productionRecipe = this._manufactory.ProductionRecipes[i];
				int num = this._workplace ? this._workplace.MaxWorkers : 1;
				VisualElement content = this.DescribeRecipe(productionRecipe, (float)num);
				yield return EntityDescription.CreateInputOutputSection(content, i);
				num2 = i;
			}
			yield break;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002377 File Offset: 0x00000577
		public VisualElement DescribeRecipe(RecipeSpec productionRecipe, float workers)
		{
			return this._productionItemFactory.CreateInputOutput(this.GetInputs(productionRecipe), this.GetOutputs(productionRecipe), this.GetCraftingTime(productionRecipe, workers));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000239A File Offset: 0x0000059A
		public IEnumerable<VisualElement> GetInputs(RecipeSpec productionRecipe)
		{
			int num;
			for (int i = 0; i < productionRecipe.Ingredients.Length; i = num + 1)
			{
				GoodAmountSpec goodAmountSpec = productionRecipe.Ingredients[i];
				DescribedGood describedGood = this._goodDescriber.GetDescribedGood(goodAmountSpec.Id);
				string tooltip = ManufactoryDescriber.GetTooltip(describedGood);
				yield return this.CreateElement(describedGood, goodAmountSpec.Amount, tooltip);
				num = i;
			}
			if (productionRecipe.ConsumesFuel)
			{
				float num2 = 1f / (float)productionRecipe.CyclesFuelLasts;
				DescribedGood describedGood2 = this._goodDescriber.GetDescribedGood(productionRecipe.Fuel);
				string tooltip2 = ManufactoryDescriber.GetTooltip(describedGood2);
				yield return this.CreateElement(describedGood2, num2.ToString("0.#"), tooltip2);
			}
			yield break;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023B1 File Offset: 0x000005B1
		public IEnumerable<VisualElement> GetOutputs(RecipeSpec productionRecipe)
		{
			int num;
			for (int i = 0; i < productionRecipe.Products.Length; i = num + 1)
			{
				GoodAmountSpec goodAmountSpec = productionRecipe.Products[i];
				DescribedGood describedGood = this._goodDescriber.GetDescribedGood(goodAmountSpec.Id);
				string tooltipWithEffects = this.GetTooltipWithEffects(goodAmountSpec.Id, describedGood);
				yield return this.CreateElement(describedGood, goodAmountSpec.Amount, tooltipWithEffects);
				num = i;
			}
			if (productionRecipe.ProducesSciencePoints)
			{
				string amount = productionRecipe.ProducedSciencePoints.ToString();
				string tooltip = this._loc.T(ManufactoryDescriber.SciencePointsLocKey);
				yield return this._describedAmountFactory.CreatePlain(ManufactoryDescriber.ScienceClass, amount, tooltip);
			}
			yield break;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023C8 File Offset: 0x000005C8
		public static string GetTooltip(DescribedGood good)
		{
			return good.DisplayName;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023D4 File Offset: 0x000005D4
		public string GetTooltipWithEffects(string goodId, DescribedGood good)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(ManufactoryDescriber.GetTooltip(good));
			this._goodEffectDescriber.DescribeEffects(goodId, stringBuilder);
			return stringBuilder.ToString().TrimEnd();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000240C File Offset: 0x0000060C
		public VisualElement CreateElement(DescribedGood good, int amount, string tooltip)
		{
			return this.CreateElement(good, amount.ToString(), tooltip);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000241D File Offset: 0x0000061D
		public VisualElement CreateElement(DescribedGood good, string amount, string tooltip)
		{
			return this._describedAmountFactory.CreatePlain("", amount, good.Icon, tooltip);
		}

		// Token: 0x04000010 RID: 16
		public static readonly string ScienceClass = "described-amount--science";

		// Token: 0x04000011 RID: 17
		public static readonly string SciencePointsLocKey = "Science.SciencePoints";

		// Token: 0x04000012 RID: 18
		public readonly GoodEffectDescriber _goodEffectDescriber;

		// Token: 0x04000013 RID: 19
		public readonly ILoc _loc;

		// Token: 0x04000014 RID: 20
		public readonly DescribedAmountFactory _describedAmountFactory;

		// Token: 0x04000015 RID: 21
		public readonly ProductionItemFactory _productionItemFactory;

		// Token: 0x04000016 RID: 22
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x04000017 RID: 23
		public Manufactory _manufactory;

		// Token: 0x04000018 RID: 24
		public Workplace _workplace;

		// Token: 0x04000019 RID: 25
		public BlockObject _blockObject;

		// Token: 0x0400001A RID: 26
		public readonly Phrase _craftingTimePhrase = Phrase.New().Format<string>(new Func<string, ILoc, string>(UnitFormatter.FormatHours));
	}
}
