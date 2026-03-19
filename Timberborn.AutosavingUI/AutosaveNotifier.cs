using System;
using System.IO;
using Timberborn.Autosaving;
using Timberborn.GameSaveRuntimeSystem;
using Timberborn.Localization;
using Timberborn.QuickNotificationSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.AutosavingUI
{
	// Token: 0x02000004 RID: 4
	public class AutosaveNotifier : ILoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public AutosaveNotifier(EventBus eventBus, QuickNotificationService quickNotificationService, ILoc loc)
		{
			this._eventBus = eventBus;
			this._quickNotificationService = quickNotificationService;
			this._loc = loc;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020DB File Offset: 0x000002DB
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E9 File Offset: 0x000002E9
		[OnEvent]
		public void OnAutosave(AutosaveEvent autosaveEvent)
		{
			if (autosaveEvent.Successful)
			{
				this.NotifyAboutSuccess();
				return;
			}
			this.NotifyAboutFailure(autosaveEvent.Exception);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002106 File Offset: 0x00000306
		public void NotifyAboutSuccess()
		{
			this._quickNotificationService.SendNotification(this._loc.T(AutosaveNotifier.Success));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002124 File Offset: 0x00000324
		public void NotifyAboutFailure(GameSaverException gameSaverException)
		{
			string text = AutosaveNotifier.IsFullDisk(gameSaverException.InnerException) ? this._loc.T(AutosaveNotifier.FailureDueToFullDisk) : this._loc.T(AutosaveNotifier.Failure);
			this._quickNotificationService.SendWarningNotification(text);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002170 File Offset: 0x00000370
		public static bool IsFullDisk(Exception exception)
		{
			IOException ex = exception as IOException;
			return ex != null && ((ex.HResult & 65535) == 39 || (ex.HResult & 65535) == 112);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string Success = "Autosave.Success";

		// Token: 0x04000007 RID: 7
		public static readonly string Failure = "Autosave.Failure";

		// Token: 0x04000008 RID: 8
		public static readonly string FailureDueToFullDisk = "Autosave.FailureDueToFullDisk";

		// Token: 0x04000009 RID: 9
		public readonly EventBus _eventBus;

		// Token: 0x0400000A RID: 10
		public readonly QuickNotificationService _quickNotificationService;

		// Token: 0x0400000B RID: 11
		public readonly ILoc _loc;
	}
}
