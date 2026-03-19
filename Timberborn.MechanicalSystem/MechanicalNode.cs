using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x02000017 RID: 23
	public class MechanicalNode : BaseComponent, IAwakableComponent, IFinishedStateListener, IUnfinishedStateListener
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600005E RID: 94 RVA: 0x00002DE4 File Offset: 0x00000FE4
		// (remove) Token: 0x0600005F RID: 95 RVA: 0x00002E1C File Offset: 0x0000101C
		public event EventHandler AddedToGraph;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000060 RID: 96 RVA: 0x00002E54 File Offset: 0x00001054
		// (remove) Token: 0x06000061 RID: 97 RVA: 0x00002E8C File Offset: 0x0000108C
		public event EventHandler TransputsInitialized;

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002EC1 File Offset: 0x000010C1
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002EC9 File Offset: 0x000010C9
		public MechanicalNodeActuals Actuals { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002ED2 File Offset: 0x000010D2
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002EDA File Offset: 0x000010DA
		public IBattery Battery { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002EE3 File Offset: 0x000010E3
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002EEB File Offset: 0x000010EB
		public MechanicalGraph Graph { get; internal set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002EF4 File Offset: 0x000010F4
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002EFC File Offset: 0x000010FC
		public ImmutableArray<Transput> Transputs { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002F05 File Offset: 0x00001105
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002F0D File Offset: 0x0000110D
		public bool IsShaft { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002F16 File Offset: 0x00001116
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002F1E File Offset: 0x0000111E
		public bool IsGenerator { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002F27 File Offset: 0x00001127
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002F2F File Offset: 0x0000112F
		public bool IsConsumer { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002F38 File Offset: 0x00001138
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002F40 File Offset: 0x00001140
		public bool IsIntermediary { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002F49 File Offset: 0x00001149
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002F51 File Offset: 0x00001151
		public bool IgnoreRotation { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002F5A File Offset: 0x0000115A
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002F62 File Offset: 0x00001162
		public bool IsDetached { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002F6B File Offset: 0x0000116B
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002F73 File Offset: 0x00001173
		public int NominalBatteryCharge { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002F7C File Offset: 0x0000117C
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002F84 File Offset: 0x00001184
		public int NominalBatteryCapacity { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002F8D File Offset: 0x0000118D
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002F95 File Offset: 0x00001195
		public float OutputMultiplier { get; private set; } = 1f;

		// Token: 0x0600007C RID: 124 RVA: 0x00002F9E File Offset: 0x0000119E
		public MechanicalNode(MechanicalGraphManager mechanicalGraphManager, TransputMap transputMap)
		{
			this._mechanicalGraphManager = mechanicalGraphManager;
			this._transputMap = transputMap;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002FCA File Offset: 0x000011CA
		public bool IsBattery
		{
			get
			{
				return this.Battery != null;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002FD5 File Offset: 0x000011D5
		public float PowerEfficiency
		{
			get
			{
				MechanicalGraph graph = this.Graph;
				if (graph == null)
				{
					return 0f;
				}
				return graph.PowerEfficiency;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002FEC File Offset: 0x000011EC
		public bool Active
		{
			get
			{
				return this._blockableObject.IsUnblocked;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002FF9 File Offset: 0x000011F9
		public bool Powered
		{
			get
			{
				MechanicalGraph graph = this.Graph;
				return graph != null && graph.Powered;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000300C File Offset: 0x0000120C
		public bool ActiveAndPowered
		{
			get
			{
				return this.Active && this.Powered;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000301E File Offset: 0x0000121E
		public bool IsConsuming
		{
			get
			{
				return this.Actuals.PowerInput > 0;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000302E File Offset: 0x0000122E
		public float NominalBatteryChargeLevel
		{
			get
			{
				if (this.NominalBatteryCapacity <= 0)
				{
					return 0f;
				}
				return (float)this.NominalBatteryCharge / (float)this.NominalBatteryCapacity;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000304E File Offset: 0x0000124E
		public void Awake()
		{
			this._mechanicalNodeSpec = base.GetComponent<MechanicalNodeSpec>();
			this.Actuals = base.GetComponent<MechanicalNodeActuals>();
			this.Battery = base.GetComponent<IBattery>();
			this._blockableObject = base.GetComponent<BlockableObject>();
			this.InitializeConstantParameters();
			base.DisableComponent();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000308C File Offset: 0x0000128C
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this.InitializeTransputs();
			this._transputMap.AddNode(this);
			this.AddOrRemoveFromGraph();
			this.InitializeActuals();
			this._blockableObject.ObjectBlocked += this.OnBlockableObjectStateChanged;
			this._blockableObject.ObjectUnblocked += this.OnBlockableObjectStateChanged;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000030EC File Offset: 0x000012EC
		public void OnExitFinishedState()
		{
			this._blockableObject.ObjectBlocked -= this.OnBlockableObjectStateChanged;
			this._blockableObject.ObjectUnblocked -= this.OnBlockableObjectStateChanged;
			this.RemoveFromGraph();
			this._transputMap.RemoveNode(this);
			base.DisableComponent();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000313F File Offset: 0x0000133F
		public void OnEnterUnfinishedState()
		{
			this.InitializeTransputs();
			this._transputMap.AddNode(this);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003153 File Offset: 0x00001353
		public void OnExitUnfinishedState()
		{
			this._transputMap.RemoveNode(this);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003161 File Offset: 0x00001361
		public void SetDetached(bool value)
		{
			if (this.IsDetached != value)
			{
				this.IsDetached = value;
				this.AddOrRemoveFromGraph();
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000317C File Offset: 0x0000137C
		public void ReverseAllTransputs()
		{
			foreach (Transput transput in this.Transputs)
			{
				transput.ReverseRotation();
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000031B0 File Offset: 0x000013B0
		public void ResetAllTransputRotations()
		{
			foreach (Transput transput in this.Transputs)
			{
				transput.ResetRotation();
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000031E4 File Offset: 0x000013E4
		public void SetOutputMultiplier(float value)
		{
			if (!this.OutputMultiplier.Equals(value))
			{
				this.OutputMultiplier = value;
				this.UpdatePowerOutput();
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000320F File Offset: 0x0000140F
		public void SetInputMultiplier(float value)
		{
			if (!this._inputMultiplier.Equals(value))
			{
				this._inputMultiplier = value;
				this.UpdatePowerInput();
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000322C File Offset: 0x0000142C
		public void SetNominalBatteryCharge(int value)
		{
			if (!this.NominalBatteryCharge.Equals(value))
			{
				this.NominalBatteryCharge = value;
				this.UpdateBatteryCharge();
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003258 File Offset: 0x00001458
		public void SetNominalBatteryCapacity(int value)
		{
			if (!this.NominalBatteryCapacity.Equals(value))
			{
				this.NominalBatteryCapacity = value;
				this.UpdateBatteryCapacity();
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003283 File Offset: 0x00001483
		public IEnumerable<Transput> TransputsWithConnections()
		{
			foreach (Transput transput in this.Transputs)
			{
				if (transput.Connected)
				{
					yield return transput;
				}
			}
			ImmutableArray<Transput>.Enumerator enumerator = default(ImmutableArray<Transput>.Enumerator);
			yield break;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003294 File Offset: 0x00001494
		public bool CanPotentiallyBePowered()
		{
			MechanicalGraph graph = this.Graph;
			bool flag = graph != null && graph.NumberOfGenerators > 0;
			MechanicalGraph graph2 = this.Graph;
			bool flag2 = graph2 != null && graph2.BatteryCharge > 0;
			return flag || flag2;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000032CE File Offset: 0x000014CE
		public void OnBlockableObjectStateChanged(object sender, EventArgs e)
		{
			this.UpdateActiveState();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000032D8 File Offset: 0x000014D8
		public void InitializeConstantParameters()
		{
			if (this._mechanicalNodeSpec != null)
			{
				this.IsShaft = this._mechanicalNodeSpec.IsShaft;
				this.IsGenerator = (this._mechanicalNodeSpec.PowerOutput > 0);
				this.IsConsumer = (this._mechanicalNodeSpec.PowerInput > 0);
				this.IsIntermediary = (!this.IsShaft && !this.IsGenerator && !this.IsConsumer);
				this._nominalPowerInput = this._mechanicalNodeSpec.PowerInput;
				this._nominalPowerOutput = this._mechanicalNodeSpec.PowerOutput;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003370 File Offset: 0x00001570
		public void InitializeActuals()
		{
			if (this._mechanicalNodeSpec != null)
			{
				this.UpdatePowerOutput();
				this.UpdatePowerInput();
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000338C File Offset: 0x0000158C
		public void InitializeTransputs()
		{
			if (new ImmutableArray<Transput>?(this.Transputs) == null)
			{
				BlockObject component = base.GetComponent<BlockObject>();
				TransputProviderSpec component2 = base.GetComponent<TransputProviderSpec>();
				List<Transput> list = new List<Transput>();
				foreach (TransputSpec transputSpec in component2.Transputs)
				{
					foreach (Direction3D direction in transputSpec.Directions.GetEnumerator())
					{
						Transput item = new Transput(this, transputSpec, direction, component);
						list.Add(item);
					}
				}
				this.Transputs = list.ToImmutableArray<Transput>();
				this.IgnoreRotation = component2.IgnoreRotation;
				EventHandler transputsInitialized = this.TransputsInitialized;
				if (transputsInitialized == null)
				{
					return;
				}
				transputsInitialized(this, null);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000345A File Offset: 0x0000165A
		public void UpdateActiveState()
		{
			this.UpdatePowerOutput();
			this.UpdatePowerInput();
			this.UpdateBatteryCharge();
			this.UpdateBatteryCapacity();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003474 File Offset: 0x00001674
		public void UpdatePowerOutput()
		{
			if (this._mechanicalNodeSpec != null)
			{
				this.Actuals.SetPowerOutput(this.Active ? Mathf.CeilToInt((float)this._nominalPowerOutput * this.OutputMultiplier) : 0);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000034AD File Offset: 0x000016AD
		public void UpdatePowerInput()
		{
			if (this._mechanicalNodeSpec != null)
			{
				this.Actuals.SetPowerInput(this.Active ? Mathf.CeilToInt((float)this._nominalPowerInput * this._inputMultiplier) : 0);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000034E6 File Offset: 0x000016E6
		public void UpdateBatteryCharge()
		{
			this.Actuals.SetBatteryCharge(this.Active ? this.NominalBatteryCharge : 0);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003504 File Offset: 0x00001704
		public void UpdateBatteryCapacity()
		{
			this.Actuals.SetBatteryCapacity(this.Active ? this.NominalBatteryCapacity : 0);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003522 File Offset: 0x00001722
		public void AddOrRemoveFromGraph()
		{
			if (base.Enabled)
			{
				if (this.IsDetached)
				{
					this.RemoveFromGraph();
					return;
				}
				this.AddToGraph();
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003541 File Offset: 0x00001741
		public void AddToGraph()
		{
			if (!this._addedToGraph)
			{
				this._mechanicalGraphManager.AddNode(this);
				this._addedToGraph = true;
				EventHandler addedToGraph = this.AddedToGraph;
				if (addedToGraph == null)
				{
					return;
				}
				addedToGraph(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003574 File Offset: 0x00001774
		public void RemoveFromGraph()
		{
			if (this._addedToGraph)
			{
				this._mechanicalGraphManager.RemoveNode(this);
				this._addedToGraph = false;
			}
		}

		// Token: 0x04000039 RID: 57
		public readonly MechanicalGraphManager _mechanicalGraphManager;

		// Token: 0x0400003A RID: 58
		public readonly TransputMap _transputMap;

		// Token: 0x0400003B RID: 59
		public BlockableObject _blockableObject;

		// Token: 0x0400003C RID: 60
		public MechanicalNodeSpec _mechanicalNodeSpec;

		// Token: 0x0400003D RID: 61
		public int _nominalPowerInput;

		// Token: 0x0400003E RID: 62
		public int _nominalPowerOutput;

		// Token: 0x0400003F RID: 63
		public float _inputMultiplier = 1f;

		// Token: 0x04000040 RID: 64
		public bool _detached;

		// Token: 0x04000041 RID: 65
		public bool _addedToGraph;
	}
}
