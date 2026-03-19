using System;
using System.Linq;
using Timberborn.Debugging;
using Timberborn.GameSaveRuntimeSystem;
using Timberborn.QuickNotificationSystem;
using UnityEngine;

namespace Timberborn.GameSaveRuntimeSystemUI
{
	// Token: 0x02000004 RID: 4
	public class GameSaverDevModule : IDevModule
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public GameSaverDevModule(GameSaver gameSaver, QuickNotificationService quickNotificationService)
		{
			this._gameSaver = gameSaver;
			this._quickNotificationService = quickNotificationService;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create(string.Format("Save {0}x to memory", GameSaverDevModule.SaveCount), new Action(this.Save))).Build();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000210C File Offset: 0x0000030C
		public void Save()
		{
			double num = this._gameSaver.BenchmarkSavingToMemory(GameSaverDevModule.SaveCount).Average((TimeSpan timeSpan) => timeSpan.TotalSeconds);
			string text = string.Format("Saved {0}x to memory in an average of {1:0.00}s", GameSaverDevModule.SaveCount, num);
			this._quickNotificationService.SendNotification(text);
			Debug.Log(text);
		}

		// Token: 0x04000006 RID: 6
		public static readonly int SaveCount = 20;

		// Token: 0x04000007 RID: 7
		public readonly GameSaver _gameSaver;

		// Token: 0x04000008 RID: 8
		public readonly QuickNotificationService _quickNotificationService;
	}
}
