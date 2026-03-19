using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntityNaming;
using Timberborn.EntitySystem;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000013 RID: 19
	public class DistrictCenter : BaseComponent, IAwakableComponent, IRegisteredComponent, IUnfinishedStateListener, IFinishedStateListener
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000339A File Offset: 0x0000159A
		// (set) Token: 0x0600007D RID: 125 RVA: 0x000033A2 File Offset: 0x000015A2
		public District District { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000033AB File Offset: 0x000015AB
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000033B3 File Offset: 0x000015B3
		public DistrictPopulation DistrictPopulation { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000033BC File Offset: 0x000015BC
		// (set) Token: 0x06000081 RID: 129 RVA: 0x000033C4 File Offset: 0x000015C4
		public DistrictBuildingRegistry DistrictBuildingRegistry { get; private set; }

		// Token: 0x06000082 RID: 130 RVA: 0x000033CD File Offset: 0x000015CD
		public DistrictCenter(IDistrictService districtService)
		{
			this._districtService = districtService;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000033DC File Offset: 0x000015DC
		public string DistrictName
		{
			get
			{
				return this._namedEntity.EntityName;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000033E9 File Offset: 0x000015E9
		public Vector3Int CenterCoordinates
		{
			get
			{
				return this._blockObject.PositionedEntrance.DoorstepCoordinates;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000033FC File Offset: 0x000015FC
		public void Awake()
		{
			this.DistrictPopulation = base.GetComponent<DistrictPopulation>();
			this.DistrictBuildingRegistry = base.GetComponent<DistrictBuildingRegistry>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._blockObjectCenter = base.GetComponent<BlockObjectCenter>();
			this._namedEntity = base.GetComponent<NamedEntity>();
			base.DisableComponent();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000344C File Offset: 0x0000164C
		public bool AccessibleIsOnDistrictRoad(Accessible accessible)
		{
			if (this.District != null)
			{
				Vector3? unblockedSingleAccess = accessible.UnblockedSingleAccess;
				if (unblockedSingleAccess != null)
				{
					Vector3 valueOrDefault = unblockedSingleAccess.GetValueOrDefault();
					return this._districtService.IsOnDistrictRoad(this.District, valueOrDefault);
				}
			}
			return false;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003490 File Offset: 0x00001690
		public bool AccessibleIsOnInstantDistrictRoad(Accessible accessible)
		{
			if (this.District != null)
			{
				Vector3? unblockedSingleAccessInstant = accessible.UnblockedSingleAccessInstant;
				if (unblockedSingleAccessInstant != null)
				{
					Vector3 valueOrDefault = unblockedSingleAccessInstant.GetValueOrDefault();
					return this._districtService.IsOnInstantDistrictRoad(this.District, valueOrDefault);
				}
			}
			return false;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000034D1 File Offset: 0x000016D1
		public bool IsOnInstantDistrictRoad(Vector3 start)
		{
			return this.District != null && this._districtService.IsOnInstantDistrictRoad(this.District, start);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000034EF File Offset: 0x000016EF
		public bool IsOnPreviewDistrictRoad(Vector3 start)
		{
			return this.District != null && this._districtService.IsOnPreviewDistrictRoad(this.District, start);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003510 File Offset: 0x00001710
		public bool IsGloballyReachableFromCitizen(Citizen citizen)
		{
			Vector3 position;
			if (!citizen.GetComponent<ICitizenPositionOverrider>().TryGetOverridenPosition(out position))
			{
				position = citizen.Transform.position;
			}
			return this.IsGloballyReachableFromPosition(position);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003540 File Offset: 0x00001740
		public bool IsGloballyReachableFromAnotherDistrict(DistrictCenter other)
		{
			Vector3 position = NavigationCoordinateSystem.GridToWorld(other.CenterCoordinates);
			return this.IsGloballyReachableFromPosition(position);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003560 File Offset: 0x00001760
		public float DistanceToCitizen(Citizen citizen)
		{
			return Vector3.Distance(this._blockObjectCenter.WorldCenterGrounded, citizen.Transform.position);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000357D File Offset: 0x0000177D
		public void OnEnterUnfinishedState()
		{
			this.District = this._districtService.AddPreviewDistrict(this.CenterCoordinates);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003596 File Offset: 0x00001796
		public void OnExitUnfinishedState()
		{
			this._districtService.RemovePreviewDistrict(this.District);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000035A9 File Offset: 0x000017A9
		public void OnEnterFinishedState()
		{
			this.District = this._districtService.AddDistrict(this.CenterCoordinates);
			base.EnableComponent();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000035C8 File Offset: 0x000017C8
		public void OnExitFinishedState()
		{
			this._districtService.RemoveDistrict(this.District);
			this.District = null;
			base.DisableComponent();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000035E8 File Offset: 0x000017E8
		public bool IsGloballyReachableFromPosition(Vector3 position)
		{
			return this._districtService.DistrictIsGloballyReachable(this.District, position);
		}

		// Token: 0x04000039 RID: 57
		public readonly IDistrictService _districtService;

		// Token: 0x0400003A RID: 58
		public BlockObject _blockObject;

		// Token: 0x0400003B RID: 59
		public BlockObjectCenter _blockObjectCenter;

		// Token: 0x0400003C RID: 60
		public NamedEntity _namedEntity;
	}
}
