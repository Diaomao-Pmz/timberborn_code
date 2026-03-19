using System;

namespace Timberborn.TickSystem
{
	// Token: 0x02000009 RID: 9
	public interface ITickableBucketService
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8
		int TotalNumberOfBuckets { get; }

		// Token: 0x06000009 RID: 9
		void AddEntity(TickableEntity tickableEntity);

		// Token: 0x0600000A RID: 10
		void RemoveEntity(TickableEntity tickableEntity);

		// Token: 0x0600000B RID: 11
		void TickBuckets(int numberOfBucketsToTick);

		// Token: 0x0600000C RID: 12
		void FinishFullTick();

		// Token: 0x0600000D RID: 13
		void TickOnce();
	}
}
