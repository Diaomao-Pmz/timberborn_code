using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.WaterSourceSystem;

namespace Timberborn.MapEditorWaterSourceSystemUI
{
	// Token: 0x02000004 RID: 4
	[Context("MapEditor")]
	internal class MapEditorWaterSourceSystemConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		protected override void Configure()
		{
			base.Bind<WaterSourceFlowPreview>().AsTransient();
			base.Bind<BadwaterFlowStopper>().AsTransient();
			base.Bind<WaterSourceFlowPreviewFragment>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<MapEditorWaterSourceSystemConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(MapEditorWaterSourceSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000215F File Offset: 0x0000035F
		private static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<WaterSource, WaterSourceFlowPreview>();
			builder.AddDecorator<UndergroundWaterSourceSpec, BadwaterFlowStopper>();
			return builder.Build();
		}

		// Token: 0x02000009 RID: 9
		private class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600001D RID: 29 RVA: 0x0000239A File Offset: 0x0000059A
			public EntityPanelModuleProvider(WaterSourceFlowPreviewFragment waterSourceFlowPreviewFragment)
			{
				this._waterSourceFlowPreviewFragment = waterSourceFlowPreviewFragment;
			}

			// Token: 0x0600001E RID: 30 RVA: 0x000023A9 File Offset: 0x000005A9
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddMiddleFragment(this._waterSourceFlowPreviewFragment, 50);
				return builder.Build();
			}

			// Token: 0x04000014 RID: 20
			private readonly WaterSourceFlowPreviewFragment _waterSourceFlowPreviewFragment;
		}
	}
}
