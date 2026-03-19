using System;
using Timberborn.MapEditorTickSystem;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.TimeSystem
{
	// Token: 0x02000016 RID: 22
	[MapEditorTickable]
	public class TickProgressService : ILoadableSingleton, ILateUpdatableSingleton, ITickableSingleton, ITickProgressService
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002D5C File Offset: 0x00000F5C
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00002D64 File Offset: 0x00000F64
		public float Progress { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002D6D File Offset: 0x00000F6D
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00002D75 File Offset: 0x00000F75
		public float SecondsPassedThisTick { get; private set; }

		// Token: 0x06000093 RID: 147 RVA: 0x00002D7E File Offset: 0x00000F7E
		public TickProgressService(ITickService tickService)
		{
			this._tickService = tickService;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002D8D File Offset: 0x00000F8D
		public void Load()
		{
			this.UpdateTimestamp();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002D95 File Offset: 0x00000F95
		public void LateUpdateSingleton()
		{
			this.UpdateTickProgress();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002D8D File Offset: 0x00000F8D
		public void Tick()
		{
			this.UpdateTimestamp();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002D9D File Offset: 0x00000F9D
		public void UpdateTimestamp()
		{
			this._lastTickTimestamp = Time.time;
			this.SecondsPassedThisTick = 0f;
			this.Progress = 0f;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public void UpdateTickProgress()
		{
			this.SecondsPassedThisTick = Time.time - this._lastTickTimestamp;
			this.Progress = Mathf.Min(this.SecondsPassedThisTick / this._tickService.TickIntervalInSeconds, 1f);
		}

		// Token: 0x04000032 RID: 50
		public readonly ITickService _tickService;

		// Token: 0x04000033 RID: 51
		public float _lastTickTimestamp;
	}
}
