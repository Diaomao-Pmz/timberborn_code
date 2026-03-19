using System;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x02000022 RID: 34
	public class SpecialGameObjects
	{
		// Token: 0x060000EB RID: 235 RVA: 0x000051E4 File Offset: 0x000033E4
		public static bool GameObjectIsRoot(GameObject target)
		{
			MeshFilter meshFilter;
			return target.name.StartsWith(SpecialGameObjects.RootMarker) || (target.TryGetComponent<MeshFilter>(ref meshFilter) && meshFilter.sharedMesh.name.StartsWith(SpecialGameObjects.RootMarker));
		}

		// Token: 0x0400008E RID: 142
		public static readonly string RootMarker = "#";
	}
}
