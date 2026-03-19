using System;
using System.Collections.Generic;
using Timberborn.DeconstructionSystem;
using Timberborn.Goods;
using Timberborn.InputSystem;
using Timberborn.RecoverableGoodSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.RecoveredGoodSystem
{
	// Token: 0x02000007 RID: 7
	public class BuildingGoodsRecoveryService : ILoadableSingleton
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public BuildingGoodsRecoveryService(EventBus eventBus, InputService inputService, RecoveredGoodStackSpawner recoveredGoodStackSpawner)
		{
			this._eventBus = eventBus;
			this._inputService = inputService;
			this._recoveredGoodStackSpawner = recoveredGoodStackSpawner;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002133 File Offset: 0x00000333
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002141 File Offset: 0x00000341
		[OnEvent]
		public void OnBuildingDeconstructed(BuildingDeconstructedEvent buildingDeconstructedEvent)
		{
			if (!this._inputService.IsKeyHeld(BuildingGoodsRecoveryService.DontRecoverGoodsKey))
			{
				this.PrepareToSpawning(buildingDeconstructedEvent.Deconstructible, buildingDeconstructedEvent.Coordinates);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000216C File Offset: 0x0000036C
		public void PrepareToSpawning(Deconstructible deconstructible, IReadOnlyList<Vector3Int> coordinates)
		{
			if (coordinates.Count > 0)
			{
				deconstructible.GetComponent<RecoverableGoodProvider>().GetRecoverableGoods(this._recoverableGoodRegistry);
				if (this._recoverableGoodRegistry.TotalAmount > 0)
				{
					this.SplitGoodsAndAddToSpawnQueue(coordinates);
					this.CheckIfAllGoodsWereRecovered(deconstructible);
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021A4 File Offset: 0x000003A4
		public void SplitGoodsAndAddToSpawnQueue(IReadOnlyList<Vector3Int> coordinates)
		{
			for (int i = 0; i < coordinates.Count; i++)
			{
				this._recoverableGoodRegistry.TakePercent(1f / (float)(coordinates.Count - i), this._recoveredGoods);
				if (this._recoveredGoods.Count > 0)
				{
					this._recoveredGoodStackSpawner.AddAwaitingGoods(coordinates[i], this._recoveredGoods);
					this._recoveredGoods.Clear();
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002214 File Offset: 0x00000414
		public void CheckIfAllGoodsWereRecovered(Deconstructible deconstructible)
		{
			if (this._recoverableGoodRegistry.TotalAmount > 0 || this._recoverableGoodRegistry.GoodAmounts.Count > 0)
			{
				throw new InvalidOperationException(string.Format("Not all goods were recovered from {0}: ", deconstructible) + string.Join<GoodAmount>(", ", this._recoverableGoodRegistry.GoodAmounts));
			}
		}

		// Token: 0x04000008 RID: 8
		public static readonly string DontRecoverGoodsKey = "DontRecoverGoods";

		// Token: 0x04000009 RID: 9
		public readonly EventBus _eventBus;

		// Token: 0x0400000A RID: 10
		public readonly InputService _inputService;

		// Token: 0x0400000B RID: 11
		public readonly RecoveredGoodStackSpawner _recoveredGoodStackSpawner;

		// Token: 0x0400000C RID: 12
		public readonly RecoverableGoodRegistry _recoverableGoodRegistry = new RecoverableGoodRegistry();

		// Token: 0x0400000D RID: 13
		public readonly List<GoodAmount> _recoveredGoods = new List<GoodAmount>();
	}
}
