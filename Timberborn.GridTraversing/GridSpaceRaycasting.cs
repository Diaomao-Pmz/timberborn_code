using System;
using UnityEngine;

namespace Timberborn.GridTraversing
{
	// Token: 0x02000004 RID: 4
	public static class GridSpaceRaycasting
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public static Vector3? HitHorizontalPlane(Ray ray, float height)
		{
			Plane plane;
			plane..ctor(Vector3.back, height);
			float num;
			if (plane.Raycast(ray, ref num))
			{
				return new Vector3?(ray.GetPoint(num));
			}
			return null;
		}
	}
}
