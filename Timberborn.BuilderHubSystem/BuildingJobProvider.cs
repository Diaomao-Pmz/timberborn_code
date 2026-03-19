using System;
using Timberborn.BehaviorSystem;
using Timberborn.ConstructionSites;
using Timberborn.Navigation;
using Timberborn.PrioritySystem;

namespace Timberborn.BuilderHubSystem
{
	// Token: 0x0200000B RID: 11
	public class BuildingJobProvider : IBuilderJobProvider
	{
		// Token: 0x0600001D RID: 29 RVA: 0x0000233F File Offset: 0x0000053F
		public BuildingJobProvider(ConstructionRegistry constructionRegistry)
		{
			this._constructionRegistry = constructionRegistry;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000234E File Offset: 0x0000054E
		public int ProviderPriority
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002354 File Offset: 0x00000554
		public ValueTuple<Behavior, Decision> GetJob(Accessible start, BehaviorAgent agent, Priority priority)
		{
			foreach (ConstructionJob constructionJob in this._constructionRegistry.GetJobs(priority))
			{
				ValueTuple<Behavior, Decision> valueTuple = constructionJob.StartConstructionJob(agent, start);
				Behavior item = valueTuple.Item1;
				Decision item2 = valueTuple.Item2;
				if (!item2.ShouldReleaseNow)
				{
					return new ValueTuple<Behavior, Decision>(item, item2);
				}
			}
			return new ValueTuple<Behavior, Decision>(null, Decision.ReleaseNow());
		}

		// Token: 0x0400000C RID: 12
		public readonly ConstructionRegistry _constructionRegistry;
	}
}
