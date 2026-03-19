using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.EnterableSystem;
using Timberborn.Navigation;
using Timberborn.WalkingSystem;
using Timberborn.WorkSystem;

namespace Timberborn.ConstructionSites
{
	// Token: 0x02000007 RID: 7
	public class BuildBehavior : Behavior, IAwakableComponent, IStartableComponent, IJobBehavior
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public void Awake()
		{
			this._builder = base.GetComponent<Builder>();
			this._behaviorAgent = base.GetComponent<BehaviorAgent>();
			this._enterer = base.GetComponent<Enterer>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002126 File Offset: 0x00000326
		public void Start()
		{
			this._walkToAccessibleExecutor = base.GetComponent<WalkToAccessibleExecutor>();
			this._buildExecutor = base.GetComponent<BuildExecutor>();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002140 File Offset: 0x00000340
		public Decision StartBuilding(ConstructionSite constructionSite)
		{
			this._builder.Reserve(constructionSite);
			return this.Decide(this._behaviorAgent);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000215C File Offset: 0x0000035C
		public override Decision Decide(BehaviorAgent agent)
		{
			if (!this._builder.HasReservedConstructionSite)
			{
				return Decision.ReleaseNow();
			}
			ConstructionSite reservedConstructionSite = this._builder.ReservedConstructionSite;
			Accessible enabledComponent = reservedConstructionSite.GetEnabledComponent<Accessible>();
			Enterable enabledComponent2 = reservedConstructionSite.GetEnabledComponent<Enterable>();
			if (enabledComponent2 && this._enterer.CurrentBuilding == enabledComponent2)
			{
				return this.BeginConstruction(reservedConstructionSite);
			}
			switch (this._walkToAccessibleExecutor.Launch(enabledComponent))
			{
			case ExecutorStatus.Success:
				if (enabledComponent2 && enabledComponent2.CanEnter)
				{
					this._enterer.Enter(enabledComponent2);
				}
				return this.BeginConstruction(reservedConstructionSite);
			case ExecutorStatus.Failure:
				this._builder.Unreserve();
				return Decision.ReleaseNextTick();
			case ExecutorStatus.Running:
				return Decision.ReturnWhenFinished(this._walkToAccessibleExecutor);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000221E File Offset: 0x0000041E
		public Decision BeginConstruction(ConstructionSite constructionSite)
		{
			if (this._buildExecutor.Launch(constructionSite))
			{
				return Decision.ReleaseWhenFinished(this._buildExecutor);
			}
			this._builder.Unreserve();
			return Decision.ReleaseNextTick();
		}

		// Token: 0x04000008 RID: 8
		public Builder _builder;

		// Token: 0x04000009 RID: 9
		public WalkToAccessibleExecutor _walkToAccessibleExecutor;

		// Token: 0x0400000A RID: 10
		public BuildExecutor _buildExecutor;

		// Token: 0x0400000B RID: 11
		public BehaviorAgent _behaviorAgent;

		// Token: 0x0400000C RID: 12
		public Enterer _enterer;
	}
}
