using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.Navigation
{
	// Token: 0x0200001E RID: 30
	public class FlowFieldCache
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00003ED0 File Offset: 0x000020D0
		public AccessFlowField GetFlowFieldAtNode(int nodeId)
		{
			AccessFlowField result;
			if (!this.TryGetFlowFieldAtNode(nodeId, out result))
			{
				throw new InvalidOperationException(string.Format("There's no cached flow field at {0}", nodeId));
			}
			return result;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003F00 File Offset: 0x00002100
		public bool TryGetFlowFieldAtNode(int nodeId, out AccessFlowField flowField)
		{
			FlowFieldCache.CacheEntry cacheEntry;
			if (this.TryGetCacheEntry(nodeId, out cacheEntry))
			{
				flowField = cacheEntry.AccessFlowField;
				return true;
			}
			flowField = null;
			return false;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003F28 File Offset: 0x00002128
		public void StartCachingAtNode(int nodeId)
		{
			FlowFieldCache.CacheEntry cacheEntry;
			if (!this.TryGetCacheEntry(nodeId, out cacheEntry))
			{
				cacheEntry = new FlowFieldCache.CacheEntry();
				this._flowFields[nodeId] = cacheEntry;
			}
			FlowFieldCache.CacheEntry cacheEntry2 = cacheEntry;
			int numberOfCachers = cacheEntry2.NumberOfCachers + 1;
			cacheEntry2.NumberOfCachers = numberOfCachers;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003F64 File Offset: 0x00002164
		public void StopCachingAtNode(int nodeId)
		{
			FlowFieldCache.CacheEntry cacheEntry;
			if (!this.TryGetCacheEntry(nodeId, out cacheEntry))
			{
				throw new InvalidOperationException(string.Format("Can't stop caching at {0}. There's no cached flow field there.", nodeId));
			}
			if (cacheEntry.NumberOfCachers == 0)
			{
				throw new InvalidOperationException(string.Format("Can't decrement cachers at {0}, it's already 0.", nodeId));
			}
			FlowFieldCache.CacheEntry cacheEntry2 = cacheEntry;
			int numberOfCachers = cacheEntry2.NumberOfCachers - 1;
			cacheEntry2.NumberOfCachers = numberOfCachers;
			if (cacheEntry.NumberOfCachers == 0)
			{
				this._flowFields.Remove(nodeId);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003FD8 File Offset: 0x000021D8
		public void OnNodesChanged(ReadOnlyList<int> nodeIds)
		{
			foreach (FlowFieldCache.CacheEntry cacheEntry in this._flowFields.Values)
			{
				cacheEntry.AccessFlowField.OnNodesChanged(nodeIds);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004034 File Offset: 0x00002234
		public bool TryGetCacheEntry(int nodeId, out FlowFieldCache.CacheEntry cacheEntry)
		{
			return this._flowFields.TryGetValue(nodeId, out cacheEntry);
		}

		// Token: 0x04000060 RID: 96
		public readonly Dictionary<int, FlowFieldCache.CacheEntry> _flowFields = new Dictionary<int, FlowFieldCache.CacheEntry>();

		// Token: 0x0200001F RID: 31
		public class CacheEntry
		{
			// Token: 0x17000018 RID: 24
			// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004056 File Offset: 0x00002256
			public AccessFlowField AccessFlowField { get; } = new AccessFlowField();

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000405E File Offset: 0x0000225E
			// (set) Token: 0x060000D6 RID: 214 RVA: 0x00004066 File Offset: 0x00002266
			public int NumberOfCachers { get; set; }
		}
	}
}
