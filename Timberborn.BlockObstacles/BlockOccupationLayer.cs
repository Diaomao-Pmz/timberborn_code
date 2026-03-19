using System;
using System.Collections.Generic;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.BlockObstacles
{
	// Token: 0x02000008 RID: 8
	public class BlockOccupationLayer
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002180 File Offset: 0x00000380
		public int GridHeight { get; }

		// Token: 0x0600000B RID: 11 RVA: 0x00002188 File Offset: 0x00000388
		public BlockOccupationLayer(int gridHeight)
		{
			this.GridHeight = gridHeight;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021A2 File Offset: 0x000003A2
		public void AddBlockOccupier(BlockOccupier blockOccupier)
		{
			this._blockOccupiers.Add(blockOccupier);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021B0 File Offset: 0x000003B0
		public bool CanBeAddedToServices()
		{
			using (List<BlockOccupier>.Enumerator enumerator = this._blockOccupiers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.CanBeAddedToServices())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000220C File Offset: 0x0000040C
		public void AddToServices()
		{
			foreach (BlockOccupier blockOccupier in this._blockOccupiers)
			{
				blockOccupier.AddToServices();
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000225C File Offset: 0x0000045C
		public void RemoveFromServices()
		{
			foreach (BlockOccupier blockOccupier in this._blockOccupiers)
			{
				blockOccupier.RemoveFromServices();
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022AC File Offset: 0x000004AC
		public bool Contains(Vector2Int coordinates)
		{
			using (List<BlockOccupier>.Enumerator enumerator = this._blockOccupiers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.BlockObject.Coordinates.XY() == coordinates)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002318 File Offset: 0x00000518
		public void Remove()
		{
			this.RemoveFromServices();
			foreach (BlockOccupier blockOccupier in this._blockOccupiers)
			{
				Object.Destroy(blockOccupier.GameObject);
			}
		}

		// Token: 0x04000009 RID: 9
		public readonly List<BlockOccupier> _blockOccupiers = new List<BlockOccupier>();
	}
}
