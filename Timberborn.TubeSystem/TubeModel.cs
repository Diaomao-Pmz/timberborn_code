using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.TemplateSystem;
using UnityEngine;

namespace Timberborn.TubeSystem
{
	// Token: 0x0200000D RID: 13
	public class TubeModel : BaseComponent, IAwakableComponent, IModelUpdater
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000025C6 File Offset: 0x000007C6
		public TubeModel(TubeConnectionService tubeConnectionService)
		{
			this._tubeConnectionService = tubeConnectionService;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025E0 File Offset: 0x000007E0
		public void Awake()
		{
			this._buildingSpec = base.GetComponent<BuildingSpec>();
			this._tubeModelSpec = base.GetComponent<TubeModelSpec>();
			this._blockObject = base.GetComponent<BlockObject>();
			this.ValidateSize();
			this.InitializeModels();
			this.UpdateModel(false, false, false, false, false, false);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002620 File Offset: 0x00000820
		public void UpdateModel()
		{
			Vector3Int origin = this.GetOrigin();
			this.UpdateModel(this.CanConnectInDirection(origin, Direction3D.Down), this.CanConnectInDirection(origin, Direction3D.Left), this.CanConnectInDirection(origin, Direction3D.Up), this.CanConnectInDirection(origin, Direction3D.Right), this.CanConnectInDirection(origin, Direction3D.Top), this.CanConnectInDirection(origin, Direction3D.Bottom));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000266A File Offset: 0x0000086A
		public void ValidateSize()
		{
			if (base.GetComponent<BlockObjectSpec>().Size != Vector3Int.one)
			{
				throw new InvalidOperationException(this._buildingSpec.GetSpec<TemplateSpec>().TemplateName + " validation failed. TubeModel is only compatible with 1x1x1-sized buildings");
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000026A4 File Offset: 0x000008A4
		public void InitializeModels()
		{
			foreach (GameObject model in base.GameObject.GetAllChildren())
			{
				this.AddModelIfValid(model);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000026F8 File Offset: 0x000008F8
		public void AddModelIfValid(GameObject model)
		{
			if (model.name.StartsWith(this._tubeModelSpec.ModelPrefix))
			{
				model.SetActive(false);
				string name = model.name;
				int length = this._tubeModelSpec.ModelPrefix.Length;
				string text = name.Substring(length, name.Length - length);
				this._models.AddVariants(model, text[0] == '1', text[1] == '1', text[2] == '1', text[3] == '1', text[4] == '1', text[5] == '1');
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000279C File Offset: 0x0000099C
		public void UpdateModel(bool down, bool left, bool up, bool right, bool top, bool bottom)
		{
			GameObject gameObject;
			Orientation orientation;
			this._models.GetMatch(down, left, up, right, top, bottom).Deconstruct(out gameObject, out orientation);
			GameObject model = gameObject;
			Orientation orientation2 = orientation;
			this.SetCurrentModel(model, orientation2);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027D8 File Offset: 0x000009D8
		public void SetCurrentModel(GameObject model, Orientation orientation)
		{
			if (this._currentModel != model || this._currentModelOrientation != orientation)
			{
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
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002850 File Offset: 0x00000A50
		public Vector3Int GetOrigin()
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
			throw new InvalidOperationException(this._buildingSpec.GetSpec<TemplateSpec>().TemplateName + " validation failed. TubeModel should have at least one block intersecting with Path, whose neighbors are checked when selecting a fitting model.");
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002900 File Offset: 0x00000B00
		public bool CanConnectInDirection(Vector3Int origin, Direction3D direction)
		{
			Direction3D direction3D = direction.RotateHorizontally(this._blockObject.Orientation);
			return this._tubeConnectionService.CanConnectInDirection(origin, direction3D);
		}

		// Token: 0x0400001F RID: 31
		public readonly TubeConnectionService _tubeConnectionService;

		// Token: 0x04000020 RID: 32
		public BuildingSpec _buildingSpec;

		// Token: 0x04000021 RID: 33
		public TubeModelSpec _tubeModelSpec;

		// Token: 0x04000022 RID: 34
		public BlockObject _blockObject;

		// Token: 0x04000023 RID: 35
		public GameObject _currentModel;

		// Token: 0x04000024 RID: 36
		public Orientation _currentModelOrientation;

		// Token: 0x04000025 RID: 37
		public readonly NeighboredValues6<GameObject> _models = new NeighboredValues6<GameObject>();
	}
}
