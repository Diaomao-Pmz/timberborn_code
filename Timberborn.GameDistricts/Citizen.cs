using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000008 RID: 8
	public class Citizen : BaseComponent, IAwakableComponent, IInitializableEntity, IPersistentEntity, IChildhoodInfluenced, IDeletableEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000A RID: 10 RVA: 0x00002120 File Offset: 0x00000320
		// (remove) Token: 0x0600000B RID: 11 RVA: 0x00002158 File Offset: 0x00000358
		public event EventHandler<ChangeAssignedDistrictEventArgs> ChangedAssignedDistrict;

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000218D File Offset: 0x0000038D
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002195 File Offset: 0x00000395
		public DistrictCenter AssignedDistrict { get; private set; }

		// Token: 0x0600000E RID: 14 RVA: 0x0000219E File Offset: 0x0000039E
		public Citizen(UnassignedCitizenRegistry unassignedCitizenRegistry, ReferenceSerializer referenceSerializer, CitizenUnstucker citizenUnstucker)
		{
			this._unassignedCitizenRegistry = unassignedCitizenRegistry;
			this._referenceSerializer = referenceSerializer;
			this._citizenUnstucker = citizenUnstucker;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021BB File Offset: 0x000003BB
		public bool HasAssignedDistrict
		{
			get
			{
				return this.AssignedDistrict;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021C8 File Offset: 0x000003C8
		public void Awake()
		{
			this._character = base.GetComponent<Character>();
			this._character.Died += this.OnDied;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021ED File Offset: 0x000003ED
		public void InitializeEntity()
		{
			if (this._character.Alive && !this.AssignedDistrict)
			{
				this._unassignedCitizenRegistry.Add(this);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002215 File Offset: 0x00000415
		public void DeleteEntity()
		{
			this.RemoveFromDistrictsAssignment();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000221D File Offset: 0x0000041D
		public void AssignInitialDistrict(DistrictCenter districtCenter)
		{
			Asserts.FieldIsNull<Citizen>(this, this.AssignedDistrict, "AssignedDistrict");
			this.AssignDistrict(districtCenter);
			districtCenter.GetComponent<DistrictCitizenLifecycleNotifier>().AddNewCitizen(this);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002244 File Offset: 0x00000444
		public void AssignDistrict(DistrictCenter districtCenter)
		{
			this.UnassignDistrict();
			this.AssignedDistrict = districtCenter;
			this._unassignedCitizenRegistry.Remove(this);
			districtCenter.DistrictPopulation.AssignCitizen(this);
			EventHandler<ChangeAssignedDistrictEventArgs> changedAssignedDistrict = this.ChangedAssignedDistrict;
			if (changedAssignedDistrict == null)
			{
				return;
			}
			changedAssignedDistrict(this, new ChangeAssignedDistrictEventArgs(null, this.AssignedDistrict));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002294 File Offset: 0x00000494
		public void UnassignDistrictIfCutOff()
		{
			if (this.AssignedDistrict != null)
			{
				if (this.AssignedDistrict)
				{
					if (!this.AssignedDistrict.IsGloballyReachableFromCitizen(this) && !this._citizenUnstucker.TryUnstuckAndKeepDistrict(this, this.AssignedDistrict))
					{
						this.UnassignDistrict();
						return;
					}
				}
				else
				{
					this.UnassignDistrict();
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022E5 File Offset: 0x000004E5
		public void Save(IEntitySaver entitySaver)
		{
			if (this.AssignedDistrict)
			{
				entitySaver.GetComponent(Citizen.CitizenKey).Set<DistrictCenter>(Citizen.AssignedDistrictKey, this.AssignedDistrict, this._referenceSerializer.Of<DistrictCenter>());
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000231C File Offset: 0x0000051C
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			DistrictCenter districtCenter;
			if (entityLoader.TryGetComponent(Citizen.CitizenKey, out objectLoader) && objectLoader.GetObsoletable<DistrictCenter>(Citizen.AssignedDistrictKey, this._referenceSerializer.Of<DistrictCenter>(), out districtCenter))
			{
				this.AssignDistrict(districtCenter);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000235C File Offset: 0x0000055C
		public void InfluenceByChildhood(Character child)
		{
			Citizen component = child.GetComponent<Citizen>();
			if (component.HasAssignedDistrict)
			{
				this.AssignDistrict(component.AssignedDistrict);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002384 File Offset: 0x00000584
		public void UnassignDistrict()
		{
			DistrictCenter assignedDistrict = this.AssignedDistrict;
			if (this.HasAssignedDistrict)
			{
				this.AssignedDistrict.DistrictPopulation.UnassignCitizen(this);
			}
			this._unassignedCitizenRegistry.Add(this);
			this.AssignedDistrict = null;
			EventHandler<ChangeAssignedDistrictEventArgs> changedAssignedDistrict = this.ChangedAssignedDistrict;
			if (changedAssignedDistrict == null)
			{
				return;
			}
			changedAssignedDistrict(this, new ChangeAssignedDistrictEventArgs(assignedDistrict, this.AssignedDistrict));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023E1 File Offset: 0x000005E1
		public void OnDied(object sender, EventArgs e)
		{
			if (this.AssignedDistrict)
			{
				this.AssignedDistrict.GetComponent<DistrictCitizenLifecycleNotifier>().RemoveDiedCitizen(this);
			}
			this.RemoveFromDistrictsAssignment();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002407 File Offset: 0x00000607
		public void RemoveFromDistrictsAssignment()
		{
			this.UnassignDistrict();
			this._unassignedCitizenRegistry.Remove(this);
		}

		// Token: 0x0400000A RID: 10
		public static readonly ComponentKey CitizenKey = new ComponentKey("Citizen");

		// Token: 0x0400000B RID: 11
		public static readonly PropertyKey<DistrictCenter> AssignedDistrictKey = new PropertyKey<DistrictCenter>("AssignedDistrict");

		// Token: 0x0400000E RID: 14
		public readonly UnassignedCitizenRegistry _unassignedCitizenRegistry;

		// Token: 0x0400000F RID: 15
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x04000010 RID: 16
		public readonly CitizenUnstucker _citizenUnstucker;

		// Token: 0x04000011 RID: 17
		public Character _character;
	}
}
