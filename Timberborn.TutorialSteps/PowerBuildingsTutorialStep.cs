using System;
using Timberborn.Buildings;
using Timberborn.Common;
using Timberborn.Localization;
using Timberborn.MechanicalSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200003E RID: 62
	public class PowerBuildingsTutorialStep : ITutorialStep
	{
		// Token: 0x060001AD RID: 429 RVA: 0x0000584A File Offset: 0x00003A4A
		public PowerBuildingsTutorialStep(BuiltBuildingService builtBuildingService, ILoc loc, string templateName, int requiredAmount, string localizedBuildingName)
		{
			this._builtBuildingService = builtBuildingService;
			this._loc = loc;
			this._templateName = templateName;
			this._requiredAmount = requiredAmount;
			this._localizedBuildingName = localizedBuildingName;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005877 File Offset: 0x00003A77
		public string Description()
		{
			return this._loc.T<string, int, int>(PowerBuildingsTutorialStep.PowerBuildingLocKey, this._localizedBuildingName, Math.Min(this.NumberOfPoweredBuildings, this._requiredAmount), this._requiredAmount);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000058A6 File Offset: 0x00003AA6
		public bool Achieved()
		{
			return this.NumberOfPoweredBuildings >= this._requiredAmount;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000058B9 File Offset: 0x00003AB9
		public int NumberOfPoweredBuildings
		{
			get
			{
				return this._builtBuildingService.GetFinishedBuildings(this._templateName).FastCount(delegate(Building building)
				{
					MechanicalGraph graph = building.GetComponent<MechanicalNode>().Graph;
					return graph != null && graph.NumberOfGenerators > 0;
				});
			}
		}

		// Token: 0x040000C7 RID: 199
		public static readonly string PowerBuildingLocKey = "Tutorial.PowerBuilding";

		// Token: 0x040000C8 RID: 200
		public readonly BuiltBuildingService _builtBuildingService;

		// Token: 0x040000C9 RID: 201
		public readonly ILoc _loc;

		// Token: 0x040000CA RID: 202
		public readonly string _templateName;

		// Token: 0x040000CB RID: 203
		public readonly int _requiredAmount;

		// Token: 0x040000CC RID: 204
		public readonly string _localizedBuildingName;
	}
}
