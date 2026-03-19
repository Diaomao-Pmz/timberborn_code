using System;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.WorkSystem;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x02000020 RID: 32
	public class WorkplaceUnlockingDialogService
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00003BC8 File Offset: 0x00001DC8
		public WorkplaceUnlockingDialogService(DialogBoxShower dialogBoxShower, InputService inputService, WorkplaceUnlockingService workplaceUnlockingService, ILoc loc)
		{
			this._dialogBoxShower = dialogBoxShower;
			this._inputService = inputService;
			this._workplaceUnlockingService = workplaceUnlockingService;
			this._loc = loc;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003BF0 File Offset: 0x00001DF0
		public void TryToUnlockWorkerType(UnlockableWorkerType unlockableWorkerType, Action callback)
		{
			if (!this._workplaceUnlockingService.Unlocked(unlockableWorkerType))
			{
				if (this._inputService.IsKeyHeld(WorkplaceUnlockingDialogService.InstantUnlockKey))
				{
					this.UnlockIgnoringScienceCost(unlockableWorkerType, callback);
					return;
				}
				if (this._workplaceUnlockingService.Unlockable(unlockableWorkerType))
				{
					this.AskForUnlockingConfirmation(unlockableWorkerType, callback);
					return;
				}
				this.ShowInsufficientSciencePointsMessage();
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003C43 File Offset: 0x00001E43
		public bool IsWorkerTypeUnlocked(UnlockableWorkerType unlockableWorkerType)
		{
			return this._workplaceUnlockingService.Unlocked(unlockableWorkerType);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003C51 File Offset: 0x00001E51
		public int GetWorkerTypeUnlockCost(UnlockableWorkerType unlockableWorkerType)
		{
			return this._workplaceUnlockingService.GetUnlockCost(unlockableWorkerType);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003C5F File Offset: 0x00001E5F
		public void UnlockIgnoringScienceCost(UnlockableWorkerType unlockableWorkerType, Action callback)
		{
			this._workplaceUnlockingService.UnlockIgnoringCost(unlockableWorkerType);
			callback();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003C74 File Offset: 0x00001E74
		public void AskForUnlockingConfirmation(UnlockableWorkerType unlockableWorkerType, Action callback)
		{
			this._dialogBoxShower.Create().SetMessage(this.GetUnlockPromptMessage(unlockableWorkerType)).SetConfirmButton(delegate()
			{
				this._workplaceUnlockingService.Unlock(unlockableWorkerType);
				callback();
			}).SetDefaultCancelButton().Show();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003CD4 File Offset: 0x00001ED4
		public void ShowInsufficientSciencePointsMessage()
		{
			this._dialogBoxShower.Create().SetLocalizedMessage(WorkplaceUnlockingDialogService.CantUnlockLocKey).Show();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003CF1 File Offset: 0x00001EF1
		public string GetUnlockPromptMessage(UnlockableWorkerType unlockableWorkerType)
		{
			return this._loc.T<int>(WorkplaceUnlockingDialogService.UnlockPromptLocKey, this._workplaceUnlockingService.GetUnlockCost(unlockableWorkerType));
		}

		// Token: 0x04000094 RID: 148
		public static readonly string CantUnlockLocKey = "Work.WorkplaceUnlock.CantUnlock";

		// Token: 0x04000095 RID: 149
		public static readonly string UnlockPromptLocKey = "Work.WorkplaceUnlock.Prompt";

		// Token: 0x04000096 RID: 150
		public static readonly string InstantUnlockKey = "InstantUnlock";

		// Token: 0x04000097 RID: 151
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x04000098 RID: 152
		public readonly InputService _inputService;

		// Token: 0x04000099 RID: 153
		public readonly WorkplaceUnlockingService _workplaceUnlockingService;

		// Token: 0x0400009A RID: 154
		public readonly ILoc _loc;
	}
}
