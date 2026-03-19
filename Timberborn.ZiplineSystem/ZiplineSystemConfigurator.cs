using System;
using Bindito.Core;
using Timberborn.BuildingsNavigation;
using Timberborn.Illumination;
using Timberborn.SelectionSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x0200001B RID: 27
	[Context("Game")]
	public class ZiplineSystemConfigurator : Configurator
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00004094 File Offset: 0x00002294
		public override void Configure()
		{
			base.Bind<CableBlock>().AsTransient();
			base.Bind<Cable>().AsTransient();
			base.Bind<ZiplineTower>().AsTransient();
			base.Bind<ZiplineTowerAnimationController>().AsTransient();
			base.Bind<ZiplineTowerIlluminator>().AsTransient();
			base.Bind<ZiplineTowerOperationValidator>().AsTransient();
			base.Bind<ZiplineTowerRegistry>().AsSingleton();
			base.Bind<ZiplineConnectionService>().AsSingleton();
			base.Bind<ZiplineConnectionBlockFactory>().AsSingleton();
			base.Bind<ZiplineGroupService>().AsSingleton();
			base.Bind<ZiplineCableRenderer>().AsSingleton();
			base.Bind<ZiplineCableNavMesh>().AsSingleton();
			base.Bind<BresenhamLineDrawer>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(ZiplineSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000415C File Offset: 0x0000235C
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<CableSpec, Cable>();
			builder.AddDecorator<Cable, HighlightableObject>();
			builder.AddDecorator<CableBlockSpec, CableBlock>();
			builder.AddDecorator<ZiplineTowerSpec, ZiplineTower>();
			builder.AddDecorator<ZiplineTower, ZiplineTowerOperationValidator>();
			builder.AddDecorator<ZiplineTower, ZiplineTowerAnimationController>();
			builder.AddDecorator<ZiplineTower, Illuminator>();
			builder.AddDecorator<ZiplineTower, ZiplineTowerIlluminator>();
			builder.AddDecorator<ZiplineTower, PathMeshHider>();
			builder.AddDecorator<ZiplineTower, PathDistrictRetriever>();
			return builder.Build();
		}
	}
}
