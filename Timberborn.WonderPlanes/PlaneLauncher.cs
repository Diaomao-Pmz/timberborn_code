using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Characters;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.TimeSystem;
using Timberborn.Wonders;
using Timberborn.WorkSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.WonderPlanes
{
	// Token: 0x0200000C RID: 12
	public class PlaneLauncher : BaseComponent, IAwakableComponent, IPersistentEntity, IDeletableEntity, IWonderBlocker
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002AFD File Offset: 0x00000CFD
		public PlaneLauncher(ITimeTriggerFactory timeTriggerFactory, ReferenceSerializer referenceSerializer)
		{
			this._timeTriggerFactory = timeTriggerFactory;
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002B20 File Offset: 0x00000D20
		public void Awake()
		{
			this._planeLauncherRotator = base.GetComponent<PlaneLauncherRotator>();
			this._wonder = base.GetComponent<Wonder>();
			this._planeCatapult = base.GetComponent<PlaneCatapult>();
			this._workplace = base.GetComponent<Workplace>();
			this._blockObjectCenter = base.GetComponent<BlockObjectCenter>();
			this._wonder.WonderActivated += this.OnWonderActivated;
			this._planeLauncherRotator.RotationFinished += this.OnRotationFinished;
			this._planeCatapult.PlaneCatapulted += this.OnPlaneCatapulted;
			base.GetComponent<WonderAnimationController>().StartAnimationFinished += this.OnStartAnimationFinished;
			this._pilotsDestructionTimeTrigger = this._timeTriggerFactory.Create(new Action(this.DestroyPilots), PlaneLauncher.DestructionDelayInHours / 24f);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002BF0 File Offset: 0x00000DF0
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(PlaneLauncher.PlaneLauncherKey);
			component.Set<Pilot>(PlaneLauncher.PilotsKey, this._pilots, this._referenceSerializer.Of<Pilot>());
			component.Set(PlaneLauncher.PilotsSentKey, this._pilotsSent);
			if (this._pilotsDestructionTimeTrigger.InProgress)
			{
				component.Set(PlaneLauncher.PilotsDestructionProgressKey, this._pilotsDestructionTimeTrigger.Progress);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002C5C File Offset: 0x00000E5C
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(PlaneLauncher.PlaneLauncherKey);
			this._pilots.AddRange(component.Get<Pilot>(PlaneLauncher.PilotsKey, this._referenceSerializer.Of<Pilot>()));
			this._pilotsSent = component.Get(PlaneLauncher.PilotsSentKey);
			if (component.Has<float>(PlaneLauncher.PilotsDestructionProgressKey))
			{
				float progress = component.Get(PlaneLauncher.PilotsDestructionProgressKey);
				this._pilotsDestructionTimeTrigger.FastForwardProgress(progress);
				this._pilotsDestructionTimeTrigger.Resume();
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002CD7 File Offset: 0x00000ED7
		public void DeleteEntity()
		{
			this._pilotsDestructionTimeTrigger.Pause();
			this.DestroyPilots();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002CEA File Offset: 0x00000EEA
		public bool IsWonderBlocked()
		{
			return this._pilots.Count > 0;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002CFC File Offset: 0x00000EFC
		public void DestroyPilots()
		{
			foreach (Pilot pilot in this._pilots)
			{
				pilot.GetComponent<Character>().DestroyCharacter();
			}
			this._pilots.Clear();
			this._pilotsSent = 0;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002D64 File Offset: 0x00000F64
		public void OnRotationFinished(object sender, EventArgs e)
		{
			if (this._pilotsSent == this._pilots.Count)
			{
				this._wonder.Deactivate();
				this._pilotsDestructionTimeTrigger.Reset();
				this._pilotsDestructionTimeTrigger.Resume();
				return;
			}
			this.StartEjectingPlane();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002DA4 File Offset: 0x00000FA4
		public void StartEjectingPlane()
		{
			List<Pilot> pilots = this._pilots;
			int pilotsSent = this._pilotsSent;
			this._pilotsSent = pilotsSent + 1;
			Pilot pilot = pilots[pilotsSent];
			this._planeCatapult.CatapultPlane(pilot);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002DDC File Offset: 0x00000FDC
		public void OnPlaneCatapulted(object sender, EventArgs e)
		{
			if (this._pilotsSent < this._pilots.Count)
			{
				float rotationAngle = 360f / (float)this._pilots.Count;
				this._planeLauncherRotator.StartRotation(rotationAngle);
				return;
			}
			this._planeLauncherRotator.RotateToOriginalPosition();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002E27 File Offset: 0x00001027
		public void OnWonderActivated(object sender, EventArgs e)
		{
			this.TeleportAndInitializePilots();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002E30 File Offset: 0x00001030
		public void TeleportAndInitializePilots()
		{
			int count = this._workplace.AssignedWorkers.Count;
			for (int i = 0; i < count; i++)
			{
				Pilot component = this._workplace.AssignedWorkers[i].GetComponent<Pilot>();
				component.PrepareForFlying(this._blockObjectCenter.WorldCenterGrounded);
				this._pilots.Add(component);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002E94 File Offset: 0x00001094
		public void OnStartAnimationFinished(object sender, EventArgs e)
		{
			this.StartEjectingPlane();
		}

		// Token: 0x04000036 RID: 54
		public static readonly float DestructionDelayInHours = 0.5f;

		// Token: 0x04000037 RID: 55
		public static readonly ComponentKey PlaneLauncherKey = new ComponentKey("PlaneLauncher");

		// Token: 0x04000038 RID: 56
		public static readonly ListKey<Pilot> PilotsKey = new ListKey<Pilot>("Pilots");

		// Token: 0x04000039 RID: 57
		public static readonly PropertyKey<int> PilotsSentKey = new PropertyKey<int>("PilotsSent");

		// Token: 0x0400003A RID: 58
		public static readonly PropertyKey<float> PilotsDestructionProgressKey = new PropertyKey<float>("PilotsDestructionProgress");

		// Token: 0x0400003B RID: 59
		public readonly ITimeTriggerFactory _timeTriggerFactory;

		// Token: 0x0400003C RID: 60
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x0400003D RID: 61
		public PlaneLauncherRotator _planeLauncherRotator;

		// Token: 0x0400003E RID: 62
		public Wonder _wonder;

		// Token: 0x0400003F RID: 63
		public PlaneCatapult _planeCatapult;

		// Token: 0x04000040 RID: 64
		public Workplace _workplace;

		// Token: 0x04000041 RID: 65
		public BlockObjectCenter _blockObjectCenter;

		// Token: 0x04000042 RID: 66
		public readonly List<Pilot> _pilots = new List<Pilot>();

		// Token: 0x04000043 RID: 67
		public int _pilotsSent;

		// Token: 0x04000044 RID: 68
		public ITimeTrigger _pilotsDestructionTimeTrigger;
	}
}
