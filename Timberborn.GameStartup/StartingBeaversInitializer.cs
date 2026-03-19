using System;
using Timberborn.Beavers;
using Timberborn.NewGameConfigurationSystem;
using UnityEngine;

namespace Timberborn.GameStartup
{
	// Token: 0x0200000D RID: 13
	public class StartingBeaversInitializer
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000243E File Offset: 0x0000063E
		public StartingBeaversInitializer(BeaverFactory beaverFactory)
		{
			this._beaverFactory = beaverFactory;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000244D File Offset: 0x0000064D
		public void Initialize(Vector3 position, int startingAdults, MinMaxSpec<float> adultAgeProgress, int startingChildren, MinMaxSpec<float> childAgeProgress)
		{
			this.SpawnBeavers(position, true, startingAdults, adultAgeProgress);
			this.SpawnBeavers(position, false, startingChildren, childAgeProgress);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002468 File Offset: 0x00000668
		public void SpawnBeavers(Vector3 position, bool adults, int numberOfBeavers, MinMaxSpec<float> lifeStageProgressRange)
		{
			float num = (numberOfBeavers > 1) ? ((lifeStageProgressRange.Max - lifeStageProgressRange.Min) / (float)(numberOfBeavers - 1)) : 0f;
			for (int i = 0; i < numberOfBeavers; i++)
			{
				float lifeStageProgress = lifeStageProgressRange.Min + num * (float)i;
				this.SpawnBeaver(position, adults, lifeStageProgress);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024B7 File Offset: 0x000006B7
		public void SpawnBeaver(Vector3 position, bool adult, float lifeStageProgress)
		{
			if (adult)
			{
				this._beaverFactory.CreateAdult(position, lifeStageProgress);
				return;
			}
			this._beaverFactory.CreateChild(position, lifeStageProgress);
		}

		// Token: 0x0400001D RID: 29
		public readonly BeaverFactory _beaverFactory;
	}
}
