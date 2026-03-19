using System;
using Timberborn.SingletonSystem;

namespace Timberborn.TickSystem
{
	// Token: 0x0200001B RID: 27
	public class TickOnlyArrayService : ILoadableSingleton, ITickableSingleton
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002BDB File Offset: 0x00000DDB
		public TickOnlyArrayService(ITickableSingletonService tickableSingletonService)
		{
			this._tickableSingletonService = tickableSingletonService;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002BEA File Offset: 0x00000DEA
		public bool AllowEdit
		{
			get
			{
				return this._forceAllowAccess || this._tickableSingletonService.ParalleTicklIsFinished || this._isLoadPhase;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002C09 File Offset: 0x00000E09
		public bool AllowFullAccess
		{
			get
			{
				return this._tickableSingletonService.IsStartingParallelTick;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002C16 File Offset: 0x00000E16
		public void Load()
		{
			this._isLoadPhase = true;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002C1F File Offset: 0x00000E1F
		public void Tick()
		{
			this._isLoadPhase = false;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002C28 File Offset: 0x00000E28
		public TickOnlyArray<T> Create<T>(int size)
		{
			return new TickOnlyArray<T>(size, this);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002C31 File Offset: 0x00000E31
		public void ForceAllowAccess()
		{
			this._forceAllowAccess = true;
		}

		// Token: 0x0400003B RID: 59
		public readonly ITickableSingletonService _tickableSingletonService;

		// Token: 0x0400003C RID: 60
		public bool _isLoadPhase;

		// Token: 0x0400003D RID: 61
		public bool _forceAllowAccess;
	}
}
