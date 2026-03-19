using System;
using Bindito.Core;
using UnityEngine;

namespace Timberborn.GameSaveRuntimeSystem
{
	// Token: 0x02000008 RID: 8
	public class GameSaverUnityAdapter : MonoBehaviour
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002406 File Offset: 0x00000606
		[Inject]
		public void InjectDependencies(GameSaver gameSaver)
		{
			this._gameSaver = gameSaver;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000240F File Offset: 0x0000060F
		public void LateUpdate()
		{
			this._gameSaver.SaveQueued();
		}

		// Token: 0x04000012 RID: 18
		[HideInInspector]
		public GameSaver _gameSaver;
	}
}
