using System;

namespace Timberborn.BlockObjectModelSystem
{
	// Token: 0x0200000C RID: 12
	public interface IBlockObjectModel
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000052 RID: 82
		bool HasUndergroundModel { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000053 RID: 83
		int UndergroundModelDepth { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000054 RID: 84
		bool HasUncoveredModel { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000055 RID: 85
		bool UnfinishedConstructionModeModel { get; }

		// Token: 0x06000056 RID: 86
		void UpdateModelVisibility();
	}
}
