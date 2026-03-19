using System;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.GameSceneLoading;
using Timberborn.SceneLoading;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.SettlementNameSystem
{
	// Token: 0x02000005 RID: 5
	public class SettlementReferenceService : ILoadableSingleton
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020D4 File Offset: 0x000002D4
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020DC File Offset: 0x000002DC
		public SettlementReference SettlementReference { get; private set; }

		// Token: 0x06000007 RID: 7 RVA: 0x000020E5 File Offset: 0x000002E5
		public SettlementReferenceService(ISceneLoader sceneLoader, GameSaveRepository gameSaveRepository)
		{
			this._sceneLoader = sceneLoader;
			this._gameSaveRepository = gameSaveRepository;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020FC File Offset: 0x000002FC
		public void Load()
		{
			GameSceneParameters sceneParameters = this._sceneLoader.GetSceneParameters<GameSceneParameters>();
			if (sceneParameters.NewGame)
			{
				string settlementName = sceneParameters.NewGameConfiguration.SettlementName;
				if (!string.IsNullOrWhiteSpace(settlementName))
				{
					this.InitializeSettlementReference(new SettlementReference(settlementName, this._gameSaveRepository.DefaultSaveDirectory));
					return;
				}
			}
			else
			{
				this.InitializeSettlementReference(sceneParameters.SaveReference.SettlementReference);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000215A File Offset: 0x0000035A
		public void InitializeAndLogSettlementName(string settlementName)
		{
			this.InitializeSettlementReference(new SettlementReference(settlementName, this._gameSaveRepository.DefaultSaveDirectory));
			Debug.Log("Initialized SettlementReference to " + settlementName);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002183 File Offset: 0x00000383
		public void InitializeSettlementReference(SettlementReference settlementReference)
		{
			if (string.IsNullOrWhiteSpace(settlementReference.SettlementName))
			{
				throw new ArgumentException(string.Format("{0} is not valid settlement name", settlementReference));
			}
			if (this.SettlementReference != null)
			{
				throw new InvalidOperationException("SettlementReference is already initialized");
			}
			this.SettlementReference = settlementReference;
		}

		// Token: 0x04000007 RID: 7
		public readonly ISceneLoader _sceneLoader;

		// Token: 0x04000008 RID: 8
		public readonly GameSaveRepository _gameSaveRepository;
	}
}
