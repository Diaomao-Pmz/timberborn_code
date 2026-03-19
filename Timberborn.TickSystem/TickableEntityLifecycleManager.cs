using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.EntitySystem;
using Timberborn.Metrics;
using Timberborn.SingletonSystem;

namespace Timberborn.TickSystem
{
	// Token: 0x02000013 RID: 19
	public class TickableEntityLifecycleManager : ILoadableSingleton
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000024FF File Offset: 0x000006FF
		public TickableEntityLifecycleManager(EventBus eventBus, ITickableBucketService tickableBucketService, IMetricsService metricsService)
		{
			this._eventBus = eventBus;
			this._tickableBucketService = tickableBucketService;
			this._metricsService = metricsService;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002532 File Offset: 0x00000732
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002540 File Offset: 0x00000740
		[OnEvent]
		public void OnEntityInitialized(EntityInitializedEvent entityInitializedEvent)
		{
			this.AddTickableEntity(entityInitializedEvent.Entity);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000254E File Offset: 0x0000074E
		[OnEvent]
		public void OnEntityDeleted(EntityDeletedEvent entityDeletedEvent)
		{
			this.RemoveTickableEntity(entityDeletedEvent.Entity);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000255C File Offset: 0x0000075C
		public void AddTickableEntity(EntityComponent entity)
		{
			entity.GetComponents<TickableComponent>(this._tickableComponentsCache);
			if (this._tickableComponentsCache.Count > 0)
			{
				IEnumerable<MeteredTickableComponent> tickableComponents = this._tickableComponentsCache.OrderBy(delegate(TickableComponent tickable)
				{
					if (!(tickable is ILateTickable))
					{
						return 0;
					}
					return 1;
				}).Select(new Func<TickableComponent, MeteredTickableComponent>(this.CreateMeteredComponent));
				TickableEntity tickableEntity = new TickableEntity(entity, tickableComponents, entity.Name);
				this._tickableBucketService.AddEntity(tickableEntity);
				this._tickableEntities[entity.EntityId] = tickableEntity;
				this._tickableComponentsCache.Clear();
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000025F8 File Offset: 0x000007F8
		public void RemoveTickableEntity(EntityComponent entity)
		{
			Guid entityId = entity.EntityId;
			TickableEntity tickableEntity;
			if (this._tickableEntities.TryGetValue(entityId, out tickableEntity))
			{
				this._tickableBucketService.RemoveEntity(tickableEntity);
				this._tickableEntities.Remove(entityId);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002638 File Offset: 0x00000838
		public MeteredTickableComponent CreateMeteredComponent(TickableComponent tickableComponent)
		{
			string name = tickableComponent.GetType().Name;
			ITimerMetric timerMetric = this._metricsService.GetTimerMetric("Tick", name);
			return new MeteredTickableComponent(tickableComponent, timerMetric, this._metricsService.MetricsEnabled);
		}

		// Token: 0x04000017 RID: 23
		public readonly EventBus _eventBus;

		// Token: 0x04000018 RID: 24
		public readonly ITickableBucketService _tickableBucketService;

		// Token: 0x04000019 RID: 25
		public readonly IMetricsService _metricsService;

		// Token: 0x0400001A RID: 26
		public readonly Dictionary<Guid, TickableEntity> _tickableEntities = new Dictionary<Guid, TickableEntity>();

		// Token: 0x0400001B RID: 27
		public readonly List<TickableComponent> _tickableComponentsCache = new List<TickableComponent>();
	}
}
