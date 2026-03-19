using System;
using Timberborn.Forestry;
using Timberborn.NaturalResources;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Achievements
{
	// Token: 0x02000050 RID: 80
	public class TreePlantingCounter : ISaveableSingleton, ILoadableSingleton
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600013E RID: 318 RVA: 0x00004BD8 File Offset: 0x00002DD8
		// (remove) Token: 0x0600013F RID: 319 RVA: 0x00004C10 File Offset: 0x00002E10
		public event EventHandler<int> CountChanged;

		// Token: 0x06000140 RID: 320 RVA: 0x00004C45 File Offset: 0x00002E45
		public TreePlantingCounter(EventBus eventBus, ISingletonLoader singletonLoader)
		{
			this._eventBus = eventBus;
			this._singletonLoader = singletonLoader;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004C5B File Offset: 0x00002E5B
		public void Save(ISingletonSaver singletonSaver)
		{
			if (this._plantedCount > 0)
			{
				singletonSaver.GetSingleton(TreePlantingCounter.TreePlantingCounterKey).Set(TreePlantingCounter.PlantedCountKey, this._plantedCount);
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004C84 File Offset: 0x00002E84
		public void Load()
		{
			this._eventBus.Register(this);
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(TreePlantingCounter.TreePlantingCounterKey, out objectLoader))
			{
				this._plantedCount = objectLoader.Get(TreePlantingCounter.PlantedCountKey);
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004CC2 File Offset: 0x00002EC2
		[OnEvent]
		public void OnNaturalResourcePlanted(NaturalResourcePlantedEvent naturalResourcePlantedEvent)
		{
			if (naturalResourcePlantedEvent.PlantedResource.HasSpec<TreeComponentSpec>())
			{
				this._plantedCount++;
				EventHandler<int> countChanged = this.CountChanged;
				if (countChanged == null)
				{
					return;
				}
				countChanged(this, this._plantedCount);
			}
		}

		// Token: 0x040000AF RID: 175
		public static readonly SingletonKey TreePlantingCounterKey = new SingletonKey("TreePlantingCounter");

		// Token: 0x040000B0 RID: 176
		public static readonly PropertyKey<int> PlantedCountKey = new PropertyKey<int>("PlantedCount");

		// Token: 0x040000B2 RID: 178
		public readonly EventBus _eventBus;

		// Token: 0x040000B3 RID: 179
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x040000B4 RID: 180
		public int _plantedCount;
	}
}
