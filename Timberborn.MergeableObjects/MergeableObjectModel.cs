using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.TemplateSystem;
using UnityEngine;

namespace Timberborn.MergeableObjects
{
	// Token: 0x02000007 RID: 7
	public class MergeableObjectModel : BaseComponent, IAwakableComponent, IModelUpdater
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public MergeableObjectModel(IBlockService blockService, PreviewBlockService previewBlockService)
		{
			this._blockService = blockService;
			this._previewBlockService = previewBlockService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211F File Offset: 0x0000031F
		public void Awake()
		{
			this._templateSpec = base.GetComponent<TemplateSpec>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._mergeableObjectModelSpec = base.GetComponent<MergeableObjectModelSpec>();
			this.ValidateSize();
			this.InitializeModels();
			this.SetMatchingModel(true, false, true, false);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000215C File Offset: 0x0000035C
		public void UpdateModel()
		{
			Vector3Int origin = this._blockObject.PositionedBlocks.GetOccupiedCoordinates().First<Vector3Int>();
			this.SetMatchingModel(this.IsMatchingInDirection(origin, Direction2D.Down), this.IsMatchingInDirection(origin, Direction2D.Left), this.IsMatchingInDirection(origin, Direction2D.Up), this.IsMatchingInDirection(origin, Direction2D.Right));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021A5 File Offset: 0x000003A5
		public void ValidateSize()
		{
			if (this._blockObject.Blocks.Size != Vector3Int.one)
			{
				throw new InvalidOperationException(this._templateSpec.TemplateName + " validation failed. MergeableObjectModel is only compatible with 1x1x1-sized buildings");
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021E0 File Offset: 0x000003E0
		public void InitializeModels()
		{
			this.AddModel("0000", false, false, false, false);
			this.AddModel("0001", false, true, false, false);
			this.AddModel("1010", false, true, false, true);
			this.AddModel("0011", false, false, true, true);
			this.AddModel("0111", false, true, true, true);
			this.AddModel("1111", true, true, true, true);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002248 File Offset: 0x00000448
		public void AddModel(string variant, bool down, bool left, bool up, bool right)
		{
			string childName = this._mergeableObjectModelSpec.ModelNamePrefix + variant;
			GameObject gameObject = base.GameObject.FindChild(childName);
			gameObject.SetActive(false);
			this._models.AddVariants(gameObject, down, left, up, right);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002290 File Offset: 0x00000490
		public void SetMatchingModel(bool down, bool left, bool up, bool right)
		{
			GameObject gameObject;
			Orientation orientation;
			this._models.GetMatch(down, left, up, right).Deconstruct(out gameObject, out orientation);
			GameObject model = gameObject;
			Orientation orientation2 = orientation;
			this.SetCurrentModel(model, orientation2);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022C8 File Offset: 0x000004C8
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

		// Token: 0x0600000F RID: 15 RVA: 0x00002340 File Offset: 0x00000540
		public bool IsMatchingInDirection(Vector3Int origin, Direction2D direction2D)
		{
			Direction2D direction2D2 = this._blockObject.Orientation.Transform(direction2D);
			Vector3Int vector3Int = origin + direction2D2.ToOffset();
			MergeableObjectModel bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<MergeableObjectModel>(vector3Int);
			MergeableObjectModel bottomObjectComponentAt2 = this._previewBlockService.GetBottomObjectComponentAt<MergeableObjectModel>(vector3Int);
			return this.IsMatchingType(bottomObjectComponentAt) || this.IsMatchingType(bottomObjectComponentAt2) || this.IsEnforced(vector3Int);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023A1 File Offset: 0x000005A1
		public bool IsMatchingType(MergeableObjectModel otherModel)
		{
			return otherModel && otherModel._templateSpec.TemplateName == this._templateSpec.TemplateName;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023C8 File Offset: 0x000005C8
		public bool IsEnforced(Vector3Int target)
		{
			MergeableObjectModelEnforcerSpec bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<MergeableObjectModelEnforcerSpec>(target);
			MergeableObjectModelEnforcerSpec bottomObjectComponentAt2 = this._previewBlockService.GetBottomObjectComponentAt<MergeableObjectModelEnforcerSpec>(target);
			return bottomObjectComponentAt != null || bottomObjectComponentAt2 != null;
		}

		// Token: 0x04000008 RID: 8
		public readonly IBlockService _blockService;

		// Token: 0x04000009 RID: 9
		public readonly PreviewBlockService _previewBlockService;

		// Token: 0x0400000A RID: 10
		public TemplateSpec _templateSpec;

		// Token: 0x0400000B RID: 11
		public BlockObject _blockObject;

		// Token: 0x0400000C RID: 12
		public MergeableObjectModelSpec _mergeableObjectModelSpec;

		// Token: 0x0400000D RID: 13
		public GameObject _currentModel;

		// Token: 0x0400000E RID: 14
		public Orientation _currentModelOrientation;

		// Token: 0x0400000F RID: 15
		public readonly NeighboredValues4<GameObject> _models = new NeighboredValues4<GameObject>();
	}
}
