using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.Coordinates;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000069 RID: 105
	public struct WorldBlock
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00008383 File Offset: 0x00006583
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0000838B File Offset: 0x0000658B
		public BlockObject Floor { readonly get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00008394 File Offset: 0x00006594
		// (set) Token: 0x060002BA RID: 698 RVA: 0x0000839C File Offset: 0x0000659C
		public BlockObject Bottom { readonly get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002BB RID: 699 RVA: 0x000083A5 File Offset: 0x000065A5
		// (set) Token: 0x060002BC RID: 700 RVA: 0x000083AD File Offset: 0x000065AD
		public BlockObject Top { readonly get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002BD RID: 701 RVA: 0x000083B6 File Offset: 0x000065B6
		// (set) Token: 0x060002BE RID: 702 RVA: 0x000083BE File Offset: 0x000065BE
		public BlockObject Corners { readonly get; private set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002BF RID: 703 RVA: 0x000083C7 File Offset: 0x000065C7
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x000083CF File Offset: 0x000065CF
		public BlockObject Path { readonly get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x000083D8 File Offset: 0x000065D8
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x000083E0 File Offset: 0x000065E0
		public BlockObject Middle { readonly get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x000083E9 File Offset: 0x000065E9
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x000083F1 File Offset: 0x000065F1
		public BlockObject Underground { readonly get; private set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x000083FA File Offset: 0x000065FA
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x00008402 File Offset: 0x00006602
		public Directions2D Entrances { readonly get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000840B File Offset: 0x0000660B
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x00008413 File Offset: 0x00006613
		public BlockOccupations NonOverridableBlockOccupations { readonly get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000841C File Offset: 0x0000661C
		public bool HasAnyObject
		{
			get
			{
				return this._blockOccupations != BlockOccupations.None || this.Underground;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00008433 File Offset: 0x00006633
		public ReadOnlyList<BlockObject> BlockObjects
		{
			get
			{
				List<BlockObject> blockObjects = this._blockObjects;
				if (blockObjects == null)
				{
					return WorldBlock.EmptyList;
				}
				return blockObjects.AsReadOnlyList<BlockObject>();
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000844C File Offset: 0x0000664C
		public void SetBlockObject(BlockObject blockObject, Block block)
		{
			BlockOccupations occupation = block.Occupation;
			if ((occupation != BlockOccupations.None || block.Underground) && (this._blockObjects == null || !this._blockObjects.Contains(blockObject)))
			{
				if (this._blockObjects == null)
				{
					this._blockObjects = new List<BlockObject>();
				}
				this._blockObjects.Add(blockObject);
			}
			this._blockOccupations |= occupation;
			if (!blockObject.Overridable)
			{
				this.NonOverridableBlockOccupations |= occupation;
			}
			this.SetBlockObject(blockObject, block, null);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000084D0 File Offset: 0x000066D0
		public void UnsetBlockObject(BlockObject blockObject, Block block)
		{
			List<BlockObject> blockObjects = this._blockObjects;
			if (blockObjects != null)
			{
				blockObjects.Remove(blockObject);
			}
			BlockOccupations occupation = block.Occupation;
			this._blockOccupations &= ~occupation;
			if (!blockObject.Overridable)
			{
				this.NonOverridableBlockOccupations &= ~occupation;
			}
			this.SetBlockObject(null, block, blockObject);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00008528 File Offset: 0x00006728
		public void AddEntrance(Direction2D direction2D)
		{
			Directions2D directions2D = direction2D.ToDirections();
			this.Entrances |= directions2D;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000854C File Offset: 0x0000674C
		public void DeleteEntrance(Direction2D direction2D)
		{
			Directions2D directions2D = direction2D.ToDirections();
			this.Entrances &= ~directions2D;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00008570 File Offset: 0x00006770
		public void GetIntersectingObjects(BlockOccupations occupations, List<BlockObject> result)
		{
			bool flag = occupations.Intersects(BlockOccupations.Top) && this.Top;
			if (flag)
			{
				result.Add(this.Top);
			}
			bool flag2 = occupations.Intersects(BlockOccupations.Bottom) && this.Bottom && (!flag || this.Bottom != this.Top);
			if (flag2)
			{
				result.Add(this.Bottom);
			}
			bool flag3 = occupations.Intersects(BlockOccupations.Floor) && this.Floor && (!flag || this.Floor != this.Top) && (!flag2 || this.Floor != this.Bottom);
			if (flag3)
			{
				result.Add(this.Floor);
			}
			bool flag4 = occupations.Intersects(BlockOccupations.Corners) && this.Corners && (!flag || this.Corners != this.Top) && (!flag2 || this.Corners != this.Bottom) && (!flag3 || this.Corners != this.Floor);
			if (flag4)
			{
				result.Add(this.Corners);
			}
			bool flag5 = occupations.Intersects(BlockOccupations.Path) && this.Path && (!flag || this.Path != this.Top) && (!flag2 || this.Path != this.Bottom) && (!flag3 || this.Path != this.Floor) && (!flag4 || this.Path != this.Corners);
			if (flag5)
			{
				result.Add(this.Path);
			}
			if (occupations.Intersects(BlockOccupations.Middle) && this.Middle && (!flag || this.Middle != this.Top) && (!flag2 || this.Middle != this.Bottom) && (!flag3 || this.Middle != this.Floor) && (!flag4 || this.Middle != this.Corners) && (!flag5 || this.Middle != this.Path))
			{
				result.Add(this.Middle);
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00008794 File Offset: 0x00006994
		public void SetBlockObject(BlockObject newBlockObject, Block block, BlockObject expectedBlockObject)
		{
			BlockOccupations occupation = block.Occupation;
			if (occupation.Intersects(BlockOccupations.Top))
			{
				WorldBlock.ValidateSetBlockObject(newBlockObject, BlockOccupations.Top, expectedBlockObject, this.Top);
				this.Top = newBlockObject;
			}
			if (occupation.Intersects(BlockOccupations.Bottom))
			{
				WorldBlock.ValidateSetBlockObject(newBlockObject, BlockOccupations.Bottom, expectedBlockObject, this.Bottom);
				this.Bottom = newBlockObject;
			}
			if (occupation.Intersects(BlockOccupations.Floor))
			{
				WorldBlock.ValidateSetBlockObject(newBlockObject, BlockOccupations.Floor, expectedBlockObject, this.Floor);
				this.Floor = newBlockObject;
			}
			if (occupation.Intersects(BlockOccupations.Corners))
			{
				WorldBlock.ValidateSetBlockObject(newBlockObject, BlockOccupations.Corners, expectedBlockObject, this.Corners);
				this.Corners = newBlockObject;
			}
			if (occupation.Intersects(BlockOccupations.Path))
			{
				WorldBlock.ValidateSetBlockObject(newBlockObject, BlockOccupations.Path, expectedBlockObject, this.Path);
				this.Path = newBlockObject;
			}
			if (occupation.Intersects(BlockOccupations.Middle))
			{
				WorldBlock.ValidateSetBlockObject(newBlockObject, BlockOccupations.Middle, expectedBlockObject, this.Middle);
				this.Middle = newBlockObject;
			}
			if (block.Underground)
			{
				this.ValidateSetBlockObjectUnderground(newBlockObject, expectedBlockObject);
				this.Underground = newBlockObject;
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00008878 File Offset: 0x00006A78
		public static void ValidateSetBlockObject(BlockObject newBlockObject, BlockOccupations blockOccupations, BlockObject expectedBlockObject, BlockObject currentBlockObject)
		{
			if (currentBlockObject != expectedBlockObject)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					string.Format("Can't set {0} on {1}, ", WorldBlock.FormatBlockObject(newBlockObject), blockOccupations),
					"there should be ",
					WorldBlock.FormatBlockObject(expectedBlockObject),
					", but is ",
					WorldBlock.FormatBlockObject(currentBlockObject)
				}));
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000088D8 File Offset: 0x00006AD8
		public void ValidateSetBlockObjectUnderground(BlockObject newBlockObject, BlockObject expectedBlockObject)
		{
			if (this.Underground != expectedBlockObject)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Can't set ",
					WorldBlock.FormatBlockObject(newBlockObject),
					" on Underground, there should be ",
					WorldBlock.FormatBlockObject(expectedBlockObject),
					", but is ",
					WorldBlock.FormatBlockObject(this.Underground)
				}));
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00008937 File Offset: 0x00006B37
		public static string FormatBlockObject(BlockObject blockObject)
		{
			if (!blockObject)
			{
				return "null";
			}
			return blockObject.ToString();
		}

		// Token: 0x04000151 RID: 337
		public static readonly ReadOnlyList<BlockObject> EmptyList = new List<BlockObject>().AsReadOnlyList<BlockObject>();

		// Token: 0x0400015B RID: 347
		public List<BlockObject> _blockObjects;

		// Token: 0x0400015C RID: 348
		public BlockOccupations _blockOccupations;
	}
}
