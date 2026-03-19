using System;
using System.Collections.Generic;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x02000021 RID: 33
	public class TransputMap : ILoadableSingleton
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000118 RID: 280 RVA: 0x00004500 File Offset: 0x00002700
		// (remove) Token: 0x06000119 RID: 281 RVA: 0x00004538 File Offset: 0x00002738
		public event EventHandler<Transput> TransputAdded;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600011A RID: 282 RVA: 0x00004570 File Offset: 0x00002770
		// (remove) Token: 0x0600011B RID: 283 RVA: 0x000045A8 File Offset: 0x000027A8
		public event EventHandler<Transput> TransputRemoved;

		// Token: 0x0600011C RID: 284 RVA: 0x000045DD File Offset: 0x000027DD
		public TransputMap(MapSize mapSize)
		{
			this._mapSize = mapSize;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000045EC File Offset: 0x000027EC
		public void Load()
		{
			this.InitializeTransputs();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000045F4 File Offset: 0x000027F4
		public void AddNode(MechanicalNode node)
		{
			foreach (Transput transput in node.Transputs)
			{
				this.SetTransput(transput);
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000462C File Offset: 0x0000282C
		public void RemoveNode(MechanicalNode node)
		{
			foreach (Transput transput in node.Transputs)
			{
				this.UnsetTransput(transput);
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004664 File Offset: 0x00002864
		public Transput GetFacingTransput(Transput transput)
		{
			if (this._mapSize.ContainsInTotal(transput.Target))
			{
				foreach (Transput transput2 in this.GetTransputsAtCoordinates(transput.Target))
				{
					if (transput2.Faces(transput))
					{
						return transput2;
					}
				}
			}
			return null;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000046DC File Offset: 0x000028DC
		public void InitializeTransputs()
		{
			int x = this._mapSize.TotalSize.x;
			int y = this._mapSize.TotalSize.y;
			int z = this._mapSize.TotalSize.z;
			this._transputs = new List<Transput>[x, y, z];
			for (int i = 0; i < x; i++)
			{
				for (int j = 0; j < y; j++)
				{
					for (int k = 0; k < z; k++)
					{
						this._transputs[i, j, k] = TransputMap.EmptyTransputs;
					}
				}
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000477C File Offset: 0x0000297C
		public void SetTransput(Transput transput)
		{
			Vector3Int coordinates = transput.Coordinates;
			List<Transput> list = this.GetTransputsAtCoordinates(coordinates);
			if (list == TransputMap.EmptyTransputs)
			{
				list = new List<Transput>();
				this._transputs[coordinates.x, coordinates.y, coordinates.z] = list;
			}
			list.Add(transput);
			EventHandler<Transput> transputAdded = this.TransputAdded;
			if (transputAdded == null)
			{
				return;
			}
			transputAdded(this, transput);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000047E0 File Offset: 0x000029E0
		public void UnsetTransput(Transput transput)
		{
			this.GetTransputsAtCoordinates(transput.Coordinates).Remove(transput);
			EventHandler<Transput> transputRemoved = this.TransputRemoved;
			if (transputRemoved == null)
			{
				return;
			}
			transputRemoved(this, transput);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004807 File Offset: 0x00002A07
		public List<Transput> GetTransputsAtCoordinates(Vector3Int coordinates)
		{
			return this._transputs[coordinates.x, coordinates.y, coordinates.z];
		}

		// Token: 0x04000068 RID: 104
		public static readonly List<Transput> EmptyTransputs = new List<Transput>();

		// Token: 0x0400006B RID: 107
		public readonly MapSize _mapSize;

		// Token: 0x0400006C RID: 108
		public List<Transput>[,,] _transputs;
	}
}
