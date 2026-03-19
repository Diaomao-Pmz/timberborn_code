using System;
using UnityEngine;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x02000012 RID: 18
	public static class ZiplineCalculator
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00003278 File Offset: 0x00001478
		public static ValueTuple<Vector3, Vector3> CalculateWorldConnections(Vector3 start, Vector3 end)
		{
			Vector3 normalized = Vector3.ProjectOnPlane((end - start).normalized, Vector3.up).normalized;
			Vector3 vector = start + normalized * ZiplineCalculator.FrontDistance;
			Vector3 vector2 = end - normalized * ZiplineCalculator.FrontDistance;
			Vector3 vector3 = Vector3.Cross(Vector3.up, normalized).normalized * ZiplineCalculator.SideDistance;
			Vector3 item = vector + vector3;
			Vector3 item2 = vector2 + vector3;
			return new ValueTuple<Vector3, Vector3>(item, item2);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003300 File Offset: 0x00001500
		public static ValueTuple<Vector3, Vector3> CalculateGridAnchors(Vector3 start, Vector3 end)
		{
			Vector3 normalized = Vector3.ProjectOnPlane((end - start).normalized, new Vector3(0f, 0f, 1f)).normalized;
			Vector3 item = start + normalized * ZiplineCalculator.FrontDistance;
			Vector3 item2 = end - normalized * ZiplineCalculator.FrontDistance;
			return new ValueTuple<Vector3, Vector3>(item, item2);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003368 File Offset: 0x00001568
		public static Vector3 CalculateTurn(Vector3 turnStart, Vector3 turnEnd, Vector3 start)
		{
			Vector3 item = ZiplineCalculator.CalculateWorldConnections(turnStart, turnEnd).Item1;
			Vector3 normalized = new Vector3(item.z - start.z, 0f, start.x - item.x).normalized;
			return turnStart + normalized * ZiplineCalculator.TurningPointOffset;
		}

		// Token: 0x04000028 RID: 40
		public static readonly float FrontDistance = 0.38f;

		// Token: 0x04000029 RID: 41
		public static readonly float SideDistance = 0.175f;

		// Token: 0x0400002A RID: 42
		public static readonly float TurningPointOffset = 0.45f;
	}
}
