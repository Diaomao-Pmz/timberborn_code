using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.Automation
{
	// Token: 0x0200000A RID: 10
	public class AutomationPartitioner
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000027BC File Offset: 0x000009BC
		public AutomationPartitioner(AutomatorPartitionFactory automatorPartitionFactory)
		{
			this._automatorPartitionFactory = automatorPartitionFactory;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027D8 File Offset: 0x000009D8
		public List<AutomatorPartition> AssignPartitions(ReadOnlyList<Automator> automators)
		{
			List<AutomatorPartition> list = new List<AutomatorPartition>();
			this.AssignPartitions(automators, list);
			return list;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000027F4 File Offset: 0x000009F4
		public void ReassignExistingPartition(AutomatorPartition partition, List<AutomatorPartition> partitions)
		{
			this.AssignPartitions(partition.Automators.AsReadOnlyList<Automator>(), partitions);
			partitions.Remove(partition);
			partition.Clear();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002818 File Offset: 0x00000A18
		public void MergePartitions(AutomatorPartition partitionA, AutomatorPartition partitionB, List<AutomatorPartition> partitions)
		{
			if (partitionA != partitionB)
			{
				AutomatorPartition automatorPartition = (partitionA.Size >= partitionB.Size) ? partitionA : partitionB;
				AutomatorPartition automatorPartition2 = (automatorPartition == partitionA) ? partitionB : partitionA;
				automatorPartition2.MergeInto(automatorPartition);
				partitions.Remove(automatorPartition2);
				automatorPartition2.Clear();
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000285C File Offset: 0x00000A5C
		public void AddAutomator(Automator automator, List<AutomatorPartition> partitions)
		{
			AutomatorPartition automatorPartition = this._automatorPartitionFactory.Create();
			automator.Partition = automatorPartition;
			automatorPartition.Add(automator);
			partitions.Add(automatorPartition);
			for (int i = 0; i < automator.InputConnections.Count; i++)
			{
				AutomatorConnection automatorConnection = automator.InputConnections[i];
				if (automatorConnection.IsConnected && automatorConnection.Transmitter.RegisteredForRunning)
				{
					AutomatorPartition partition = automatorConnection.Transmitter.Partition;
					if (partition != automator.Partition)
					{
						this.MergePartitions(partition, automator.Partition, partitions);
					}
				}
			}
			for (int j = 0; j < automator.OutputConnections.Count; j++)
			{
				AutomatorConnection automatorConnection2 = automator.OutputConnections[j];
				if (automatorConnection2.Receiver.RegisteredForRunning)
				{
					AutomatorPartition partition2 = automatorConnection2.Receiver.Partition;
					if (partition2 != automator.Partition)
					{
						this.MergePartitions(partition2, automator.Partition, partitions);
					}
				}
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002954 File Offset: 0x00000B54
		public void RemoveAutomator(Automator automator, List<AutomatorPartition> partitions)
		{
			AutomatorPartition partition = automator.Partition;
			automator.Partition = null;
			partition.Automators.Remove(automator);
			this.AssignPartitions(partition.Automators.AsReadOnlyList<Automator>(), partitions);
			partitions.Remove(partition);
			partition.Clear();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000299C File Offset: 0x00000B9C
		public void AssignPartitions(ReadOnlyList<Automator> automators, List<AutomatorPartition> partitions)
		{
			for (int i = 0; i < automators.Count; i++)
			{
				automators[i].Partition = null;
			}
			Asserts.CollectionIsEmpty<Automator>(this._queue, "_queue");
			for (int j = 0; j < automators.Count; j++)
			{
				Automator automator = automators[j];
				if (automator.Partition == null)
				{
					AutomatorPartition automatorPartition = this._automatorPartitionFactory.Create();
					partitions.Add(automatorPartition);
					automator.Partition = automatorPartition;
					this._queue.Enqueue(automator);
					Automator automator2;
					while (this._queue.TryDequeue(ref automator2))
					{
						automatorPartition.Add(automator2);
						this.EnqueueInputs(automator2, automatorPartition);
						this.EnqueueOutputs(automator2, automatorPartition);
					}
				}
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A50 File Offset: 0x00000C50
		public void EnqueueInputs(Automator current, AutomatorPartition partition)
		{
			ReadOnlyList<AutomatorConnection> inputConnections = current.InputConnections;
			for (int i = 0; i < inputConnections.Count; i++)
			{
				AutomatorConnection automatorConnection = inputConnections[i];
				if (automatorConnection.IsConnected)
				{
					Automator transmitter = automatorConnection.Transmitter;
					if (transmitter.RegisteredForRunning && transmitter.Partition == null)
					{
						transmitter.Partition = partition;
						this._queue.Enqueue(transmitter);
					}
				}
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public void EnqueueOutputs(Automator current, AutomatorPartition partition)
		{
			ReadOnlyList<AutomatorConnection> outputConnections = current.OutputConnections;
			for (int i = 0; i < outputConnections.Count; i++)
			{
				Automator receiver = outputConnections[i].Receiver;
				if (receiver.RegisteredForRunning && receiver.Partition == null)
				{
					receiver.Partition = partition;
					this._queue.Enqueue(receiver);
				}
			}
		}

		// Token: 0x0400001A RID: 26
		public readonly AutomatorPartitionFactory _automatorPartitionFactory;

		// Token: 0x0400001B RID: 27
		public readonly Queue<Automator> _queue = new Queue<Automator>();
	}
}
