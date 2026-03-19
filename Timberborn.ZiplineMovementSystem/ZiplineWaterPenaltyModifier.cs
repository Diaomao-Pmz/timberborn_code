using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WalkingSystem;

namespace Timberborn.ZiplineMovementSystem
{
	// Token: 0x02000010 RID: 16
	public class ZiplineWaterPenaltyModifier : BaseComponent, IAwakableComponent, IWaterPenaltyModifier
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003240 File Offset: 0x00001440
		public float WaterPenaltyModifier
		{
			get
			{
				if (!this._ziplineVisitor.IsOnZipline)
				{
					return 1f;
				}
				return 0.5f;
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000325A File Offset: 0x0000145A
		public void Awake()
		{
			this._ziplineVisitor = base.GetComponent<ZiplineVisitor>();
		}

		// Token: 0x04000034 RID: 52
		public ZiplineVisitor _ziplineVisitor;
	}
}
