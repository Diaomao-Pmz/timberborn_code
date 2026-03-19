using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using UnityEngine;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x0200000D RID: 13
	public class MechanicalGraph
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002584 File Offset: 0x00000784
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000258C File Offset: 0x0000078C
		public int PowerSupply { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002595 File Offset: 0x00000795
		// (set) Token: 0x0600002C RID: 44 RVA: 0x0000259D File Offset: 0x0000079D
		public int PowerDemand { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000025A6 File Offset: 0x000007A6
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000025AE File Offset: 0x000007AE
		public int BatteryCharge { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000025B7 File Offset: 0x000007B7
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000025BF File Offset: 0x000007BF
		public int BatteryCapacity { get; private set; }

		// Token: 0x06000031 RID: 49 RVA: 0x000025C8 File Offset: 0x000007C8
		public MechanicalGraph(EventBus eventBus, MechanicalGraphRegistry mechanicalGraphRegistry, IDayNightCycle dayNightCycle)
		{
			this._eventBus = eventBus;
			this._mechanicalGraphRegistry = mechanicalGraphRegistry;
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002606 File Offset: 0x00000806
		public IEnumerable<MechanicalNode> Nodes
		{
			get
			{
				return this._nodes.AsReadOnlyEnumerable<MechanicalNode>();
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002613 File Offset: 0x00000813
		public ReadOnlyList<MechanicalNode> Generators
		{
			get
			{
				return this._generators.AsReadOnlyList<MechanicalNode>();
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002620 File Offset: 0x00000820
		public ReadOnlyList<MechanicalNode> Batteries
		{
			get
			{
				return this._batteries.AsReadOnlyList<MechanicalNode>();
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000262D File Offset: 0x0000082D
		public int NumberOfGenerators
		{
			get
			{
				return this._generators.Count;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000263A File Offset: 0x0000083A
		public bool Powered
		{
			get
			{
				return this.PowerSupply > 0 || this.BatteryCharge > 0;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002650 File Offset: 0x00000850
		public int PowerSurplus
		{
			get
			{
				return this.PowerSupply - this.PowerDemand;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000265F File Offset: 0x0000085F
		public bool RequiresPower
		{
			get
			{
				return this.PowerDemand > 0 || this.BatteryCharge < this.BatteryCapacity;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000267A File Offset: 0x0000087A
		public bool Valid
		{
			get
			{
				MechanicalNode mechanicalNode = this._nodes.FirstOrDefault<MechanicalNode>();
				return ((mechanicalNode != null) ? mechanicalNode.Graph : null) == this;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002696 File Offset: 0x00000896
		public float BatteryChargeLevel
		{
			get
			{
				if (this.BatteryCapacity <= 0)
				{
					return 0f;
				}
				return (float)this.BatteryCharge / (float)this.BatteryCapacity;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000026B6 File Offset: 0x000008B6
		public float PowerEfficiency
		{
			get
			{
				if (this.PowerDemand <= 0)
				{
					return (float)((this.PowerSupply > 0) ? 1 : 0);
				}
				return Mathf.Min((float)(this.PowerSupply + this.BatteryPower) / (float)this.PowerDemand, 1f);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000026F0 File Offset: 0x000008F0
		public void AddNode(MechanicalNode mechanicalNode)
		{
			if (this._nodes.Add(mechanicalNode))
			{
				MechanicalGraph graph = mechanicalNode.Graph;
				if (graph != null)
				{
					graph.RemoveNode(mechanicalNode);
				}
				mechanicalNode.Graph = this;
				this.AddNodeToCounters(mechanicalNode);
				this._mechanicalGraphRegistry.AddGraph(mechanicalNode.Graph);
				if (mechanicalNode.IsGenerator)
				{
					this._generators.Add(mechanicalNode);
					this._eventBus.Post(new MechanicalGraphGeneratorAddedEvent(mechanicalNode.Graph));
				}
				if (mechanicalNode.IsBattery)
				{
					this._batteries.Add(mechanicalNode);
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000277C File Offset: 0x0000097C
		public void RemoveNode(MechanicalNode mechanicalNode)
		{
			if (this._nodes.Remove(mechanicalNode))
			{
				this.RemoveNodeFromCounters(mechanicalNode);
				this._mechanicalGraphRegistry.RemoveGraph(mechanicalNode.Graph);
				mechanicalNode.Graph = null;
				if (mechanicalNode.IsGenerator)
				{
					this._generators.Remove(mechanicalNode);
				}
				if (mechanicalNode.IsBattery)
				{
					this._batteries.Remove(mechanicalNode);
				}
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000027E0 File Offset: 0x000009E0
		public int BatteryPower
		{
			get
			{
				if (this.PowerSurplus >= 0)
				{
					return 0;
				}
				return Mathf.Min(-this.PowerSurplus, this.MaxBatteryPowerThisTick);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000027FF File Offset: 0x000009FF
		public int MaxBatteryPowerThisTick
		{
			get
			{
				return Mathf.CeilToInt((float)this.BatteryCharge / this._dayNightCycle.FixedDeltaTimeInHours);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000281C File Offset: 0x00000A1C
		public void AddNodeToCounters(MechanicalNode mechanicalNode)
		{
			MechanicalNodeActuals actuals = mechanicalNode.Actuals;
			this.PowerSupply += actuals.PowerOutput;
			this.PowerDemand += actuals.PowerInput;
			this.BatteryCharge += actuals.BatteryCharge;
			this.BatteryCapacity += actuals.BatteryCapacity;
			actuals.PowerOutputChanged += this.OnNodePowerOutputChanged;
			actuals.PowerInputChanged += this.OnNodePowerInputChanged;
			actuals.BatteryChargeChanged += this.OnNodeBatteryChargeChanged;
			actuals.BatteryCapacityChanged += this.OnNodeBatteryCapacityChanged;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000028C4 File Offset: 0x00000AC4
		public void RemoveNodeFromCounters(MechanicalNode mechanicalNode)
		{
			MechanicalNodeActuals actuals = mechanicalNode.Actuals;
			this.PowerSupply -= actuals.PowerOutput;
			this.PowerDemand -= actuals.PowerInput;
			this.BatteryCharge -= actuals.BatteryCharge;
			this.BatteryCapacity -= actuals.BatteryCapacity;
			actuals.PowerOutputChanged -= this.OnNodePowerOutputChanged;
			actuals.PowerInputChanged -= this.OnNodePowerInputChanged;
			actuals.BatteryChargeChanged -= this.OnNodeBatteryChargeChanged;
			actuals.BatteryCapacityChanged -= this.OnNodeBatteryCapacityChanged;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000296C File Offset: 0x00000B6C
		public void OnNodePowerOutputChanged(object sender, ValueChangedEventArgs<int> e)
		{
			this.PowerSupply += e.NewValue - e.OldValue;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000298A File Offset: 0x00000B8A
		public void OnNodePowerInputChanged(object sender, ValueChangedEventArgs<int> e)
		{
			this.PowerDemand += e.NewValue - e.OldValue;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000029A8 File Offset: 0x00000BA8
		public void OnNodeBatteryChargeChanged(object sender, ValueChangedEventArgs<int> e)
		{
			this.BatteryCharge += e.NewValue - e.OldValue;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029C6 File Offset: 0x00000BC6
		public void OnNodeBatteryCapacityChanged(object sender, ValueChangedEventArgs<int> e)
		{
			this.BatteryCapacity += e.NewValue - e.OldValue;
		}

		// Token: 0x04000015 RID: 21
		public readonly EventBus _eventBus;

		// Token: 0x04000016 RID: 22
		public readonly MechanicalGraphRegistry _mechanicalGraphRegistry;

		// Token: 0x04000017 RID: 23
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000018 RID: 24
		public readonly HashSet<MechanicalNode> _nodes = new HashSet<MechanicalNode>();

		// Token: 0x04000019 RID: 25
		public readonly List<MechanicalNode> _generators = new List<MechanicalNode>();

		// Token: 0x0400001A RID: 26
		public readonly List<MechanicalNode> _batteries = new List<MechanicalNode>();
	}
}
