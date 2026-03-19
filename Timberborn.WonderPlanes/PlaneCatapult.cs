using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.Localization;
using Timberborn.NotificationSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.WonderPlanes
{
	// Token: 0x0200000A RID: 10
	public class PlaneCatapult : BaseComponent, IAwakableComponent, IUpdatableComponent, IPersistentEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000024 RID: 36 RVA: 0x00002724 File Offset: 0x00000924
		// (remove) Token: 0x06000025 RID: 37 RVA: 0x0000275C File Offset: 0x0000095C
		public event EventHandler PlaneCatapulted;

		// Token: 0x06000026 RID: 38 RVA: 0x00002791 File Offset: 0x00000991
		public PlaneCatapult(NotificationBus notificationBus, ILoc loc, ReferenceSerializer referenceSerializer)
		{
			this._notificationBus = notificationBus;
			this._loc = loc;
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027AE File Offset: 0x000009AE
		public void Awake()
		{
			this._planeSpawner = base.GetComponent<PlaneSpawner>();
			this._speedCurve = base.GetComponent<PlaneCatapultSpec>().SpeedCurve.ToAnimationCurve();
			base.DisableComponent();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000027D8 File Offset: 0x000009D8
		public void Update()
		{
			if (this._catapultedPlane)
			{
				this.UpdatePlane();
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000027ED File Offset: 0x000009ED
		public void Save(IEntitySaver entitySaver)
		{
			if (this._catapultedPlane)
			{
				entitySaver.GetComponent(PlaneCatapult.PlaneCatapultKey).Set<Plane>(PlaneCatapult.CurrentPlaneKey, this._catapultedPlane, this._referenceSerializer.Of<Plane>());
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002824 File Offset: 0x00000A24
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(PlaneCatapult.PlaneCatapultKey, out objectLoader))
			{
				this._catapultedPlane = objectLoader.Get<Plane>(PlaneCatapult.CurrentPlaneKey, this._referenceSerializer.Of<Plane>());
				if (this._catapultedPlane)
				{
					base.EnableComponent();
				}
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002870 File Offset: 0x00000A70
		public void CatapultPlane(Pilot pilot)
		{
			if (this._catapultedPlane)
			{
				return;
			}
			base.EnableComponent();
			this._remainingPlaneWaitTime = PlaneCatapult.PlaneWaitTimeInSeconds;
			this._catapultedPlane = this._planeSpawner.SpawnPlane(pilot);
			Character component = pilot.GetComponent<Character>();
			this._notificationBus.Post(this._loc.T<string>(PlaneCatapult.LaunchedLocKey, component.FirstName), component);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000028D8 File Offset: 0x00000AD8
		public void UpdatePlane()
		{
			if (this._remainingPlaneWaitTime > 0f)
			{
				this._remainingPlaneWaitTime -= Time.deltaTime;
				return;
			}
			float num = Vector3.Magnitude(this._catapultedPlane.Transform.position - this._planeSpawner.SpawnPosition) / PlaneCatapult.RunwayLength;
			this._catapultedPlane.SetSpeed(this._speedCurve.Evaluate(num));
			if (num >= 1f)
			{
				this._catapultedPlane.StartFreeFlight();
				this._catapultedPlane = null;
				base.DisableComponent();
				EventHandler planeCatapulted = this.PlaneCatapulted;
				if (planeCatapulted == null)
				{
					return;
				}
				planeCatapulted(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000027 RID: 39
		public static readonly float PlaneWaitTimeInSeconds = 1f;

		// Token: 0x04000028 RID: 40
		public static readonly string LaunchedLocKey = "Beaver.Launched";

		// Token: 0x04000029 RID: 41
		public static readonly ComponentKey PlaneCatapultKey = new ComponentKey("PlaneCatapult");

		// Token: 0x0400002A RID: 42
		public static readonly PropertyKey<Plane> CurrentPlaneKey = new PropertyKey<Plane>("CurrentPlane");

		// Token: 0x0400002B RID: 43
		public static readonly float RunwayLength = 10f;

		// Token: 0x0400002D RID: 45
		public readonly NotificationBus _notificationBus;

		// Token: 0x0400002E RID: 46
		public readonly ILoc _loc;

		// Token: 0x0400002F RID: 47
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x04000030 RID: 48
		public PlaneSpawner _planeSpawner;

		// Token: 0x04000031 RID: 49
		public AnimationCurve _speedCurve;

		// Token: 0x04000032 RID: 50
		public PlaneCatapultSpec _planeCatapultSpec;

		// Token: 0x04000033 RID: 51
		public Plane _catapultedPlane;

		// Token: 0x04000034 RID: 52
		public float _remainingPlaneWaitTime;
	}
}
