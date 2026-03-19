using System;
using Bindito.Unity;
using Timberborn.AssetSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.PrefabOptimization;
using Timberborn.Rendering;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.Terraforming
{
	// Token: 0x0200000A RID: 10
	public class DrillScrewBuilder : BaseComponent, IAwakableComponent, IInitializableEntity, IPreviewStateListener
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000026B5 File Offset: 0x000008B5
		public DrillScrewBuilder(VerticalShapeBuilder verticalShapeBuilder, Highlighter highlighter, IInstantiator instantiator, IPrefabOptimizationChain prefabOptimizationChain, IAssetLoader assetLoader)
		{
			this._verticalShapeBuilder = verticalShapeBuilder;
			this._highlighter = highlighter;
			this._instantiator = instantiator;
			this._prefabOptimizationChain = prefabOptimizationChain;
			this._assetLoader = assetLoader;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026E4 File Offset: 0x000008E4
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._entityMaterials = base.GetComponent<EntityMaterials>();
			this._drillScrewBuilderSpec = base.GetComponent<DrillScrewBuilderSpec>();
			this._drillScrewRotator = base.GetComponent<DrillScrewRotator>();
			this._screwHeadPrefab = this._assetLoader.Load<GameObject>(this._drillScrewBuilderSpec.ScrewHeadPrefabPath);
			this._screwAxisPrefab = this._assetLoader.Load<GameObject>(this._drillScrewBuilderSpec.ScrewAxisPrefabPath);
			this._parent = base.GameObject.FindChildTransform(this._drillScrewBuilderSpec.ParentName);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002775 File Offset: 0x00000975
		public void InitializeEntity()
		{
			this.CreateScrewInstance(false);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000277E File Offset: 0x0000097E
		public void OnEnterPreviewState()
		{
			this.CreateScrewInstance(true);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002788 File Offset: 0x00000988
		public void CreateScrewInstance(bool isPreview)
		{
			if (!this._screwInstance)
			{
				this._screwInstance = this._verticalShapeBuilder.Build(this._parent, this.GetShapeInfo(isPreview));
				this.SetupScrewInstance();
				this._highlighter.ResetAllHighlights(this);
				this._entityMaterials.AddMaterials(this._screwInstance);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027E3 File Offset: 0x000009E3
		public VerticalShapeInfo GetShapeInfo(bool isPreview)
		{
			return new VerticalShapeInfo(isPreview ? DrillScrewBuilder.PreviewShapeLength : this.GetDistanceToBottomOfMap(), this._prefabOptimizationChain.Process(this._screwHeadPrefab), this._prefabOptimizationChain.Process(this._screwAxisPrefab), "DrillScrew");
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002824 File Offset: 0x00000A24
		public void SetupScrewInstance()
		{
			this._screwInstance.transform.SetLocalPositionAndRotation(this.GetScrewPosition(), Quaternion.identity);
			this._drillScrewRotator.Add(this._screwInstance.transform);
			this._instantiator.AddComponent<CapsuleCollider>(this._screwInstance).radius = this._drillScrewBuilderSpec.DrillRadius;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002884 File Offset: 0x00000A84
		public Vector3 GetScrewPosition()
		{
			Vector3 vector = this._blockObject.Orientation.TransformInWorldSpace(this._drillScrewBuilderSpec.AnchorPosition);
			Vector3 coordinates = this._blockObject.Blocks.Pivot(this._blockObject.Coordinates, this._blockObject.Orientation);
			Vector3 vector2 = vector + CoordinateSystem.GridToWorld(coordinates);
			return this._parent.transform.InverseTransformPoint(vector2);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000028F0 File Offset: 0x00000AF0
		public int GetDistanceToBottomOfMap()
		{
			return Mathf.CeilToInt(this._parent.transform.TransformPoint(this._drillScrewBuilderSpec.AnchorPosition).y) + 1;
		}

		// Token: 0x0400001A RID: 26
		public static readonly int PreviewShapeLength = 2;

		// Token: 0x0400001B RID: 27
		public readonly VerticalShapeBuilder _verticalShapeBuilder;

		// Token: 0x0400001C RID: 28
		public readonly Highlighter _highlighter;

		// Token: 0x0400001D RID: 29
		public readonly IInstantiator _instantiator;

		// Token: 0x0400001E RID: 30
		public readonly IPrefabOptimizationChain _prefabOptimizationChain;

		// Token: 0x0400001F RID: 31
		public readonly IAssetLoader _assetLoader;

		// Token: 0x04000020 RID: 32
		public BlockObject _blockObject;

		// Token: 0x04000021 RID: 33
		public EntityMaterials _entityMaterials;

		// Token: 0x04000022 RID: 34
		public DrillScrewBuilderSpec _drillScrewBuilderSpec;

		// Token: 0x04000023 RID: 35
		public DrillScrewRotator _drillScrewRotator;

		// Token: 0x04000024 RID: 36
		public GameObject _screwHeadPrefab;

		// Token: 0x04000025 RID: 37
		public GameObject _screwAxisPrefab;

		// Token: 0x04000026 RID: 38
		public Transform _parent;

		// Token: 0x04000027 RID: 39
		public GameObject _screwInstance;
	}
}
