using System;
using System.Collections.Generic;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.Planting
{
	// Token: 0x0200001F RID: 31
	public class PlantingMap
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00003703 File Offset: 0x00001903
		public PlantingMap(Vector3Int size)
		{
			this._size = size;
			this._resourceIds = new string[size.x, size.y, size.z];
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003732 File Offset: 0x00001932
		public IEnumerable<Vector3Int> GetCoordinatesWithSetResource()
		{
			int num;
			for (int x = 0; x < this._size.x; x = num + 1)
			{
				for (int y = 0; y < this._size.y; y = num + 1)
				{
					for (int z = 0; z < this._size.z; z = num + 1)
					{
						if (this._resourceIds[x, y, z] != null)
						{
							yield return new Vector3Int(x, y, z);
						}
						num = z;
					}
					num = y;
				}
				num = x;
			}
			yield break;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003742 File Offset: 0x00001942
		public string GetResource(Vector3Int coordinates)
		{
			if (!this.Contains(coordinates))
			{
				return null;
			}
			return this._resourceIds[coordinates.x, coordinates.y, coordinates.z];
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003770 File Offset: 0x00001970
		public void SetResource(IEnumerable<Vector3Int> coordinates, string resource)
		{
			foreach (Vector3Int coordinates2 in coordinates)
			{
				this.SetResource(coordinates2, resource);
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000037BC File Offset: 0x000019BC
		public void SetResourceIfEmpty(IEnumerable<Vector3Int> coordinates, string resource)
		{
			foreach (Vector3Int coordinates2 in coordinates)
			{
				if (this.GetResource(coordinates2) == null)
				{
					this.SetResource(coordinates2, resource);
				}
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003810 File Offset: 0x00001A10
		public void SetResource(Vector3Int coordinates, string resource)
		{
			if (this.Contains(coordinates))
			{
				this._resourceIds[coordinates.x, coordinates.y, coordinates.z] = resource;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000383C File Offset: 0x00001A3C
		public void UnsetResource(Vector3Int coordinates)
		{
			if (this.Contains(coordinates))
			{
				this._resourceIds[coordinates.x, coordinates.y, coordinates.z] = null;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003868 File Offset: 0x00001A68
		public bool Contains(Vector3Int coordinates)
		{
			return Sizing.SizeContains(this._size, coordinates);
		}

		// Token: 0x04000053 RID: 83
		public Vector3Int _size;

		// Token: 0x04000054 RID: 84
		public readonly string[,,] _resourceIds;
	}
}
