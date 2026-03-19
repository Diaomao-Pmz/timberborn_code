using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterMovementSystem;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.MortalComponents;
using Timberborn.WalkingSystem;
using Timberborn.ZiplineSystem;

namespace Timberborn.ZiplineMovementSystem
{
	// Token: 0x0200000E RID: 14
	public class ZiplineVisitor : BaseComponent, IAwakableComponent, IInitializableEntity, IPostLoadableEntity, IDeadNeededComponent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000042 RID: 66 RVA: 0x00002EE4 File Offset: 0x000010E4
		// (remove) Token: 0x06000043 RID: 67 RVA: 0x00002F1C File Offset: 0x0000111C
		public event EventHandler EnteredZipline;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000044 RID: 68 RVA: 0x00002F54 File Offset: 0x00001154
		// (remove) Token: 0x06000045 RID: 69 RVA: 0x00002F8C File Offset: 0x0000118C
		public event EventHandler ExitedZipline;

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002FC1 File Offset: 0x000011C1
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002FC9 File Offset: 0x000011C9
		public bool IsOnZipline { get; private set; }

		// Token: 0x06000048 RID: 72 RVA: 0x00002FD2 File Offset: 0x000011D2
		public ZiplineVisitor(ZiplineGroupService ziplineGroupService)
		{
			this._ziplineGroupService = ziplineGroupService;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002FE1 File Offset: 0x000011E1
		public void Awake()
		{
			this._movementAnimator = base.GetComponent<MovementAnimator>();
			this._ziplinePathTracker = base.GetComponent<ZiplinePathTracker>();
			this._citizen = base.GetComponent<Citizen>();
			this._navMeshObserver = base.GetComponent<NavMeshObserver>();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003014 File Offset: 0x00001214
		public void InitializeEntity()
		{
			this._movementAnimator.GroupIdUpdated += this.OnGroupIdUpdated;
			this._citizen.ChangedAssignedDistrict += this.OnAssignedDistrictChanged;
			this._navMeshObserver.PlacedOnNavMesh += this.OnPlacedOnNavMesh;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003066 File Offset: 0x00001266
		public void PostLoadEntity()
		{
			this.PostLoadValidateVisit();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000306E File Offset: 0x0000126E
		public void PostLoadValidateVisit()
		{
			if (this._ziplinePathTracker.IsOnNavMesh())
			{
				this.EnterZipline();
				this._movementAnimator.SetPostLoadGroupId(this._ziplineGroupService.RegularGroupId);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000309C File Offset: 0x0000129C
		public void OnGroupIdUpdated(object sender, GroupIdUpdatedEventArgs e)
		{
			bool flag = this._ziplineGroupService.IsAnyZiplineGroup(e.GroupId);
			if (flag && !this.IsOnZipline)
			{
				this.EnterZipline();
				return;
			}
			if (!flag && this.IsOnZipline)
			{
				this.ExitZipline();
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000030DF File Offset: 0x000012DF
		public void OnAssignedDistrictChanged(object sender, ChangeAssignedDistrictEventArgs e)
		{
			if (!this._citizen.HasAssignedDistrict && !this._ziplinePathTracker.IsOnNavMesh() && this.IsOnZipline)
			{
				this.ExitZipline();
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003109 File Offset: 0x00001309
		public void OnPlacedOnNavMesh(object sender, EventArgs e)
		{
			if (!this._ziplinePathTracker.IsOnNavMesh() && this.IsOnZipline)
			{
				this.ExitZipline();
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003126 File Offset: 0x00001326
		public void EnterZipline()
		{
			this.IsOnZipline = true;
			EventHandler enteredZipline = this.EnteredZipline;
			if (enteredZipline == null)
			{
				return;
			}
			enteredZipline(this, EventArgs.Empty);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003145 File Offset: 0x00001345
		public void ExitZipline()
		{
			this.IsOnZipline = false;
			EventHandler exitedZipline = this.ExitedZipline;
			if (exitedZipline == null)
			{
				return;
			}
			exitedZipline(this, EventArgs.Empty);
		}

		// Token: 0x0400002D RID: 45
		public readonly ZiplineGroupService _ziplineGroupService;

		// Token: 0x0400002E RID: 46
		public MovementAnimator _movementAnimator;

		// Token: 0x0400002F RID: 47
		public ZiplinePathTracker _ziplinePathTracker;

		// Token: 0x04000030 RID: 48
		public NavMeshObserver _navMeshObserver;

		// Token: 0x04000031 RID: 49
		public Citizen _citizen;
	}
}
