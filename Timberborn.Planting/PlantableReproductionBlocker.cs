using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.NaturalResourcesReproduction;
using UnityEngine;

namespace Timberborn.Planting
{
	// Token: 0x0200000D RID: 13
	public class PlantableReproductionBlocker : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002495 File Offset: 0x00000695
		public PlantableReproductionBlocker(PlantingService plantingService)
		{
			this._plantingService = plantingService;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024A4 File Offset: 0x000006A4
		public void Awake()
		{
			this._reproducible = base.GetComponent<Reproducible>();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024B4 File Offset: 0x000006B4
		public void Start()
		{
			Vector3Int coordinates = base.GetComponent<BlockObject>().Coordinates;
			if (this._plantingService.IsResourceAt(coordinates))
			{
				this.BlockReproduction();
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024E1 File Offset: 0x000006E1
		public void BlockReproduction()
		{
			this._reproducible.BlockReproduction(this);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024EF File Offset: 0x000006EF
		public void UnblockReproduction()
		{
			this._reproducible.UnblockReproduction(this);
		}

		// Token: 0x04000014 RID: 20
		public readonly PlantingService _plantingService;

		// Token: 0x04000015 RID: 21
		public Reproducible _reproducible;
	}
}
