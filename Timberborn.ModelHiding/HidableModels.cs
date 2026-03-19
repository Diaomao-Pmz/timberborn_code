using System;
using System.Collections.Generic;
using Timberborn.BlockObjectModelSystem;
using Timberborn.Common;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.ModelHiding
{
	// Token: 0x0200000B RID: 11
	public class HidableModels : ILoadableSingleton
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000024FC File Offset: 0x000006FC
		public HidableModels(MapSize mapSize)
		{
			this._mapSize = mapSize;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002518 File Offset: 0x00000718
		public void Load()
		{
			for (int i = 0; i <= this._mapSize.TotalSize.z; i++)
			{
				this._models.Add(new HashSet<BlockObjectModelController>());
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002554 File Offset: 0x00000754
		public void Add(BlockObjectModelController model)
		{
			this.Remove(model);
			ValueTuple<int, int> modelRange = HidableModels.GetModelRange(model);
			int item = modelRange.Item1;
			int item2 = modelRange.Item2;
			for (int i = item; i < item2; i++)
			{
				this._models[i].Add(model);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000259C File Offset: 0x0000079C
		public void Remove(BlockObjectModelController model)
		{
			ValueTuple<int, int> modelRange = HidableModels.GetModelRange(model);
			int item = modelRange.Item1;
			int item2 = modelRange.Item2;
			for (int i = item; i < item2; i++)
			{
				this._models[i].Remove(model);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025DB File Offset: 0x000007DB
		public ReadOnlyHashSet<BlockObjectModelController> ModelsAt(int level)
		{
			return this._models[level].AsReadOnlyHashSet<BlockObjectModelController>();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025F0 File Offset: 0x000007F0
		public bool TryGetHidableRange(out int minLevel, out int maxLevel)
		{
			minLevel = int.MaxValue;
			maxLevel = int.MinValue;
			for (int i = 0; i < this._models.Count; i++)
			{
				if (!this._models[i].IsEmpty<BlockObjectModelController>())
				{
					minLevel = Math.Min(i, minLevel);
					maxLevel = Math.Max(i, maxLevel);
				}
			}
			return minLevel != int.MaxValue;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002654 File Offset: 0x00000854
		public static ValueTuple<int, int> GetModelRange(BlockObjectModelController model)
		{
			int item = model.HasUndergroundModel ? model.UndergroundBaseZ : model.BlockObject.GetBaseLevel();
			int topLevel = model.BlockObject.GetTopLevel();
			return new ValueTuple<int, int>(item, topLevel);
		}

		// Token: 0x0400000F RID: 15
		public readonly MapSize _mapSize;

		// Token: 0x04000010 RID: 16
		public readonly List<HashSet<BlockObjectModelController>> _models = new List<HashSet<BlockObjectModelController>>();
	}
}
