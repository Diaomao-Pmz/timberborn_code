using System;
using Timberborn.SingletonSystem;

namespace Timberborn.TerrainPhysics
{
	// Token: 0x02000017 RID: 23
	public class TerrainPhysicsValidationEnabler : IPostLoadableSingleton
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000354F File Offset: 0x0000174F
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003557 File Offset: 0x00001757
		public bool Enabled { get; private set; }

		// Token: 0x0600006A RID: 106 RVA: 0x00003560 File Offset: 0x00001760
		public void PostLoad()
		{
			this.Enable();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003568 File Offset: 0x00001768
		public void Enable()
		{
			this.Enabled = true;
		}
	}
}
