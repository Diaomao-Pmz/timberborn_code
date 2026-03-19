using System;
using UnityEngine;

namespace Timberborn.MapThumbnailCapturing
{
	// Token: 0x02000004 RID: 4
	public readonly struct CameraConfiguration
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public Vector3 Position { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C8 File Offset: 0x000002C8
		public Quaternion Rotation { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020D0 File Offset: 0x000002D0
		public float ShadowDistance { get; }

		// Token: 0x06000006 RID: 6 RVA: 0x000020D8 File Offset: 0x000002D8
		public CameraConfiguration(Vector3 position, Quaternion rotation, float shadowDistance)
		{
			this.Position = position;
			this.Rotation = rotation;
			this.ShadowDistance = shadowDistance;
		}
	}
}
