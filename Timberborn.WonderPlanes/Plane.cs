using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.Common;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.WonderPlanes
{
	// Token: 0x02000009 RID: 9
	public class Plane : BaseComponent, IAwakableComponent, IUpdatableComponent, IPersistentEntity
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000024D4 File Offset: 0x000006D4
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000024DC File Offset: 0x000006DC
		public Transform PilotSeatTransform { get; private set; }

		// Token: 0x06000019 RID: 25 RVA: 0x000024E5 File Offset: 0x000006E5
		public void Awake()
		{
			this._planeSpec = base.GetComponent<PlaneSpec>();
			this.PilotSeatTransform = base.GameObject.FindChildTransform(this._planeSpec.PilotSeatName);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002510 File Offset: 0x00000710
		public void Update()
		{
			if (this._isFreeFlying)
			{
				this.RotateTowardHorizontalFlight(Time.deltaTime);
			}
			base.Transform.position += base.Transform.forward * (this._speed * Time.deltaTime);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002564 File Offset: 0x00000764
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(Plane.PlaneKey);
			component.Set(Plane.PositionKey, base.Transform.position);
			component.Set(Plane.RotationKey, base.Transform.rotation);
			component.Set(Plane.SpeedKey, this._speed);
			component.Set(Plane.IsFreeFlyingKey, this._isFreeFlying);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025CC File Offset: 0x000007CC
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(Plane.PlaneKey);
			base.Transform.SetPositionAndRotation(component.Get(Plane.PositionKey), component.Get(Plane.RotationKey));
			this.ComputeFinalRotation();
			this._speed = component.Get(Plane.SpeedKey);
			this._isFreeFlying = component.Get(Plane.IsFreeFlyingKey);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000262E File Offset: 0x0000082E
		public void Initialize(Transform spawnPointTransform)
		{
			base.Transform.SetPositionAndRotation(spawnPointTransform.position, spawnPointTransform.rotation);
			this.ComputeFinalRotation();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000264D File Offset: 0x0000084D
		public void SetSpeed(float speed)
		{
			this._speed = speed;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002656 File Offset: 0x00000856
		public void StartFreeFlight()
		{
			this._isFreeFlying = true;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000265F File Offset: 0x0000085F
		public void ComputeFinalRotation()
		{
			this._horizontalRotation = Quaternion.Euler(0f, base.Transform.eulerAngles.y, base.Transform.eulerAngles.z);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002691 File Offset: 0x00000891
		public void RotateTowardHorizontalFlight(float deltaTime)
		{
			base.Transform.rotation = Quaternion.RotateTowards(base.Transform.rotation, this._horizontalRotation, deltaTime * this._planeSpec.RotationSpeed);
		}

		// Token: 0x0400001C RID: 28
		public static readonly ComponentKey PlaneKey = new ComponentKey("Plane");

		// Token: 0x0400001D RID: 29
		public static readonly PropertyKey<float> SpeedKey = new PropertyKey<float>("Speed");

		// Token: 0x0400001E RID: 30
		public static readonly PropertyKey<Vector3> PositionKey = new PropertyKey<Vector3>("Position");

		// Token: 0x0400001F RID: 31
		public static readonly PropertyKey<Quaternion> RotationKey = new PropertyKey<Quaternion>("Rotation");

		// Token: 0x04000020 RID: 32
		public static readonly PropertyKey<bool> IsFreeFlyingKey = new PropertyKey<bool>("IsFreeFlying");

		// Token: 0x04000022 RID: 34
		public PlaneSpec _planeSpec;

		// Token: 0x04000023 RID: 35
		public CharacterModel _pilotCharacterModel;

		// Token: 0x04000024 RID: 36
		public float _speed;

		// Token: 0x04000025 RID: 37
		public bool _isFreeFlying;

		// Token: 0x04000026 RID: 38
		public Quaternion _horizontalRotation;
	}
}
