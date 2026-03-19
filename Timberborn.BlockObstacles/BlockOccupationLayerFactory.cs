using System;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using UnityEngine;

namespace Timberborn.BlockObstacles
{
	// Token: 0x02000009 RID: 9
	public class BlockOccupationLayerFactory : ILoadableSingleton
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002374 File Offset: 0x00000574
		public BlockOccupationLayerFactory(BlockObjectFactory blockObjectFactory, TemplateService templateService)
		{
			this._blockObjectFactory = blockObjectFactory;
			this._templateService = templateService;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000238A File Offset: 0x0000058A
		public void Load()
		{
			this._blockOccupierTemplate = this._templateService.GetSingle<BlockOccupierSpec>().GetSpec<BlockObjectSpec>();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023A4 File Offset: 0x000005A4
		public BlockOccupationLayer Create(Transform parent, Vector3 anchorPosition, int gridHeight, Vector2 layerSize)
		{
			BlockOccupationLayer blockOccupationLayer = new BlockOccupationLayer(gridHeight);
			int num = 0;
			while ((float)num < layerSize.x)
			{
				int num2 = 0;
				while ((float)num2 < layerSize.y)
				{
					Vector3 occupierLocalPosition;
					occupierLocalPosition..ctor(anchorPosition.x + (float)num, 0f, anchorPosition.z + (float)num2);
					BlockOccupier blockOccupier = this.CreateBlockOccupier(parent, gridHeight, occupierLocalPosition);
					blockOccupationLayer.AddBlockOccupier(blockOccupier);
					num2++;
				}
				num++;
			}
			return blockOccupationLayer;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002410 File Offset: 0x00000610
		public BlockOccupier CreateBlockOccupier(Transform parent, int gridHeight, Vector3 occupierLocalPosition)
		{
			Vector3Int occupierWorldGridPosition = BlockOccupationLayerFactory.GetOccupierWorldGridPosition(parent, gridHeight, occupierLocalPosition);
			return this.CreateBlockOccupier(parent, occupierWorldGridPosition);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002430 File Offset: 0x00000630
		public static Vector3Int GetOccupierWorldGridPosition(Transform parent, int layerHeight, Vector3 localBlockOccupierPosition)
		{
			Vector3Int result = CoordinateSystem.WorldToGridInt(parent.TransformPoint(localBlockOccupierPosition));
			result.z = layerHeight;
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002453 File Offset: 0x00000653
		public BlockOccupier CreateBlockOccupier(Transform parent, Vector3Int worldBlockOccupierGridPosition)
		{
			BlockObject blockObject = this._blockObjectFactory.CreateAsPreview(this._blockOccupierTemplate, parent, new Placement(worldBlockOccupierGridPosition));
			blockObject.GameObject.name = string.Format("{0} {1}", "BlockOccupier", worldBlockOccupierGridPosition);
			return blockObject.GetComponent<BlockOccupier>();
		}

		// Token: 0x0400000A RID: 10
		public readonly BlockObjectFactory _blockObjectFactory;

		// Token: 0x0400000B RID: 11
		public readonly TemplateService _templateService;

		// Token: 0x0400000C RID: 12
		public BlockObjectSpec _blockOccupierTemplate;
	}
}
