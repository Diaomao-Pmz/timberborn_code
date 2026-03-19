using System;
using Bindito.Core;
using Timberborn.BottomBarSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.Fields;
using Timberborn.SimpleOutputBuildingsUI;
using Timberborn.TemplateInstantiation;
using Timberborn.YielderFinding;

namespace Timberborn.FieldsUI
{
	// Token: 0x0200000C RID: 12
	[Context("Game")]
	public class FieldsUIConfigurator : Configurator
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002620 File Offset: 0x00000820
		public override void Configure()
		{
			base.Bind<FarmHouseFragment>().AsSingleton();
			base.Bind<FieldsButton>().AsSingleton();
			base.Bind<FarmHouseToggleFactory>().AsSingleton();
			base.Bind<FarmHouseBatchControlRowItemFactory>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(FieldsUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<FieldsUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<BottomBarModule>().ToProvider<FieldsUIConfigurator.BottomBarModuleProvider>().AsSingleton();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000269C File Offset: 0x0000089C
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<FarmHouse, YieldStatus>();
			builder.AddDecorator<FarmHouse, SimpleOutputInventoryFragmentEnabler>();
			return builder.Build();
		}

		// Token: 0x0200000D RID: 13
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600002F RID: 47 RVA: 0x000026BC File Offset: 0x000008BC
			public EntityPanelModuleProvider(FarmHouseFragment farmHouseFragment)
			{
				this._farmHouseFragment = farmHouseFragment;
			}

			// Token: 0x06000030 RID: 48 RVA: 0x000026CB File Offset: 0x000008CB
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddMiddleFragment(this._farmHouseFragment, 1);
				return builder.Build();
			}

			// Token: 0x04000025 RID: 37
			public readonly FarmHouseFragment _farmHouseFragment;
		}

		// Token: 0x0200000E RID: 14
		public class BottomBarModuleProvider : IProvider<BottomBarModule>
		{
			// Token: 0x06000031 RID: 49 RVA: 0x000026E4 File Offset: 0x000008E4
			public BottomBarModuleProvider(FieldsButton fieldsButton)
			{
				this._fieldsButton = fieldsButton;
			}

			// Token: 0x06000032 RID: 50 RVA: 0x000026F3 File Offset: 0x000008F3
			public BottomBarModule Get()
			{
				BottomBarModule.Builder builder = new BottomBarModule.Builder();
				builder.AddLeftSectionElement(this._fieldsButton, 30);
				return builder.Build();
			}

			// Token: 0x04000026 RID: 38
			public readonly FieldsButton _fieldsButton;
		}
	}
}
