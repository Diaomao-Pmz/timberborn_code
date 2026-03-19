using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.EntitySystem;

namespace Timberborn.BlockSystem
{
	// Token: 0x0200000E RID: 14
	public class BlockObjectBatchLoader
	{
		// Token: 0x06000072 RID: 114 RVA: 0x0000319C File Offset: 0x0000139C
		public void AddToServices(IEnumerable<EntityComponent> entities)
		{
			foreach (BlockObject blockObject2 in (from component in entities
			select component.GetComponent<BlockObject>() into blockObject
			where blockObject
			orderby blockObject.CoordinatesAtBaseZ.z
			select blockObject).ThenBy(new Func<BlockObject, int>(BlockObjectBatchLoader.GetHighestOccupation)).ToList<BlockObject>())
			{
				blockObject2.AddToServiceAfterLoad();
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000326C File Offset: 0x0000146C
		public static int GetHighestOccupation(BlockObject blockObject)
		{
			int num = 0;
			foreach (Block block in blockObject.Blocks.GetAllBlocks())
			{
				if (block.Coordinates.z == blockObject.CoordinatesAtBaseZ.z)
				{
					int blockHighestOccupation = BlockObjectBatchLoader.GetBlockHighestOccupation(block);
					if (blockHighestOccupation > num)
					{
						num = blockHighestOccupation;
					}
				}
			}
			return num;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000032D4 File Offset: 0x000014D4
		public static int GetBlockHighestOccupation(Block block)
		{
			BlockOccupations occupation = block.Occupation;
			if (occupation == BlockOccupations.None)
			{
				return 0;
			}
			if ((occupation & BlockOccupations.Top) != BlockOccupations.None)
			{
				return 6;
			}
			if ((occupation & BlockOccupations.Corners) != BlockOccupations.None)
			{
				return 5;
			}
			if ((occupation & BlockOccupations.Middle) != BlockOccupations.None)
			{
				return 4;
			}
			if ((occupation & BlockOccupations.Bottom) != BlockOccupations.None)
			{
				return 3;
			}
			if ((occupation & BlockOccupations.Path) != BlockOccupations.None)
			{
				return 2;
			}
			if ((occupation & BlockOccupations.Floor) != BlockOccupations.None)
			{
				return 1;
			}
			throw new ArgumentOutOfRangeException("occupation", occupation, "Unknown occupation type");
		}
	}
}
