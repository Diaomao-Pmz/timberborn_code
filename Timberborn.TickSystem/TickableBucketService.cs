using System;

namespace Timberborn.TickSystem
{
	// Token: 0x0200000F RID: 15
	public class TickableBucketService : ITickableBucketService
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002160 File Offset: 0x00000360
		public TickableBucketService(ITickableSingletonService tickableSingletonService)
		{
			this._tickableSingletonService = tickableSingletonService;
			this._tickableEntityBuckets = new TickableEntityBucket[TickableBucketService.NumberOfEntityBuckets];
			for (int i = 0; i < this._tickableEntityBuckets.Length; i++)
			{
				this._tickableEntityBuckets[i] = new TickableEntityBucket();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000021AA File Offset: 0x000003AA
		public int TotalNumberOfBuckets
		{
			get
			{
				return TickableBucketService.NumberOfEntityBuckets + TickableBucketService.NumberOfSingletonBuckets;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000021B7 File Offset: 0x000003B7
		public void AddEntity(TickableEntity tickableEntity)
		{
			this.GetEntityBucket(tickableEntity).Add(tickableEntity);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000021C6 File Offset: 0x000003C6
		public void RemoveEntity(TickableEntity tickableEntity)
		{
			this.GetEntityBucket(tickableEntity).Remove(tickableEntity);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000021D5 File Offset: 0x000003D5
		public void TickBuckets(int numberOfBucketsToTick)
		{
			while (numberOfBucketsToTick-- > 0)
			{
				this.TickNextBucket();
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000021E8 File Offset: 0x000003E8
		public void FinishFullTick()
		{
			while (this._nextBucketIndex != 0)
			{
				this.TickNextBucket();
			}
			this._tickableSingletonService.ForceFinishParallelTick();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002205 File Offset: 0x00000405
		public void TickOnce()
		{
			if (this._nextBucketIndex != 0)
			{
				this.FinishFullTick();
			}
			this.TickNextBucket();
			this.FinishFullTick();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002224 File Offset: 0x00000424
		public TickableEntityBucket GetEntityBucket(TickableEntity tickableEntity)
		{
			int entityBucketIndex = TickableBucketService.GetEntityBucketIndex(tickableEntity.EntityId);
			return this._tickableEntityBuckets[entityBucketIndex];
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002248 File Offset: 0x00000448
		public static int GetEntityBucketIndex(Guid entityId)
		{
			double num = (double)Math.Abs(entityId.GetHashCode());
			long num2 = (long)((ulong)int.MinValue);
			return (int)(num / (double)num2 * (double)TickableBucketService.NumberOfEntityBuckets);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000227A File Offset: 0x0000047A
		public void TickNextBucket()
		{
			if (this._nextBucketIndex < TickableBucketService.NumberOfSingletonBuckets)
			{
				this._tickableSingletonService.TickAll();
			}
			else
			{
				this._tickableEntityBuckets[this._nextBucketIndex - TickableBucketService.NumberOfSingletonBuckets].TickAll();
			}
			this.MoveNextTickedBucketIndex();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000022B4 File Offset: 0x000004B4
		public void MoveNextTickedBucketIndex()
		{
			this._nextBucketIndex = this.NextBucketIndex(this._nextBucketIndex);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000022C8 File Offset: 0x000004C8
		public int NextBucketIndex(int bucketIndex)
		{
			if (++bucketIndex < this.TotalNumberOfBuckets)
			{
				return bucketIndex;
			}
			return 0;
		}

		// Token: 0x0400000B RID: 11
		public static readonly int NumberOfEntityBuckets = 128;

		// Token: 0x0400000C RID: 12
		public static readonly int NumberOfSingletonBuckets = 1;

		// Token: 0x0400000D RID: 13
		public readonly ITickableSingletonService _tickableSingletonService;

		// Token: 0x0400000E RID: 14
		public readonly TickableEntityBucket[] _tickableEntityBuckets;

		// Token: 0x0400000F RID: 15
		public int _nextBucketIndex;
	}
}
