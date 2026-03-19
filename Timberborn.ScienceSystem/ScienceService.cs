using System;
using Timberborn.MapStateSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.ScienceSystem
{
	// Token: 0x0200000D RID: 13
	public class ScienceService : ISaveableSingleton, ILoadableSingleton
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000027BA File Offset: 0x000009BA
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000027C2 File Offset: 0x000009C2
		public int SciencePoints { get; private set; }

		// Token: 0x06000039 RID: 57 RVA: 0x000027CB File Offset: 0x000009CB
		public ScienceService(ISingletonLoader singletonLoader, MapEditorMode mapEditorMode)
		{
			this._singletonLoader = singletonLoader;
			this._mapEditorMode = mapEditorMode;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027E4 File Offset: 0x000009E4
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(ScienceService.ScienceServiceKey, out objectLoader))
			{
				this.SciencePoints = objectLoader.Get(ScienceService.SciencePointsKey);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002816 File Offset: 0x00000A16
		public void Save(ISingletonSaver singletonSaver)
		{
			if (!this._mapEditorMode.IsMapEditor)
			{
				singletonSaver.GetSingleton(ScienceService.ScienceServiceKey).Set(ScienceService.SciencePointsKey, this.SciencePoints);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002840 File Offset: 0x00000A40
		public void AddPoints(int amount)
		{
			this.SciencePoints += amount;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002850 File Offset: 0x00000A50
		public void SubtractPoints(int amount)
		{
			if (this.SciencePoints - amount < 0)
			{
				throw new ArgumentException(string.Format("Can't subtract {0} science points, ", amount) + string.Format("there are only {0} points stored", this.SciencePoints));
			}
			this.SciencePoints -= amount;
		}

		// Token: 0x04000022 RID: 34
		public static readonly SingletonKey ScienceServiceKey = new SingletonKey("ScienceService");

		// Token: 0x04000023 RID: 35
		public static readonly PropertyKey<int> SciencePointsKey = new PropertyKey<int>("SciencePoints");

		// Token: 0x04000025 RID: 37
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000026 RID: 38
		public readonly MapEditorMode _mapEditorMode;
	}
}
