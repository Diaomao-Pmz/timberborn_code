using System;
using Bindito.Core;
using Timberborn.Beavers;
using Timberborn.BottomBarSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.BeaversUI
{
	// Token: 0x02000013 RID: 19
	[Context("Game")]
	public class BeaversUIConfigurator : Configurator
	{
		// Token: 0x06000055 RID: 85 RVA: 0x0000310C File Offset: 0x0000130C
		public override void Configure()
		{
			base.Bind<BeaverEntityBadge>().AsTransient();
			base.Bind<BeaverSelectionSound>().AsTransient();
			base.Bind<AdulthoodFragment>().AsSingleton();
			base.Bind<BeaverBuildingViewFactory>().AsSingleton();
			base.Bind<BeaverBuildingsFragment>().AsSingleton();
			base.Bind<BeaverGeneratorTool>().AsSingleton();
			base.Bind<BeaverGeneratorButton>().AsSingleton();
			base.Bind<BeaverBuildingsBatchControlRowItemFactory>().AsSingleton();
			base.Bind<AdulthoodBatchControlRowItemFactory>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BeaversUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<BeaversUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<BottomBarModule>().ToProvider<BeaversUIConfigurator.BottomBarModuleProvider>().AsSingleton();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000031C4 File Offset: 0x000013C4
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BeaverSpec, BeaverEntityBadge>();
			builder.AddDecorator<BeaverSpec, BeaverSelectionSound>();
			return builder.Build();
		}

		// Token: 0x02000014 RID: 20
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000058 RID: 88 RVA: 0x000031E4 File Offset: 0x000013E4
			public EntityPanelModuleProvider(AdulthoodFragment adulthoodFragment, BeaverBuildingsFragment beaverBuildingsFragment)
			{
				this._adulthoodFragment = adulthoodFragment;
				this._beaverBuildingsFragment = beaverBuildingsFragment;
			}

			// Token: 0x06000059 RID: 89 RVA: 0x000031FA File Offset: 0x000013FA
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._adulthoodFragment, 0);
				builder.AddTopFragment(this._beaverBuildingsFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000062 RID: 98
			public readonly AdulthoodFragment _adulthoodFragment;

			// Token: 0x04000063 RID: 99
			public readonly BeaverBuildingsFragment _beaverBuildingsFragment;
		}

		// Token: 0x02000015 RID: 21
		public class BottomBarModuleProvider : IProvider<BottomBarModule>
		{
			// Token: 0x0600005A RID: 90 RVA: 0x00003220 File Offset: 0x00001420
			public BottomBarModuleProvider(BeaverGeneratorButton beaverGeneratorButton)
			{
				this._beaverGeneratorButton = beaverGeneratorButton;
			}

			// Token: 0x0600005B RID: 91 RVA: 0x0000322F File Offset: 0x0000142F
			public BottomBarModule Get()
			{
				BottomBarModule.Builder builder = new BottomBarModule.Builder();
				builder.AddLeftSectionElement(this._beaverGeneratorButton, 70);
				return builder.Build();
			}

			// Token: 0x04000064 RID: 100
			public readonly BeaverGeneratorButton _beaverGeneratorButton;
		}
	}
}
