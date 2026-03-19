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
	// Token: 0x0200000F RID: 15
	public abstract class BuildAchievement : Achievement
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002ACB File Offset: 0x00000CCB
		public override string Id { get; }

		// Token: 0x06000038 RID: 56 RVA: 0x00002AD3 File Offset: 0x00000CD3
		public BuildAchievement(EventBus eventBus, EntityComponentRegistry entityComponentRegistry, string id, string requiredPrefix)
		{
			this.Id = id;
			this._eventBus = eventBus;
			this._entityComponentRegistry = entityComponentRegistry;
			this._requiredPrefix = requiredPrefix;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002AF8 File Offset: 0x00000CF8
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			TemplateSpec component = enteredFinishedStateEvent.BlockObject.GetComponent<TemplateSpec>();
			if (component != null && component.TemplateName.StartsWith(this._requiredPrefix))
			{
				base.Unlock();
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002B34 File Offset: 0x00000D34
		public override void EnableInternal()
		{
			if ((from s in this._entityComponentRegistry.GetEnabled<Building>()
			where s.GetComponent<BlockObject>().IsFinished
			select s).Any((Building s) => s.GetComponent<TemplateSpec>().TemplateName.StartsWith(this._requiredPrefix)))
			{
				base.Unlock();
				return;
			}
			this._eventBus.Register(this);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B96 File Offset: 0x00000D96
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x04000021 RID: 33
		public readonly EventBus _eventBus;

		// Token: 0x04000022 RID: 34
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x04000023 RID: 35
		public readonly string _requiredPrefix;
	}
}
