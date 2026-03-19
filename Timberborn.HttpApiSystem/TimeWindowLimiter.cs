using System;
using System.Collections.Generic;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x02000043 RID: 67
	public class TimeWindowLimiter
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00006C5E File Offset: 0x00004E5E
		public TimeWindowLimiter(int maxOccurrences, TimeSpan timeWindow)
		{
			this._maxOccurrences = maxOccurrences;
			this._windowDurationInTicks = timeWindow.Ticks;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006C88 File Offset: 0x00004E88
		public bool TryAcquirePermit()
		{
			long ticks = DateTime.Now.Ticks;
			if (this._ticksQueue.Count < this._maxOccurrences)
			{
				this._ticksQueue.Enqueue(ticks);
				return true;
			}
			if (this._ticksQueue.Peek() < ticks - this._windowDurationInTicks)
			{
				this._ticksQueue.Dequeue();
				this._ticksQueue.Enqueue(ticks);
				return true;
			}
			return false;
		}

		// Token: 0x0400010D RID: 269
		public readonly Queue<long> _ticksQueue = new Queue<long>();

		// Token: 0x0400010E RID: 270
		public readonly int _maxOccurrences;

		// Token: 0x0400010F RID: 271
		public readonly long _windowDurationInTicks;
	}
}
