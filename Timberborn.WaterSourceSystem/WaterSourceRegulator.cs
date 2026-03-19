using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.DuplicationSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x0200001E RID: 30
	public class WaterSourceRegulator : BaseComponent, IAwakableComponent, IPersistentEntity, IDuplicable<WaterSourceRegulator>, IDuplicable, IWaterStrengthModifier, IFinishedStateListener, IAutomatableNeeder, ITerminal
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000F4 RID: 244 RVA: 0x000037D0 File Offset: 0x000019D0
		// (remove) Token: 0x060000F5 RID: 245 RVA: 0x00003808 File Offset: 0x00001A08
		public event EventHandler<bool> OpenStateChanged;

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000383D File Offset: 0x00001A3D
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00003845 File Offset: 0x00001A45
		public bool IsOpen { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000384E File Offset: 0x00001A4E
		public bool OpenMode
		{
			get
			{
				return this._regulatorState == WaterSourceRegulator.RegulatorState.Open;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00003859 File Offset: 0x00001A59
		public bool ClosedMode
		{
			get
			{
				return this._regulatorState == WaterSourceRegulator.RegulatorState.Closed;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00003864 File Offset: 0x00001A64
		public bool AutomatedMode
		{
			get
			{
				return this._regulatorState == WaterSourceRegulator.RegulatorState.Automated;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00003864 File Offset: 0x00001A64
		public bool NeedsAutomatable
		{
			get
			{
				return this._regulatorState == WaterSourceRegulator.RegulatorState.Automated;
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000386F File Offset: 0x00001A6F
		public void Awake()
		{
			this._underlyingWaterSource = base.GetComponent<UnderlyingWaterSource>();
			this._automatable = base.GetComponent<Automatable>();
			base.DisableComponent();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000388F File Offset: 0x00001A8F
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(WaterSourceRegulator.WaterSourceRegulatorKey).Set<WaterSourceRegulator.RegulatorState>(WaterSourceRegulator.RegulatorStateKey, this._regulatorState);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000038AC File Offset: 0x00001AAC
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(WaterSourceRegulator.WaterSourceRegulatorKey);
			this.LoadOpenStateFromComponent(component);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000038CC File Offset: 0x00001ACC
		public void DuplicateFrom(WaterSourceRegulator source)
		{
			this.SetRegulatorState(source._regulatorState);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000038DA File Offset: 0x00001ADA
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this.UpdateModifierState();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000038E8 File Offset: 0x00001AE8
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this._underlyingWaterSource.RemoveWaterStrengthModifier(this);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000038FC File Offset: 0x00001AFC
		public void Open()
		{
			this.SetRegulatorState(WaterSourceRegulator.RegulatorState.Open);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003905 File Offset: 0x00001B05
		public void Close()
		{
			this.SetRegulatorState(WaterSourceRegulator.RegulatorState.Closed);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000390E File Offset: 0x00001B0E
		public void Automate()
		{
			this.SetRegulatorState(WaterSourceRegulator.RegulatorState.Automated);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003663 File Offset: 0x00001863
		public float GetStrengthModifier()
		{
			return 0f;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003917 File Offset: 0x00001B17
		public void Evaluate()
		{
			if (this._regulatorState == WaterSourceRegulator.RegulatorState.Automated)
			{
				this.UpdateOpenState();
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003928 File Offset: 0x00001B28
		[BackwardCompatible(2025, 12, 19, Compatibility.Save)]
		public void LoadOpenStateFromComponent(IObjectLoader component)
		{
			if (component.Has<WaterSourceRegulator.RegulatorState>(WaterSourceRegulator.RegulatorStateKey))
			{
				this._regulatorState = component.Get<WaterSourceRegulator.RegulatorState>(WaterSourceRegulator.RegulatorStateKey);
			}
			else
			{
				PropertyKey<bool> key = new PropertyKey<bool>("IsOpen");
				if (component.Has<bool>(key) && component.Get(key))
				{
					this._regulatorState = WaterSourceRegulator.RegulatorState.Open;
				}
			}
			this.UpdateOpenState();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00003980 File Offset: 0x00001B80
		public void SetRegulatorState(WaterSourceRegulator.RegulatorState regulatorState)
		{
			if (this._regulatorState != regulatorState)
			{
				this._regulatorState = regulatorState;
				this.UpdateOpenState();
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00003998 File Offset: 0x00001B98
		public void UpdateOpenState()
		{
			bool flag = this._regulatorState == WaterSourceRegulator.RegulatorState.Open || (this._regulatorState == WaterSourceRegulator.RegulatorState.Automated && this._automatable.State == ConnectionState.On);
			if (this.IsOpen != flag)
			{
				this.IsOpen = flag;
				this.UpdateModifierState();
				EventHandler<bool> openStateChanged = this.OpenStateChanged;
				if (openStateChanged == null)
				{
					return;
				}
				openStateChanged(this, this.IsOpen);
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000039F8 File Offset: 0x00001BF8
		public void UpdateModifierState()
		{
			if (base.Enabled)
			{
				if (this.IsOpen)
				{
					this._underlyingWaterSource.RemoveWaterStrengthModifier(this);
					return;
				}
				this._underlyingWaterSource.AddWaterStrengthModifier(this);
			}
		}

		// Token: 0x0400004A RID: 74
		public static readonly ComponentKey WaterSourceRegulatorKey = new ComponentKey("WaterSourceRegulator");

		// Token: 0x0400004B RID: 75
		public static readonly PropertyKey<WaterSourceRegulator.RegulatorState> RegulatorStateKey = new PropertyKey<WaterSourceRegulator.RegulatorState>("RegulatorState");

		// Token: 0x0400004E RID: 78
		public UnderlyingWaterSource _underlyingWaterSource;

		// Token: 0x0400004F RID: 79
		public Automatable _automatable;

		// Token: 0x04000050 RID: 80
		public WaterSourceRegulator.RegulatorState _regulatorState = WaterSourceRegulator.RegulatorState.Closed;

		// Token: 0x0200001F RID: 31
		public enum RegulatorState
		{
			// Token: 0x04000052 RID: 82
			Open,
			// Token: 0x04000053 RID: 83
			Closed,
			// Token: 0x04000054 RID: 84
			Automated
		}
	}
}
