using System;
using Timberborn.BlockSystem;
using UnityEngine;

namespace Timberborn.StatusSystem
{
	// Token: 0x0200000B RID: 11
	public interface IStatusIconOffsetter
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000020 RID: 32
		float TopBound { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000021 RID: 33
		float FinishedTopBound { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000022 RID: 34
		Vector3 Position { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000023 RID: 35
		Vector2Int Key { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000024 RID: 36
		bool StatusActive { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37
		BlockObject BlockObject { get; }

		// Token: 0x06000026 RID: 38
		void UpdateIcon();

		// Token: 0x06000027 RID: 39
		float GetUnfinishedTopBound();
	}
}
