using System;
using Bindito.Core;
using Timberborn.BottomBarSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.Forestry;
using Timberborn.SimpleOutputBuildingsUI;
using Timberborn.TemplateInstantiation;
using Timberborn.YielderFinding;

namespace Timberborn.ForestryUI
{
	// Token: 0x0200000D RID: 13
	[Context("Game")]
	public class ForestryUIConfigurator : Configurator
	{
		// Token: 0x06000027 RID: 39 RVA: 0x0000257C File Offset: 0x0000077C
		public override void Configure()
		{
			base.Bind<ForestryButton>().AsSingleton();
			base.Bind<TreeCuttingAreaSelectionTool>().AsSingleton();
			base.Bind<TreeCuttingAreaUnselectionTool>().AsSingleton();
			base.Bind<TreeCuttingAreaButton>().AsSingleton();
			base.Bind<ForesterFragment>().AsSingleton();
			base.Bind<ForesterBatchControlRowItemFactory>().AsSingleton();
			base.Bind<TreeCuttingAreaVisualizer>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(ForestryUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<BottomBarModule>().ToProvider<ForestryUIConfigurator.BottomBarModuleProvider>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<ForestryUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000261C File Offset: 0x0000081C
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<LumberjackFlagSpec, YieldStatus>();
			builder.AddDecorator<LumberjackFlagSpec, SimpleOutputInventoryFragmentEnabler>();
			return builder.Build();
		}

		// Token: 0x0200000E RID: 14
		public class BottomBarModuleProvider : IProvider<BottomBarModule>
		{
			// Token: 0x0600002A RID: 42 RVA: 0x0000263C File Offset: 0x0000083C
			public BottomBarModuleProvider(ForestryButton forestryButton, TreeCuttingAreaButton treeCuttingAreaButton)
			{
				this._forestryButton = forestryButton;
				this._treeCuttingAreaButton = treeCuttingAreaButton;
			}

			// Token: 0x0600002B RID: 43 RVA: 0x00002652 File Offset: 0x00000852
			public BottomBarModule Get()
			{
				BottomBarModule.Builder builder = new BottomBarModule.Builder();
				builder.AddLeftSectionElement(this._treeCuttingAreaButton, 20);
				builder.AddLeftSectionElement(this._forestryButton, 40);
				return builder.Build();
			}

			// Token: 0x0400001E RID: 30
			public readonly ForestryButton _forestryButton;

			// Token: 0x0400001F RID: 31
			public readonly TreeCuttingAreaButton _treeCuttingAreaButton;
		}

		// Token: 0x0200000F RID: 15
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600002C RID: 44 RVA: 0x0000267A File Offset: 0x0000087A
			public EntityPanelModuleProvider(ForesterFragment foresterFragment)
			{
				this._foresterFragment = foresterFragment;
			}

			// Token: 0x0600002D RID: 45 RVA: 0x00002689 File Offset: 0x00000889
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddMiddleFragment(this._foresterFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000020 RID: 32
			public readonly ForesterFragment _foresterFragment;
		}
	}
}
