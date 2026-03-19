using System;
using UnityEngine;

namespace Timberborn.CharacterMovementSystem
{
	// Token: 0x02000007 RID: 7
	public readonly struct AnimatedPathCorner
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public Vector3 Position { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public float Time { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002110 File Offset: 0x00000310
		public float Speed { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002118 File Offset: 0x00000318
		public float DistanceToPathCorner { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002120 File Offset: 0x00000320
		public int GroupId { get; }

		// Token: 0x0600000C RID: 12 RVA: 0x00002128 File Offset: 0x00000328
		public AnimatedPathCorner(Vector3 position, float time, float speed, float distanceToPathCorner, int groupId)
		{
			this.Position = position;
			this.Time = time;
			this.Speed = speed;
			this.DistanceToPathCorner = distanceToPathCorner;
			this.GroupId = groupId;
		}
	}
}
