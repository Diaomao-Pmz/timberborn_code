using System;
using System.Collections.Generic;
using Bindito.Core;
using Timberborn.BlueprintPrefabSystem;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x0200001F RID: 31
	public class PrefabOptimizationChainProvider : IProvider<IPrefabOptimizationChain>
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00004FDE File Offset: 0x000031DE
		public PrefabOptimizationChainProvider(TimbermeshPrefabOptimizer timbermeshPrefabOptimizer, AutoAtlasingPrefabOptimizer autoAtlasingPrefabOptimizer, VertexColorPrefabOptimizer vertexColorPrefabOptimizer, MergeMeshesByMaterialPrefabOptimizer mergeMeshesByMaterialPrefabOptimizer, DestroyEmptyChildrenPrefabOptimizer destroyEmptyChildrenPrefabOptimizer, BlueprintPrefabConverter blueprintPrefabConverter)
		{
			this._timbermeshPrefabOptimizer = timbermeshPrefabOptimizer;
			this._autoAtlasingPrefabOptimizer = autoAtlasingPrefabOptimizer;
			this._vertexColorPrefabOptimizer = vertexColorPrefabOptimizer;
			this._mergeMeshesByMaterialPrefabOptimizer = mergeMeshesByMaterialPrefabOptimizer;
			this._destroyEmptyChildrenPrefabOptimizer = destroyEmptyChildrenPrefabOptimizer;
			this._blueprintPrefabConverter = blueprintPrefabConverter;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005014 File Offset: 0x00003214
		public IPrefabOptimizationChain Get()
		{
			List<IPrefabOptimizer> list = new List<IPrefabOptimizer>
			{
				this._timbermeshPrefabOptimizer
			};
			if (PrefabOptimizationChainConfiguration.AutoAtlasing)
			{
				list.Add(this._autoAtlasingPrefabOptimizer);
			}
			if (PrefabOptimizationChainConfiguration.VertexColor)
			{
				list.Add(this._vertexColorPrefabOptimizer);
			}
			if (PrefabOptimizationChainConfiguration.MergeMeshesByMaterial)
			{
				list.Add(this._mergeMeshesByMaterialPrefabOptimizer);
			}
			if (PrefabOptimizationChainConfiguration.DestroyEmptyChildren)
			{
				list.Add(this._destroyEmptyChildrenPrefabOptimizer);
			}
			return new PrefabOptimizationChain(list, this._blueprintPrefabConverter);
		}

		// Token: 0x04000085 RID: 133
		public readonly TimbermeshPrefabOptimizer _timbermeshPrefabOptimizer;

		// Token: 0x04000086 RID: 134
		public readonly AutoAtlasingPrefabOptimizer _autoAtlasingPrefabOptimizer;

		// Token: 0x04000087 RID: 135
		public readonly VertexColorPrefabOptimizer _vertexColorPrefabOptimizer;

		// Token: 0x04000088 RID: 136
		public readonly MergeMeshesByMaterialPrefabOptimizer _mergeMeshesByMaterialPrefabOptimizer;

		// Token: 0x04000089 RID: 137
		public readonly DestroyEmptyChildrenPrefabOptimizer _destroyEmptyChildrenPrefabOptimizer;

		// Token: 0x0400008A RID: 138
		public readonly BlueprintPrefabConverter _blueprintPrefabConverter;
	}
}
