using System;
using System.Diagnostics;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.TickSystem
{
	// Token: 0x02000018 RID: 24
	public class Ticker : ILoadableSingleton
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002A15 File Offset: 0x00000C15
		public Ticker(ITickService tickService, ITickableBucketService tickableBucketService)
		{
			this._tickService = tickService;
			this._tickableBucketService = tickableBucketService;
			this._stopwatch = new Stopwatch();
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002A38 File Offset: 0x00000C38
		public double LengthOfLastTickInSeconds
		{
			get
			{
				return this._stopwatch.Elapsed.TotalSeconds;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002A58 File Offset: 0x00000C58
		public void Load()
		{
			this._secondsPerBucket = this._tickService.TickIntervalInSeconds / (float)this._tickableBucketService.TotalNumberOfBuckets;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002A78 File Offset: 0x00000C78
		public void Update(float deltaTimeInSeconds)
		{
			int numberOfBucketsToTick = this.GetNumberOfBucketsToTick(deltaTimeInSeconds);
			this._stopwatch.Restart();
			this._tickableBucketService.TickBuckets(numberOfBucketsToTick);
			this._stopwatch.Stop();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002AAF File Offset: 0x00000CAF
		public void FinishFullTick()
		{
			this._tickableBucketService.FinishFullTick();
			this._accumulatedDeltaTime = 0f;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002AC7 File Offset: 0x00000CC7
		public void TickOnce()
		{
			this._tickableBucketService.TickOnce();
			this._accumulatedDeltaTime = 0f;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public int GetNumberOfBucketsToTick(float deltaTimeInSeconds)
		{
			this._accumulatedDeltaTime += deltaTimeInSeconds;
			int num = Mathf.FloorToInt(this._accumulatedDeltaTime / this._secondsPerBucket);
			this._accumulatedDeltaTime -= (float)num * this._secondsPerBucket;
			return num;
		}

		// Token: 0x04000033 RID: 51
		public readonly ITickService _tickService;

		// Token: 0x04000034 RID: 52
		public readonly ITickableBucketService _tickableBucketService;

		// Token: 0x04000035 RID: 53
		public readonly Stopwatch _stopwatch;

		// Token: 0x04000036 RID: 54
		public float _secondsPerBucket;

		// Token: 0x04000037 RID: 55
		public float _accumulatedDeltaTime;
	}
}
