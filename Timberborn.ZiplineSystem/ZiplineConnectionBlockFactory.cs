using System;
using System.Collections.Immutable;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Coordinates;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x02000014 RID: 20
	public class ZiplineConnectionBlockFactory : ILoadableSingleton
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000033F7 File Offset: 0x000015F7
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000033FF File Offset: 0x000015FF
		public BlockObjectSpec ZiplineConnectionBlock { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003408 File Offset: 0x00001608
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003410 File Offset: 0x00001610
		public BlockOccupations ZiplineConnectionOccupation { get; private set; }

		// Token: 0x06000087 RID: 135 RVA: 0x00003419 File Offset: 0x00001619
		public ZiplineConnectionBlockFactory(BlockObjectFactory blockObjectFactory, ISpecService specService)
		{
			this._blockObjectFactory = blockObjectFactory;
			this._specService = specService;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003430 File Offset: 0x00001630
		public void Load()
		{
			this.ZiplineConnectionBlock = this._specService.GetBlueprint(ZiplineConnectionBlockFactory.ZiplineConnectionBlockPath).GetSpec<BlockObjectSpec>();
			ImmutableArray<BlockSpec> blocks = this.ZiplineConnectionBlock.Blocks;
			if (blocks.Length != 1)
			{
				throw new InvalidOperationException("Zipline connection block must be 1x1x1 in size");
			}
			this.ZiplineConnectionOccupation = blocks[0].Occupations;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000348C File Offset: 0x0000168C
		public BlockObject CreateConnection(Transform parent, Vector3Int gridPosition)
		{
			BlockObject blockObject = this._blockObjectFactory.CreateAsPreview(this.ZiplineConnectionBlock, parent, new Placement(gridPosition));
			blockObject.GameObject.name = string.Format("{0} {1}", "ZiplineConnectionBlockPath", gridPosition);
			blockObject.MarkAsFinishedAndAddToServices();
			return blockObject;
		}

		// Token: 0x0400002C RID: 44
		public static readonly string ZiplineConnectionBlockPath = "Models/ZiplineCable/ZiplineConnectionBlock.blueprint";

		// Token: 0x0400002F RID: 47
		public readonly BlockObjectFactory _blockObjectFactory;

		// Token: 0x04000030 RID: 48
		public readonly ISpecService _specService;
	}
}
