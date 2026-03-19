using System;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x0200000A RID: 10
	public readonly struct Block
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000025DF File Offset: 0x000007DF
		public Vector3Int Coordinates { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000025E7 File Offset: 0x000007E7
		public MatterBelow MatterBelow { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000025EF File Offset: 0x000007EF
		public BlockOccupations Occupation { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000025F7 File Offset: 0x000007F7
		public BlockStackable Stackable { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000025FF File Offset: 0x000007FF
		public bool OccupyAllBelow { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002607 File Offset: 0x00000807
		public bool OptionallyUnderground { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000260F File Offset: 0x0000080F
		public bool Underground { get; }

		// Token: 0x0600001F RID: 31 RVA: 0x00002617 File Offset: 0x00000817
		public Block(Vector3Int coordinates, MatterBelow matterBelow, BlockOccupations occupation, BlockStackable stackable, bool occupyAllBelow, bool optionallyUnderground, bool underground)
		{
			this.Coordinates = coordinates;
			this.MatterBelow = matterBelow;
			this.Occupation = occupation;
			this.Stackable = stackable;
			this.OccupyAllBelow = occupyAllBelow;
			this.OptionallyUnderground = optionallyUnderground;
			this.Underground = underground;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000264E File Offset: 0x0000084E
		public static Block From(Vector3Int coordinates, BlockSpec blockSpec)
		{
			return new Block(coordinates, blockSpec.MatterBelow, blockSpec.Occupations, blockSpec.Stackable, blockSpec.OccupyAllBelow, false, blockSpec.Underground);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002675 File Offset: 0x00000875
		public static Block From(Vector3Int coordinates, Block block)
		{
			return new Block(coordinates, block.MatterBelow, block.Occupation, block.Stackable, block.OccupyAllBelow, block.OptionallyUnderground, block.Underground);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026A7 File Offset: 0x000008A7
		public static Block FullFrom(Vector3Int coordinates)
		{
			return new Block(coordinates, MatterBelow.Any, BlockOccupations.All, BlockStackable.None, false, true, false);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000026B5 File Offset: 0x000008B5
		public bool IsOccupied
		{
			get
			{
				return this.Occupation > BlockOccupations.None;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000026C0 File Offset: 0x000008C0
		public bool IsFoundationBlock
		{
			get
			{
				return this.MatterBelow.IsSolidMatter();
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026CD File Offset: 0x000008CD
		public bool IsIntersecting(Block other)
		{
			return this.Coordinates == other.Coordinates && this.Occupation.Intersects(other.Occupation);
		}
	}
}
