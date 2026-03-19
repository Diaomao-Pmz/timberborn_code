using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.AchievementSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.GameFactionSystem;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using Timberborn.TickSystem;
using Timberborn.Workshops;

namespace Timberborn.Achievements
{
	// Token: 0x02000053 RID: 83
	public class WorkingRefineryForEachRecipeAchievement : Achievement, ITickableSingleton
	{
		// Token: 0x06000154 RID: 340 RVA: 0x00004EE1 File Offset: 0x000030E1
		public WorkingRefineryForEachRecipeAchievement(EventBus eventBus, EntityComponentRegistry entityComponentRegistry, FactionService factionService, TemplateService templateService)
		{
			this._eventBus = eventBus;
			this._entityComponentRegistry = entityComponentRegistry;
			this._factionService = factionService;
			this._templateService = templateService;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00004F1C File Offset: 0x0000311C
		public override string Id
		{
			get
			{
				return "WORKING_REFINERY_FOR_EACH_RECIPE";
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00004F23 File Offset: 0x00003123
		public void Tick()
		{
			if (base.IsEnabled)
			{
				this.CheckUnlockCondition();
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00004F34 File Offset: 0x00003134
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			BlockObject blockObject = enteredFinishedStateEvent.BlockObject;
			TemplateSpec component = blockObject.GetComponent<TemplateSpec>();
			if (component != null && component.TemplateName == WorkingRefineryForEachRecipeAchievement.TemplateId)
			{
				this._refineries.Add(blockObject.GetComponent<Manufactory>());
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00004F78 File Offset: 0x00003178
		[OnEvent]
		public void OnExitedFinishedState(ExitedFinishedStateEvent exitedFinishedStateEvent)
		{
			BlockObject blockObject = exitedFinishedStateEvent.BlockObject;
			TemplateSpec component = blockObject.GetComponent<TemplateSpec>();
			if (component != null && component.TemplateName == WorkingRefineryForEachRecipeAchievement.TemplateId)
			{
				this._refineries.Remove(blockObject.GetComponent<Manufactory>());
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00004FBC File Offset: 0x000031BC
		public override void EnableInternal()
		{
			if (this._factionService.Current.Id == AchievementHelper.Folktails)
			{
				this._eventBus.Register(this);
				this._requiredRecipesCount = this._templateService.GetAll<ManufactorySpec>().Single((ManufactorySpec m) => m.GetSpec<TemplateSpec>().TemplateName == WorkingRefineryForEachRecipeAchievement.TemplateId).ProductionRecipeIds.Length;
				this.ValidateInitialCount();
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005039 File Offset: 0x00003239
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005048 File Offset: 0x00003248
		public void CheckUnlockCondition()
		{
			foreach (Manufactory manufactory in this._refineries)
			{
				if (manufactory.IsReadyToProduce && manufactory.ProductionProgress > 0f && this._uniqueRecipes.Add(manufactory.CurrentRecipe.Id) && this._uniqueRecipes.Count >= this._requiredRecipesCount)
				{
					base.Unlock();
					return;
				}
			}
			this._uniqueRecipes.Clear();
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000050E8 File Offset: 0x000032E8
		public void ValidateInitialCount()
		{
			foreach (Manufactory item in from manufactory in this._entityComponentRegistry.GetEnabled<Manufactory>()
			where manufactory.GetComponent<BlockObject>().IsFinished
			where manufactory.GetComponent<TemplateSpec>().TemplateName == WorkingRefineryForEachRecipeAchievement.TemplateId
			select manufactory)
			{
				this._refineries.Add(item);
			}
		}

		// Token: 0x040000C0 RID: 192
		public static readonly string TemplateId = "Refinery.Folktails";

		// Token: 0x040000C1 RID: 193
		public readonly EventBus _eventBus;

		// Token: 0x040000C2 RID: 194
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x040000C3 RID: 195
		public readonly FactionService _factionService;

		// Token: 0x040000C4 RID: 196
		public readonly TemplateService _templateService;

		// Token: 0x040000C5 RID: 197
		public readonly HashSet<Manufactory> _refineries = new HashSet<Manufactory>();

		// Token: 0x040000C6 RID: 198
		public readonly HashSet<string> _uniqueRecipes = new HashSet<string>();

		// Token: 0x040000C7 RID: 199
		public int _requiredRecipesCount;
	}
}
