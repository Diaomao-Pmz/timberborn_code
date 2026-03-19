using System;
using System.Collections.Generic;
using Timberborn.TickSystem;

namespace Timberborn.TimeSystem
{
	// Token: 0x0200001C RID: 28
	public class TimeTriggerService : ITickableSingleton
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00003106 File Offset: 0x00001306
		public TimeTriggerService(IDayNightCycle dayNightCycle)
		{
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003136 File Offset: 0x00001336
		public void Tick()
		{
			this.FindReadyToTrigger();
			this.Trigger();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003144 File Offset: 0x00001344
		public void Add(TimeTrigger timeTrigger, float triggerTimestamp)
		{
			this.Remove(timeTrigger);
			long nextId = this._nextId;
			this._nextId = nextId + 1L;
			TimeTriggerService.SortableKey sortableKey = new TimeTriggerService.SortableKey(triggerTimestamp, nextId);
			this._sortedTimeTriggers[sortableKey] = timeTrigger;
			this._timeTriggerKeys[timeTrigger] = sortableKey;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000318C File Offset: 0x0000138C
		public void Remove(TimeTrigger timeTrigger)
		{
			TimeTriggerService.SortableKey key;
			if (this._timeTriggerKeys.TryGetValue(timeTrigger, out key))
			{
				this._sortedTimeTriggers.Remove(key);
				this._timeTriggerKeys.Remove(timeTrigger);
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000031C4 File Offset: 0x000013C4
		public void FindReadyToTrigger()
		{
			float partialDayNumber = this._dayNightCycle.PartialDayNumber;
			foreach (KeyValuePair<TimeTriggerService.SortableKey, TimeTrigger> keyValuePair in this._sortedTimeTriggers)
			{
				TimeTriggerService.SortableKey sortableKey;
				TimeTrigger timeTrigger;
				keyValuePair.Deconstruct(ref sortableKey, ref timeTrigger);
				TimeTriggerService.SortableKey sortableKey2 = sortableKey;
				TimeTrigger item = timeTrigger;
				if (sortableKey2.Timestamp > partialDayNumber)
				{
					break;
				}
				this._triggersToTrigger.Add(item);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003248 File Offset: 0x00001448
		public void Trigger()
		{
			foreach (TimeTrigger timeTrigger in this._triggersToTrigger)
			{
				this.Trigger(timeTrigger);
			}
			this._triggersToTrigger.Clear();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000032A8 File Offset: 0x000014A8
		public void Trigger(TimeTrigger timeTrigger)
		{
			this.Remove(timeTrigger);
			timeTrigger.Finish();
		}

		// Token: 0x04000043 RID: 67
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000044 RID: 68
		public readonly SortedDictionary<TimeTriggerService.SortableKey, TimeTrigger> _sortedTimeTriggers = new SortedDictionary<TimeTriggerService.SortableKey, TimeTrigger>();

		// Token: 0x04000045 RID: 69
		public readonly Dictionary<TimeTrigger, TimeTriggerService.SortableKey> _timeTriggerKeys = new Dictionary<TimeTrigger, TimeTriggerService.SortableKey>();

		// Token: 0x04000046 RID: 70
		public readonly List<TimeTrigger> _triggersToTrigger = new List<TimeTrigger>();

		// Token: 0x04000047 RID: 71
		public long _nextId;

		// Token: 0x0200001D RID: 29
		public readonly struct SortableKey : IComparable<TimeTriggerService.SortableKey>
		{
			// Token: 0x17000031 RID: 49
			// (get) Token: 0x060000B6 RID: 182 RVA: 0x000032B7 File Offset: 0x000014B7
			public float Timestamp { get; }

			// Token: 0x060000B7 RID: 183 RVA: 0x000032BF File Offset: 0x000014BF
			public SortableKey(float timestamp, long id)
			{
				this.Timestamp = timestamp;
				this._id = id;
			}

			// Token: 0x060000B8 RID: 184 RVA: 0x000032D0 File Offset: 0x000014D0
			public int CompareTo(TimeTriggerService.SortableKey other)
			{
				int num = this.Timestamp.CompareTo(other.Timestamp);
				if (num == 0)
				{
					return this._id.CompareTo(other._id);
				}
				return num;
			}

			// Token: 0x04000049 RID: 73
			public readonly long _id;
		}
	}
}
