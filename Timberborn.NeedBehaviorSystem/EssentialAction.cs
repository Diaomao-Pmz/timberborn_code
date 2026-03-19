using System;
using Timberborn.Effects;
using UnityEngine;

namespace Timberborn.NeedBehaviorSystem
{
	// Token: 0x02000017 RID: 23
	public readonly struct EssentialAction
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000304D File Offset: 0x0000124D
		public Vector3 Position { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003055 File Offset: 0x00001255
		public ContinuousEffect Effect { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000305D File Offset: 0x0000125D
		public float MinDurationInHours { get; }

		// Token: 0x0600006A RID: 106 RVA: 0x00003065 File Offset: 0x00001265
		public EssentialAction(Vector3 position, ContinuousEffect effect, float minDurationInHours)
		{
			this.Position = position;
			this.Effect = effect;
			this.MinDurationInHours = minDurationInHours;
		}
	}
}
