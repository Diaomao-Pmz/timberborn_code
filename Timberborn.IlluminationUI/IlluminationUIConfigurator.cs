using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.Illumination;
using Timberborn.TemplateInstantiation;

namespace Timberborn.IlluminationUI
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	public class IlluminationUIConfigurator : Configurator
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002554 File Offset: 0x00000754
		public override void Configure()
		{
			base.Bind<NightTimeLightController>().AsTransient();
			base.Bind<CustomizableIlluminatorFragment>().AsSingleton();
			base.Bind<CustomizeIlluminationFragment>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(IlluminationUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<IlluminationUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025B3 File Offset: 0x000007B3
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<NightTimeLightControllerSpec, NightTimeLightController>();
			builder.AddDecorator<NightTimeLightController, Illuminator>();
			return builder.Build();
		}

		// Token: 0x0200000B RID: 11
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600001F RID: 31 RVA: 0x000025D3 File Offset: 0x000007D3
			public EntityPanelModuleProvider(CustomizableIlluminatorFragment customizableIlluminatorFragment, CustomizeIlluminationFragment customizeIlluminationFragment)
			{
				this._customizableIlluminatorFragment = customizableIlluminatorFragment;
				this._customizeIlluminationFragment = customizeIlluminationFragment;
			}

			// Token: 0x06000020 RID: 32 RVA: 0x000025E9 File Offset: 0x000007E9
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddRightHeaderFragment(this._customizeIlluminationFragment, 20);
				builder.AddBottomFragment(this._customizableIlluminatorFragment, 200);
				return builder.Build();
			}

			// Token: 0x04000017 RID: 23
			public readonly CustomizableIlluminatorFragment _customizableIlluminatorFragment;

			// Token: 0x04000018 RID: 24
			public readonly CustomizeIlluminationFragment _customizeIlluminationFragment;
		}
	}
}
