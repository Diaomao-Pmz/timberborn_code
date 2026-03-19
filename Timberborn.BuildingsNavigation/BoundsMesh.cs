using System;
using System.Collections.Generic;
using Timberborn.PrefabOptimization;
using UnityEngine;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x02000007 RID: 7
	public class BoundsMesh
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public void Initialize(Material baseMaterial)
		{
			this._baseMaterial = baseMaterial;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000210C File Offset: 0x0000030C
		public void Reset()
		{
			foreach (BoundsMeshLayer boundsMeshLayer in this._layers.Values)
			{
				boundsMeshLayer.Reset();
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002164 File Offset: 0x00000364
		public void Build()
		{
			foreach (BoundsMeshLayer boundsMeshLayer in this._layers.Values)
			{
				boundsMeshLayer.Build();
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021BC File Offset: 0x000003BC
		public void Draw()
		{
			foreach (BoundsMeshLayer boundsMeshLayer in this._layers.Values)
			{
				boundsMeshLayer.Draw();
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002214 File Offset: 0x00000414
		public void Append(int index, IntermediateMesh mesh, TranslationTransform translation)
		{
			BoundsMeshLayer boundsMeshLayer;
			if (!this._layers.TryGetValue(index, out boundsMeshLayer))
			{
				boundsMeshLayer = (this._layers[index] = BoundsMeshLayer.Create(this._baseMaterial, index));
			}
			boundsMeshLayer.AppendMesh(mesh, translation);
		}

		// Token: 0x04000008 RID: 8
		public readonly Dictionary<int, BoundsMeshLayer> _layers = new Dictionary<int, BoundsMeshLayer>();

		// Token: 0x04000009 RID: 9
		public Material _baseMaterial;
	}
}
