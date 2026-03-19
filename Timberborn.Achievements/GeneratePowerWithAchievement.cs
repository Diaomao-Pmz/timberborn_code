using System;
using System.Collections.Generic;
using Timberborn.AchievementSystem;
using Timberborn.MechanicalSystem;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000028 RID: 40
	public abstract class GeneratePowerWithAchievement<T> : Achievement, ITickableSingleton
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003958 File Offset: 0x00001B58
		public override string Id { get; }

		// Token: 0x060000AA RID: 170 RVA: 0x00003960 File Offset: 0x00001B60
		public GeneratePowerWithAchievement(MechanicalGraphRegistry mechanicalGraphRegistry, EventBus eventBus, string id, int requiredPower)
		{
			this.Id = id;
			this._mechanicalGraphRegistry = mechanicalGraphRegistry;
			this._eventBus = eventBus;
			this._requiredPower = requiredPower;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003990 File Offset: 0x00001B90
		public void Tick()
		{
			if (base.IsEnabled && this.IsAnyCandidateProperlyPowered())
			{
				base.Unlock();
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000039A8 File Offset: 0x00001BA8
		[OnEvent]
		public void OnMechanicalGraphCreated(MechanicalGraphCreatedEvent mechanicalGraphCreatedEvent)
		{
			this.FindGraphCandidates();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000039A8 File Offset: 0x00001BA8
		[OnEvent]
		public void OnMechanicalGraphRemoved(MechanicalGraphRemovedEvent mechanicalGraphRemovedEvent)
		{
			this.FindGraphCandidates();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000039A8 File Offset: 0x00001BA8
		[OnEvent]
		public void OnMechanicalGraphGeneratorAdded(MechanicalGraphGeneratorAddedEvent mechanicalGraphGeneratorAddedEvent)
		{
			this.FindGraphCandidates();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000039B0 File Offset: 0x00001BB0
		public override void EnableInternal()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000039BE File Offset: 0x00001BBE
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
			this._graphCandidates.Clear();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000039D8 File Offset: 0x00001BD8
		public void FindGraphCandidates()
		{
			this._graphCandidates.Clear();
			foreach (MechanicalGraph mechanicalGraph in this._mechanicalGraphRegistry.MechanicalGraphs)
			{
				if (GeneratePowerWithAchievement<T>.IsValidGraphCandidate(mechanicalGraph))
				{
					this._graphCandidates.Add(mechanicalGraph);
				}
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003A4C File Offset: 0x00001C4C
		public static bool IsValidGraphCandidate(MechanicalGraph candidateGraph)
		{
			using (List<MechanicalNode>.Enumerator enumerator = candidateGraph.Generators.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetComponent<T>() == null)
					{
						return false;
					}
				}
			}
			return candidateGraph.NumberOfGenerators > 0;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public bool IsAnyCandidateProperlyPowered()
		{
			foreach (MechanicalGraph mechanicalGraph in this._graphCandidates)
			{
				if (mechanicalGraph != null && mechanicalGraph.PowerSupply >= this._requiredPower)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400005C RID: 92
		public readonly MechanicalGraphRegistry _mechanicalGraphRegistry;

		// Token: 0x0400005D RID: 93
		public readonly EventBus _eventBus;

		// Token: 0x0400005E RID: 94
		public readonly List<MechanicalGraph> _graphCandidates = new List<MechanicalGraph>();

		// Token: 0x0400005F RID: 95
		public readonly int _requiredPower;
	}
}
