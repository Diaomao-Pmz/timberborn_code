using System;
using Bindito.Core;
using Timberborn.AlertPanelSystem;
using Timberborn.BottomBarSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.Options;

namespace Timberborn.MapEditorUI
{
	// Token: 0x0200000C RID: 12
	[Context("MapEditor")]
	public class MapEditorUIConfigurator : Configurator
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public override void Configure()
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

		// Token: 0x0200000D RID: 13
		public class AlertPanelModuleProvider : IProvider<AlertPanelModule>
		{
			// Token: 0x06000045 RID: 69 RVA: 0x00002E59 File Offset: 0x00001059
			public AlertPanelModuleProvider(NoStartingLocationAlertFragment noStartingLocationAlertFragment, NonCompatibleMapAlertFragment nonCompatibleMapAlertFragment)
			{
				this._noStartingLocationAlertFragment = noStartingLocationAlertFragment;
				this._nonCompatibleMapAlertFragment = nonCompatibleMapAlertFragment;
			}

			// Token: 0x06000046 RID: 70 RVA: 0x00002E6F File Offset: 0x0000106F
			public AlertPanelModule Get()
			{
				AlertPanelModule.Builder builder = new AlertPanelModule.Builder();
				builder.AddAlertFragment(this._noStartingLocationAlertFragment, 0);
				builder.AddAlertFragment(this._nonCompatibleMapAlertFragment, 1);
				return builder.Build();
			}

			// Token: 0x04000049 RID: 73
			public readonly NoStartingLocationAlertFragment _noStartingLocationAlertFragment;

			// Token: 0x0400004A RID: 74
			public readonly NonCompatibleMapAlertFragment _nonCompatibleMapAlertFragment;
		}

		// Token: 0x0200000E RID: 14
		public class BottomBarModuleProvider : IProvider<BottomBarModule>
		{
			// Token: 0x06000047 RID: 71 RVA: 0x00002E95 File Offset: 0x00001095
			public BottomBarModuleProvider(MapEditorToolButtons mapEditorToolButtons, MapEditorBlockObjectButtons mapEditorBlockObjectButtons)
			{
				this._mapEditorToolButtons = mapEditorToolButtons;
				this._mapEditorBlockObjectButtons = mapEditorBlockObjectButtons;
			}

			// Token: 0x06000048 RID: 72 RVA: 0x00002EAB File Offset: 0x000010AB
			public BottomBarModule Get()
			{
				BottomBarModule.Builder builder = new BottomBarModule.Builder();
				builder.AddLeftSectionElement(this._mapEditorToolButtons, 20);
				builder.AddMiddleSectionElements(this._mapEditorBlockObjectButtons);
				return builder.Build();
			}

			// Token: 0x0400004B RID: 75
			public readonly MapEditorToolButtons _mapEditorToolButtons;

			// Token: 0x0400004C RID: 76
			public readonly MapEditorBlockObjectButtons _mapEditorBlockObjectButtons;
		}

		// Token: 0x0200000F RID: 15
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000049 RID: 73 RVA: 0x00002ED1 File Offset: 0x000010D1
			public EntityPanelModuleProvider(DeleteBlockObjectFragment deleteBlockObjectFragment)
			{
				this._deleteBlockObjectFragment = deleteBlockObjectFragment;
			}

			// Token: 0x0600004A RID: 74 RVA: 0x00002EE0 File Offset: 0x000010E0
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddLeftHeaderFragment(this._deleteBlockObjectFragment, 0);
				return builder.Build();
			}

			// Token: 0x0400004D RID: 77
			public readonly DeleteBlockObjectFragment _deleteBlockObjectFragment;
		}
	}
}
