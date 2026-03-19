using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Localization;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200000D RID: 13
	public class BuildingTutorialStep : ITutorialStep, ITutorialStepWithTool
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002504 File Offset: 0x00000704
		public BuildingTutorialStep(BuiltBuildingService builtBuildingService, ILoc loc, IEnumerable<string> templateNames, bool onlyFinishedBuildings, int requiredAmount, string mainLocKey, string localizedBuildingName)
		{
			this._builtBuildingService = builtBuildingService;
			this._loc = loc;
			this._templateNames = templateNames.ToImmutableArray<string>();
			this._onlyFinishedBuildings = onlyFinishedBuildings;
			this._requiredAmount = requiredAmount;
			this._mainLocKey = mainLocKey;
			this._localizedBuildingName = localizedBuildingName;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002551 File Offset: 0x00000751
		public bool KeepBlinking
		{
			get
			{
				return this.NumberOfAllBuildings < this._requiredAmount;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002561 File Offset: 0x00000761
		public string Description()
		{
			return this._loc.T<string, int, int>(this._mainLocKey, this._localizedBuildingName, Math.Min(this.NumberOfBuildings, this._requiredAmount), this._requiredAmount);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002591 File Offset: 0x00000791
		public bool Achieved()
		{
			return this.NumberOfBuildings >= this._requiredAmount;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000025A4 File Offset: 0x000007A4
		public int NumberOfBuildings
		{
			get
			{
				if (!this._onlyFinishedBuildings)
				{
					return this.NumberOfAllBuildings;
				}
				return this._builtBuildingService.NumberOfFinishedBuildings(this._templateNames);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000025CB File Offset: 0x000007CB
		public int NumberOfAllBuildings
		{
			get
			{
				return this._builtBuildingService.NumberOfAllBuildings(this._templateNames);
			}
		}

		// Token: 0x04000018 RID: 24
		public readonly BuiltBuildingService _builtBuildingService;

		// Token: 0x04000019 RID: 25
		public readonly ILoc _loc;

		// Token: 0x0400001A RID: 26
		public readonly ImmutableArray<string> _templateNames;

		// Token: 0x0400001B RID: 27
		public readonly bool _onlyFinishedBuildings;

		// Token: 0x0400001C RID: 28
		public readonly int _requiredAmount;

		// Token: 0x0400001D RID: 29
		public readonly string _mainLocKey;

		// Token: 0x0400001E RID: 30
		public readonly string _localizedBuildingName;
	}
}
