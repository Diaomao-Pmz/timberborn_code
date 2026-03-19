using System;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000011 RID: 17
	public class BuildCampfireAchievement : BuildAchievement
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002BD5 File Offset: 0x00000DD5
		public BuildCampfireAchievement(EventBus eventBus, EntityComponentRegistry entityComponentRegistry) : base(eventBus, entityComponentRegistry, "BUILD_CAMPFIRE", "Campfire.")
		{
		}
	}
}
