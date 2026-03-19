using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Timberborn.Automation
{
	// Token: 0x02000013 RID: 19
	public class AutomatorPartition
	{
		// Token: 0x060000AE RID: 174 RVA: 0x000040FB File Offset: 0x000022FB
		public AutomatorPartition(AutomationPlan automationPlan, AutomationDebugger automationDebugger)
		{
			this._automationPlan = automationPlan;
			this._automationDebugger = automationDebugger;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004127 File Offset: 0x00002327
		public int Size
		{
			get
			{
				return this.Automators.Count;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004134 File Offset: 0x00002334
		public string DebuggingId
		{
			get
			{
				return string.Format("{0:x8}", this.GetHashCode());
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000414B File Offset: 0x0000234B
		public bool IsSampling
		{
			get
			{
				return this._automationPlan.IsSampling;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004158 File Offset: 0x00002358
		public bool IsSequential
		{
			get
			{
				return this._automationPlan.IsSequential;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004165 File Offset: 0x00002365
		public void Clear()
		{
			this.Automators.Clear();
			this._postponedAutomatorListeners.Clear();
			this.InvalidatePlan();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004183 File Offset: 0x00002383
		public void EvaluateCombinational()
		{
			if (!this._evaluating)
			{
				this._evaluating = true;
				this.UpdatePlan();
				this._automationPlan.EvaluateCombinational();
				this.EvaluatePostponedAutomatorListeners();
				this._evaluating = false;
				this.IsScheduled = false;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000041B9 File Offset: 0x000023B9
		public void EvaluateNext()
		{
			this._automationPlan.EvaluateNext();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000041C6 File Offset: 0x000023C6
		public void CommitTick()
		{
			this._automationPlan.CommitTick();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000041D3 File Offset: 0x000023D3
		public void Sample()
		{
			this._automationPlan.Sample();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000041E0 File Offset: 0x000023E0
		public void EvaluateTerminal()
		{
			this._automationPlan.EvaluateTerminal();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000041ED File Offset: 0x000023ED
		public void Add(Automator automator)
		{
			this.Automators.Add(automator);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000041FC File Offset: 0x000023FC
		public void MergeInto(AutomatorPartition destination)
		{
			for (int i = 0; i < this.Automators.Count; i++)
			{
				Automator automator = this.Automators[i];
				automator.Partition = destination;
				destination.Add(automator);
			}
			destination.InvalidatePlan();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004240 File Offset: 0x00002440
		public void NotifyOrPostponeAutomatorListeners(Automator automator)
		{
			if (this._evaluating)
			{
				if (!automator.PostponedNotifyListeners)
				{
					automator.PostponedNotifyListeners = true;
					this._postponedAutomatorListeners.Enqueue(automator);
					return;
				}
			}
			else
			{
				automator.NotifyListenersNow();
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000426C File Offset: 0x0000246C
		public void UpdatePlan()
		{
			if (!this._planReady)
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				this._automationPlan.Build(this.Automators);
				this._planReady = true;
				this._automationDebugger.PlanningTimeMs.Register(stopwatch);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000042B0 File Offset: 0x000024B0
		public void InvalidatePlan()
		{
			this._automationPlan.Clear();
			this._planReady = false;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000042C4 File Offset: 0x000024C4
		public ImmutableArray<Automator> GetPlanSnapshot()
		{
			return this._automationPlan.GetSnapshot();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000042D4 File Offset: 0x000024D4
		public void EvaluatePostponedAutomatorListeners()
		{
			Automator automator;
			while (this._postponedAutomatorListeners.TryDequeue(ref automator))
			{
				automator.PostponedNotifyListeners = false;
				automator.NotifyListenersNow();
			}
		}

		// Token: 0x04000054 RID: 84
		public readonly List<Automator> Automators = new List<Automator>();

		// Token: 0x04000055 RID: 85
		public bool IsScheduled;

		// Token: 0x04000056 RID: 86
		public readonly AutomationPlan _automationPlan;

		// Token: 0x04000057 RID: 87
		public readonly AutomationDebugger _automationDebugger;

		// Token: 0x04000058 RID: 88
		public readonly Queue<Automator> _postponedAutomatorListeners = new Queue<Automator>();

		// Token: 0x04000059 RID: 89
		public bool _planReady;

		// Token: 0x0400005A RID: 90
		public bool _evaluating;
	}
}
