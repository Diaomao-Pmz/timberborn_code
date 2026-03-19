using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using UnityEngine;

namespace Timberborn.Attractions
{
	// Token: 0x0200000A RID: 10
	public class AttractionFire : TickableComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x0600001D RID: 29 RVA: 0x0000235D File Offset: 0x0000055D
		public AttractionFire(EventBus eventBus, IDayNightCycle dayNightCycle)
		{
			this._eventBus = eventBus;
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002374 File Offset: 0x00000574
		public void Awake()
		{
			string woodstackName = base.GetComponent<AttractionFireSpec>().WoodstackName;
			if (!string.IsNullOrWhiteSpace(woodstackName))
			{
				this._woodstack = base.GameObject.FindChild(woodstackName);
				this._initialWoodstackScale = this._woodstack.transform.localScale;
			}
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._fire = base.GetComponent<Fire>();
			base.DisableComponent();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023DC File Offset: 0x000005DC
		public override void StartTickable()
		{
			ParticleSystem.MainModule singleFlame = this._fire.SingleFlame;
			this._initialFlamesStartSizeConstMax = singleFlame.startSize.constantMax;
			this._initialFlamesStartLifetimeConstMax = singleFlame.startLifetime.constantMax;
			this._initialLightIntensity = this._fire.Light.intensity;
			this.UpdateFireState();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000243B File Offset: 0x0000063B
		public override void Tick()
		{
			if (this._fireIsOn)
			{
				this.UpdateFireAnimation();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000244C File Offset: 0x0000064C
		public void OnEnterFinishedState()
		{
			this._eventBus.Register(this);
			this._blockableObject.ObjectBlocked += this.OnObjectBlocked;
			this._blockableObject.ObjectUnblocked += this.OnObjectUnblocked;
			base.EnableComponent();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000249C File Offset: 0x0000069C
		public void OnExitFinishedState()
		{
			this._eventBus.Unregister(this);
			this._blockableObject.ObjectBlocked -= this.OnObjectBlocked;
			this._blockableObject.ObjectUnblocked -= this.OnObjectUnblocked;
			base.DisableComponent();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024E9 File Offset: 0x000006E9
		[OnEvent]
		public void OnNighttimeStartEvent(NighttimeStartEvent nighttimeStartEvent)
		{
			this.UpdateFireState();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024E9 File Offset: 0x000006E9
		[OnEvent]
		public void OnDaytimeStartEvent(DaytimeStartEvent daytimeStartEvent)
		{
			this.UpdateFireState();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024E9 File Offset: 0x000006E9
		public void OnObjectUnblocked(object sender, EventArgs e)
		{
			this.UpdateFireState();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024E9 File Offset: 0x000006E9
		public void OnObjectBlocked(object sender, EventArgs e)
		{
			this.UpdateFireState();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024F4 File Offset: 0x000006F4
		public void UpdateFireState()
		{
			bool flag = this._blockableObject.IsUnblocked && this._dayNightCycle.IsNighttime;
			if (!this._fireIsOn && flag)
			{
				this.StartFire();
				return;
			}
			if (this._fireIsOn && !flag)
			{
				this.StopFire();
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002542 File Offset: 0x00000742
		public void StartFire()
		{
			this._fire.Enable();
			if (this._woodstack)
			{
				this._woodstack.SetActive(true);
			}
			this._fireIsOn = true;
			this.UpdateFireAnimation();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002575 File Offset: 0x00000775
		public void StopFire()
		{
			this._fire.Disable();
			if (this._woodstack)
			{
				this._woodstack.SetActive(false);
			}
			this._fireIsOn = false;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025A4 File Offset: 0x000007A4
		public void UpdateFireAnimation()
		{
			float num = this._dayNightCycle.HoursToNextStartOf(TimeOfDay.Daytime) / this._dayNightCycle.NighttimeLengthInHours;
			float num2 = Mathf.Min(num + 0.25f, 1f);
			ParticleSystem.MainModule singleFlame = this._fire.SingleFlame;
			singleFlame.startSize = new ParticleSystem.MinMaxCurve(singleFlame.startSize.constantMin, num2 * this._initialFlamesStartSizeConstMax);
			singleFlame.startLifetime = new ParticleSystem.MinMaxCurve(singleFlame.startLifetime.constantMin, num2 * this._initialFlamesStartLifetimeConstMax);
			this._fire.SetSpeedMultiplier(num2);
			this._fire.Light.intensity = this._initialLightIntensity * num2;
			if (this._woodstack)
			{
				this._woodstack.transform.localScale = new Vector3(this._initialWoodstackScale.x, num, this._initialWoodstackScale.z);
			}
		}

		// Token: 0x04000013 RID: 19
		public readonly EventBus _eventBus;

		// Token: 0x04000014 RID: 20
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000015 RID: 21
		public GameObject _woodstack;

		// Token: 0x04000016 RID: 22
		public BlockableObject _blockableObject;

		// Token: 0x04000017 RID: 23
		public Fire _fire;

		// Token: 0x04000018 RID: 24
		public float _initialLightIntensity;

		// Token: 0x04000019 RID: 25
		public float _initialFlamesStartSizeConstMax;

		// Token: 0x0400001A RID: 26
		public float _initialFlamesStartLifetimeConstMax;

		// Token: 0x0400001B RID: 27
		public Vector3 _initialWoodstackScale;

		// Token: 0x0400001C RID: 28
		public bool _fireIsOn;
	}
}
