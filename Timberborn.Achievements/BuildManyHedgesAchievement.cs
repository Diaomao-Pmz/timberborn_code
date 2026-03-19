using System;
using System.Linq;
using Timberborn.AchievementSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000019 RID: 25
	public class BuildManyHedgesAchievement : Achievement
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002F64 File Offset: 0x00001164
		public BuildManyHedgesAchievement(EventBus eventBus, EntityComponentRegistry entityComponentRegistry)
		{
			this._eventBus = eventBus;
			this._entityComponentRegistry = entityComponentRegistry;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002F7A File Offset: 0x0000117A
		public override string Id
		{
			get
			{
				return "BUILD_MANY_HEDGES";
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002F84 File Offset: 0x00001184
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			TemplateSpec component = enteredFinishedStateEvent.BlockObject.GetComponent<TemplateSpec>();
			if (component != null && component.TemplateName == BuildManyHedgesAchievement.TemplateName)
			{
				int num = this._hedgeCount + 1;
				this._hedgeCount = num;
				if (num >= BuildManyHedgesAchievement.HedgesRequired)
				{
					base.Unlock();
				}
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002FD8 File Offset: 0x000011D8
		[OnEvent]
		public void OnExitedFinishedState(ExitedFinishedStateEvent exitedFinishedStateEvent)
		{
			TemplateSpec component = exitedFinishedStateEvent.BlockObject.GetComponent<TemplateSpec>();
			if (component != null && component.TemplateName == BuildManyHedgesAchievement.TemplateName)
			{
				this._hedgeCount--;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000301A File Offset: 0x0000121A
		public override void EnableInternal()
		{
			this._eventBus.Register(this);
			this.ValidateInitialHedges();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000302E File Offset: 0x0000122E
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000303C File Offset: 0x0000123C
		public void ValidateInitialHedges()
		{
			this._hedgeCount = (from spec in this._entityComponentRegistry.GetEnabled<Building>()
			where spec.GetComponent<TemplateSpec>().TemplateName == BuildManyHedgesAchievement.TemplateName
			select spec).Count((Building spec) => spec.GetComponent<BlockObject>().IsFinished);
			if (this._hedgeCount >= BuildManyHedgesAchievement.HedgesRequired)
			{
				base.Unlock();
			}
		}

		// Token: 0x04000034 RID: 52
		public static readonly int HedgesRequired = 200;

		// Token: 0x04000035 RID: 53
		public static readonly string TemplateName = "Hedge.Folktails";

		// Token: 0x04000036 RID: 54
		public readonly EventBus _eventBus;

		// Token: 0x04000037 RID: 55
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x04000038 RID: 56
		public int _hedgeCount;
	}
}
