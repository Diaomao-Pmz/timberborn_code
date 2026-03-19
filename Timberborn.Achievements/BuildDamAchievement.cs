using System;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000012 RID: 18
	public class BuildDamAchievement : BuildAchievement
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002BE9 File Offset: 0x00000DE9
		public BuildDamAchievement(EventBus eventBus, EntityComponentRegistry entityComponentRegistry) : base(eventBus, entityComponentRegistry, "BUILD_DAM", "Dam.")
		{
		}
	}
}
