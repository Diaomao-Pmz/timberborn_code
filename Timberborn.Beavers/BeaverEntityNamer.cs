using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntityNaming;

namespace Timberborn.Beavers
{
	// Token: 0x0200000B RID: 11
	public class BeaverEntityNamer : BaseComponent, IEntityNamer
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000022CD File Offset: 0x000004CD
		public BeaverEntityNamer(BeaverNameService beaverNameService)
		{
			this._beaverNameService = beaverNameService;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000022DC File Offset: 0x000004DC
		public int EntityNamerPriority
		{
			get
			{
				return 20;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022E0 File Offset: 0x000004E0
		public string GenerateEntityName()
		{
			return this._beaverNameService.RandomName();
		}

		// Token: 0x0400000B RID: 11
		public readonly BeaverNameService _beaverNameService;
	}
}
