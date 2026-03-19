using System;
using System.IO;
using Bindito.Core;
using Timberborn.ErrorReporting;
using Timberborn.MapSystem;
using UnityEngine;

namespace Timberborn.MapEditorScene
{
	// Token: 0x02000005 RID: 5
	public class MapEditorSceneExceptionStateSaver : MonoBehaviour
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000021CC File Offset: 0x000003CC
		[Inject]
		public void InjectDependencies(MapSaver mapSaver)
		{
			this._mapSaver = mapSaver;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021D5 File Offset: 0x000003D5
		public void Awake()
		{
			ExceptionListener.FirstUncaughtException += this.OnFirstUncaughtException;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021E8 File Offset: 0x000003E8
		public void OnDestroy()
		{
			ExceptionListener.FirstUncaughtException -= this.OnFirstUncaughtException;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021FB File Offset: 0x000003FB
		private void OnFirstUncaughtException(object sender, EventArgs e)
		{
			Debug.Log("Creating an exception map save");
			ErrorReporter.ExceptionSave = this.CreateExceptionSave();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002214 File Offset: 0x00000414
		private byte[] CreateExceptionSave()
		{
			try
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					this._mapSaver.Save(memoryStream);
					return memoryStream.ToArray();
				}
			}
			catch (Exception arg)
			{
				Debug.LogWarning(string.Format("Failed to create an exception map save due to exception: {0}", arg));
			}
			return null;
		}

		// Token: 0x04000003 RID: 3
		private MapSaver _mapSaver;
	}
}
