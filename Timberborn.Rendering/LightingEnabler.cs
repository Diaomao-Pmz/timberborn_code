using System;
using Timberborn.BaseComponentSystem;

namespace Timberborn.Rendering
{
	// Token: 0x02000011 RID: 17
	public class LightingEnabler : BaseComponent, IStartableComponent
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002C33 File Offset: 0x00000E33
		public LightingEnabler(MaterialColorer materialColorer)
		{
			this._materialColorer = materialColorer;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002C44 File Offset: 0x00000E44
		public void Start()
		{
			this._materialColorer.EnableLighting(this, null);
		}

		// Token: 0x04000023 RID: 35
		public readonly MaterialColorer _materialColorer;
	}
}
