using System;
using Bindito.Core;
using UnityEngine;

namespace Timberborn.Autosaving
{
	// Token: 0x0200000B RID: 11
	public class AutosaverUnityAdapter : MonoBehaviour
	{
		// Token: 0x0600002F RID: 47 RVA: 0x0000269D File Offset: 0x0000089D
		[Inject]
		public void InjectDependencies(Autosaver autosaver)
		{
			this._autosaver = autosaver;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026A6 File Offset: 0x000008A6
		public void OnApplicationQuit()
		{
			this._autosaver.CreateExitSave();
		}

		// Token: 0x0400001A RID: 26
		[HideInInspector]
		public Autosaver _autosaver;
	}
}
