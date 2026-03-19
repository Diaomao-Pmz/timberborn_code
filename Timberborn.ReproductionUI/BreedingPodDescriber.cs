using System;
using System.Collections.Generic;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Goods;
using Timberborn.GoodsUI;
using Timberborn.Localization;
using Timberborn.Reproduction;

namespace Timberborn.ReproductionUI
{
	// Token: 0x02000007 RID: 7
	public class BreedingPodDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000021F3 File Offset: 0x000003F3
		public BreedingPodDescriber(GoodDescriber goodDescriber, ILoc loc)
		{
			this._goodDescriber = goodDescriber;
			this._loc = loc;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002209 File Offset: 0x00000409
		public void Awake()
		{
			this._breedingPod = base.GetComponent<BreedingPod>();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002217 File Offset: 0x00000417
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			yield return EntityDescription.CreateTextSection(this.Describe(), 60);
			yield break;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002228 File Offset: 0x00000428
		public string Describe()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringListBuilder stringListBuilder = new StringListBuilder(stringBuilder, ", ");
			stringBuilder.AppendLine(SpecialStrings.RowStarter + this._loc.T(BreedingPodDescriber.NutrientBringingLocKey));
			stringBuilder.Append(SpecialStrings.RowStarter + this._loc.T(BreedingPodDescriber.NutrientsNeededLocKey) + " ");
			foreach (GoodAmountSpec goodAmountSpec in this._breedingPod.NutrientsPerCycle)
			{
				stringListBuilder.BeginItem();
				GoodAmount goodAmount = new GoodAmount(goodAmountSpec.Id, goodAmountSpec.Amount * this._breedingPod.CyclesUntilFullyGrown);
				stringBuilder.Append(this._goodDescriber.Describe(goodAmount));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400000F RID: 15
		public static readonly string NutrientsNeededLocKey = "Breeding.NutrientsNeeded";

		// Token: 0x04000010 RID: 16
		public static readonly string NutrientBringingLocKey = "Breeding.NutrientBringing";

		// Token: 0x04000011 RID: 17
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x04000012 RID: 18
		public readonly ILoc _loc;

		// Token: 0x04000013 RID: 19
		public BreedingPod _breedingPod;
	}
}
