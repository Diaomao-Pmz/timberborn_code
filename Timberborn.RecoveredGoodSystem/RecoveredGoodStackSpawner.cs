using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Goods;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.RecoveredGoodSystem
{
	// Token: 0x02000018 RID: 24
	public class RecoveredGoodStackSpawner : IUpdatableSingleton
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x000037B8 File Offset: 0x000019B8
		public RecoveredGoodStackSpawner(IBlockService blockService, RecoveredGoodStackCoordinatesFinder recoveredGoodStackCoordinatesFinder, RecoveredGoodStackFactory recoveredGoodStackFactory)
		{
			this._blockService = blockService;
			this._recoveredGoodStackCoordinatesFinder = recoveredGoodStackCoordinatesFinder;
			this._recoveredGoodStackFactory = recoveredGoodStackFactory;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000037EB File Offset: 0x000019EB
		public void AddAwaitingGoods(Vector3Int position, IEnumerable<GoodAmount> recoveredGoods)
		{
			this._awaitingGoods.GetOrAdd(position).AddRange(recoveredGoods);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000037FF File Offset: 0x000019FF
		public void UpdateSingleton()
		{
			if (this._awaitingGoods.Count > 0)
			{
				this.ValidateAwaitingGoods();
				this.SpawnValidatedGoods();
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000381C File Offset: 0x00001A1C
		public void ValidateAwaitingGoods()
		{
			foreach (KeyValuePair<Vector3Int, List<GoodAmount>> keyValuePair in this._awaitingGoods)
			{
				this.ValidateAwaitingGood(keyValuePair.Key, keyValuePair.Value);
			}
			this._awaitingGoods.Clear();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003888 File Offset: 0x00001A88
		public void ValidateAwaitingGood(Vector3Int coordinates, IReadOnlyCollection<GoodAmount> awaitingGood)
		{
			Vector3Int vector3Int;
			if (this._recoveredGoodStackCoordinatesFinder.FindValidCoordinates(coordinates, out vector3Int) && !this.TryMergeAwaitingGood(vector3Int, awaitingGood))
			{
				this._validatedGoods.GetOrAdd(vector3Int).AddRange(awaitingGood);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000038C4 File Offset: 0x00001AC4
		public bool TryMergeAwaitingGood(Vector3Int coordinate, IEnumerable<GoodAmount> awaitingGood)
		{
			RecoveredGoodStack recoveredGoodStack = this._blockService.GetObjectsWithComponentAt<RecoveredGoodStack>(coordinate).FirstOrDefault<RecoveredGoodStack>();
			if (recoveredGoodStack)
			{
				recoveredGoodStack.GiveGoodAmounts(awaitingGood);
				return true;
			}
			return false;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000038F8 File Offset: 0x00001AF8
		public void SpawnValidatedGoods()
		{
			foreach (KeyValuePair<Vector3Int, List<GoodAmount>> keyValuePair in this._validatedGoods)
			{
				this._recoveredGoodStackFactory.Create(keyValuePair.Key, keyValuePair.Value);
			}
			this._validatedGoods.Clear();
		}

		// Token: 0x04000054 RID: 84
		public readonly IBlockService _blockService;

		// Token: 0x04000055 RID: 85
		public readonly RecoveredGoodStackCoordinatesFinder _recoveredGoodStackCoordinatesFinder;

		// Token: 0x04000056 RID: 86
		public readonly RecoveredGoodStackFactory _recoveredGoodStackFactory;

		// Token: 0x04000057 RID: 87
		public readonly Dictionary<Vector3Int, List<GoodAmount>> _awaitingGoods = new Dictionary<Vector3Int, List<GoodAmount>>();

		// Token: 0x04000058 RID: 88
		public readonly Dictionary<Vector3Int, List<GoodAmount>> _validatedGoods = new Dictionary<Vector3Int, List<GoodAmount>>();
	}
}
