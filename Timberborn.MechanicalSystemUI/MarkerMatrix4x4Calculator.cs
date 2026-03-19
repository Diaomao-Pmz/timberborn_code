using System;
using Timberborn.Coordinates;
using Timberborn.MechanicalSystem;
using UnityEngine;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x0200000F RID: 15
	public class MarkerMatrix4x4Calculator
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002589 File Offset: 0x00000789
		public Matrix4x4 CalculateMatrixFrom(Transput transput)
		{
			return Matrix4x4.TRS(MarkerMatrix4x4Calculator.GetPosition(transput), transput.Direction.ToRotation(), Vector3.one);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025A8 File Offset: 0x000007A8
		public static Vector3 GetPosition(Transput transput)
		{
			Vector3Int coordinates = transput.Coordinates;
			Vector3Int vector3Int = transput.Direction.ToOffset();
			return CoordinateSystem.GridToWorld(new Vector3((float)coordinates.x + (float)vector3Int.x * 0.5f + 0.5f, (float)coordinates.y + (float)vector3Int.y * 0.5f + 0.5f, (float)coordinates.z + (float)vector3Int.z * 0.5f + 0.5f));
		}
	}
}
