using System;
using System.Collections.Generic;
using Timberborn.Buildings;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Localization;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000048 RID: 72
	public class SelectStockpileGoodTutorialStep : ITutorialStep
	{
		// Token: 0x060001EC RID: 492 RVA: 0x0000601D File Offset: 0x0000421D
		public SelectStockpileGoodTutorialStep(BuiltBuildingService builtBuildingService, ILoc loc, string templateName, GoodSpec requiredGood, int requiredAmount, string mainLocKey, string localizedBuildingName)
		{
			this._builtBuildingService = builtBuildingService;
			this._loc = loc;
			this._templateName = templateName;
			this._requiredGood = requiredGood;
			this._requiredAmount = requiredAmount;
			this._mainLocKey = mainLocKey;
			this._localizedBuildingName = localizedBuildingName;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000605C File Offset: 0x0000425C
		public string Description()
		{
			int param = Math.Min(this.GetNumberOfValidBuildings(), this._requiredAmount);
			string param2 = this._loc.T<int, int>(SelectStockpileGoodTutorialStep.ProgressLocKey, param, this._requiredAmount);
			string value = this._requiredGood.PluralDisplayName.Value;
			return this._loc.T<string, string, string>(this._mainLocKey, this._localizedBuildingName, value, param2);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000060BD File Offset: 0x000042BD
		public bool Achieved()
		{
			return this.GetNumberOfValidBuildings() >= this._requiredAmount;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000060D0 File Offset: 0x000042D0
		public int GetNumberOfValidBuildings()
		{
			IReadOnlyList<Building> finishedBuildings = this._builtBuildingService.GetFinishedBuildings(this._templateName);
			IReadOnlyList<Building> unfinishedBuildings = this._builtBuildingService.GetUnfinishedBuildings(this._templateName);
			int numberOfValidBuildings = this.GetNumberOfValidBuildings(finishedBuildings);
			if (numberOfValidBuildings < this._requiredAmount)
			{
				return numberOfValidBuildings + this.GetNumberOfValidBuildings(unfinishedBuildings);
			}
			return numberOfValidBuildings;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006120 File Offset: 0x00004320
		public int GetNumberOfValidBuildings(IReadOnlyList<Building> buildingsSpecs)
		{
			int num = 0;
			for (int i = 0; i < buildingsSpecs.Count; i++)
			{
				if (buildingsSpecs[i].GetComponent<SingleGoodAllower>().AllowedGood == this._requiredGood.Id && ++num >= this._requiredAmount)
				{
					return num;
				}
			}
			return num;
		}

		// Token: 0x040000E9 RID: 233
		public static readonly string ProgressLocKey = "Tutorial.Progress";

		// Token: 0x040000EA RID: 234
		public readonly BuiltBuildingService _builtBuildingService;

		// Token: 0x040000EB RID: 235
		public readonly ILoc _loc;

		// Token: 0x040000EC RID: 236
		public readonly string _templateName;

		// Token: 0x040000ED RID: 237
		public readonly GoodSpec _requiredGood;

		// Token: 0x040000EE RID: 238
		public readonly int _requiredAmount;

		// Token: 0x040000EF RID: 239
		public readonly string _mainLocKey;

		// Token: 0x040000F0 RID: 240
		public readonly string _localizedBuildingName;
	}
}
