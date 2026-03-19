using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.NeedSpecs;

namespace Timberborn.NeedSystem
{
	// Token: 0x0200000C RID: 12
	public class Needs
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002E71 File Offset: 0x00001071
		public ImmutableArray<Need> AllNeeds { get; }

		// Token: 0x06000067 RID: 103 RVA: 0x00002E7C File Offset: 0x0000107C
		public Needs(IEnumerable<Need> needs)
		{
			List<Need> list = needs.ToList<Need>();
			this._needArray = new Need[list.Count];
			int num = 0;
			foreach (Need need in list)
			{
				this._needDictionary[need.NeedSpec.Id] = need;
				this._needArray[num++] = need;
			}
			this.AllNeeds = this._needArray.ToImmutableArray<Need>();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002F24 File Offset: 0x00001124
		public bool TryGetNeed(string needId, out Need need)
		{
			return this._needDictionary.TryGetValue(needId, out need);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002F34 File Offset: 0x00001134
		public bool TryGetBackwardCompatibleNeed(string needId, out Need need)
		{
			Need need2 = this._needArray.SingleOrDefault((Need n) => n.NeedSpec.BackwardCompatibleIds.Contains(needId));
			if (need2 != null)
			{
				need = need2;
				return true;
			}
			need = null;
			return false;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002F74 File Offset: 0x00001174
		public NeedSpec GetNeedSpec(string needId)
		{
			Need need;
			if (!this._needDictionary.TryGetValue(needId, out need))
			{
				return null;
			}
			return need.NeedSpec;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002F99 File Offset: 0x00001199
		public bool Has(string needId)
		{
			return this._needDictionary.ContainsKey(needId);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002FA8 File Offset: 0x000011A8
		public bool Any(Predicate<Need> predicate)
		{
			for (int i = 0; i < this._needArray.Length; i++)
			{
				if (predicate(this._needArray[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000029 RID: 41
		public readonly Dictionary<string, Need> _needDictionary = new Dictionary<string, Need>();

		// Token: 0x0400002A RID: 42
		public readonly Need[] _needArray;
	}
}
