using System;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x0200004C RID: 76
	public static class NavigationCoordinateSystem
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00005346 File Offset: 0x00003546
		public static Vector3 GridToWorld(Vector3Int coordinates)
		{
			return CoordinateSystem.GridToWorldCentered(coordinates);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005350 File Offset: 0x00003550
		public static Vector3Int WorldToGridInt(Vector3 position)
		{
			Vector3 vector;
			vector..ctor(0f, 0.1f, 0f);
			return CoordinateSystem.WorldToGridInt(position + vector);
		}
	}
}
