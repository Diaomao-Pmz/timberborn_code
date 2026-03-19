using System;
using Timberborn.BlockObjectTools;
using Timberborn.Buildings;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.ScienceSystem;
using Timberborn.ToolSystem;

namespace Timberborn.BuildingTools
{
	// Token: 0x02000008 RID: 8
	public class BuildingToolLocker : IToolLocker
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002453 File Offset: 0x00000653
		public BuildingToolLocker(InputService inputService, BuildingUnlockingService buildingUnlockingService, DialogBoxShower dialogBoxShower, ILoc loc)
		{
			this._inputService = inputService;
			this._buildingUnlockingService = buildingUnlockingService;
			this._dialogBoxShower = dialogBoxShower;
			this._loc = loc;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002478 File Offset: 0x00000678
		public bool ShouldLock(ITool tool)
		{
			BuildingSpec buildingSpec;
			return BuildingToolLocker.TryGetBuildingFromTool(tool, out buildingSpec) && !this._buildingUnlockingService.Unlocked(buildingSpec);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024A0 File Offset: 0x000006A0
		public void TryToUnlock(ITool tool, Action successCallback, Action failCallback)
		{
			BuildingSpec buildingFromToolUnsafe = BuildingToolLocker.GetBuildingFromToolUnsafe(tool);
			if (this._inputService.IsKeyHeld(BuildingToolLocker.InstantUnlockKey))
			{
				this.UnlockIgnoringScienceCost(buildingFromToolUnsafe, successCallback);
				return;
			}
			if (this._buildingUnlockingService.Unlockable(buildingFromToolUnsafe))
			{
				this.AskForUnlockingConfirmation(buildingFromToolUnsafe, successCallback, failCallback);
				return;
			}
			this.ShowInsufficientSciencePointsMessage(buildingFromToolUnsafe, failCallback);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024F0 File Offset: 0x000006F0
		public static bool TryGetBuildingFromTool(ITool tool, out BuildingSpec buildingSpec)
		{
			BlockObjectTool blockObjectTool = tool as BlockObjectTool;
			if (blockObjectTool != null)
			{
				BuildingSpec spec = blockObjectTool.Template.GetSpec<BuildingSpec>();
				if (spec != null)
				{
					buildingSpec = spec;
					return true;
				}
			}
			buildingSpec = null;
			return false;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002520 File Offset: 0x00000720
		public static BuildingSpec GetBuildingFromToolUnsafe(ITool tool)
		{
			BuildingSpec result;
			if (BuildingToolLocker.TryGetBuildingFromTool(tool, out result))
			{
				return result;
			}
			throw new ArgumentException(string.Format("Tool {0} is not a BlockObjectTool with a Building component", tool.GetType()));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000254E File Offset: 0x0000074E
		public void UnlockIgnoringScienceCost(BuildingSpec buildingSpec, Action successCallback)
		{
			this._buildingUnlockingService.UnlockIgnoringCost(buildingSpec);
			successCallback();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002564 File Offset: 0x00000764
		public void AskForUnlockingConfirmation(BuildingSpec buildingSpec, Action successCallback, Action failCallback)
		{
			this._dialogBoxShower.Create().SetMessage(this.GetMessage(buildingSpec, BuildingToolLocker.UnlockPromptLocKey)).SetConfirmButton(delegate()
			{
				this._buildingUnlockingService.Unlock(buildingSpec);
				successCallback();
			}).SetCancelButton(failCallback).Show();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025CA File Offset: 0x000007CA
		public void ShowInsufficientSciencePointsMessage(BuildingSpec buildingSpec, Action failCallback)
		{
			this._dialogBoxShower.Create().SetMessage(this.GetMessage(buildingSpec, BuildingToolLocker.CantUnlockLocKey)).SetConfirmButton(failCallback).Show();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025F4 File Offset: 0x000007F4
		public string GetMessage(BuildingSpec buildingSpec, string key)
		{
			string param = this._loc.T(buildingSpec.GetSpec<LabeledEntitySpec>().DisplayNameLocKey);
			return this._loc.T<string, int>(key, param, buildingSpec.ScienceCost);
		}

		// Token: 0x04000019 RID: 25
		public static readonly string CantUnlockLocKey = "BuildingTools.CantUnlock";

		// Token: 0x0400001A RID: 26
		public static readonly string UnlockPromptLocKey = "BuildingTools.UnlockPrompt";

		// Token: 0x0400001B RID: 27
		public static readonly string InstantUnlockKey = "InstantUnlock";

		// Token: 0x0400001C RID: 28
		public readonly InputService _inputService;

		// Token: 0x0400001D RID: 29
		public readonly BuildingUnlockingService _buildingUnlockingService;

		// Token: 0x0400001E RID: 30
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x0400001F RID: 31
		public readonly ILoc _loc;
	}
}
