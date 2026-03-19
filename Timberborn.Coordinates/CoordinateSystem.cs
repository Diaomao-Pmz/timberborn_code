using System;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.Coordinates
{
	// Token: 0x02000005 RID: 5
	public static class CoordinateSystem
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public static Vector3 WorldToGrid(Vector3 position)
		{
			return new Vector3(position.x, position.z, position.y);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D9 File Offset: 0x000002D9
		public static Vector3Int WorldToGridInt(Vector3 position)
		{
			return CoordinateSystem.WorldToGrid(position).FloorToInt();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E6 File Offset: 0x000002E6
		public static Ray WorldToGrid(Ray ray)
		{
			return new Ray(CoordinateSystem.WorldToGrid(ray.origin), CoordinateSystem.WorldToGrid(ray.direction));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002105 File Offset: 0x00000305
		public static Vector3 GridToWorld(Vector3Int coordinates)
		{
			return new Vector3((float)coordinates.x, (float)coordinates.z, (float)coordinates.y);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020C0 File Offset: 0x000002C0
		public static Vector3 GridToWorld(Vector3 coordinates)
		{
			return new Vector3(coordinates.x, coordinates.z, coordinates.y);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002124 File Offset: 0x00000324
		public static Vector3 GridToWorldCentered(Vector3Int coordinates)
		{
			return CoordinateSystem.CenterWorld(CoordinateSystem.GridToWorld(coordinates));
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002131 File Offset: 0x00000331
		public static Vector3 GridToWorldCentered(Vector3 coordinates)
		{
			return CoordinateSystem.CenterWorld(CoordinateSystem.GridToWorld(coordinates));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000213E File Offset: 0x0000033E
		public static Ray GridToWorld(Ray ray)
		{
			return new Ray(CoordinateSystem.GridToWorld(ray.origin), CoordinateSystem.GridToWorld(ray.direction));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000215D File Offset: 0x0000035D
		public static Vector3 CenterWorld(Vector3 position)
		{
			return position + new Vector3(0.5f, 0f, 0.5f);
		}
	}
}
