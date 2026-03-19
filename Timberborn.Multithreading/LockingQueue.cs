using System;
using System.Collections.Generic;
using System.Threading;

namespace Timberborn.Multithreading
{
	// Token: 0x0200000A RID: 10
	public class LockingQueue<T>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000020C0 File Offset: 0x000002C0
		public int Count
		{
			get
			{
				object lockObject = this._lockObject;
				int count;
				lock (lockObject)
				{
					count = this._queue.Count;
				}
				return count;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002108 File Offset: 0x00000308
		public void Add(T item)
		{
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				this._queue.Enqueue(item);
				Monitor.Pulse(this._lockObject);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000215C File Offset: 0x0000035C
		public void CompleteAdding()
		{
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				this._addingCompleted = true;
				Monitor.PulseAll(this._lockObject);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000021A8 File Offset: 0x000003A8
		public bool TryTakeBlocking(out T item)
		{
			object lockObject = this._lockObject;
			bool result;
			lock (lockObject)
			{
				while (!this._queue.TryDequeue(ref item))
				{
					if (this._addingCompleted)
					{
						item = default(T);
						return false;
					}
					Monitor.Wait(this._lockObject);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x04000006 RID: 6
		public readonly object _lockObject = new object();

		// Token: 0x04000007 RID: 7
		public readonly Queue<T> _queue = new Queue<T>();

		// Token: 0x04000008 RID: 8
		public bool _addingCompleted;
	}
}
