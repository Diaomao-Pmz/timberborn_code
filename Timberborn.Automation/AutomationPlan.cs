using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Common;

namespace Timberborn.Automation
{
	// Token: 0x0200000B RID: 11
	public class AutomationPlan
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002B0C File Offset: 0x00000D0C
		public AutomationPlan(AutomationPlanVersioner automationPlanVersioner)
		{
			this._automationPlanVersioner = automationPlanVersioner;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002B5D File Offset: 0x00000D5D
		public bool IsSampling
		{
			get
			{
				return !this._samplingPlan.IsEmpty<Automator>();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002B6D File Offset: 0x00000D6D
		public bool IsSequential
		{
			get
			{
				return !this._sequentialPlan.IsEmpty<Automator>();
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002B7D File Offset: 0x00000D7D
		public void Clear()
		{
			this._combinationalPlan.Clear();
			this._samplingPlan.Clear();
			this._sequentialPlan.Clear();
			this._terminalPlan.Clear();
			this._queue.Clear();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public void Build(List<Automator> all)
		{
			this.Clear();
			long num = this._automationPlanVersioner.AcquirePlanVersion();
			for (int i = 0; i < all.Count; i++)
			{
				all[i].Indegree = 0;
			}
			for (int j = 0; j < all.Count; j++)
			{
				Automator automator = all[j];
				if (automator.IsCombinationalTransmitter)
				{
					for (int k = 0; k < automator.InputConnections.Count; k++)
					{
						AutomatorConnection automatorConnection = automator.InputConnections[k];
						if (automatorConnection.IsConnected && automatorConnection.Transmitter.RegisteredForRunning && automatorConnection.Transmitter.IsCombinationalTransmitter)
						{
							automator.Indegree++;
						}
					}
				}
			}
			for (int l = 0; l < all.Count; l++)
			{
				Automator automator2 = all[l];
				if (automator2.IsCombinationalTransmitter && automator2.Indegree == 0)
				{
					this._queue.Enqueue(automator2);
				}
			}
			Automator automator3;
			while (this._queue.TryDequeue(ref automator3))
			{
				this._combinationalPlan.Add(automator3);
				automator3.PlanVersion = num;
				for (int m = 0; m < automator3.OutputConnections.Count; m++)
				{
					Automator receiver = automator3.OutputConnections[m].Receiver;
					if (receiver.IsCombinationalTransmitter)
					{
						receiver.Indegree--;
						if (receiver.Indegree == 0)
						{
							this._queue.Enqueue(receiver);
						}
					}
				}
			}
			for (int n = 0; n < all.Count; n++)
			{
				Automator automator4 = all[n];
				bool flag = automator4.IsCombinationalTransmitter && automator4.PlanVersion != num;
				automator4.SetCyclicOrBlocked(flag);
				if (flag)
				{
					this._combinationalPlan.Add(automator4);
				}
			}
			for (int num2 = 0; num2 < all.Count; num2++)
			{
				Automator automator5 = all[num2];
				if (automator5.IsSamplingTransmitter)
				{
					this._samplingPlan.Add(automator5);
				}
			}
			for (int num3 = 0; num3 < all.Count; num3++)
			{
				Automator automator6 = all[num3];
				if (automator6.IsSequentialTransmitter)
				{
					this._sequentialPlan.Add(automator6);
				}
			}
			for (int num4 = 0; num4 < all.Count; num4++)
			{
				Automator automator7 = all[num4];
				if (automator7.IsTerminal)
				{
					this._terminalPlan.Add(automator7);
				}
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002E40 File Offset: 0x00001040
		public void Sample()
		{
			for (int i = 0; i < this._samplingPlan.Count; i++)
			{
				this._samplingPlan[i].Sample();
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002E74 File Offset: 0x00001074
		public void EvaluateCombinational()
		{
			for (int i = 0; i < this._combinationalPlan.Count; i++)
			{
				this._combinationalPlan[i].EvaluateCombinational();
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002EA8 File Offset: 0x000010A8
		public void EvaluateNext()
		{
			for (int i = 0; i < this._sequentialPlan.Count; i++)
			{
				this._sequentialPlan[i].EvaluateNext();
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002EDC File Offset: 0x000010DC
		public void CommitTick()
		{
			for (int i = 0; i < this._sequentialPlan.Count; i++)
			{
				this._sequentialPlan[i].CommitTick();
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F10 File Offset: 0x00001110
		public void EvaluateTerminal()
		{
			for (int i = 0; i < this._terminalPlan.Count; i++)
			{
				this._terminalPlan[i].EvaluateTerminal();
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002F44 File Offset: 0x00001144
		public ImmutableArray<Automator> GetSnapshot()
		{
			return this._combinationalPlan.ToImmutableArray<Automator>();
		}

		// Token: 0x0400001C RID: 28
		public readonly AutomationPlanVersioner _automationPlanVersioner;

		// Token: 0x0400001D RID: 29
		public readonly Queue<Automator> _queue = new Queue<Automator>();

		// Token: 0x0400001E RID: 30
		public readonly List<Automator> _combinationalPlan = new List<Automator>();

		// Token: 0x0400001F RID: 31
		public readonly List<Automator> _samplingPlan = new List<Automator>();

		// Token: 0x04000020 RID: 32
		public readonly List<Automator> _sequentialPlan = new List<Automator>();

		// Token: 0x04000021 RID: 33
		public readonly List<Automator> _terminalPlan = new List<Automator>();
	}
}
