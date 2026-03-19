using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.Illumination;
using Timberborn.NotificationSystem;
using Timberborn.Persistence;
using Timberborn.QuickNotificationSystem;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200001C RID: 28
	public class Indicator : BaseComponent, IAwakableComponent, IPersistentEntity, IDuplicable<Indicator>, IDuplicable, IFinishedStateListener, IAutomatableNeeder, ITerminal, IRegisteredComponent
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600011B RID: 283 RVA: 0x00004564 File Offset: 0x00002764
		// (remove) Token: 0x0600011C RID: 284 RVA: 0x0000459C File Offset: 0x0000279C
		public event EventHandler PinnedIndicatorModified;

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000045D1 File Offset: 0x000027D1
		// (set) Token: 0x0600011E RID: 286 RVA: 0x000045D9 File Offset: 0x000027D9
		public IndicatorPinnedMode PinnedMode { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000045E2 File Offset: 0x000027E2
		// (set) Token: 0x06000120 RID: 288 RVA: 0x000045EA File Offset: 0x000027EA
		public bool IsWarningEnabled { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000045F3 File Offset: 0x000027F3
		// (set) Token: 0x06000122 RID: 290 RVA: 0x000045FB File Offset: 0x000027FB
		public bool IsJournalEntryEnabled { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00004604 File Offset: 0x00002804
		// (set) Token: 0x06000124 RID: 292 RVA: 0x0000460C File Offset: 0x0000280C
		public bool IsColorReplicationEnabled { get; private set; }

		// Token: 0x06000125 RID: 293 RVA: 0x00004615 File Offset: 0x00002815
		public Indicator(QuickNotificationService quickNotificationService, EventBus eventBus, NotificationBus notificationBus)
		{
			this._quickNotificationService = quickNotificationService;
			this._eventBus = eventBus;
			this._notificationBus = notificationBus;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00003095 File Offset: 0x00001295
		public bool NeedsAutomatable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00004632 File Offset: 0x00002832
		public string IndicatorName
		{
			get
			{
				return this._automator.AutomatorName;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000463F File Offset: 0x0000283F
		public bool State
		{
			get
			{
				return this._automatable.State == ConnectionState.On;
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004650 File Offset: 0x00002850
		public void Awake()
		{
			this._automator = base.GetComponent<Automator>();
			this._automatable = base.GetComponent<Automatable>();
			this._customizableIlluminator = base.GetComponent<CustomizableIlluminator>();
			this._illuminatorToggle = base.GetComponent<Illuminator>().CreateToggle();
			base.GetComponent<CustomizableIlluminator>().AppliedColorChanged += this.OnAppliedColorChanged;
			base.DisableComponent();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000046AF File Offset: 0x000028AF
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this.NotifyPinnedIndicatorModified();
			this.ResubscribeToInputColor();
			this.ReplicateInputColor();
			this._automatable.InputReconnected += this.OnInputReconnected;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000046E0 File Offset: 0x000028E0
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this.NotifyPinnedIndicatorModified();
			this._automatable.InputReconnected -= this.OnInputReconnected;
			this.UnsubscribeFromInputColor();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000470C File Offset: 0x0000290C
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(Indicator.ComponentKey);
			if (this.PinnedMode != IndicatorPinnedMode.Never)
			{
				component.Set<IndicatorPinnedMode>(Indicator.PinnedModeKey, this.PinnedMode);
			}
			if (this.IsWarningEnabled)
			{
				component.Set(Indicator.IsWarningEnabledKey, this.IsWarningEnabled);
			}
			if (this.IsJournalEntryEnabled)
			{
				component.Set(Indicator.IsJournalEntryEnabledKey, this.IsJournalEntryEnabled);
			}
			if (this.IsColorReplicationEnabled)
			{
				component.Set(Indicator.IsColorReplicationEnabledKey, this.IsColorReplicationEnabled);
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000478C File Offset: 0x0000298C
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(Indicator.ComponentKey);
			this.PinnedMode = (component.Has<IndicatorPinnedMode>(Indicator.PinnedModeKey) ? component.Get<IndicatorPinnedMode>(Indicator.PinnedModeKey) : IndicatorPinnedMode.Never);
			this.IsWarningEnabled = (component.Has<bool>(Indicator.IsWarningEnabledKey) && component.Get(Indicator.IsWarningEnabledKey));
			this.IsJournalEntryEnabled = (component.Has<bool>(Indicator.IsJournalEntryEnabledKey) && component.Get(Indicator.IsJournalEntryEnabledKey));
			this.IsColorReplicationEnabled = (component.Has<bool>(Indicator.IsColorReplicationEnabledKey) && component.Get(Indicator.IsColorReplicationEnabledKey));
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000482C File Offset: 0x00002A2C
		public void Evaluate()
		{
			bool flag = this._automatable.State == ConnectionState.On;
			bool? previousState = this._previousState;
			bool flag2 = flag;
			if (!(previousState.GetValueOrDefault() == flag2 & previousState != null))
			{
				this._illuminatorToggle.Toggle(flag);
				if (this._previousState != null && flag)
				{
					this.EvaluateRisingEdge();
				}
				this._previousState = new bool?(flag);
				this.NotifyPinnedIndicatorModified();
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004898 File Offset: 0x00002A98
		public void DuplicateFrom(Indicator source)
		{
			this.SetPinnedMode(source.PinnedMode);
			this.SetWarningEnabled(source.IsWarningEnabled);
			this.SetJournalEntryEnabled(source.IsJournalEntryEnabled);
			this.SetColorReplicationEnabled(source.IsColorReplicationEnabled);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000048CA File Offset: 0x00002ACA
		public void SetPinnedMode(IndicatorPinnedMode value)
		{
			if (this.PinnedMode != value)
			{
				this.PinnedMode = value;
				this._eventBus.Post(new IndicatorPinnedModeChangedEvent());
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000048EC File Offset: 0x00002AEC
		public void SetWarningEnabled(bool value)
		{
			this.IsWarningEnabled = value;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000048F5 File Offset: 0x00002AF5
		public void SetJournalEntryEnabled(bool value)
		{
			this.IsJournalEntryEnabled = value;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000048FE File Offset: 0x00002AFE
		public void SetColorReplicationEnabled(bool value)
		{
			this.IsColorReplicationEnabled = value;
			this.ResubscribeToInputColor();
			this.ReplicateInputColor();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004913 File Offset: 0x00002B13
		public void OnAppliedColorChanged(object sender, EventArgs e)
		{
			this.NotifyPinnedIndicatorModified();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000491B File Offset: 0x00002B1B
		public void EvaluateRisingEdge()
		{
			if (this.IsWarningEnabled)
			{
				this.ShowWarning();
			}
			if (this.IsJournalEntryEnabled)
			{
				this.AddJournalEntry();
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004939 File Offset: 0x00002B39
		public void ShowWarning()
		{
			this._quickNotificationService.SendWarningNotification(this.IndicatorName);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000494C File Offset: 0x00002B4C
		public void AddJournalEntry()
		{
			this._notificationBus.Post(this.IndicatorName, this);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004960 File Offset: 0x00002B60
		public void NotifyPinnedIndicatorModified()
		{
			if (this.PinnedMode != IndicatorPinnedMode.Never)
			{
				EventHandler pinnedIndicatorModified = this.PinnedIndicatorModified;
				if (pinnedIndicatorModified == null)
				{
					return;
				}
				pinnedIndicatorModified(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004980 File Offset: 0x00002B80
		public void OnInputReconnected(object sender, EventArgs e)
		{
			this.ResubscribeToInputColor();
			this.ReplicateInputColor();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004990 File Offset: 0x00002B90
		public void ResubscribeToInputColor()
		{
			this.UnsubscribeFromInputColor();
			if (this.IsColorReplicationEnabled)
			{
				Automator input = this._automatable.Input;
				this._inputCustomizableIlluminator = ((input != null) ? input.GetComponent<CustomizableIlluminator>() : null);
				if (this._inputCustomizableIlluminator != null && this._inputCustomizableIlluminator)
				{
					this._inputCustomizableIlluminator.CustomColorChanged += this.OnInputCustomColorChanged;
					this._customizableIlluminator.Lock();
				}
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000049FF File Offset: 0x00002BFF
		public void UnsubscribeFromInputColor()
		{
			if (this._inputCustomizableIlluminator != null)
			{
				this._inputCustomizableIlluminator.CustomColorChanged -= this.OnInputCustomColorChanged;
				this._inputCustomizableIlluminator = null;
				this._customizableIlluminator.Unlock();
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004A32 File Offset: 0x00002C32
		public void OnInputCustomColorChanged(object sender, EventArgs e)
		{
			this.ReplicateInputColor();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004A3A File Offset: 0x00002C3A
		public void ReplicateInputColor()
		{
			if (this._inputCustomizableIlluminator != null)
			{
				this._customizableIlluminator.SetIsCustomized(true);
				this._customizableIlluminator.SetCustomColor(this._inputCustomizableIlluminator.CustomColor);
			}
		}

		// Token: 0x04000079 RID: 121
		public static readonly ComponentKey ComponentKey = new ComponentKey("Indicator");

		// Token: 0x0400007A RID: 122
		public static readonly PropertyKey<IndicatorPinnedMode> PinnedModeKey = new PropertyKey<IndicatorPinnedMode>("PinnedMode");

		// Token: 0x0400007B RID: 123
		public static readonly PropertyKey<bool> IsWarningEnabledKey = new PropertyKey<bool>("IsWarningEnabled");

		// Token: 0x0400007C RID: 124
		public static readonly PropertyKey<bool> IsJournalEntryEnabledKey = new PropertyKey<bool>("IsJournalEntryEnabled");

		// Token: 0x0400007D RID: 125
		public static readonly PropertyKey<bool> IsColorReplicationEnabledKey = new PropertyKey<bool>("IsColorReplicationEnabled");

		// Token: 0x04000083 RID: 131
		public readonly QuickNotificationService _quickNotificationService;

		// Token: 0x04000084 RID: 132
		public readonly NotificationBus _notificationBus;

		// Token: 0x04000085 RID: 133
		public readonly EventBus _eventBus;

		// Token: 0x04000086 RID: 134
		public Automator _automator;

		// Token: 0x04000087 RID: 135
		public Automatable _automatable;

		// Token: 0x04000088 RID: 136
		public CustomizableIlluminator _customizableIlluminator;

		// Token: 0x04000089 RID: 137
		public IlluminatorToggle _illuminatorToggle;

		// Token: 0x0400008A RID: 138
		public bool? _previousState;

		// Token: 0x0400008B RID: 139
		public CustomizableIlluminator _inputCustomizableIlluminator;
	}
}
