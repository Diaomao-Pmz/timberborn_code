using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.PathSystem
{
	// Token: 0x0200000A RID: 10
	public class DrivewayModel : BaseComponent, IAwakableComponent, IStartableComponent, IModelUpdater
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000022CB File Offset: 0x000004CB
		public DrivewayModel(IConnectionService connectionService, ITerrainService terrainService, DrivewayModelInstantiator drivewayModelInstantiator, IBlockService blockService)
		{
			this._connectionService = connectionService;
			this._terrainService = terrainService;
			this._drivewayModelInstantiator = drivewayModelInstantiator;
			this._blockService = blockService;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022F0 File Offset: 0x000004F0
		public Driveway Driveway
		{
			get
			{
				return this._drivewayModelSpec.Driveway;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022FD File Offset: 0x000004FD
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._drivewayModelSpec = base.GetComponent<DrivewayModelSpec>();
			this.ValidateDriveway();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000231D File Offset: 0x0000051D
		public void Start()
		{
			this._model = this._drivewayModelInstantiator.InstantiateModel(this, this.GetLocalCoordinates(), this.GetLocalDirection());
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002340 File Offset: 0x00000540
		public void UpdateModel()
		{
			Vector3Int positionedCoordinates = this.GetPositionedCoordinates();
			Direction2D positionedDirection = this.GetPositionedDirection();
			bool flag = this._connectionService.CanConnectInDirection(positionedCoordinates, positionedDirection);
			Direction2D secondPositionedDirection = this.GetSecondPositionedDirection();
			switch (this._drivewayModelSpec.DrivewayMode)
			{
			case DrivewayMode.Unidirectional:
				break;
			case DrivewayMode.StrictlyUnidirectional:
				flag &= !this._connectionService.CanConnectInDirection(positionedCoordinates, secondPositionedDirection);
				break;
			case DrivewayMode.Bidirectional:
				flag &= this._connectionService.CanConnectInDirection(positionedCoordinates, secondPositionedDirection);
				break;
			default:
				throw new NotSupportedException(string.Format("Unexpected value: {0}", this._drivewayModelSpec.DrivewayMode));
			}
			bool flag2 = this._terrainService.OnGround(positionedCoordinates);
			bool active = flag && (flag2 || this.IsOnBlockObjectWithGroundPath(positionedCoordinates));
			GameObject model = this._model;
			if (model == null)
			{
				return;
			}
			model.SetActive(active);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002410 File Offset: 0x00000610
		public void ValidateDriveway()
		{
			if (!this._blockObject.Entrance.HasEntrance && !this._drivewayModelSpec.HasCustomCoordinates)
			{
				throw new Exception("BlockObject " + base.Name + " with DrivewayModel must have Entrance or custom coordinates.");
			}
			if (this.Driveway == Driveway.None)
			{
				throw new Exception("DrivewayModel " + base.Name + " must have Driveway set to a value different than None.");
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000247A File Offset: 0x0000067A
		public Vector3Int GetLocalCoordinates()
		{
			if (!this._drivewayModelSpec.HasCustomCoordinates)
			{
				return this._blockObject.Entrance.Coordinates;
			}
			return this._drivewayModelSpec.CustomCoordinates;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024A5 File Offset: 0x000006A5
		public Direction2D GetLocalDirection()
		{
			if (!this._drivewayModelSpec.HasCustomCoordinates)
			{
				return Direction2D.Down;
			}
			return this._drivewayModelSpec.CustomDirection;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024C4 File Offset: 0x000006C4
		public Vector3Int GetPositionedCoordinates()
		{
			if (!this._drivewayModelSpec.HasCustomCoordinates)
			{
				return this._blockObject.PositionedEntrance.DoorstepCoordinates;
			}
			return this._blockObject.TransformCoordinates(this._drivewayModelSpec.CustomCoordinates - this._drivewayModelSpec.CustomDirection.ToOffset());
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000251A File Offset: 0x0000071A
		public Direction2D GetPositionedDirection()
		{
			if (!(this._drivewayModelSpec != null))
			{
				return this._blockObject.PositionedEntrance.Direction2D;
			}
			return this._blockObject.TransformDirection(this._drivewayModelSpec.CustomDirection);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002551 File Offset: 0x00000751
		public Direction2D GetSecondPositionedDirection()
		{
			return this.GetPositionedDirection().Next().Next();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002564 File Offset: 0x00000764
		public bool IsOnBlockObjectWithGroundPath(Vector3Int coordinates)
		{
			PathModelTypeEnforcerSpec topObjectComponentAt = this._blockService.GetTopObjectComponentAt<PathModelTypeEnforcerSpec>(coordinates - new Vector3Int(0, 0, 1));
			return topObjectComponentAt != null && topObjectComponentAt.PathModelType == PathModelType.Ground;
		}

		// Token: 0x04000018 RID: 24
		public readonly IConnectionService _connectionService;

		// Token: 0x04000019 RID: 25
		public readonly ITerrainService _terrainService;

		// Token: 0x0400001A RID: 26
		public readonly DrivewayModelInstantiator _drivewayModelInstantiator;

		// Token: 0x0400001B RID: 27
		public readonly IBlockService _blockService;

		// Token: 0x0400001C RID: 28
		public BlockObject _blockObject;

		// Token: 0x0400001D RID: 29
		public DrivewayModelSpec _drivewayModelSpec;

		// Token: 0x0400001E RID: 30
		public GameObject _model;
	}
}
