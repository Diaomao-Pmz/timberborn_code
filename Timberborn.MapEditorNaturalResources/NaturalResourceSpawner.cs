using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Gathering;
using Timberborn.Growing;
using Timberborn.NaturalResources;
using UnityEngine;

namespace Timberborn.MapEditorNaturalResources
{
	// Token: 0x02000007 RID: 7
	public class NaturalResourceSpawner
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002400 File Offset: 0x00000600
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002408 File Offset: 0x00000608
		public bool RandomizeYieldGrowth { get; set; } = true;

		// Token: 0x06000017 RID: 23 RVA: 0x00002411 File Offset: 0x00000611
		public NaturalResourceSpawner(NaturalResourceFactory naturalResourceFactory, IRandomNumberGenerator randomNumberGenerator, IBlockService blockService)
		{
			this._naturalResourceFactory = naturalResourceFactory;
			this._randomNumberGenerator = randomNumberGenerator;
			this._blockService = blockService;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002438 File Offset: 0x00000638
		public void Spawn(IEnumerable<SpawnableResource> spawnableResources, Vector3Int coordinates)
		{
			if (!this._blockService.AnyObjectAt(coordinates))
			{
				SpawnableResource enumerableElement = this._randomNumberGenerator.GetEnumerableElement<SpawnableResource>(spawnableResources);
				NaturalResource naturalResource = this._naturalResourceFactory.SpawnIgnoringConstraintsAndRandomizePosition(enumerableElement.Id, coordinates);
				if (naturalResource != null)
				{
					bool mature = !enumerableElement.IsSeedling;
					this.SetGrowStage(naturalResource, mature);
					this.SetGatherableYieldGrowStage(naturalResource, mature);
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002494 File Offset: 0x00000694
		private void SetGrowStage(NaturalResource naturalResource, bool mature)
		{
			Growable component = naturalResource.GetComponent<Growable>();
			if (component != null)
			{
				float growthProgress = mature ? 1f : this._randomNumberGenerator.Range(0f, 0.8f);
				component.IncreaseGrowthProgress(growthProgress);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024D4 File Offset: 0x000006D4
		private void SetGatherableYieldGrowStage(NaturalResource naturalResource, bool mature)
		{
			if (mature)
			{
				GatherableYieldGrower component = naturalResource.GetComponent<GatherableYieldGrower>();
				if (component != null && component.GetComponent<Gatherable>().UsableWithCurrentFeatureToggles)
				{
					int num = this.RandomizeYieldGrowth ? this._randomNumberGenerator.Range(1, 100) : 100;
					component.FastForwardGrowth((float)num / 100f);
				}
			}
		}

		// Token: 0x04000009 RID: 9
		private readonly NaturalResourceFactory _naturalResourceFactory;

		// Token: 0x0400000A RID: 10
		private readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400000B RID: 11
		private readonly IBlockService _blockService;
	}
}
