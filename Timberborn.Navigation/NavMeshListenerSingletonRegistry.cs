using System;
using System.Collections.Immutable;
using Timberborn.SingletonSystem;

namespace Timberborn.Navigation
{
	// Token: 0x0200005A RID: 90
	public class NavMeshListenerSingletonRegistry : ILoadableSingleton
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x000061B5 File Offset: 0x000043B5
		public NavMeshListenerSingletonRegistry(ISingletonRepository singletonRepository)
		{
			this._singletonRepository = singletonRepository;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000061C4 File Offset: 0x000043C4
		public void Load()
		{
			this._navMeshListeners = this._singletonRepository.GetSingletons<ISingletonNavMeshListener>().ToImmutableArray<ISingletonNavMeshListener>();
			this._prioritizedNavMeshListeners = this._singletonRepository.GetSingletons<IPrioritizedSingletonNavMeshListener>().ToImmutableArray<IPrioritizedSingletonNavMeshListener>();
			this._previewNavMeshListeners = this._singletonRepository.GetSingletons<ISingletonPreviewNavMeshListener>().ToImmutableArray<ISingletonPreviewNavMeshListener>();
			this._prioritizedPreviewNavMeshListeners = this._singletonRepository.GetSingletons<IPrioritizedSingletonPreviewNavMeshListener>().ToImmutableArray<IPrioritizedSingletonPreviewNavMeshListener>();
			this._instantNavMeshListeners = this._singletonRepository.GetSingletons<ISingletonInstantNavMeshListener>().ToImmutableArray<ISingletonInstantNavMeshListener>();
			this._prioritizedInstantNavMeshListeners = this._singletonRepository.GetSingletons<IPrioritizedSingletonInstantNavMeshListener>().ToImmutableArray<IPrioritizedSingletonInstantNavMeshListener>();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00006258 File Offset: 0x00004458
		public void NotifyAll(NavMeshUpdate navMeshUpdate)
		{
			for (int i = 0; i < this._prioritizedNavMeshListeners.Length; i++)
			{
				this._prioritizedNavMeshListeners[i].OnNavMeshUpdated(navMeshUpdate);
			}
			for (int j = 0; j < this._navMeshListeners.Length; j++)
			{
				this._navMeshListeners[j].OnNavMeshUpdated(navMeshUpdate);
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000062B8 File Offset: 0x000044B8
		public void NotifyAllPreview(NavMeshUpdate navMeshUpdate)
		{
			for (int i = 0; i < this._prioritizedPreviewNavMeshListeners.Length; i++)
			{
				this._prioritizedPreviewNavMeshListeners[i].OnPreviewNavMeshUpdated(navMeshUpdate);
			}
			for (int j = 0; j < this._previewNavMeshListeners.Length; j++)
			{
				this._previewNavMeshListeners[j].OnPreviewNavMeshUpdated(navMeshUpdate);
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00006318 File Offset: 0x00004518
		public void NotifyAllInstant(NavMeshUpdate navMeshUpdate)
		{
			for (int i = 0; i < this._prioritizedInstantNavMeshListeners.Length; i++)
			{
				this._prioritizedInstantNavMeshListeners[i].OnInstantNavMeshUpdated(navMeshUpdate);
			}
			for (int j = 0; j < this._instantNavMeshListeners.Length; j++)
			{
				this._instantNavMeshListeners[j].OnInstantNavMeshUpdated(navMeshUpdate);
			}
		}

		// Token: 0x040000BE RID: 190
		public readonly ISingletonRepository _singletonRepository;

		// Token: 0x040000BF RID: 191
		public ImmutableArray<ISingletonNavMeshListener> _navMeshListeners;

		// Token: 0x040000C0 RID: 192
		public ImmutableArray<IPrioritizedSingletonNavMeshListener> _prioritizedNavMeshListeners;

		// Token: 0x040000C1 RID: 193
		public ImmutableArray<ISingletonPreviewNavMeshListener> _previewNavMeshListeners;

		// Token: 0x040000C2 RID: 194
		public ImmutableArray<IPrioritizedSingletonPreviewNavMeshListener> _prioritizedPreviewNavMeshListeners;

		// Token: 0x040000C3 RID: 195
		public ImmutableArray<ISingletonInstantNavMeshListener> _instantNavMeshListeners;

		// Token: 0x040000C4 RID: 196
		public ImmutableArray<IPrioritizedSingletonInstantNavMeshListener> _prioritizedInstantNavMeshListeners;
	}
}
