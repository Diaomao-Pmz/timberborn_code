using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.ObjectPool;

namespace Timberborn.Multithreading
{
	// Token: 0x02000013 RID: 19
	public class ScheduledTaskPool
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00002E1E File Offset: 0x0000101E
		public ScheduledTaskPool(ISnapshotCollector snapshotCollector)
		{
			this._snapshotCollector = snapshotCollector;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002E38 File Offset: 0x00001038
		public ScheduledTask<T> Rent<T>() where T : struct, ITaskRunner
		{
			return this.GetOrAddPool<T>().Get();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002E45 File Offset: 0x00001045
		public void Return<T>(ScheduledTask<T> task) where T : struct, ITaskRunner
		{
			this.GetOrAddPool<T>().Return(task);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002E54 File Offset: 0x00001054
		public DefaultObjectPool<ScheduledTask<T>> GetOrAddPool<T>() where T : struct, ITaskRunner
		{
			Type typeFromHandle = typeof(T);
			object obj;
			if (!this._pools.TryGetValue(typeFromHandle, out obj))
			{
				Func<Type, ScheduledTaskPool, object> valueFactory = new Func<Type, ScheduledTaskPool, object>(this.CreatePool<T>);
				obj = (DefaultObjectPool<ScheduledTask<T>>)this._pools.GetOrAdd<ScheduledTaskPool>(typeFromHandle, valueFactory, this);
			}
			return (DefaultObjectPool<ScheduledTask<T>>)obj;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002EA3 File Offset: 0x000010A3
		public object CreatePool<T>(Type type, ScheduledTaskPool scheduledTaskPool) where T : struct, ITaskRunner
		{
			return ScheduledTaskPool.CreatePool<T>(scheduledTaskPool, this._snapshotCollector);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002EB1 File Offset: 0x000010B1
		public static DefaultObjectPool<ScheduledTask<T>> CreatePool<T>(ScheduledTaskPool scheduledTaskPool, ISnapshotCollector snapshotCollector) where T : struct, ITaskRunner
		{
			return new DefaultObjectPool<ScheduledTask<T>>(new ScheduledTaskPool.ScheduledTaskPooledObjectPolicy<T>(scheduledTaskPool, snapshotCollector), int.MaxValue);
		}

		// Token: 0x04000027 RID: 39
		public readonly ISnapshotCollector _snapshotCollector;

		// Token: 0x04000028 RID: 40
		public readonly ConcurrentDictionary<Type, object> _pools = new ConcurrentDictionary<Type, object>();

		// Token: 0x02000014 RID: 20
		public class ScheduledTaskPooledObjectPolicy<T> : IPooledObjectPolicy<ScheduledTask<T>> where T : struct, ITaskRunner
		{
			// Token: 0x06000064 RID: 100 RVA: 0x00002EC4 File Offset: 0x000010C4
			public ScheduledTaskPooledObjectPolicy(ScheduledTaskPool scheduledTaskPool, ISnapshotCollector snapshotCollector)
			{
				this._scheduledTaskPool = scheduledTaskPool;
				this._snapshotCollector = snapshotCollector;
			}

			// Token: 0x06000065 RID: 101 RVA: 0x00002EDA File Offset: 0x000010DA
			public ScheduledTask<T> Create()
			{
				return new ScheduledTask<T>(this._scheduledTaskPool, this._snapshotCollector);
			}

			// Token: 0x06000066 RID: 102 RVA: 0x00002EED File Offset: 0x000010ED
			public bool Return(ScheduledTask<T> task)
			{
				task.Reset();
				return true;
			}

			// Token: 0x04000029 RID: 41
			public readonly ScheduledTaskPool _scheduledTaskPool;

			// Token: 0x0400002A RID: 42
			public readonly ISnapshotCollector _snapshotCollector;
		}
	}
}
