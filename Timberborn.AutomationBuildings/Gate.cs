using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000013 RID: 19
	public class Gate : BaseComponent, IAwakableComponent, IDeletableEntity, IPersistentEntity, IFinishedStateListener, IAutomatableNeeder, IDuplicable<Gate>, IDuplicable, ITerminal
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000AC RID: 172 RVA: 0x00003710 File Offset: 0x00001910
		// (remove) Token: 0x060000AD RID: 173 RVA: 0x00003748 File Offset: 0x00001948
		public event EventHandler StateChanged;

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000377D File Offset: 0x0000197D
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00003785 File Offset: 0x00001985
		public bool IsConflict { get; private set; }

		// Token: 0x060000B0 RID: 176 RVA: 0x0000378E File Offset: 0x0000198E
		public Gate(GateUpdater gateUpdater)
		{
			this._gateUpdater = gateUpdater;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000379D File Offset: 0x0000199D
		public bool OpenMode
		{
			get
			{
				return this._gateOpeningMode == GateOpeningMode.Open;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000037A8 File Offset: 0x000019A8
		public bool ClosedMode
		{
			get
			{
				return this._gateOpeningMode == GateOpeningMode.Closed;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000037B3 File Offset: 0x000019B3
		public bool AutomatedMode
		{
			get
			{
				return this._gateOpeningMode == GateOpeningMode.Automated;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000037BE File Offset: 0x000019BE
		public bool NeedsAutomatable
		{
			get
			{
				return this.AutomatedMode;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000037C6 File Offset: 0x000019C6
		public bool IsOpenByAutomation
		{
			get
			{
				return this.AutomatedMode && this._automatable.State != ConnectionState.Off;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000037E3 File Offset: 0x000019E3
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._automatable = base.GetComponent<Automatable>();
			this._gateNavMeshBlocker = base.GetComponent<GateNavMeshBlocker>();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003809 File Offset: 0x00001A09
		public void DeleteEntity()
		{
			if (this._gateNavMeshBlocker.NavMeshBlocked)
			{
				this._gateNavMeshBlocker.Unblock();
			}
			this._gateUpdater.Remove(this);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000382F File Offset: 0x00001A2F
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(Gate.ComponentKey).Set<GateOpeningMode>(Gate.OpeningModeKey, this._gateOpeningMode);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000384C File Offset: 0x00001A4C
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(Gate.ComponentKey);
			this._gateOpeningMode = component.Get<GateOpeningMode>(Gate.OpeningModeKey);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003876 File Offset: 0x00001A76
		public void OnEnterFinishedState()
		{
			this.UpdateState();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000387E File Offset: 0x00001A7E
		public void OnExitFinishedState()
		{
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003880 File Offset: 0x00001A80
		public void DuplicateFrom(Gate source)
		{
			this._gateOpeningMode = source._gateOpeningMode;
			this.UpdateState();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003894 File Offset: 0x00001A94
		public void Evaluate()
		{
			if (this._gateOpeningMode == GateOpeningMode.Automated)
			{
				this.UpdateState();
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000038A5 File Offset: 0x00001AA5
		public void Open()
		{
			this.SetOpeningMode(GateOpeningMode.Open);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000038AE File Offset: 0x00001AAE
		public void Close()
		{
			this.SetOpeningMode(GateOpeningMode.Closed);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Automate()
		{
			this.SetOpeningMode(GateOpeningMode.Automated);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000038C0 File Offset: 0x00001AC0
		public void EnableConflict()
		{
			this.IsConflict = true;
			this.NotifyStateChanged();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000038CF File Offset: 0x00001ACF
		public void DisableConflict()
		{
			this.IsConflict = false;
			this.NotifyStateChanged();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000038DE File Offset: 0x00001ADE
		public void BlockNavMesh()
		{
			this._gateNavMeshBlocker.Block();
			this.NotifyStateChanged();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000038F1 File Offset: 0x00001AF1
		public void UnblockNavMesh()
		{
			this._gateNavMeshBlocker.Unblock();
			this.NotifyStateChanged();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003904 File Offset: 0x00001B04
		public void SetOpeningMode(GateOpeningMode gateOpeningMode)
		{
			if (this._gateOpeningMode != gateOpeningMode)
			{
				this._gateOpeningMode = gateOpeningMode;
				this.UpdateState();
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000391C File Offset: 0x00001B1C
		public void UpdateState()
		{
			if (this._blockObject.IsFinished)
			{
				if (this._gateOpeningMode == GateOpeningMode.Open || this.IsOpenByAutomation)
				{
					this._gateUpdater.ScheduleToOpen(this);
					return;
				}
				this._gateUpdater.ScheduleToClose(this);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003954 File Offset: 0x00001B54
		public void NotifyStateChanged()
		{
			EventHandler stateChanged = this.StateChanged;
			if (stateChanged == null)
			{
				return;
			}
			stateChanged(this, EventArgs.Empty);
		}

		// Token: 0x0400004A RID: 74
		public static readonly ComponentKey ComponentKey = new ComponentKey("Gate");

		// Token: 0x0400004B RID: 75
		public static readonly PropertyKey<GateOpeningMode> OpeningModeKey = new PropertyKey<GateOpeningMode>("OpeningMode");

		// Token: 0x0400004E RID: 78
		public readonly GateUpdater _gateUpdater;

		// Token: 0x0400004F RID: 79
		public BlockObject _blockObject;

		// Token: 0x04000050 RID: 80
		public Automatable _automatable;

		// Token: 0x04000051 RID: 81
		public GateNavMeshBlocker _gateNavMeshBlocker;

		// Token: 0x04000052 RID: 82
		public GateOpeningMode _gateOpeningMode;
	}
}
