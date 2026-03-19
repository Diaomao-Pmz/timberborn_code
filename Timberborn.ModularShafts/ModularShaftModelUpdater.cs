using System;
using Bindito.Unity;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Coordinates;
using Timberborn.MechanicalSystem;
using Timberborn.MechanicalSystemUI;
using Timberborn.Rendering;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.ModularShafts
{
	// Token: 0x0200000E RID: 14
	public class ModularShaftModelUpdater : BaseComponent, IAwakableComponent, IStartableComponent, IMechanicalModelUpdater, IModelUpdater
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000049 RID: 73 RVA: 0x00002904 File Offset: 0x00000B04
		// (remove) Token: 0x0600004A RID: 74 RVA: 0x0000293C File Offset: 0x00000B3C
		public event EventHandler ModelUpdated;

		// Token: 0x0600004B RID: 75 RVA: 0x00002971 File Offset: 0x00000B71
		public ModularShaftModelUpdater(IInstantiator instantiator, ModularShaftModelService modularShaftModelService, MaterialColorer materialColorer)
		{
			this._instantiator = instantiator;
			this._modularShaftModelService = modularShaftModelService;
			this._materialColorer = materialColorer;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002990 File Offset: 0x00000B90
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._highlightableObject = base.GetComponent<HighlightableObject>();
			this._modularShaftCover = base.GetComponent<ModularShaftCover>();
			this._modularShaftVariantFinder = base.GetComponent<ModularShaftVariantFinder>();
			this._blockObjectModel = base.GetComponent<IBlockObjectModel>();
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._mechanicalNode.AddedToGraph += delegate(object _, EventArgs _)
			{
				this.UpdateModel();
			};
			this._parent = base.GetComponent<BuildingModel>().FinishedModel.transform;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A12 File Offset: 0x00000C12
		public void Start()
		{
			this.UpdateModel();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002A1C File Offset: 0x00000C1C
		public void UpdateModel()
		{
			ShaftVariant shaftVariant = this._modularShaftVariantFinder.FindBestVariant();
			if (!this._modelInstance || shaftVariant != this._currentShaftVariant)
			{
				this._currentShaftVariant = shaftVariant;
				this.UpdateModelInstance();
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002A5D File Offset: 0x00000C5D
		public void UpdateModelInstance()
		{
			if (this._modelInstance)
			{
				Object.Destroy(this._modelInstance);
			}
			this.SpawnModelInstance();
			this.UpdateModelVisuals();
			EventHandler modelUpdated = this.ModelUpdated;
			if (modelUpdated == null)
			{
				return;
			}
			modelUpdated(this, EventArgs.Empty);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A9C File Offset: 0x00000C9C
		public void SpawnModelInstance()
		{
			GameObject gameObject2;
			Orientation orientation2;
			if (!this._modularShaftCover)
			{
				GameObject gameObject;
				Orientation orientation;
				this._modularShaftModelService.GetModel(this._currentShaftVariant).Deconstruct(out gameObject, out orientation);
				gameObject2 = gameObject;
				orientation2 = orientation;
			}
			else
			{
				GameObject gameObject;
				Orientation orientation;
				this._modularShaftModelService.GetStackableModel(this._currentShaftVariant).Deconstruct(out gameObject, out orientation);
				gameObject2 = gameObject;
				orientation2 = orientation;
			}
			this._modelInstance = this._instantiator.Instantiate(gameObject2, this._parent);
			this._modelInstance.transform.SetLocalPositionAndRotation(ModularShaftModelUpdater.ModelOffset, orientation2.ToWorldSpaceRotation());
			this._modelInstance.name = gameObject2.name + " @ " + orientation2.ToString();
			this._modelInstance.SetActive(true);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B64 File Offset: 0x00000D64
		public void UpdateModelVisuals()
		{
			if (this._modularShaftCover)
			{
				this._modularShaftCover.UpdateModel();
			}
			this._highlightableObject.RefreshHighlight();
			if (this._blockObject.IsUnfinished)
			{
				this._materialColorer.EnableGrayscale(this._modelInstance);
			}
			this._blockObjectModel.UpdateModelVisibility();
		}

		// Token: 0x04000023 RID: 35
		public static readonly Vector3 ModelOffset = new Vector3(0.5f, 0f, 0.5f);

		// Token: 0x04000025 RID: 37
		public readonly IInstantiator _instantiator;

		// Token: 0x04000026 RID: 38
		public readonly ModularShaftModelService _modularShaftModelService;

		// Token: 0x04000027 RID: 39
		public readonly MaterialColorer _materialColorer;

		// Token: 0x04000028 RID: 40
		public BlockObject _blockObject;

		// Token: 0x04000029 RID: 41
		public HighlightableObject _highlightableObject;

		// Token: 0x0400002A RID: 42
		public ModularShaftCover _modularShaftCover;

		// Token: 0x0400002B RID: 43
		public ModularShaftVariantFinder _modularShaftVariantFinder;

		// Token: 0x0400002C RID: 44
		public IBlockObjectModel _blockObjectModel;

		// Token: 0x0400002D RID: 45
		public MechanicalNode _mechanicalNode;

		// Token: 0x0400002E RID: 46
		public Transform _parent;

		// Token: 0x0400002F RID: 47
		public ShaftVariant _currentShaftVariant;

		// Token: 0x04000030 RID: 48
		public GameObject _modelInstance;
	}
}
