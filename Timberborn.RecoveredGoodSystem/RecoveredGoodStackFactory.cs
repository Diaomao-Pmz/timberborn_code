using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.Goods;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using UnityEngine;

namespace Timberborn.RecoveredGoodSystem
{
	// Token: 0x02000014 RID: 20
	public class RecoveredGoodStackFactory : ILoadableSingleton
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000323D File Offset: 0x0000143D
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003245 File Offset: 0x00001445
		public BlockSpec GoodStackBlockSpec { get; private set; }

		// Token: 0x06000081 RID: 129 RVA: 0x0000324E File Offset: 0x0000144E
		public RecoveredGoodStackFactory(BlockObjectFactory blockObjectFactory, IRandomNumberGenerator randomNumberGenerator, TemplateService templateService)
		{
			this._blockObjectFactory = blockObjectFactory;
			this._randomNumberGenerator = randomNumberGenerator;
			this._templateService = templateService;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000326B File Offset: 0x0000146B
		public void Load()
		{
			this._recoveredGoodStackTemplate = this._templateService.GetSingle<RecoveredGoodStackSpec>().GetSpec<BlockObjectSpec>();
			this.GoodStackBlockSpec = this._recoveredGoodStackTemplate.Blocks.Single<BlockSpec>();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000329C File Offset: 0x0000149C
		public void Create(Vector3Int coordinate, IEnumerable<GoodAmount> recoveredGoods)
		{
			RecoveredGoodStack component = this._blockObjectFactory.CreateFinished(this._recoveredGoodStackTemplate, new Placement(coordinate)).GetComponent<RecoveredGoodStack>();
			component.SetInitialGoods(recoveredGoods);
			this.RandomizeRotation(component);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000032D4 File Offset: 0x000014D4
		public void RandomizeRotation(RecoveredGoodStack recoveredGoodStack)
		{
			int rotation = this._randomNumberGenerator.Range(0, 360);
			recoveredGoodStack.GetComponent<RecoveredGoodStackModel>().SetRotation(rotation);
		}

		// Token: 0x04000044 RID: 68
		public readonly BlockObjectFactory _blockObjectFactory;

		// Token: 0x04000045 RID: 69
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000046 RID: 70
		public readonly TemplateService _templateService;

		// Token: 0x04000047 RID: 71
		public BlockObjectSpec _recoveredGoodStackTemplate;
	}
}
