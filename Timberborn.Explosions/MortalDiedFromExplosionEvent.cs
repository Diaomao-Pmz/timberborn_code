using System;
using Timberborn.BaseComponentSystem;

namespace Timberborn.Explosions
{
	// Token: 0x02000015 RID: 21
	public class MortalDiedFromExplosionEvent
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003962 File Offset: 0x00001B62
		public BaseComponent Source { get; }

		// Token: 0x06000081 RID: 129 RVA: 0x0000396A File Offset: 0x00001B6A
		public MortalDiedFromExplosionEvent(BaseComponent source)
		{
			this.Source = source;
		}
	}
}
