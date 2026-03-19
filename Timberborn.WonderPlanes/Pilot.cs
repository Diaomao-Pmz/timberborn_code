using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.EnterableSystem;
using Timberborn.EntitySystem;
using Timberborn.MortalComponents;
using Timberborn.Persistence;
using Timberborn.StatusSystem;
using Timberborn.WalkingSystem;
using Timberborn.Wandering;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.WonderPlanes
{
	// Token: 0x02000008 RID: 8
	public class Pilot : BaseComponent, IAwakableComponent, IPersistentEntity, IInitializableEntity, IDeletableEntity
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021E5 File Offset: 0x000003E5
		public Pilot(DeadComponentDisabler deadComponentDisabler, EntityService entityService, ReferenceSerializer referenceSerializer)
		{
			this._deadComponentDisabler = deadComponentDisabler;
			this._entityService = entityService;
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002204 File Offset: 0x00000404
		public void Awake()
		{
			this._characterModel = base.GetComponent<CharacterModel>();
			this._characterAnimator = base.GetComponent<CharacterAnimator>();
			this._strandedStatus = base.GetComponent<StrandedStatus>();
			this._enterer = base.GetComponent<Enterer>();
			this._navMeshObserver = base.GetComponent<NavMeshObserver>();
			StatusIconCycler componentInChildren = base.GetComponentInChildren<StatusIconCycler>(true);
			this._statusVisibilityToggle = componentInChildren.GetStatusVisibilityToggle();
			base.DisableComponent();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002267 File Offset: 0x00000467
		public void InitializeEntity()
		{
			if (this._planeLauncherPosition != null)
			{
				this.PrepareForFlying(this._planeLauncherPosition.Value);
			}
			this.ShowPilot();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002290 File Offset: 0x00000490
		public void Save(IEntitySaver entitySaver)
		{
			if (this._plane || this._planeLauncherPosition != null)
			{
				IObjectSaver component = entitySaver.GetComponent(Pilot.PilotKey);
				if (this._plane)
				{
					component.Set<Plane>(Pilot.PlaneKey, this._plane, this._referenceSerializer.Of<Plane>());
				}
				if (this._planeLauncherPosition != null)
				{
					component.Set(Pilot.PlaneLauncherPositionKey, this._planeLauncherPosition.Value);
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002310 File Offset: 0x00000510
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(Pilot.PilotKey, out objectLoader))
			{
				this._planeLauncherPosition = (objectLoader.Has<Vector3>(Pilot.PlaneLauncherPositionKey) ? new Vector3?(objectLoader.Get(Pilot.PlaneLauncherPositionKey)) : null);
				if (objectLoader.Has<Plane>(Pilot.PlaneKey))
				{
					this.AssignPlane(objectLoader.Get<Plane>(Pilot.PlaneKey, this._referenceSerializer.Of<Plane>()));
				}
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002383 File Offset: 0x00000583
		public void DeleteEntity()
		{
			if (this._plane)
			{
				this._entityService.Delete(this._plane);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023A4 File Offset: 0x000005A4
		public void PrepareForFlying(Vector3 planeLauncherPosition)
		{
			this._planeLauncherPosition = new Vector3?(planeLauncherPosition);
			this._enterer.UnreserveSlotAndExit();
			this._deadComponentDisabler.DisableComponentsDeadDoNotNeed(this);
			this._characterModel.Hide();
			this._characterAnimator.SetBool(Pilot.AnimationName, true);
			this._navMeshObserver.Disable();
			this._characterModel.Position = this._planeLauncherPosition.Value;
			base.EnableComponent();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002417 File Offset: 0x00000617
		public void AssignPlane(Plane plane)
		{
			this._plane = plane;
			base.Transform.SetParent(plane.PilotSeatTransform);
			this.ShowPilot();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002437 File Offset: 0x00000637
		public bool IsFlying
		{
			get
			{
				return this._plane;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002444 File Offset: 0x00000644
		public void ShowPilot()
		{
			if (this.IsFlying)
			{
				this._characterModel.IgnoreModelBlockade();
				this._characterModel.Show();
				this._statusVisibilityToggle.Hide();
				this._characterModel.PositionAtTarget(this._plane.PilotSeatTransform);
				this._strandedStatus.Disable();
			}
		}

		// Token: 0x0400000D RID: 13
		public static readonly string AnimationName = "Piloting";

		// Token: 0x0400000E RID: 14
		public static readonly ComponentKey PilotKey = new ComponentKey("Pilot");

		// Token: 0x0400000F RID: 15
		public static readonly PropertyKey<Plane> PlaneKey = new PropertyKey<Plane>("Plane");

		// Token: 0x04000010 RID: 16
		public static readonly PropertyKey<Vector3> PlaneLauncherPositionKey = new PropertyKey<Vector3>("PlaneLauncherPosition");

		// Token: 0x04000011 RID: 17
		public readonly DeadComponentDisabler _deadComponentDisabler;

		// Token: 0x04000012 RID: 18
		public readonly EntityService _entityService;

		// Token: 0x04000013 RID: 19
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x04000014 RID: 20
		public CharacterModel _characterModel;

		// Token: 0x04000015 RID: 21
		public CharacterAnimator _characterAnimator;

		// Token: 0x04000016 RID: 22
		public StrandedStatus _strandedStatus;

		// Token: 0x04000017 RID: 23
		public NavMeshObserver _navMeshObserver;

		// Token: 0x04000018 RID: 24
		public StatusVisibilityToggle _statusVisibilityToggle;

		// Token: 0x04000019 RID: 25
		public Enterer _enterer;

		// Token: 0x0400001A RID: 26
		public Vector3? _planeLauncherPosition;

		// Token: 0x0400001B RID: 27
		public Plane _plane;
	}
}
