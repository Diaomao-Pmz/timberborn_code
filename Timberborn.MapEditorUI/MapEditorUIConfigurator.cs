using System;
using Bindito.Core;
using Timberborn.AlertPanelSystem;
using Timberborn.BottomBarSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.Options;

namespace Timberborn.MapEditorUI
{
	// Token: 0x02000008 RID: 8
	[Context("MapEditor")]
	internal class MapEditorUIConfigurator : Configurator
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000028C0 File Offset: 0x00000AC0
		protected override void Configure()
		{
			base.Bind<NoStartingLocationAlertFragment>().AsSingleton();
			base.Bind<NonCompatibleMapAlertFragment>().AsSingleton();
			base.Bind<MapEditorToolButtons>().AsSingleton();
			base.Bind<MapEditorBlockObjectButtons>().AsSingleton();
			base.Bind<IOptionsBox>().To<MapEditorOptionsBox>().AsSingleton();
			base.Bind<FilePanel>().AsSingleton();
			base.Bind<DeleteBlockObjectFragment>().AsSingleton();
			base.MultiBind<AlertPanelModule>().ToProvider<MapEditorUIConfigurator.AlertPanelModuleProvider>().AsSingleton();
			base.MultiBind<BottomBarModule>().ToProvider<MapEditorUIConfigurator.BottomBarModuleProvider>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<MapEditorUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000010 RID: 16
		private class AlertPanelModuleProvider : IProvider<AlertPanelModule>
		{
			// Token: 0x06000050 RID: 80 RVA: 0x00002FC7 File Offset: 0x000011C7
			public AlertPanelModuleProvider(NoStartingLocationAlertFragment noStartingLocationAlertFragment, NonCompatibleMapAlertFragment nonCompatibleMapAlertFragment)
			{
				this._noStartingLocationAlertFragment = noStartingLocationAlertFragment;
				this._nonCompatibleMapAlertFragment = nonCompatibleMapAlertFragment;
			}

			// Token: 0x06000051 RID: 81 RVA: 0x00002FDD File Offset: 0x000011DD
			public AlertPanelModule Get()
			{
				AlertPanelModule.Builder builder = new AlertPanelModule.Builder();
				builder.AddAlertFragment(this._noStartingLocationAlertFragment, 0);
				builder.AddAlertFragment(this._nonCompatibleMapAlertFragment, 1);
				return builder.Build();
			}

			// Token: 0x04000057 RID: 87
			private readonly NoStartingLocationAlertFragment _noStartingLocationAlertFragment;

			// Token: 0x04000058 RID: 88
			private readonly NonCompatibleMapAlertFragment _nonCompatibleMapAlertFragment;
		}

		// Token: 0x02000011 RID: 17
		private class BottomBarModuleProvider : IProvider<BottomBarModule>
		{
			// Token: 0x06000052 RID: 82 RVA: 0x00003003 File Offset: 0x00001203
			public BottomBarModuleProvider(MapEditorToolButtons mapEditorToolButtons, MapEditorBlockObjectButtons mapEditorBlockObjectButtons)
			{
				this._mapEditorToolButtons = mapEditorToolButtons;
				this._mapEditorBlockObjectButtons = mapEditorBlockObjectButtons;
			}

			// Token: 0x06000053 RID: 83 RVA: 0x00003019 File Offset: 0x00001219
			public BottomBarModule Get()
			{
				BottomBarModule.Builder builder = new BottomBarModule.Builder();
				builder.AddLeftSectionElement(this._mapEditorToolButtons, 20);
				builder.AddMiddleSectionElements(this._mapEditorBlockObjectButtons);
				return builder.Build();
			}

			// Token: 0x04000059 RID: 89
			private readonly MapEditorToolButtons _mapEditorToolButtons;

			// Token: 0x0400005A RID: 90
			private readonly MapEditorBlockObjectButtons _mapEditorBlockObjectButtons;
		}

		// Token: 0x02000012 RID: 18
		private class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000054 RID: 84 RVA: 0x0000303F File Offset: 0x0000123F
			public EntityPanelModuleProvider(DeleteBlockObjectFragment deleteBlockObjectFragment)
			{
				this._deleteBlockObjectFragment = deleteBlockObjectFragment;
			}

			// Token: 0x06000055 RID: 85 RVA: 0x0000304E File Offset: 0x0000124E
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddLeftHeaderFragment(this._deleteBlockObjectFragment, 0);
				return builder.Build();
			}

			// Token: 0x0400005B RID: 91
			private readonly DeleteBlockObjectFragment _deleteBlockObjectFragment;
		}
	}
}
