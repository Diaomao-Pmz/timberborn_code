using System;
using Bindito.Core;
using Timberborn.Debugging;
using Timberborn.EntityPanelSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.ZiplineSystem;

namespace Timberborn.ZiplineSystemUI
{
	// Token: 0x02000012 RID: 18
	[Context("Game")]
	public class ZiplineSystemUIConfigurator : Configurator
	{
		// Token: 0x06000065 RID: 101 RVA: 0x0000323C File Offset: 0x0000143C
		public override void Configure()
		{
			base.Bind<ZiplineTowerPreview>().AsTransient();
			base.Bind<ZiplineTowerFragment>().AsSingleton();
			base.Bind<ZiplineConnectionAddingTool>().AsSingleton();
			base.Bind<ZiplineConnectionButtonFactory>().AsSingleton();
			base.Bind<ZiplinePreviewCableRenderer>().AsSingleton();
			base.Bind<ConnectionCandidates>().AsSingleton();
			base.Bind<ZiplinePreviewTooltip>().AsSingleton();
			base.MultiBind<IDevModule>().To<ZiplineConnectionDevModule>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<ZiplineSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(ZiplineSystemUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000032DC File Offset: 0x000014DC
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<ZiplineTower, ZiplineTowerPreview>();
			return builder.Build();
		}

		// Token: 0x02000013 RID: 19
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000068 RID: 104 RVA: 0x000032F6 File Offset: 0x000014F6
			public EntityPanelModuleProvider(ZiplineTowerFragment ziplineTowerFragment)
			{
				this._ziplineTowerFragment = ziplineTowerFragment;
			}

			// Token: 0x06000069 RID: 105 RVA: 0x00003305 File Offset: 0x00001505
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddMiddleFragment(this._ziplineTowerFragment, 0);
				return builder.Build();
			}

			// Token: 0x0400005F RID: 95
			public readonly ZiplineTowerFragment _ziplineTowerFragment;
		}
	}
}
