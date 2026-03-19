using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;

namespace Timberborn.Automation
{
	// Token: 0x0200000E RID: 14
	public class AutomationRunner : IPostLoadableSingleton, IUpdatableSingleton, ITickableSingleton, ILateTickable, IAutomationRunnerDebugger
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00003034 File Offset: 0x00001234
		public AutomationRunner(AutomationPartitioner automationPartitioner, AutomationDebugger automationDebugger, ISingletonRepository singletonRepository, AutomatorRegistry automatorRegistry)
		{
			this._automationPartitioner = automationPartitioner;
			this._automationDebugger = automationDebugger;
			this._singletonRepository = singletonRepository;
			this._automatorRegistry = automatorRegistry;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003085 File Offset: 0x00001285
		public int PartitionCount
		{
			get
			{
				List<AutomatorPartition> partitions = this._partitions;
				if (partitions == null)
				{
					return 0;
				}
				return partitions.Count;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003098 File Offset: 0x00001298
		public void PostLoad()
		{
			this.CollectSingletons();
			this.AssignPartitions();
			this.SampleSingletons();
			this.SamplePartitions();
			this.ScheduleAllPartitions();
			this._loaded = true;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000030C0 File Offset: 0x000012C0
		public void Tick()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			this.EvaluateScheduled(false);
			this.EvaluateNextPartitions();
			this.CommitTickSingletons();
			this.CommitTickPartitions();
			this.SampleSingletons();
			this.SamplePartitions();
			this.EvaluateScheduled(false);
			this._automationDebugger.TickEvaluationTimeMs.Register(stopwatch);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003110 File Offset: 0x00001310
		public void UpdateSingleton()
		{
			this.EvaluateScheduled(true);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000311C File Offset: 0x0000131C
		public void Register(Automator automator)
		{
			this._automatorRegistry.Register(automator);
			if (this._loaded)
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				this._automationPartitioner.AddAutomator(automator, this._partitions);
				this.UpdateSecondaryPartitionLists();
				this._automationDebugger.AddingTimeMs.Register(stopwatch);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000316C File Offset: 0x0000136C
		public void Unregister(Automator automator)
		{
			this._automatorRegistry.Unregister(automator);
			if (this._loaded)
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				this._automationPartitioner.RemoveAutomator(automator, this._partitions);
				this.UpdateSecondaryPartitionLists();
				this._automationDebugger.RemovingTimeMs.Register(stopwatch);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000031BC File Offset: 0x000013BC
		public void ReassignExistingPartition(AutomatorPartition partition)
		{
			if (this._loaded)
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				this._automationPartitioner.ReassignExistingPartition(partition, this._partitions);
				this.UpdateSecondaryPartitionLists();
				this._automationDebugger.PartitioningTimeMs.Register(stopwatch);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003200 File Offset: 0x00001400
		public void MergePartitions(AutomatorPartition partitionA, AutomatorPartition partitionB)
		{
			if (this._loaded)
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				this._automationPartitioner.MergePartitions(partitionA, partitionB, this._partitions);
				this.UpdateSecondaryPartitionLists();
				this._automationDebugger.MergingTimeMs.Register(stopwatch);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003245 File Offset: 0x00001445
		public ImmutableArray<AutomatorPartition> GetPartitionsSnapshot()
		{
			return this._partitions.ToImmutableArray<AutomatorPartition>();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003252 File Offset: 0x00001452
		public void Schedule(AutomatorPartition automatorPartition)
		{
			if (!automatorPartition.IsScheduled)
			{
				this._scheduledPartitions.Add(automatorPartition);
				automatorPartition.IsScheduled = true;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000326F File Offset: 0x0000146F
		public void CollectSingletons()
		{
			this._samplingSingletons = this._singletonRepository.GetSingletons<ISamplingSingleton>().ToImmutableArray<ISamplingSingleton>();
			this._committingSingletons = this._singletonRepository.GetSingletons<ICommittingSingleton>().ToImmutableArray<ICommittingSingleton>();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000032A0 File Offset: 0x000014A0
		public void AssignPartitions()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			this._partitions = this._automationPartitioner.AssignPartitions(this._automatorRegistry.Automators);
			this.UpdateSecondaryPartitionLists();
			this._automationDebugger.PartitioningTimeMs.Register(stopwatch);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000032E8 File Offset: 0x000014E8
		public void ScheduleAllPartitions()
		{
			for (int i = 0; i < this._partitions.Count; i++)
			{
				this.Schedule(this._partitions[i]);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003320 File Offset: 0x00001520
		public void EvaluateScheduled(bool evaluateNext)
		{
			if (!this._scheduledPartitions.IsEmpty<AutomatorPartition>())
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				for (int i = 0; i < this._scheduledPartitions.Count; i++)
				{
					AutomatorPartition automatorPartition = this._scheduledPartitions[i];
					automatorPartition.EvaluateCombinational();
					automatorPartition.EvaluateTerminal();
					if (evaluateNext)
					{
						automatorPartition.EvaluateNext();
					}
				}
				this._scheduledPartitions.Clear();
				this._automationDebugger.EvaluationTimeMs.Register(stopwatch);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003394 File Offset: 0x00001594
		public void SamplePartitions()
		{
			for (int i = 0; i < this._samplingPartitions.Count; i++)
			{
				this._samplingPartitions[i].Sample();
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000033C8 File Offset: 0x000015C8
		public void EvaluateNextPartitions()
		{
			for (int i = 0; i < this._sequentialPartitions.Count; i++)
			{
				this._sequentialPartitions[i].EvaluateNext();
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000033FC File Offset: 0x000015FC
		public void CommitTickPartitions()
		{
			for (int i = 0; i < this._sequentialPartitions.Count; i++)
			{
				this._sequentialPartitions[i].CommitTick();
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003430 File Offset: 0x00001630
		public void SampleSingletons()
		{
			for (int i = 0; i < this._samplingSingletons.Length; i++)
			{
				this._samplingSingletons[i].Sample();
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003464 File Offset: 0x00001664
		public void CommitTickSingletons()
		{
			for (int i = 0; i < this._committingSingletons.Length; i++)
			{
				this._committingSingletons[i].CommitTick();
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003498 File Offset: 0x00001698
		public void UpdateSecondaryPartitionLists()
		{
			this.UpdateAllPlans();
			this.UpdateSamplingPartitions();
			this.UpdateSequentialPartitions();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000034AC File Offset: 0x000016AC
		public void UpdateAllPlans()
		{
			for (int i = 0; i < this._partitions.Count; i++)
			{
				this._partitions[i].UpdatePlan();
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000034E0 File Offset: 0x000016E0
		public void UpdateSamplingPartitions()
		{
			this._samplingPartitions.Clear();
			for (int i = 0; i < this._partitions.Count; i++)
			{
				AutomatorPartition automatorPartition = this._partitions[i];
				if (automatorPartition.IsSampling)
				{
					this._samplingPartitions.Add(automatorPartition);
				}
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003530 File Offset: 0x00001730
		public void UpdateSequentialPartitions()
		{
			this._sequentialPartitions.Clear();
			for (int i = 0; i < this._partitions.Count; i++)
			{
				AutomatorPartition automatorPartition = this._partitions[i];
				if (automatorPartition.IsSequential)
				{
					this._sequentialPartitions.Add(automatorPartition);
				}
			}
		}

		// Token: 0x04000024 RID: 36
		public readonly AutomationPartitioner _automationPartitioner;

		// Token: 0x04000025 RID: 37
		public readonly AutomationDebugger _automationDebugger;

		// Token: 0x04000026 RID: 38
		public readonly ISingletonRepository _singletonRepository;

		// Token: 0x04000027 RID: 39
		public readonly AutomatorRegistry _automatorRegistry;

		// Token: 0x04000028 RID: 40
		public readonly List<AutomatorPartition> _scheduledPartitions = new List<AutomatorPartition>();

		// Token: 0x04000029 RID: 41
		public readonly List<AutomatorPartition> _samplingPartitions = new List<AutomatorPartition>();

		// Token: 0x0400002A RID: 42
		public readonly List<AutomatorPartition> _sequentialPartitions = new List<AutomatorPartition>();

		// Token: 0x0400002B RID: 43
		public ImmutableArray<ISamplingSingleton> _samplingSingletons;

		// Token: 0x0400002C RID: 44
		public ImmutableArray<ICommittingSingleton> _committingSingletons;

		// Token: 0x0400002D RID: 45
		public List<AutomatorPartition> _partitions;

		// Token: 0x0400002E RID: 46
		public bool _loaded;
	}
}
