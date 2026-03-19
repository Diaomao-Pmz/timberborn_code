using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.Illumination;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000021 RID: 33
	public class Lever : BaseComponent, IAwakableComponent, IAutomatorListener, IPersistentEntity, IDuplicable<Lever>, IDuplicable, IRegisteredComponent, ITransmitter
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000156 RID: 342 RVA: 0x00004C14 File Offset: 0x00002E14
		// (remove) Token: 0x06000157 RID: 343 RVA: 0x00004C4C File Offset: 0x00002E4C
		public event EventHandler IsSpringReturnChanged;

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00004C81 File Offset: 0x00002E81
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00004C89 File Offset: 0x00002E89
		public bool IsOn { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00004C92 File Offset: 0x00002E92
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00004C9A File Offset: 0x00002E9A
		public bool IsSpringReturn { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00004CA3 File Offset: 0x00002EA3
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00004CAB File Offset: 0x00002EAB
		public bool IsPinned { get; private set; }

		// Token: 0x0600015E RID: 350 RVA: 0x00004CB4 File Offset: 0x00002EB4
		public Lever(SpringReturnService springReturnService, EventBus eventBus)
		{
			this._springReturnService = springReturnService;
			this._eventBus = eventBus;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00004CCA File Offset: 0x00002ECA
		public string LeverName
		{
			get
			{
				return this._automator.AutomatorName;
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00004CD7 File Offset: 0x00002ED7
		public void Awake()
		{
			this._automator = base.GetComponent<Automator>();
			base.GetComponent<CustomizableIlluminator>().AppliedColorChanged += this.OnAppliedColorChanged;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00004CFC File Offset: 0x00002EFC
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(Lever.LeverKey);
			if (this.IsOn)
			{
				component.Set(Lever.IsOnKey, true);
			}
			if (this.IsSpringReturn)
			{
				component.Set(Lever.IsSpringReturnKey, true);
			}
			if (this.IsPinned)
			{
				component.Set(Lever.IsPinnedKey, true);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00004D54 File Offset: 0x00002F54
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(Lever.LeverKey, out objectLoader))
			{
				if (objectLoader.Has<bool>(Lever.IsOnKey) && objectLoader.Get(Lever.IsOnKey))
				{
					this.SwitchOn();
				}
				this.IsPinned = (objectLoader.Has<bool>(Lever.IsPinnedKey) && objectLoader.Get(Lever.IsPinnedKey));
				this.SetSpringReturn(objectLoader.Has<bool>(Lever.IsSpringReturnKey) && objectLoader.Get(Lever.IsSpringReturnKey));
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00004DD2 File Offset: 0x00002FD2
		public void DuplicateFrom(Lever source)
		{
			this.IsOn = source.IsOn;
			this.SetSpringReturn(source.IsSpringReturn);
			this.SetPinned(source.IsPinned);
			this.UpdateOutputState();
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00004DFE File Offset: 0x00002FFE
		public void Press()
		{
			if (!this._isPressed)
			{
				if (this.IsSpringReturn)
				{
					this.Toggle();
				}
				this._isPressed = true;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00004E1D File Offset: 0x0000301D
		public void Release()
		{
			if (this._isPressed)
			{
				if (this.IsSpringReturn)
				{
					if (this.IsOn && !this._registeredForSpringReturn)
					{
						this.SwitchOff();
					}
				}
				else
				{
					this.Toggle();
				}
				this._isPressed = false;
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00004E54 File Offset: 0x00003054
		public void SwitchState(bool newValue)
		{
			if (this.IsOn != newValue)
			{
				this.IsOn = newValue;
				this.UpdateOutputState();
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00004E6C File Offset: 0x0000306C
		public void SetSpringReturn(bool value)
		{
			if (this.IsSpringReturn != value)
			{
				this.IsSpringReturn = value;
				this.RegisterForSpringReturn();
				EventHandler isSpringReturnChanged = this.IsSpringReturnChanged;
				if (isSpringReturnChanged == null)
				{
					return;
				}
				isSpringReturnChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00004E9A File Offset: 0x0000309A
		public void SetPinned(bool value)
		{
			if (this.IsPinned != value)
			{
				this.IsPinned = value;
				this._eventBus.Post(new LeverPinnedChangedEvent());
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004EBC File Offset: 0x000030BC
		public void OnAutomatorStateChanged()
		{
			this.PostPinnedLeverModified();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00004EC4 File Offset: 0x000030C4
		public void SpringReturnToOff()
		{
			if (this.IsSpringReturn && !this._isPressed)
			{
				this.SwitchOff();
			}
			this._registeredForSpringReturn = false;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004EBC File Offset: 0x000030BC
		public void OnAppliedColorChanged(object sender, EventArgs e)
		{
			this.PostPinnedLeverModified();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00004EE3 File Offset: 0x000030E3
		public void SwitchOn()
		{
			this.SwitchState(true);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004EEC File Offset: 0x000030EC
		public void SwitchOff()
		{
			this.SwitchState(false);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004EF5 File Offset: 0x000030F5
		public void Toggle()
		{
			this.SwitchState(!this.IsOn);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004F06 File Offset: 0x00003106
		public void UpdateOutputState()
		{
			this._automator.SetState(this.IsOn);
			this.RegisterForSpringReturn();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004F1F File Offset: 0x0000311F
		public void RegisterForSpringReturn()
		{
			if (this.IsOn && this.IsSpringReturn && !this._registeredForSpringReturn)
			{
				this._springReturnService.Register(this);
				this._registeredForSpringReturn = true;
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00004F4C File Offset: 0x0000314C
		public void PostPinnedLeverModified()
		{
			if (this.IsPinned)
			{
				this._eventBus.Post(new PinnedLeverModified(this));
			}
		}

		// Token: 0x04000094 RID: 148
		public static readonly ComponentKey LeverKey = new ComponentKey("Lever");

		// Token: 0x04000095 RID: 149
		public static readonly PropertyKey<bool> IsOnKey = new PropertyKey<bool>("IsOn");

		// Token: 0x04000096 RID: 150
		public static readonly PropertyKey<bool> IsSpringReturnKey = new PropertyKey<bool>("IsSpringReturn");

		// Token: 0x04000097 RID: 151
		public static readonly PropertyKey<bool> IsPinnedKey = new PropertyKey<bool>("IsPinned");

		// Token: 0x0400009C RID: 156
		public readonly SpringReturnService _springReturnService;

		// Token: 0x0400009D RID: 157
		public readonly EventBus _eventBus;

		// Token: 0x0400009E RID: 158
		public Automator _automator;

		// Token: 0x0400009F RID: 159
		public bool _registeredForSpringReturn;

		// Token: 0x040000A0 RID: 160
		public bool _isPressed;
	}
}
