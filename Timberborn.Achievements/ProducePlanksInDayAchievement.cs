using System;
using System.Collections.Generic;
using Timberborn.AchievementSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.GameFactionSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using Timberborn.Workshops;
using Timberborn.WorldPersistence;

namespace Timberborn.Achievements
{
	// Token: 0x0200003C RID: 60
	public class ProducePlanksInDayAchievement : Achievement, ILoadableSingleton, ISaveableSingleton
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x000043AD File Offset: 0x000025AD
		public ProducePlanksInDayAchievement(EventBus eventBus, ISingletonLoader singletonLoader, FactionService factionService, EntityComponentRegistry entityComponentRegistry)
		{
			this._eventBus = eventBus;
			this._singletonLoader = singletonLoader;
			this._factionService = factionService;
			this._entityComponentRegistry = entityComponentRegistry;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000043DD File Offset: 0x000025DD
		public override string Id
		{
			get
			{
				return "PRODUCE_PLANKS_IN_DAY";
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000043E4 File Offset: 0x000025E4
		public void Save(ISingletonSaver singletonSaver)
		{
			if (this._planksProduced > 0 && this._planksProduced < ProducePlanksInDayAchievement.PlanksToProducePerDay)
			{
				singletonSaver.GetSingleton(ProducePlanksInDayAchievement.ProducePlanksInDayKey).Set(ProducePlanksInDayAchievement.PlanksProducedKey, this._planksProduced);
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004418 File Offset: 0x00002618
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(ProducePlanksInDayAchievement.ProducePlanksInDayKey, out objectLoader))
			{
				this._planksProduced = objectLoader.Get(ProducePlanksInDayAchievement.PlanksProducedKey);
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000444C File Offset: 0x0000264C
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			Manufactory component = enteredFinishedStateEvent.BlockObject.GetComponent<Manufactory>();
			if (component != null)
			{
				this.AddManufactory(component);
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004470 File Offset: 0x00002670
		[OnEvent]
		public void OnExitedFinishedState(ExitedFinishedStateEvent exitedFinishedStateEvent)
		{
			Manufactory component = exitedFinishedStateEvent.BlockObject.GetComponent<Manufactory>();
			if (component != null)
			{
				this.RemoveManufactory(component);
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004493 File Offset: 0x00002693
		[OnEvent]
		public void OnDaytimeStartEvent(DaytimeStartEvent daytimeStartEvent)
		{
			this._planksProduced = 0;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000449C File Offset: 0x0000269C
		public override void EnableInternal()
		{
			if (this._factionService.Current.Id == AchievementHelper.IronTeeth)
			{
				this._eventBus.Register(this);
				foreach (Manufactory manufactory in this._entityComponentRegistry.GetEnabled<Manufactory>())
				{
					this.AddManufactory(manufactory);
				}
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004518 File Offset: 0x00002718
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
			foreach (Manufactory manufactory in this._manufactories)
			{
				manufactory.ProductionFinished -= this.OnProductionFinished;
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004580 File Offset: 0x00002780
		public void AddManufactory(Manufactory manufactory)
		{
			if (this._manufactories.Add(manufactory))
			{
				manufactory.ProductionFinished += this.OnProductionFinished;
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000045A2 File Offset: 0x000027A2
		public void RemoveManufactory(Manufactory manufactory)
		{
			this._manufactories.Remove(manufactory);
			manufactory.ProductionFinished -= this.OnProductionFinished;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000045C4 File Offset: 0x000027C4
		public void OnProductionFinished(object sender, EventArgs e)
		{
			Manufactory manufactory = (Manufactory)sender;
			if (manufactory.HasCurrentRecipe && manufactory.CurrentRecipe.Id == ProducePlanksInDayAchievement.RecipeId)
			{
				this._planksProduced++;
			}
			if (this._planksProduced >= ProducePlanksInDayAchievement.PlanksToProducePerDay)
			{
				base.Unlock();
			}
		}

		// Token: 0x04000087 RID: 135
		public static readonly SingletonKey ProducePlanksInDayKey = new SingletonKey("ProducePlanksInDay");

		// Token: 0x04000088 RID: 136
		public static readonly PropertyKey<int> PlanksProducedKey = new PropertyKey<int>("PlanksProduced");

		// Token: 0x04000089 RID: 137
		public static readonly int PlanksToProducePerDay = 500;

		// Token: 0x0400008A RID: 138
		public static readonly string RecipeId = "Plank";

		// Token: 0x0400008B RID: 139
		public readonly EventBus _eventBus;

		// Token: 0x0400008C RID: 140
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400008D RID: 141
		public readonly FactionService _factionService;

		// Token: 0x0400008E RID: 142
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x0400008F RID: 143
		public readonly HashSet<Manufactory> _manufactories = new HashSet<Manufactory>();

		// Token: 0x04000090 RID: 144
		public int _planksProduced;
	}
}
