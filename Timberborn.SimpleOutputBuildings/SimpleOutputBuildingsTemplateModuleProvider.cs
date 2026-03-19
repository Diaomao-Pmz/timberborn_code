using System;
using Bindito.Core;
using Timberborn.InventoryNeedSystem;
using Timberborn.InventorySystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.SimpleOutputBuildings
{
	// Token: 0x02000008 RID: 8
	public class SimpleOutputBuildingsTemplateModuleProvider : IProvider<TemplateModule>
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000213D File Offset: 0x0000033D
		public SimpleOutputBuildingsTemplateModuleProvider(SimpleOutputInventoryInitializer simpleOutputInventoryInitializer)
		{
			this._simpleOutputInventoryInitializer = simpleOutputInventoryInitializer;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000214C File Offset: 0x0000034C
		public TemplateModule Get()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDedicatedDecorator<SimpleOutputInventory, Inventory>(this._simpleOutputInventoryInitializer);
			builder.AddDecorator<SimpleOutputInventorySpec, SimpleOutputInventory>();
			builder.AddDecorator<SimpleOutputInventory, InventoryNeedBehavior>();
			return builder.Build();
		}

		// Token: 0x04000008 RID: 8
		public readonly SimpleOutputInventoryInitializer _simpleOutputInventoryInitializer;
	}
}
