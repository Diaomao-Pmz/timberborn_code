using System;
using UnityEngine;

namespace Timberborn.LevelVisibilitySystem
{
	// Token: 0x02000005 RID: 5
	public interface ILevelVisibilityService
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000004 RID: 4
		// (remove) Token: 0x06000005 RID: 5
		event EventHandler<int> MaxVisibleLevelChanged;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6
		int MaxVisibleLevel { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7
		bool LevelIsAtMin { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8
		bool LevelIsAtMax { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9
		bool TerrainLevelIsAtMax { get; }

		// Token: 0x0600000A RID: 10
		void SetMaxVisibleLevel(int newMaxVisibleLevel);

		// Token: 0x0600000B RID: 11
		void ResetMaxVisibleLevel();

		// Token: 0x0600000C RID: 12
		bool BlockIsVisible(Vector3Int coordinates);

		// Token: 0x0600000D RID: 13
		void SetLevelsWithAnythingHidable(int minLevel, int maxLevel);

		// Token: 0x0600000E RID: 14
		void ResetLevelsWithAnythingHidable();
	}
}
