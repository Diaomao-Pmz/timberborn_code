using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.GameCycleSystem;
using Timberborn.MapStateSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.ActivatorSystem
{
	// Token: 0x0200000D RID: 13
	public class TimedComponentActivator : TickableComponent, IPersistentEntity, IAwakableComponent, IInitializableEntity, IDuplicable<TimedComponentActivator>, IDuplicable, IDeletableEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000045 RID: 69 RVA: 0x00002A34 File Offset: 0x00000C34
		// (remove) Token: 0x06000046 RID: 70 RVA: 0x00002A6C File Offset: 0x00000C6C
		public event EventHandler CountdownActivated;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000047 RID: 71 RVA: 0x00002AA4 File Offset: 0x00000CA4
		// (remove) Token: 0x06000048 RID: 72 RVA: 0x00002ADC File Offset: 0x00000CDC
		public event EventHandler Activated;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002B11 File Offset: 0x00000D11
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002B19 File Offset: 0x00000D19
		public int CyclesUntilCountdownActivation { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002B22 File Offset: 0x00000D22
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002B2A File Offset: 0x00000D2A
		public float DaysUntilActivation { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002B33 File Offset: 0x00000D33
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002B3B File Offset: 0x00000D3B
		public bool IsEnabled { get; private set; }

		// Token: 0x0600004F RID: 79 RVA: 0x00002B44 File Offset: 0x00000D44
		public TimedComponentActivator(EventBus eventBus, MapEditorMode mapEditorMode, GameCycleService gameCycleService, IDayNightCycle dayNightCycle)
		{
			this._eventBus = eventBus;
			this._mapEditorMode = mapEditorMode;
			this._gameCycleService = gameCycleService;
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002B69 File Offset: 0x00000D69
		public bool CountdownIsActive
		{
			get
			{
				return !this._mapEditorMode.IsMapEditor && this.IsEnabled && this._gameCycleService.Cycle >= this.CyclesUntilCountdownActivation && this.DaysPassedWithHours < this.DaysUntilActivation;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002BA3 File Offset: 0x00000DA3
		public bool IsPastActivationTime
		{
			get
			{
				return !this._mapEditorMode.IsMapEditor && this._gameCycleService.Cycle >= this.CyclesUntilCountdownActivation && this.DaysPassedWithHours >= this.DaysUntilActivation;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public bool IsOptional
		{
			get
			{
				return this._spec.IsOptionallyActivable;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002BE5 File Offset: 0x00000DE5
		public float ActivationProgress
		{
			get
			{
				return this.DaysPassedWithHours / this.DaysUntilActivation;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public float DaysLeftUntilActivation
		{
			get
			{
				return this.DaysUntilActivation - this.DaysPassedWithHours;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002C03 File Offset: 0x00000E03
		public bool IsDuplicable
		{
			get
			{
				return this._mapEditorMode.IsMapEditor;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002C10 File Offset: 0x00000E10
		public void Awake()
		{
			this._spec = base.GetComponent<TimedComponentActivatorSpec>();
			this._activableComponent = base.GetComponent<IActivableComponent>();
			this.CyclesUntilCountdownActivation = this._spec.CyclesUntilCountdownActivation;
			this.DaysUntilActivation = this._spec.DaysUntilActivation;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002C4C File Offset: 0x00000E4C
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(TimedComponentActivator.TimeActivatedComponentKey);
			component.Set(TimedComponentActivator.IsEnabledKey, this.IsEnabled);
			component.Set(TimedComponentActivator.CyclesUntilCountdownActivationKey, this.CyclesUntilCountdownActivation);
			component.Set(TimedComponentActivator.DaysUntilActivationKey, this.DaysUntilActivation);
			component.Set(TimedComponentActivator.DaysPassedKey, this._daysPassed);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002CA8 File Offset: 0x00000EA8
		[BackwardCompatible(2025, 9, 16, Compatibility.Save)]
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(TimedComponentActivator.TimeActivatedComponentKey, out objectLoader))
			{
				this.IsEnabled = objectLoader.Get(TimedComponentActivator.IsEnabledKey);
				this.CyclesUntilCountdownActivation = objectLoader.Get(TimedComponentActivator.CyclesUntilCountdownActivationKey);
				this.DaysUntilActivation = objectLoader.Get(TimedComponentActivator.DaysUntilActivationKey);
				this._daysPassed = objectLoader.Get(TimedComponentActivator.DaysPassedKey);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002D08 File Offset: 0x00000F08
		public void InitializeEntity()
		{
			if (!this.IsOptional)
			{
				this.IsEnabled = true;
			}
			this.InitializeActivableComponent();
			this._eventBus.Register(this);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002D2B File Offset: 0x00000F2B
		public void DeleteEntity()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002D39 File Offset: 0x00000F39
		public void DuplicateFrom(TimedComponentActivator source)
		{
			if (this.IsOptional)
			{
				this.IsEnabled = source.IsEnabled;
			}
			this.CyclesUntilCountdownActivation = source.CyclesUntilCountdownActivation;
			this.DaysUntilActivation = source.DaysUntilActivation;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002D67 File Offset: 0x00000F67
		public override void Tick()
		{
			this.ActivateIfItsTime();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002D6F File Offset: 0x00000F6F
		public void EnableActivator()
		{
			this._activableComponent.Deactivate();
			this.IsEnabled = true;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002D83 File Offset: 0x00000F83
		public void DisableActivator()
		{
			this._activableComponent.Activate();
			this.IsEnabled = false;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002D98 File Offset: 0x00000F98
		[OnEvent]
		public void OnCycleDayStarted(CycleDayStartedEvent cycleDayStartedEvent)
		{
			if (!this._mapEditorMode.IsMapEditor && this.IsEnabled)
			{
				if (this._gameCycleService.Cycle == this.CyclesUntilCountdownActivation && this._gameCycleService.CycleDay == 1)
				{
					EventHandler countdownActivated = this.CountdownActivated;
					if (countdownActivated == null)
					{
						return;
					}
					countdownActivated(this, EventArgs.Empty);
					return;
				}
				else if (this._gameCycleService.Cycle >= this.CyclesUntilCountdownActivation)
				{
					this._daysPassed += 1f;
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002E17 File Offset: 0x00001017
		public void SetCyclesUntilCountdownActivation(int cyclesUntilCountdownActivation)
		{
			this.CyclesUntilCountdownActivation = cyclesUntilCountdownActivation;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002E20 File Offset: 0x00001020
		public void SetDaysUntilActivation(float daysUntilActivation)
		{
			this.DaysUntilActivation = daysUntilActivation;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002E29 File Offset: 0x00001029
		public float DaysPassedWithHours
		{
			get
			{
				return this._daysPassed + this._dayNightCycle.DayProgress;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002E3D File Offset: 0x0000103D
		public void InitializeActivableComponent()
		{
			if (this.IsEnabled)
			{
				if (this.IsPastActivationTime)
				{
					this._activableComponent.Activate();
					return;
				}
				this._activableComponent.Deactivate();
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002E66 File Offset: 0x00001066
		public void ActivateIfItsTime()
		{
			if (!this._wasActivated && this.IsPastActivationTime)
			{
				this._activableComponent.Activate();
				EventHandler activated = this.Activated;
				if (activated != null)
				{
					activated(this, EventArgs.Empty);
				}
				this._wasActivated = true;
			}
		}

		// Token: 0x0400001C RID: 28
		public static readonly ComponentKey TimeActivatedComponentKey = new ComponentKey("TimeActivatedComponent");

		// Token: 0x0400001D RID: 29
		public static readonly PropertyKey<bool> IsEnabledKey = new PropertyKey<bool>("IsEnabled");

		// Token: 0x0400001E RID: 30
		public static readonly PropertyKey<int> CyclesUntilCountdownActivationKey = new PropertyKey<int>("CyclesUntilCountdownActivation");

		// Token: 0x0400001F RID: 31
		public static readonly PropertyKey<float> DaysUntilActivationKey = new PropertyKey<float>("DaysUntilActivation");

		// Token: 0x04000020 RID: 32
		public static readonly PropertyKey<float> DaysPassedKey = new PropertyKey<float>("DaysPassed");

		// Token: 0x04000026 RID: 38
		public readonly EventBus _eventBus;

		// Token: 0x04000027 RID: 39
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x04000028 RID: 40
		public readonly GameCycleService _gameCycleService;

		// Token: 0x04000029 RID: 41
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400002A RID: 42
		public TimedComponentActivatorSpec _spec;

		// Token: 0x0400002B RID: 43
		public IActivableComponent _activableComponent;

		// Token: 0x0400002C RID: 44
		public float _daysPassed;

		// Token: 0x0400002D RID: 45
		public bool _wasActivated;
	}
}
