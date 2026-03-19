using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.Buildings
{
	// Token: 0x02000008 RID: 8
	public class BuildingAccessible : BaseComponent, IAwakableComponent, IFinishedStateListener, IPreviewSelectionListener, IPostPlacementChangeListener, IRegisteredComponent, IAccessibleNeeder
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002127 File Offset: 0x00000327
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000212F File Offset: 0x0000032F
		public Accessible Accessible { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002138 File Offset: 0x00000338
		public string AccessibleComponentName
		{
			get
			{
				return "Building";
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002140 File Offset: 0x00000340
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._buildingAccessibleSpec = base.GetComponent<BuildingAccessibleSpec>();
			if (!base.GetComponent<BlockObjectSpec>().Entrance.HasEntrance)
			{
				throw new InvalidOperationException(base.Name + " is BuildingAccessible but doesn't have a BlockObjectSpec entrance");
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000218D File Offset: 0x0000038D
		public void SetAccessible(Accessible accessible)
		{
			this.Accessible = accessible;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002196 File Offset: 0x00000396
		public void OnEnterFinishedState()
		{
			this.UpdateAccess();
			this.EnableAccesses();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021A4 File Offset: 0x000003A4
		public void OnExitFinishedState()
		{
			this.Accessible.ClearAccesses();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021B1 File Offset: 0x000003B1
		public void OnPreviewSelect()
		{
			this.EnableAccesses();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021B9 File Offset: 0x000003B9
		public void OnPreviewUnselect()
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021BB File Offset: 0x000003BB
		public void OnPostPlacementChanged()
		{
			this.UpdateAccess();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021C3 File Offset: 0x000003C3
		public Vector3 CalculateAccess()
		{
			if (!this._buildingAccessibleSpec.ForceOneFinalAccess)
			{
				return this.CalculateAccessAtEntrance();
			}
			return this.CalculateAccessFromLocalAccess();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021DF File Offset: 0x000003DF
		public Vector3 CalculateAccessFromLocalAccess()
		{
			return this._buildingAccessibleSpec.CalculateAccessFromLocalAccess(this._blockObject);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021F4 File Offset: 0x000003F4
		public void EnableAccesses()
		{
			if (this._buildingAccessibleSpec.ForceOneFinalAccess)
			{
				this.Accessible.SetAccesses(Enumerables.One<Vector3>(this._access), null);
				return;
			}
			this.Accessible.SetAccesses(Enumerables.One<Vector3>(this._access), new Vector3?(this.CalculateAccessFromLocalAccess()));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000224F File Offset: 0x0000044F
		public void UpdateAccess()
		{
			this._access = this.CalculateAccess();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000225D File Offset: 0x0000045D
		public Vector3 CalculateAccessAtEntrance()
		{
			return CoordinateSystem.GridToWorldCentered(this._blockObject.PositionedEntrance.Coordinates);
		}

		// Token: 0x0400000A RID: 10
		public BlockObject _blockObject;

		// Token: 0x0400000B RID: 11
		public BuildingAccessibleSpec _buildingAccessibleSpec;

		// Token: 0x0400000C RID: 12
		public Vector3 _access;
	}
}
