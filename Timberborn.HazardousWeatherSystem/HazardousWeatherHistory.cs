using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.HazardousWeatherSystem
{
	// Token: 0x02000008 RID: 8
	public class HazardousWeatherHistory : ISaveableSingleton, ILoadableSingleton
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000025EB File Offset: 0x000007EB
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000025F3 File Offset: 0x000007F3
		public int CurrentStreak { get; private set; }

		// Token: 0x0600001A RID: 26 RVA: 0x000025FC File Offset: 0x000007FC
		public HazardousWeatherHistory(EventBus eventBus, ISingletonLoader singletonLoader, HazardousWeatherHistoryDataSerializer hazardousWeatherHistoryDataSerializer)
		{
			this._eventBus = eventBus;
			this._singletonLoader = singletonLoader;
			this._hazardousWeatherHistoryDataSerializer = hazardousWeatherHistoryDataSerializer;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000262F File Offset: 0x0000082F
		public string CurrentStreakId
		{
			get
			{
				return this._history.Last<HazardousWeatherHistoryData>().HazardousWeatherId;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002641 File Offset: 0x00000841
		public void Save(ISingletonSaver singletonSaver)
		{
			singletonSaver.GetSingleton(HazardousWeatherHistory.HazardousWeatherHistoryKey).Set<HazardousWeatherHistoryData>(HazardousWeatherHistory.HistoryDataKey, this._history, this._hazardousWeatherHistoryDataSerializer);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002664 File Offset: 0x00000864
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(HazardousWeatherHistory.HazardousWeatherHistoryKey, out objectLoader))
			{
				if (objectLoader.Has<HazardousWeatherHistoryData>(HazardousWeatherHistory.HistoryDataKey))
				{
					foreach (HazardousWeatherHistoryData hazardousWeatherHistoryData in objectLoader.Get<HazardousWeatherHistoryData>(HazardousWeatherHistory.HistoryDataKey, this._hazardousWeatherHistoryDataSerializer))
					{
						this.AddHazardousWeatherData(hazardousWeatherHistoryData);
					}
				}
				if (this._history.Any<HazardousWeatherHistoryData>())
				{
					this.CalculateStreakFromHistory();
				}
			}
			this._eventBus.Register(this);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002704 File Offset: 0x00000904
		[OnEvent]
		public void OnHazardousWeatherSelected(HazardousWeatherSelectedEvent hazardousWeatherSelectedEvent)
		{
			this.AddHazardousWeatherData(new HazardousWeatherHistoryData(hazardousWeatherSelectedEvent.SelectedWeather.Id, hazardousWeatherSelectedEvent.Duration));
			this.CalculateStreakFromHistory();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002728 File Offset: 0x00000928
		public bool TryGetPreviousHazardousWeatherData(out HazardousWeatherHistoryData hazardousWeatherHistoryData)
		{
			if (this._history.Count > 1)
			{
				List<HazardousWeatherHistoryData> history = this._history;
				hazardousWeatherHistoryData = history[history.Count - 2];
				return true;
			}
			hazardousWeatherHistoryData = null;
			return false;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002753 File Offset: 0x00000953
		public int GetCyclesCount(string hazardousWeatherId)
		{
			return CollectionExtensions.GetValueOrDefault<string, int>(this._cyclesCount, hazardousWeatherId, 0);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002764 File Offset: 0x00000964
		public void AddHazardousWeatherData(HazardousWeatherHistoryData hazardousWeatherHistoryData)
		{
			this._history.Add(hazardousWeatherHistoryData);
			string hazardousWeatherId = hazardousWeatherHistoryData.HazardousWeatherId;
			if (!this._cyclesCount.TryAdd(hazardousWeatherId, 1))
			{
				Dictionary<string, int> cyclesCount = this._cyclesCount;
				string key = hazardousWeatherId;
				int num = cyclesCount[key];
				cyclesCount[key] = num + 1;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000027AC File Offset: 0x000009AC
		public void CalculateStreakFromHistory()
		{
			this.CurrentStreak = 0;
			string currentStreakId = this.CurrentStreakId;
			for (int i = this._history.Count - 1; i >= 0; i--)
			{
				if (!(currentStreakId == this._history[i].HazardousWeatherId))
				{
					return;
				}
				int currentStreak = this.CurrentStreak;
				this.CurrentStreak = currentStreak + 1;
			}
		}

		// Token: 0x04000026 RID: 38
		public static readonly SingletonKey HazardousWeatherHistoryKey = new SingletonKey("HazardousWeatherHistory");

		// Token: 0x04000027 RID: 39
		public static readonly ListKey<HazardousWeatherHistoryData> HistoryDataKey = new ListKey<HazardousWeatherHistoryData>("HistoryData");

		// Token: 0x04000029 RID: 41
		public readonly EventBus _eventBus;

		// Token: 0x0400002A RID: 42
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400002B RID: 43
		public readonly HazardousWeatherHistoryDataSerializer _hazardousWeatherHistoryDataSerializer;

		// Token: 0x0400002C RID: 44
		public readonly List<HazardousWeatherHistoryData> _history = new List<HazardousWeatherHistoryData>();

		// Token: 0x0400002D RID: 45
		public readonly Dictionary<string, int> _cyclesCount = new Dictionary<string, int>();
	}
}
