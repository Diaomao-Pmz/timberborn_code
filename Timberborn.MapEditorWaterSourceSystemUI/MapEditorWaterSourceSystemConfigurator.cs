using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.WaterSourceSystem;

namespace Timberborn.MapEditorWaterSourceSystemUI
{
	// Token: 0x02000005 RID: 5
	[Context("MapEditor")]
	public class MapEditorWaterSourceSystemConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public override void Configure()
		{
			base.Bind<WaterSourceFlowPreview>().AsTransient();
			base.Bind<BadwaterFlowStopper>().AsTransient();
			base.Bind<WaterSourceFlowPreviewFragment>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<MapEditorWaterSourceSystemConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(MapEditorWaterSourceSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000215F File Offset: 0x0000035F
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<WaterSource, WaterSourceFlowPreview>();
			builder.AddDecorator<UndergroundWaterSourceSpec, BadwaterFlowStopper>();
			return builder.Build();
		}

		// Token: 0x02000006 RID: 6
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600000A RID: 10 RVA: 0x0000217F File Offset: 0x0000037F
			public EntityPanelModuleProvider(WaterSourceFlowPreviewFragment waterSourceFlowPreviewFragment)
			{
				this._waterSourceFlowPreviewFragment = waterSourceFlowPreviewFragment;
			}

			// Token: 0x0600000B RID: 11 RVA: 0x0000218E File Offset: 0x0000038E
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddMiddleFragment(this._waterSourceFlowPreviewFragment, 50);
				return builder.Build();
			}

			// Token: 0x04000008 RID: 8
			public readonly WaterSourceFlowPreviewFragment _waterSourceFlowPreviewFragment;
		}
	}
}
