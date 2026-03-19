using System;
using Timberborn.BaseComponentSystem;
using Timberborn.LifeSystem;

namespace Timberborn.Bots
{
	// Token: 0x0200000C RID: 12
	public class BotLongevity : BaseComponent, ILongevity
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002391 File Offset: 0x00000591
		public float ExpectedLongevity
		{
			get
			{
				return 1f;
			}
		}
	}
}
