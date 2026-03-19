using System;
using Bindito.Core;
using Timberborn.Rendering;
using Timberborn.TemplateInstantiation;

namespace Timberborn.DecalSystem
{
	// Token: 0x02000012 RID: 18
	[Context("Game")]
	public class DecalSystemConfigurator : Configurator
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00002E14 File Offset: 0x00001014
		public override void Configure()
		{
			base.Bind<DecalSupplier>().AsTransient();
			base.Bind<DecalSupplierBuildingIcon>().AsTransient();
			base.Bind<FlippableDecal>().AsTransient();
			base.Bind<IDecalService>().To<DecalService>().AsSingleton();
			base.Bind<UserDecalService>().AsSingleton();
			base.Bind<UserDecalTextureRepository>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(DecalSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002E8B File Offset: 0x0000108B
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<DecalSupplierBuildingIconSpec, DecalSupplierBuildingIcon>();
			builder.AddDecorator<DecalSupplierBuildingIcon, EntityMaterials>();
			builder.AddDecorator<DecalSupplierSpec, DecalSupplier>();
			builder.AddDecorator<FlippableDecalSpec, FlippableDecal>();
			return builder.Build();
		}
	}
}
