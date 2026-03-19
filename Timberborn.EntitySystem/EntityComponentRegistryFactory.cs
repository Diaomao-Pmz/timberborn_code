using System;

namespace Timberborn.EntitySystem
{
	// Token: 0x0200000B RID: 11
	public class EntityComponentRegistryFactory
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002619 File Offset: 0x00000819
		public EntityComponentRegistryFactory(RegisteredComponentService registeredComponentService)
		{
			this._registeredComponentService = registeredComponentService;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002628 File Offset: 0x00000828
		public EntityComponentRegistry Create()
		{
			return new EntityComponentRegistry(this._registeredComponentService);
		}

		// Token: 0x04000017 RID: 23
		public readonly RegisteredComponentService _registeredComponentService;
	}
}
