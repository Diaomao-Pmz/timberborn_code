using System;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.ToolSystem;

namespace Timberborn.PlantingUI
{
	// Token: 0x02000016 RID: 22
	public class PlantableToolLocker : IToolLocker
	{
		// Token: 0x0600006C RID: 108 RVA: 0x000030F2 File Offset: 0x000012F2
		public PlantableToolLocker(ILoc loc, InputService inputService, DialogBoxShower dialogBoxShower, UnlockedPlantableGroupsRegistry unlockedPlantableGroupsRegistry)
		{
			this._loc = loc;
			this._inputService = inputService;
			this._dialogBoxShower = dialogBoxShower;
			this._unlockedPlantableGroupsRegistry = unlockedPlantableGroupsRegistry;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003118 File Offset: 0x00001318
		public bool ShouldLock(ITool tool)
		{
			PlantingTool plantingTool;
			return PlantableToolLocker.IsPlantingTool(tool, out plantingTool) && this._unlockedPlantableGroupsRegistry.IsLocked(plantingTool.PlantableSpec);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003144 File Offset: 0x00001344
		public void TryToUnlock(ITool tool, Action successCallback, Action failCallback)
		{
			PlantingTool plantingToolUnsafe = PlantableToolLocker.GetPlantingToolUnsafe(tool);
			if (this._unlockedPlantableGroupsRegistry.IsLocked(plantingToolUnsafe.PlantableSpec) && !this._inputService.IsKeyHeld(PlantableToolLocker.InstantUnlockKey))
			{
				this.ShowLockedMessage(plantingToolUnsafe, failCallback);
				return;
			}
			successCallback();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000318C File Offset: 0x0000138C
		public static PlantingTool GetPlantingToolUnsafe(ITool tool)
		{
			PlantingTool result;
			if (PlantableToolLocker.IsPlantingTool(tool, out result))
			{
				return result;
			}
			throw new InvalidOperationException(string.Format("Tool {0} is not a {1}", tool, "PlantingTool"));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000031BA File Offset: 0x000013BA
		public static bool IsPlantingTool(ITool tool, out PlantingTool plantingTool)
		{
			plantingTool = (tool as PlantingTool);
			return plantingTool != null;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000031CC File Offset: 0x000013CC
		public void ShowLockedMessage(PlantingTool plantingTool, Action failCallback)
		{
			string displayNameLocKey = plantingTool.PlantableSpec.GetSpec<LabeledEntitySpec>().DisplayNameLocKey;
			string message = this._loc.T<string, string>(PlantableToolLocker.UnlockPromptLocKey, plantingTool.BuildingName, this._loc.T(displayNameLocKey));
			this._dialogBoxShower.Create().SetMessage(message).SetConfirmButton(failCallback).Show();
		}

		// Token: 0x04000045 RID: 69
		public static readonly string UnlockPromptLocKey = "Planting.UnlockPrompt";

		// Token: 0x04000046 RID: 70
		public static readonly string InstantUnlockKey = "InstantUnlock";

		// Token: 0x04000047 RID: 71
		public readonly ILoc _loc;

		// Token: 0x04000048 RID: 72
		public readonly InputService _inputService;

		// Token: 0x04000049 RID: 73
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x0400004A RID: 74
		public readonly UnlockedPlantableGroupsRegistry _unlockedPlantableGroupsRegistry;
	}
}
