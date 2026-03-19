using System;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000067 RID: 103
	public class NodeIdService : ILoadableSingleton
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000733B File Offset: 0x0000553B
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00007343 File Offset: 0x00005543
		public int NumberOfNodes { get; private set; }

		// Token: 0x06000224 RID: 548 RVA: 0x0000734C File Offset: 0x0000554C
		public NodeIdService(INavMeshSizeProvider navMeshSizeProvider)
		{
			this._navMeshSizeProvider = navMeshSizeProvider;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000735C File Offset: 0x0000555C
		public void Load()
		{
			Vector3Int size = this._navMeshSizeProvider.Size;
			this._size = size + 2 * NodeIdService.Boundary;
			this.NumberOfNodes = this._size.x * this._size.y * this._size.z;
			this._minCoords = -NodeIdService.Boundary;
			this._maxCoords = size - new Vector3Int(1, 1, 1) + NodeIdService.Boundary;
			this.InitializeIdToCoordinatesTable();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000073EC File Offset: 0x000055EC
		public Vector3Int IdToGrid(int nodeId)
		{
			Vector3Int result;
			try
			{
				result = this._idToCoordinatesTable[nodeId];
			}
			catch (Exception)
			{
				throw new ArgumentOutOfRangeException(string.Format("Coordinates {0} of node {1} are out of map", this.IdToGridSlow(nodeId), nodeId));
			}
			return result;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000743C File Offset: 0x0000563C
		public Vector3 IdToWorld(int nodeId)
		{
			return NavigationCoordinateSystem.GridToWorld(this.IdToGrid(nodeId));
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000744C File Offset: 0x0000564C
		public int GridToId(Vector3Int coordinates)
		{
			Vector3Int vector3Int = coordinates + NodeIdService.Boundary;
			int x = vector3Int.x;
			int y = vector3Int.y;
			int z = vector3Int.z;
			return x * this._size.y * this._size.z + y * this._size.z + z;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000074A4 File Offset: 0x000056A4
		public int WorldToId(Vector3 position)
		{
			return this.GridToId(NavigationCoordinateSystem.WorldToGridInt(position));
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000074B2 File Offset: 0x000056B2
		public float Distance(int fromNodeId, int toNodeId)
		{
			return Vector3.Distance(this.IdToGrid(fromNodeId), this.IdToGrid(toNodeId));
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000074D1 File Offset: 0x000056D1
		public bool Contains(Vector3 worldPosition)
		{
			return this.Contains(NavigationCoordinateSystem.WorldToGridInt(worldPosition));
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000074E0 File Offset: 0x000056E0
		public bool Contains(Vector3Int navMeshCoordinates)
		{
			int x = navMeshCoordinates.x;
			int y = navMeshCoordinates.y;
			int z = navMeshCoordinates.z;
			return x >= this._minCoords.x && x <= this._maxCoords.x && y >= this._minCoords.y && y <= this._maxCoords.y && z >= this._minCoords.z && z <= this._maxCoords.z;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00007560 File Offset: 0x00005760
		public void InitializeIdToCoordinatesTable()
		{
			this._idToCoordinatesTable = new Vector3Int[this.NumberOfNodes];
			int num = 0;
			for (int i = this._minCoords.x; i <= this._maxCoords.x; i++)
			{
				for (int j = this._minCoords.y; j <= this._maxCoords.y; j++)
				{
					for (int k = this._minCoords.z; k <= this._maxCoords.z; k++)
					{
						this._idToCoordinatesTable[num++] = new Vector3Int(i, j, k);
					}
				}
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000075F8 File Offset: 0x000057F8
		public Vector3Int IdToGridSlow(int nodeId)
		{
			int num = nodeId % this._size.z;
			int num2 = nodeId / this._size.z % this._size.y;
			return new Vector3Int(nodeId / (this._size.y * this._size.z), num2, num) - NodeIdService.Boundary;
		}

		// Token: 0x04000102 RID: 258
		public static readonly Vector3Int Boundary = new Vector3Int(1, 1, 1);

		// Token: 0x04000104 RID: 260
		public readonly INavMeshSizeProvider _navMeshSizeProvider;

		// Token: 0x04000105 RID: 261
		public Vector3Int _size;

		// Token: 0x04000106 RID: 262
		public Vector3Int _minCoords;

		// Token: 0x04000107 RID: 263
		public Vector3Int _maxCoords;

		// Token: 0x04000108 RID: 264
		public Vector3Int[] _idToCoordinatesTable;
	}
}
