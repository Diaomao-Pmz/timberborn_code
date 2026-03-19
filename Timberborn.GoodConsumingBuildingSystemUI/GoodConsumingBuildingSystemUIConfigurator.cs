using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.GoodConsumingBuildingSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.GoodConsumingBuildingSystemUI
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	public class GoodConsumingBuildingSystemUIConfigurator : Configurator
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000025B8 File Offset: 0x000007B8
		public override void Configure()
		{
			base.Bind<GoodConsumingBuildingDescriber>().AsTransient();
			base.Bind<GoodConsumingBuildingFragment>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(GoodConsumingBuildingSystemUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<GoodConsumingBuildingSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000260B File Offset: 0x0000080B
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<GoodConsumingBuilding, GoodConsumingBuildingDescriber>();
			return builder.Build();
		}

		// Token: 0x02000009 RID: 9
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000023 RID: 35 RVA: 0x00002625 File Offset: 0x00000825
			public EntityPanelModuleProvider(GoodConsumingBuildingFragment goodConsumingBuildingFragment)
			{
				this._goodConsumingBuildingFragment = goodConsumingBuildingFragment;
			}

			// Token: 0x06000024 RID: 36 RVA: 0x00002634 File Offset: 0x00000834
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddBottomFragment(this._goodConsumingBuildingFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000023 RID: 35
			public readonly GoodConsumingBuildingFragment _goodConsumingBuildingFragment;
		}
	}
}
