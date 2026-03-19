using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.GameFactionSystem;
using Timberborn.TemplateSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.PathSystem
{
	// Token: 0x0200000E RID: 14
	public class DynamicPathModel : BaseComponent, IAwakableComponent, IModelUpdater
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002DE8 File Offset: 0x00000FE8
		public DynamicPathModel(IConnectionService connectionService, FactionService factionService, StackableBlockService stackableBlockService, IBlockService blockService, PreviewBlockService previewBlockService, ITerrainService terrainService)
		{
			this._connectionService = connectionService;
			this._factionService = factionService;
			this._stackableBlockService = stackableBlockService;
			this._blockService = blockService;
			this._previewBlockService = previewBlockService;
			this._terrainService = terrainService;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E3E File Offset: 0x0000103E
		public void Awake()
		{
			this._buildingSpec = base.GetComponent<BuildingSpec>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._dynamicPathModelSpec = base.GetComponent<DynamicPathModelSpec>();
			this.ValidateSize();
			this.InitializeModels();
			this.SetMatchingModel(false, false, false, false);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E7C File Offset: 0x0000107C
		public void UpdateModel()
		{
			Vector3Int pathOrigin = this.GetPathOrigin();
			this.SetMatchingModel(this.CanConnectInDirection(pathOrigin, Direction2D.Down), this.CanConnectInDirection(pathOrigin, Direction2D.Left), this.CanConnectInDirection(pathOrigin, Direction2D.Up), this.CanConnectInDirection(pathOrigin, Direction2D.Right));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002EB6 File Offset: 0x000010B6
		public void ValidateSize()
		{
			if (base.GetComponent<BlockObjectSpec>().Size != Vector3Int.one)
			{
				throw new InvalidOperationException(this._buildingSpec.GetSpec<TemplateSpec>().TemplateName + " validation failed. DynamicPathModel is only compatible with 1x1x1-sized buildings");
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002EF0 File Offset: 0x000010F0
		public void InitializeModels()
		{
			this.AddModel("0000", false, false, false, false);
			this.AddModel("0010", false, false, true, false);
			this.AddModel("1010", true, false, true, false);
			this.AddModel("0011", false, false, true, true);
			this.AddModel("0111", false, true, true, true);
			this.AddModel("1111", true, true, true, true);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002F58 File Offset: 0x00001158
		public void AddModel(string variant, bool down, bool left, bool up, bool right)
		{
			if (!string.IsNullOrWhiteSpace(this._dynamicPathModelSpec.GroundModelPrefix))
			{
				this._groundModels.AddVariants(this.GetModelVariant(this._dynamicPathModelSpec.GroundModelPrefix, variant, this._factionService.Current.PathMaterial.Asset), down, left, up, right);
			}
			if (!string.IsNullOrWhiteSpace(this._dynamicPathModelSpec.RoofModelPrefix))
			{
				this._roofModels.AddVariants(this.GetModelVariant(this._dynamicPathModelSpec.RoofModelPrefix, variant, this._factionService.Current.BaseWoodMaterial.Asset), down, left, up, right);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002FFC File Offset: 0x000011FC
		public GameObject GetModelVariant(string prefix, string variant, Material material)
		{
			string childName = prefix + variant;
			GameObject gameObject = base.GameObject.FindChild(childName);
			gameObject.SetActive(false);
			gameObject.GetComponentInChildren<Renderer>().sharedMaterial = material;
			return gameObject;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003030 File Offset: 0x00001230
		public void SetMatchingModel(bool down, bool left, bool up, bool right)
		{
			NeighboredValues4<GameObject> neighboredValues = this.IsValidForGroundModel() ? this._groundModels : this._roofModels;
			if (!neighboredValues.IsEmpty)
			{
				GameObject gameObject;
				Orientation orientation;
				neighboredValues.GetMatch(down, left, up, right).Deconstruct(out gameObject, out orientation);
				GameObject model = gameObject;
				Orientation orientation2 = orientation;
				this.SetCurrentModel(model, orientation2);
				return;
			}
			if (this._currentModel)
			{
				this._currentModel.SetActive(false);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000309C File Offset: 0x0000129C
		public bool IsValidForGroundModel()
		{
			Vector3Int coordinates = this._blockObject.Coordinates;
			if (!this.IsEnforced(coordinates, PathModelType.Roof))
			{
				Vector3Int vector3Int = coordinates - new Vector3Int(0, 0, 1);
				bool flag = this._terrainService.OnGround(coordinates) || this._stackableBlockService.IsUnfinishedGroundBlockAt(vector3Int);
				bool flag2 = this.IsEnforced(vector3Int, PathModelType.Ground);
				return flag || flag2;
			}
			return false;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000030F8 File Offset: 0x000012F8
		public bool IsEnforced(Vector3Int coordinates, PathModelType modelType)
		{
			PathModelTypeEnforcer pathModelTypeEnforcer = this._blockService.GetObjectsWithComponentAt<PathModelTypeEnforcer>(coordinates).FirstOrDefault<PathModelTypeEnforcer>() ?? this._previewBlockService.GetObjectsWithComponentAt<PathModelTypeEnforcer>(coordinates).FirstOrDefault<PathModelTypeEnforcer>();
			return pathModelTypeEnforcer != null && pathModelTypeEnforcer.PathModelType == modelType;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000313C File Offset: 0x0000133C
		public void SetCurrentModel(GameObject model, Orientation orientation)
		{
			if (!(this._currentModel != model) && this._currentModelOrientation == orientation)
			{
				GameObject currentModel = this._currentModel;
				if (currentModel == null || currentModel.activeSelf)
				{
					return;
				}
			}
			if (this._currentModel)
			{
				this._currentModel.SetActive(false);
			}
			Vector3 vector = CoordinateSystem.GridToWorld(orientation.ToPivotOffset());
			Quaternion quaternion = orientation.ToWorldSpaceRotation();
			model.transform.SetLocalPositionAndRotation(vector, quaternion);
			this._currentModel = model;
			this._currentModelOrientation = orientation;
			this._currentModel.SetActive(true);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000031CC File Offset: 0x000013CC
		public Vector3Int GetPathOrigin()
		{
			foreach (Block block2 in from block in this._blockObject.PositionedBlocks.GetOccupiedBlocks()
			orderby block.Coordinates.z descending
			select block)
			{
				if (block2.Occupation.Intersects(BlockOccupations.Path))
				{
					return block2.Coordinates;
				}
			}
			throw new InvalidOperationException(this._buildingSpec.GetSpec<TemplateSpec>().TemplateName + " validation failed. DynamicPathModel should have at least one block intersecting with Path, whose neighbors are checked when selecting a fitting model.");
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000327C File Offset: 0x0000147C
		public bool CanConnectInDirection(Vector3Int origin, Direction2D direction2D)
		{
			Direction2D direction2D2 = this._blockObject.Orientation.Transform(direction2D);
			return this._connectionService.CanConnectInDirection(origin, direction2D2);
		}

		// Token: 0x04000034 RID: 52
		public readonly IConnectionService _connectionService;

		// Token: 0x04000035 RID: 53
		public readonly FactionService _factionService;

		// Token: 0x04000036 RID: 54
		public readonly StackableBlockService _stackableBlockService;

		// Token: 0x04000037 RID: 55
		public readonly IBlockService _blockService;

		// Token: 0x04000038 RID: 56
		public readonly PreviewBlockService _previewBlockService;

		// Token: 0x04000039 RID: 57
		public readonly ITerrainService _terrainService;

		// Token: 0x0400003A RID: 58
		public BuildingSpec _buildingSpec;

		// Token: 0x0400003B RID: 59
		public BlockObject _blockObject;

		// Token: 0x0400003C RID: 60
		public DynamicPathModelSpec _dynamicPathModelSpec;

		// Token: 0x0400003D RID: 61
		public GameObject _currentModel;

		// Token: 0x0400003E RID: 62
		public Orientation _currentModelOrientation;

		// Token: 0x0400003F RID: 63
		public readonly NeighboredValues4<GameObject> _groundModels = new NeighboredValues4<GameObject>();

		// Token: 0x04000040 RID: 64
		public readonly NeighboredValues4<GameObject> _roofModels = new NeighboredValues4<GameObject>();
	}
}
