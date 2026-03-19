using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.MechanicalSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.PowerManagement
{
	// Token: 0x02000007 RID: 7
	public class Clutch : BaseComponent, IAwakableComponent, IPersistentEntity, IInitializableEntity, IDuplicable<Clutch>, IDuplicable, IAutomatableNeeder, ITerminal
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public event EventHandler IsEngagedChanged;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000216D File Offset: 0x0000036D
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002175 File Offset: 0x00000375
		public ClutchMode Mode { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000217E File Offset: 0x0000037E
		public bool NeedsAutomatable
		{
			get
			{
				return this.Mode == ClutchMode.Automated;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000218C File Offset: 0x0000038C
		public bool IsEngaged
		{
			get
			{
				bool result;
				switch (this.Mode)
				{
				case ClutchMode.Engaged:
					result = true;
					break;
				case ClutchMode.Disengaged:
					result = false;
					break;
				case ClutchMode.Automated:
					result = (this._automatable.State == ConnectionState.On);
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				return result;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021D4 File Offset: 0x000003D4
		public void Awake()
		{
			this._automatable = base.GetComponent<Automatable>();
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021EE File Offset: 0x000003EE
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(Clutch.ClutchKey).Set<ClutchMode>(Clutch.ModeKey, this.Mode);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000220C File Offset: 0x0000040C
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(Clutch.ClutchKey);
			this.Mode = component.Get<ClutchMode>(Clutch.ModeKey);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002236 File Offset: 0x00000436
		public void InitializeEntity()
		{
			this.ApplyState();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000223E File Offset: 0x0000043E
		public void DuplicateFrom(Clutch source)
		{
			this.Mode = source.Mode;
			this.ApplyState();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002252 File Offset: 0x00000452
		public void SetMode(ClutchMode value)
		{
			if (this.Mode != value)
			{
				this.Mode = value;
				this.ApplyState();
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002236 File Offset: 0x00000436
		public void Evaluate()
		{
			this.ApplyState();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000226C File Offset: 0x0000046C
		public void ApplyState()
		{
			bool? wasEngaged = this._wasEngaged;
			bool isEngaged = this.IsEngaged;
			if (!(wasEngaged.GetValueOrDefault() == isEngaged & wasEngaged != null))
			{
				this._mechanicalNode.SetDetached(!this.IsEngaged);
				EventHandler isEngagedChanged = this.IsEngagedChanged;
				if (isEngagedChanged != null)
				{
					isEngagedChanged(this, EventArgs.Empty);
				}
				this._wasEngaged = new bool?(this.IsEngaged);
			}
		}

		// Token: 0x04000008 RID: 8
		public static readonly ComponentKey ClutchKey = new ComponentKey("Clutch");

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<ClutchMode> ModeKey = new PropertyKey<ClutchMode>("Mode");

		// Token: 0x0400000C RID: 12
		public Automatable _automatable;

		// Token: 0x0400000D RID: 13
		public MechanicalNode _mechanicalNode;

		// Token: 0x0400000E RID: 14
		public bool? _wasEngaged;
	}
}
