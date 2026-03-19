using System;
using System.Collections.Generic;
using Timberborn.MapStateSystem;
using UnityEngine;

namespace Timberborn.GridTraversing
{
	// Token: 0x02000005 RID: 5
	public class GridTraversal
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020FD File Offset: 0x000002FD
		public GridTraversal(MapSize mapSize)
		{
			this._mapSize = mapSize;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000210C File Offset: 0x0000030C
		public IEnumerable<TraversedCoordinates> TraverseRay(Ray ray)
		{
			int levelAboveMaxBuildingLevel = this._mapSize.TotalSize.z + 2;
			Vector3 origin = ray.origin;
			Vector3 direction = ray.direction.normalized;
			if (origin.z > (float)levelAboveMaxBuildingLevel)
			{
				Plane plane;
				plane..ctor(new Vector3(0f, 0f, -1f), (float)levelAboveMaxBuildingLevel);
				float num;
				if (plane.Raycast(ray, ref num))
				{
					origin = ray.GetPoint(num);
				}
			}
			if (origin.z < -1f)
			{
				Plane plane2;
				plane2..ctor(new Vector3(0f, 0f, -1f), -1f);
				float num2;
				if (plane2.Raycast(ray, ref num2))
				{
					origin = ray.GetPoint(num2);
				}
			}
			int x = Mathf.FloorToInt(origin.x);
			int y = Mathf.FloorToInt(origin.y);
			int z = Mathf.FloorToInt(origin.z);
			int stepX = Math.Sign(direction.x);
			int stepY = Math.Sign(direction.y);
			int stepZ = Math.Sign(direction.z);
			double tMaxX = GridTraversal.Intbound((double)origin.x, (double)direction.x);
			double tMaxY = GridTraversal.Intbound((double)origin.y, (double)direction.y);
			double tMaxZ = GridTraversal.Intbound((double)origin.z, (double)direction.z);
			double tDeltaX = (direction.x == 0f) ? double.PositiveInfinity : ((double)((float)stepX / direction.x));
			double tDeltaY = (direction.y == 0f) ? double.PositiveInfinity : ((double)((float)stepY / direction.y));
			double tDeltaZ = (direction.z == 0f) ? double.PositiveInfinity : ((double)((float)stepZ / direction.z));
			int num4;
			for (int iteration = 0; iteration < 10000; iteration = num4)
			{
				double num3;
				Vector3Int face;
				if (tMaxX < tMaxZ)
				{
					if (tMaxX < tMaxY)
					{
						num3 = tMaxX;
						x += stepX;
						tMaxX += tDeltaX;
						face..ctor(-stepX, 0, 0);
					}
					else
					{
						num3 = tMaxY;
						y += stepY;
						tMaxY += tDeltaY;
						face..ctor(0, -stepY, 0);
					}
				}
				else if (tMaxZ < tMaxY)
				{
					num3 = tMaxZ;
					z += stepZ;
					tMaxZ += tDeltaZ;
					face..ctor(0, 0, -stepZ);
				}
				else
				{
					num3 = tMaxY;
					y += stepY;
					tMaxY += tDeltaY;
					face..ctor(0, -stepY, 0);
				}
				if (z < -1 || z > levelAboveMaxBuildingLevel)
				{
					yield break;
				}
				Vector3Int coordinates;
				coordinates..ctor(x, y, z);
				Vector3 intersection = origin + direction * (float)num3;
				yield return new TraversedCoordinates(coordinates, face, intersection);
				num4 = iteration + 1;
			}
			yield break;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002123 File Offset: 0x00000323
		public static double Intbound(double s, double ds)
		{
			if (ds == 0.0)
			{
				return double.PositiveInfinity;
			}
			return ((ds > 0.0) ? (Math.Ceiling(s) - s) : (s - Math.Floor(s))) / Math.Abs(ds);
		}

		// Token: 0x04000006 RID: 6
		public readonly MapSize _mapSize;
	}
}
