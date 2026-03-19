using System;
using Timberborn.AssetSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.PrefabOptimization;
using Timberborn.Rendering;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000033 RID: 51
	public class WaterInputPipeSegmentCreator : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600025E RID: 606 RVA: 0x00007648 File Offset: 0x00005848
		public WaterInputPipeSegmentCreator(OptimizedPrefabInstantiator optimizedPrefabInstantiator, IAssetLoader assetLoader, IRandomNumberGenerator randomNumberGenerator, MaterialColorer materialColorer)
		{
			this._optimizedPrefabInstantiator = optimizedPrefabInstantiator;
			this._assetLoader = assetLoader;
			this._randomNumberGenerator = randomNumberGenerator;
			this._materialColorer = materialColorer;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00007670 File Offset: 0x00005870
		public void Awake()
		{
			this._entityMaterials = base.GetComponent<EntityMaterials>();
			WaterInputSpec component = base.GetComponent<WaterInputSpec>();
			this._prefab = this._assetLoader.Load<GameObject>(component.PipeSegmentPrefabPath);
			this._parent = base.GameObject.FindChildTransform(component.PipeParentName);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000076C0 File Offset: 0x000058C0
		public PipeSegment CreateFinished()
		{
			GameObject gameObject = this._optimizedPrefabInstantiator.Instantiate(this._prefab, this._parent);
			this._entityMaterials.AddMaterials(gameObject);
			float enumerableElement = this._randomNumberGenerator.GetEnumerableElement<float>(WaterInputPipeSegmentCreator.PipeRotations);
			return PipeSegment.Create(gameObject, enumerableElement);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000770C File Offset: 0x0000590C
		public PipeSegment CreateUnfinished()
		{
			PipeSegment pipeSegment = this.CreateFinished();
			this._materialColorer.EnableGrayscale(pipeSegment.Root);
			return pipeSegment;
		}

		// Token: 0x040000EC RID: 236
		public static readonly float[] PipeRotations = new float[]
		{
			0f,
			90f,
			180f,
			270f
		};

		// Token: 0x040000ED RID: 237
		public readonly OptimizedPrefabInstantiator _optimizedPrefabInstantiator;

		// Token: 0x040000EE RID: 238
		public readonly IAssetLoader _assetLoader;

		// Token: 0x040000EF RID: 239
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x040000F0 RID: 240
		public readonly MaterialColorer _materialColorer;

		// Token: 0x040000F1 RID: 241
		public EntityMaterials _entityMaterials;

		// Token: 0x040000F2 RID: 242
		public GameObject _prefab;

		// Token: 0x040000F3 RID: 243
		public Transform _parent;
	}
}
