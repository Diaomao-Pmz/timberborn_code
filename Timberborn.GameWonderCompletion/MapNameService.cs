using System;
using Timberborn.GameSceneLoading;
using Timberborn.MapRepositorySystem;
using Timberborn.Persistence;
using Timberborn.SceneLoading;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.GameWonderCompletion
{
	// Token: 0x0200000B RID: 11
	public class MapNameService : ILoadableSingleton, ISaveableSingleton
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000025E2 File Offset: 0x000007E2
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000025EA File Offset: 0x000007EA
		public string Name { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000025F3 File Offset: 0x000007F3
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000025FB File Offset: 0x000007FB
		public bool IsResource { get; private set; }

		// Token: 0x06000031 RID: 49 RVA: 0x00002604 File Offset: 0x00000804
		public MapNameService(ISingletonLoader singletonLoader, ISceneLoader sceneLoader)
		{
			this._singletonLoader = singletonLoader;
			this._sceneLoader = sceneLoader;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000261A File Offset: 0x0000081A
		public bool HasMapName
		{
			get
			{
				return this.Name != null;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002625 File Offset: 0x00000825
		public void Save(ISingletonSaver singletonSaver)
		{
			if (this.HasMapName)
			{
				IObjectSaver singleton = singletonSaver.GetSingleton(MapNameService.MapNameServiceKey);
				singleton.Set(MapNameService.NameKey, this.Name);
				singleton.Set(MapNameService.IsResourceKey, this.IsResource);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000265C File Offset: 0x0000085C
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(MapNameService.MapNameServiceKey, out objectLoader))
			{
				this.Name = objectLoader.Get(MapNameService.NameKey);
				this.IsResource = objectLoader.Get(MapNameService.IsResourceKey);
				return;
			}
			GameSceneParameters sceneParameters = this._sceneLoader.GetSceneParameters<GameSceneParameters>();
			if (sceneParameters.NewGameConfiguration != null)
			{
				MapFileReference mapFileReference = sceneParameters.NewGameConfiguration.MapFileReference;
				this.Name = mapFileReference.Name;
				this.IsResource = mapFileReference.Resource;
			}
		}

		// Token: 0x04000015 RID: 21
		public static readonly SingletonKey MapNameServiceKey = new SingletonKey("MapNameService");

		// Token: 0x04000016 RID: 22
		public static readonly PropertyKey<string> NameKey = new PropertyKey<string>("Name");

		// Token: 0x04000017 RID: 23
		public static readonly PropertyKey<bool> IsResourceKey = new PropertyKey<bool>("IsResource");

		// Token: 0x0400001A RID: 26
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400001B RID: 27
		public readonly ISceneLoader _sceneLoader;
	}
}
