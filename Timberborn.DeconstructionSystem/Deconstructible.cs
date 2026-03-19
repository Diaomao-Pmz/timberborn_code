using System;
using Timberborn.BaseComponentSystem;

namespace Timberborn.DeconstructionSystem
{
	// Token: 0x02000008 RID: 8
	public class Deconstructible : BaseComponent
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002124 File Offset: 0x00000324
		public void DisableDeconstruction()
		{
			base.DisableComponent();
		}
	}
}
