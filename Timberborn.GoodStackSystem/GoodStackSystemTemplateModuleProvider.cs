using System;
using Bindito.Core;
using Timberborn.InventorySystem;
using Timberborn.Rendering;
using Timberborn.TemplateInstantiation;
using Timberborn.WorkSystem;

namespace Timberborn.GoodStackSystem
{
	// Token: 0x02000014 RID: 20
	public class GoodStackSystemTemplateModuleProvider : IProvider<TemplateModule>
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002BE2 File Offset: 0x00000DE2
		public GoodStackSystemTemplateModuleProvider(GoodStackInventoryInitializer goodStackInventoryInitializer)
		{
			this._goodStackInventoryInitializer = goodStackInventoryInitializer;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002BF1 File Offset: 0x00000DF1
		public TemplateModule Get()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<GoodStack, GoodStackAccessible>();
			builder.AddDecorator<GoodStack, EntityMaterials>();
			builder.AddDecorator<GoodStack, GoodStackModel>();
			builder.AddDecorator<Worker, GoodStackRetrieverBehavior>();
			builder.AddDedicatedDecorator<IGoodStackInventory, Inventory>(this._goodStackInventoryInitializer);
			return builder.Build();
		}

		// Token: 0x04000031 RID: 49
		public readonly GoodStackInventoryInitializer _goodStackInventoryInitializer;
	}
}
