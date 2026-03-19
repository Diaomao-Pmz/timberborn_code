using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.LinkedBuildingSystem;
using Timberborn.Localization;
using Timberborn.StatusSystem;

namespace Timberborn.DistributionSystem
{
	// Token: 0x02000010 RID: 16
	public class DistrictCrossingValidator : BaseComponent, IAwakableComponent, IInitializableEntity, IFinishedStateListener
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00002F10 File Offset: 0x00001110
		public DistrictCrossingValidator(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002F1F File Offset: 0x0000111F
		public bool CanExport
		{
			get
			{
				return this.IsValid && this._linked.IsValid && !this.IsConnectedToSameDistrict;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002F44 File Offset: 0x00001144
		public void Awake()
		{
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			this._statusSubject = base.GetComponent<StatusSubject>();
			base.GetComponent<LinkedBuilding>().BuildingLinked += this.OnBuildingLinked;
			this._sameDistrictStatusToggle = StatusToggle.CreateNormalStatusWithFloatingIcon("UnreachableObject", this._loc.T(DistrictCrossingValidator.InvalidConnectionLocKey), 0f);
			this._linkedInvalidStatusToggle = StatusToggle.CreateNormalStatus("UnreachableObject", this._loc.T(DistrictCrossingValidator.InvalidConnectionLocKey));
			base.DisableComponent();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002FCB File Offset: 0x000011CB
		public void InitializeEntity()
		{
			this._statusSubject.RegisterStatus(this._sameDistrictStatusToggle);
			this._statusSubject.RegisterStatus(this._linkedInvalidStatusToggle);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002FEF File Offset: 0x000011EF
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this._districtBuilding.ReassignedDistrict += this.OnReassignedDistrict;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000300E File Offset: 0x0000120E
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this._districtBuilding.ReassignedDistrict -= this.OnReassignedDistrict;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000302D File Offset: 0x0000122D
		public bool IsValid
		{
			get
			{
				return this.IsLinkedAndFinished && this.IsProperlyConnected;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000303F File Offset: 0x0000123F
		public bool IsLinkedAndFinished
		{
			get
			{
				return this._linked && base.Enabled;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003056 File Offset: 0x00001256
		public bool IsProperlyConnected
		{
			get
			{
				return this._districtBuilding.District && this._districtBuilding.District != this._linked._districtBuilding.District;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000308C File Offset: 0x0000128C
		public bool IsConnectedToSameDistrict
		{
			get
			{
				return this._districtBuilding.District && this._districtBuilding.District == this._linked._districtBuilding.District;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000030BF File Offset: 0x000012BF
		public void OnBuildingLinked(object sender, LinkedBuilding e)
		{
			this._linked = e.GetComponent<DistrictCrossingValidator>();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000030CD File Offset: 0x000012CD
		public void OnReassignedDistrict(object sender, EventArgs e)
		{
			this.ValidateIfLinkedAndFinished();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000030D5 File Offset: 0x000012D5
		public void ValidateIfLinkedAndFinished()
		{
			if (this.IsLinkedAndFinished && this._linked.IsLinkedAndFinished)
			{
				this.Validate();
				this._linked.Validate();
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000030FD File Offset: 0x000012FD
		public void Validate()
		{
			this.CheckLinkedConnection();
			this.CheckSameDistrictStatus();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000310B File Offset: 0x0000130B
		public void CheckLinkedConnection()
		{
			if (this._linked.IsProperlyConnected)
			{
				this._linkedInvalidStatusToggle.Deactivate();
				return;
			}
			this._linkedInvalidStatusToggle.Activate();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003131 File Offset: 0x00001331
		public void CheckSameDistrictStatus()
		{
			if (this.IsConnectedToSameDistrict)
			{
				this._sameDistrictStatusToggle.Activate();
				this._linkedInvalidStatusToggle.Deactivate();
				return;
			}
			this._sameDistrictStatusToggle.Deactivate();
		}

		// Token: 0x04000021 RID: 33
		public static readonly string InvalidConnectionLocKey = "Status.Distribution.InvalidConnection";

		// Token: 0x04000022 RID: 34
		public readonly ILoc _loc;

		// Token: 0x04000023 RID: 35
		public DistrictBuilding _districtBuilding;

		// Token: 0x04000024 RID: 36
		public StatusSubject _statusSubject;

		// Token: 0x04000025 RID: 37
		public DistrictCrossingValidator _linked;

		// Token: 0x04000026 RID: 38
		public StatusToggle _sameDistrictStatusToggle;

		// Token: 0x04000027 RID: 39
		public StatusToggle _linkedInvalidStatusToggle;
	}
}
