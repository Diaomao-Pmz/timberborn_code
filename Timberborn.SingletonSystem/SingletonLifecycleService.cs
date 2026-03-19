using System;
using System.Collections.Immutable;

namespace Timberborn.SingletonSystem
{
	// Token: 0x02000014 RID: 20
	public class SingletonLifecycleService
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000251D File Offset: 0x0000071D
		public SingletonLifecycleService(ISingletonRepository singletonRepository)
		{
			this._singletonRepository = singletonRepository;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000252C File Offset: 0x0000072C
		public void LoadAll()
		{
			this._loadableSingletons = this._singletonRepository.GetSingletons<ILoadableSingleton>().ToImmutableArray<ILoadableSingleton>();
			this._nonSingletonLoaders = this._singletonRepository.GetSingletons<INonSingletonLoader>().ToImmutableArray<INonSingletonLoader>();
			this._postLoadableSingletons = this._singletonRepository.GetSingletons<IPostLoadableSingleton>().ToImmutableArray<IPostLoadableSingleton>();
			this._nonSingletonPostLoaders = this._singletonRepository.GetSingletons<INonSingletonPostLoader>().ToImmutableArray<INonSingletonPostLoader>();
			this._unloadableSingletons = this._singletonRepository.GetSingletons<IUnloadableSingleton>().ToImmutableArray<IUnloadableSingleton>();
			this._updatableSingletons = this._singletonRepository.GetSingletons<IUpdatableSingleton>().ToImmutableArray<IUpdatableSingleton>();
			this._lateUpdatableSingletons = this._singletonRepository.GetSingletons<ILateUpdatableSingleton>().ToImmutableArray<ILateUpdatableSingleton>();
			this.LoadSingletons();
			this.LoadNonSingletons();
			this.PostLoadSingletons();
			this.PostLoadNonSingletons();
			this._initializedSuccessfully = true;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025F2 File Offset: 0x000007F2
		public void UnloadAll()
		{
			this.UnloadSingletons();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025FA File Offset: 0x000007FA
		public void UpdateAll()
		{
			if (this._initializedSuccessfully)
			{
				this.UpdateSingletons();
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000260A File Offset: 0x0000080A
		public void LateUpdateAll()
		{
			if (this._initializedSuccessfully)
			{
				this.LateUpdateSingletons();
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000261C File Offset: 0x0000081C
		public void LoadSingletons()
		{
			for (int i = 0; i < this._loadableSingletons.Length; i++)
			{
				this._loadableSingletons[i].Load();
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002650 File Offset: 0x00000850
		public void LoadNonSingletons()
		{
			for (int i = 0; i < this._nonSingletonLoaders.Length; i++)
			{
				this._nonSingletonLoaders[i].LoadNonSingletons();
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002684 File Offset: 0x00000884
		public void PostLoadSingletons()
		{
			for (int i = 0; i < this._postLoadableSingletons.Length; i++)
			{
				this._postLoadableSingletons[i].PostLoad();
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026B8 File Offset: 0x000008B8
		public void PostLoadNonSingletons()
		{
			for (int i = 0; i < this._nonSingletonPostLoaders.Length; i++)
			{
				this._nonSingletonPostLoaders[i].PostLoadNonSingletons();
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026EC File Offset: 0x000008EC
		public void UnloadSingletons()
		{
			for (int i = 0; i < this._unloadableSingletons.Length; i++)
			{
				this._unloadableSingletons[i].Unload();
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002720 File Offset: 0x00000920
		public void UpdateSingletons()
		{
			for (int i = 0; i < this._updatableSingletons.Length; i++)
			{
				this._updatableSingletons[i].UpdateSingleton();
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002754 File Offset: 0x00000954
		public void LateUpdateSingletons()
		{
			for (int i = 0; i < this._lateUpdatableSingletons.Length; i++)
			{
				this._lateUpdatableSingletons[i].LateUpdateSingleton();
			}
		}

		// Token: 0x04000014 RID: 20
		public readonly ISingletonRepository _singletonRepository;

		// Token: 0x04000015 RID: 21
		public bool _initializedSuccessfully;

		// Token: 0x04000016 RID: 22
		public ImmutableArray<ILoadableSingleton> _loadableSingletons;

		// Token: 0x04000017 RID: 23
		public ImmutableArray<INonSingletonLoader> _nonSingletonLoaders;

		// Token: 0x04000018 RID: 24
		public ImmutableArray<IPostLoadableSingleton> _postLoadableSingletons;

		// Token: 0x04000019 RID: 25
		public ImmutableArray<INonSingletonPostLoader> _nonSingletonPostLoaders;

		// Token: 0x0400001A RID: 26
		public ImmutableArray<IUnloadableSingleton> _unloadableSingletons;

		// Token: 0x0400001B RID: 27
		public ImmutableArray<IUpdatableSingleton> _updatableSingletons;

		// Token: 0x0400001C RID: 28
		public ImmutableArray<ILateUpdatableSingleton> _lateUpdatableSingletons;
	}
}
