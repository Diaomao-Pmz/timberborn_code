using System;
using Timberborn.Buildings;
using Timberborn.Common;
using Timberborn.GameDistricts;
using Timberborn.Localization;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000021 RID: 33
	public class ConnectBuildingsTutorialStep : ITutorialStep
	{
		// Token: 0x060000EA RID: 234 RVA: 0x000040BD File Offset: 0x000022BD
		public ConnectBuildingsTutorialStep(BuiltBuildingService builtBuildingService, ILoc loc, string templateName, int requiredAmount, string localizedBuildingName, bool countUnfinishedBuildings)
		{
			this._builtBuildingService = builtBuildingService;
			this._loc = loc;
			this._templateName = templateName;
			this._requiredAmount = requiredAmount;
			this._localizedBuildingName = localizedBuildingName;
			this._countUnfinishedBuildings = countUnfinishedBuildings;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000040F2 File Offset: 0x000022F2
		public string Description()
		{
			return this._loc.T<string, int, int>(ConnectBuildingsTutorialStep.ConnectBuildingLocKey, this._localizedBuildingName, Math.Min(this.NumberOfConnectedBuildings, this._requiredAmount), this._requiredAmount);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004121 File Offset: 0x00002321
		public bool Achieved()
		{
			return this.NumberOfConnectedBuildings >= this._requiredAmount;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004134 File Offset: 0x00002334
		public int NumberOfConnectedBuildings
		{
			get
			{
				return this.NumberOfConnectedFinishedBuildings + (this._countUnfinishedBuildings ? this.NumberOfConnectedUnfinishedBuildings : 0);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000414E File Offset: 0x0000234E
		public int NumberOfConnectedFinishedBuildings
		{
			get
			{
				return this._builtBuildingService.GetFinishedBuildings(this._templateName).FastCount((Building building) => building.GetComponent<DistrictBuilding>().InstantDistrict);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004185 File Offset: 0x00002385
		public int NumberOfConnectedUnfinishedBuildings
		{
			get
			{
				return this._builtBuildingService.GetUnfinishedBuildings(this._templateName).FastCount((Building building) => building.GetComponent<DistrictBuilding>().ConstructionDistrict);
			}
		}

		// Token: 0x0400006A RID: 106
		public static readonly string ConnectBuildingLocKey = "Tutorial.ConnectBuilding";

		// Token: 0x0400006B RID: 107
		public readonly BuiltBuildingService _builtBuildingService;

		// Token: 0x0400006C RID: 108
		public readonly ILoc _loc;

		// Token: 0x0400006D RID: 109
		public readonly string _templateName;

		// Token: 0x0400006E RID: 110
		public readonly int _requiredAmount;

		// Token: 0x0400006F RID: 111
		public readonly string _localizedBuildingName;

		// Token: 0x04000070 RID: 112
		public readonly bool _countUnfinishedBuildings;
	}
}
