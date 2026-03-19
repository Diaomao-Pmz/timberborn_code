using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.SingletonSystem;

namespace Timberborn.ToolSystem
{
	// Token: 0x0200001A RID: 26
	public class ToolUnlockingService
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002877 File Offset: 0x00000A77
		public ToolUnlockingService(EventBus eventBus, IEnumerable<IToolLocker> toolLockers)
		{
			this._eventBus = eventBus;
			this._toolLockers = toolLockers.ToImmutableArray<IToolLocker>();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000289D File Offset: 0x00000A9D
		public bool IsLocked(ITool tool)
		{
			return this._activeLockers.ContainsKey(tool);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000028AC File Offset: 0x00000AAC
		public void LockIfNeeded(ITool tool)
		{
			IToolLocker toolLocker = this._toolLockers.SingleOrDefault((IToolLocker locker) => locker.ShouldLock(tool));
			if (toolLocker != null)
			{
				this._activeLockers[tool] = toolLocker;
				this._eventBus.Post(new ToolLockedEvent(tool));
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000290C File Offset: 0x00000B0C
		public void Unlock(ITool tool)
		{
			if (this.IsLocked(tool))
			{
				this.UnlockInternal(tool, delegate
				{
				});
				return;
			}
			throw new InvalidOperationException(string.Format("Tool {0} is not locked, cannot unlock it.", tool));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000295C File Offset: 0x00000B5C
		public void TryToUnlock(ITool tool)
		{
			this.TryToUnlock(tool, delegate()
			{
			}, delegate()
			{
			});
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000029B0 File Offset: 0x00000BB0
		public void TryToUnlock(ITool tool, Action successCallback, Action failCallback)
		{
			IToolLocker toolLocker;
			if (this._activeLockers.TryGetValue(tool, out toolLocker))
			{
				toolLocker.TryToUnlock(tool, delegate
				{
					this.UnlockInternal(tool, successCallback);
				}, failCallback);
				return;
			}
			throw new InvalidOperationException(string.Format("Tool {0} is not locked, cannot unlock it.", tool));
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002A1C File Offset: 0x00000C1C
		public void UnlockInternal(ITool tool, Action successCallback)
		{
			if (this._activeLockers.Remove(tool))
			{
				this._eventBus.Post(new ToolUnlockedEvent(tool));
				successCallback();
			}
		}

		// Token: 0x04000021 RID: 33
		public readonly EventBus _eventBus;

		// Token: 0x04000022 RID: 34
		public readonly ImmutableArray<IToolLocker> _toolLockers;

		// Token: 0x04000023 RID: 35
		public readonly Dictionary<ITool, IToolLocker> _activeLockers = new Dictionary<ITool, IToolLocker>();
	}
}
