using System;
using System.IO;
using Bindito.Core;
using Timberborn.ErrorReporting;
using Timberborn.GameSaveRuntimeSystem;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.GameScene
{
	// Token: 0x02000007 RID: 7
	public class GameSceneExceptionStateSaver : MonoBehaviour
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002281 File Offset: 0x00000481
		[Inject]
		public void InjectDependencies(GameSaver gameSaver, TickOnlyArrayService tickOnlyArrayService)
		{
			this._gameSaver = gameSaver;
			this._tickOnlyArrayService = tickOnlyArrayService;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002291 File Offset: 0x00000491
		public void Awake()
		{
			ExceptionListener.FirstUncaughtException += this.OnFirstUncaughtException;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022A4 File Offset: 0x000004A4
		public void OnDestroy()
		{
			ExceptionListener.FirstUncaughtException -= this.OnFirstUncaughtException;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022B7 File Offset: 0x000004B7
		public void OnFirstUncaughtException(object sender, EventArgs e)
		{
			Debug.Log("Creating an exception game save");
			ErrorReporter.ExceptionSave = this.CreateExceptionSave();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022D0 File Offset: 0x000004D0
		public byte[] CreateExceptionSave()
		{
			try
			{
				this._tickOnlyArrayService.ForceAllowAccess();
				using (MemoryStream memoryStream = new MemoryStream())
				{
					this._gameSaver.SaveWithoutFinishingTick(memoryStream);
					return memoryStream.ToArray();
				}
			}
			catch (Exception arg)
			{
				Debug.LogWarning(string.Format("Failed to create an exception game save due to exception: {0}", arg));
			}
			return null;
		}

		// Token: 0x0400000F RID: 15
		[HideInInspector]
		public GameSaver _gameSaver;

		// Token: 0x04000010 RID: 16
		[HideInInspector]
		public TickOnlyArrayService _tickOnlyArrayService;
	}
}
