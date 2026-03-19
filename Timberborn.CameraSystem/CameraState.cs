using System;
using UnityEngine;

namespace Timberborn.CameraSystem
{
	// Token: 0x0200000E RID: 14
	public readonly struct CameraState : IEquatable<CameraState>
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000369C File Offset: 0x0000189C
		public Vector3 Target { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000036A4 File Offset: 0x000018A4
		public float ZoomLevel { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000036AC File Offset: 0x000018AC
		public float HorizontalAngle { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000036B4 File Offset: 0x000018B4
		public float VerticalAngle { get; }

		// Token: 0x06000083 RID: 131 RVA: 0x000036BC File Offset: 0x000018BC
		public CameraState(Vector3 target, float zoomLevel, float horizontalAngle, float verticalAngle)
		{
			this.Target = target;
			this.ZoomLevel = zoomLevel;
			this.HorizontalAngle = horizontalAngle;
			this.VerticalAngle = verticalAngle;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000036DC File Offset: 0x000018DC
		public bool Equals(CameraState other)
		{
			return this.Target.Equals(other.Target) && this.ZoomLevel.Equals(other.ZoomLevel) && this.HorizontalAngle.Equals(other.HorizontalAngle) && this.VerticalAngle.Equals(other.VerticalAngle);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003748 File Offset: 0x00001948
		public override bool Equals(object obj)
		{
			if (obj is CameraState)
			{
				CameraState other = (CameraState)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000376D File Offset: 0x0000196D
		public override int GetHashCode()
		{
			return HashCode.Combine<Vector3, float, float, float>(this.Target, this.ZoomLevel, this.HorizontalAngle, this.VerticalAngle);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000378C File Offset: 0x0000198C
		public static bool operator ==(CameraState left, CameraState right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003796 File Offset: 0x00001996
		public static bool operator !=(CameraState left, CameraState right)
		{
			return !left.Equals(right);
		}
	}
}
