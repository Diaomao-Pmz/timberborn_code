using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x0200000F RID: 15
	public class NodeAnimation
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002871 File Offset: 0x00000A71
		public string Name { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002879 File Offset: 0x00000A79
		public int FrameCount { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002881 File Offset: 0x00000A81
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002889 File Offset: 0x00000A89
		public bool HasDifferentPositions { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002892 File Offset: 0x00000A92
		// (set) Token: 0x06000055 RID: 85 RVA: 0x0000289A File Offset: 0x00000A9A
		public bool HasDifferentRotations { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000028A3 File Offset: 0x00000AA3
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000028AB File Offset: 0x00000AAB
		public bool HasDifferentScales { get; private set; }

		// Token: 0x06000058 RID: 88 RVA: 0x000028B4 File Offset: 0x00000AB4
		public NodeAnimation(string name, int frameCount, Vector3[] positions, Quaternion[] rotations, Vector3[] scales, bool hasDifferentPositions, bool hasDifferentRotations, bool hasDifferentScales)
		{
			this.Name = name;
			this.FrameCount = frameCount;
			this._positions = positions;
			this._rotations = rotations;
			this._scales = scales;
			this.HasDifferentPositions = hasDifferentPositions;
			this.HasDifferentRotations = hasDifferentRotations;
			this.HasDifferentScales = hasDifferentScales;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002904 File Offset: 0x00000B04
		public static NodeAnimation Create(string name, int frameCount, Vector3[] positions, Quaternion[] rotations, Vector3[] scales)
		{
			bool hasDifferentPositions = NodeAnimation.HasDifferentValues(positions);
			bool hasDifferentRotations = NodeAnimation.HasDifferentValues(rotations);
			bool hasDifferentScales = NodeAnimation.HasDifferentValues(scales);
			return new NodeAnimation(name, frameCount, positions, rotations, scales, hasDifferentPositions, hasDifferentRotations, hasDifferentScales);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002935 File Offset: 0x00000B35
		public Vector3 GetPositionUnsafe(int frame)
		{
			return this._positions[frame];
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002943 File Offset: 0x00000B43
		public Quaternion GetRotationUnsafe(int frame)
		{
			return this._rotations[frame];
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002951 File Offset: 0x00000B51
		public Vector3 GetScaleUnsafe(int frame)
		{
			return this._scales[frame];
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002960 File Offset: 0x00000B60
		public static bool HasDifferentValues(IReadOnlyList<Vector3> input)
		{
			Vector3 vector = input[0];
			for (int i = 1; i < input.Count; i++)
			{
				if ((input[i] - vector).magnitude > NodeAnimation.DistanceDifferenceValidationThreshold)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000029A8 File Offset: 0x00000BA8
		public static bool HasDifferentValues(IReadOnlyList<Quaternion> input)
		{
			Quaternion quaternion = input[0];
			for (int i = 1; i < input.Count; i++)
			{
				if (Quaternion.Angle(input[i], quaternion) > NodeAnimation.AngleDifferenceValidationThreshold)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000017 RID: 23
		public static readonly float DistanceDifferenceValidationThreshold = 0.001f;

		// Token: 0x04000018 RID: 24
		public static readonly float AngleDifferenceValidationThreshold = 0.01f;

		// Token: 0x0400001E RID: 30
		public readonly Vector3[] _positions;

		// Token: 0x0400001F RID: 31
		public readonly Quaternion[] _rotations;

		// Token: 0x04000020 RID: 32
		public readonly Vector3[] _scales;
	}
}
