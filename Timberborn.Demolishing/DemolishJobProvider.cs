using System;
using Timberborn.BehaviorSystem;
using Timberborn.BuilderHubSystem;
using Timberborn.Navigation;
using Timberborn.PrioritySystem;

namespace Timberborn.Demolishing
{
	// Token: 0x0200001C RID: 28
	public class DemolishJobProvider : IBuilderJobProvider
	{
		// Token: 0x060000CA RID: 202 RVA: 0x000037DF File Offset: 0x000019DF
		public DemolishJobProvider(DemolishJobs demolishJobs)
		{
			this._demolishJobs = demolishJobs;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000037EE File Offset: 0x000019EE
		public int ProviderPriority
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000037F4 File Offset: 0x000019F4
		public ValueTuple<Behavior, Decision> GetJob(Accessible start, BehaviorAgent agent, Priority priority)
		{
			Demolisher component = agent.GetComponent<Demolisher>();
			DemolishJob demolishJob = null;
			float num = float.MaxValue;
			foreach (DemolishJob demolishJob2 in this._demolishJobs.GetJobs(priority))
			{
				float num2;
				if (demolishJob2.CanStartJob(component) && DemolishJobProvider.IsReachable(demolishJob2, start, out num2) && num2 < num)
				{
					demolishJob = demolishJob2;
					num = num2;
				}
			}
			if (demolishJob)
			{
				ValueTuple<Behavior, Decision> valueTuple = demolishJob.StartBuilderJob(component);
				Behavior item = valueTuple.Item1;
				Decision item2 = valueTuple.Item2;
				if (!item2.ShouldReleaseNow)
				{
					return new ValueTuple<Behavior, Decision>(item, item2);
				}
			}
			return new ValueTuple<Behavior, Decision>(null, Decision.ReleaseNow());
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000038B8 File Offset: 0x00001AB8
		public static bool IsReachable(DemolishJob job, Accessible start, out float distance)
		{
			return job.GetComponent<ReachableDemolishable>().IsReachable(start, out distance);
		}

		// Token: 0x04000040 RID: 64
		public readonly DemolishJobs _demolishJobs;
	}
}
