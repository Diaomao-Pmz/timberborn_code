using System;
using Bindito.Core;
using Timberborn.Debugging;
using Timberborn.EntityPanelSystem;

namespace Timberborn.InventorySystemUI
{
	// Token: 0x02000019 RID: 25
	[Context("Game")]
	public class InventorySystemUIConfigurator : Configurator
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00003344 File Offset: 0x00001544
		public override void Configure()
		{
			base.Bind<InventoryDebugFragment>().AsSingleton();
			base.Bind<ModifyInventoryBox>().AsSingleton();
			base.Bind<InformationalRowsFactory>().AsSingleton();
			base.Bind<InventoryFragmentBuilderFactory>().AsSingleton();
			base.Bind<InventoryRowUpdater>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<InventorySystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<IDevModule>().To<InventoryFillerDevModule>().AsSingleton();
		}

		// Token: 0x0200001A RID: 26
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000070 RID: 112 RVA: 0x000033B7 File Offset: 0x000015B7
			public EntityPanelModuleProvider(InventoryDebugFragment inventoryDebugFragment)
			{
				this._inventoryDebugFragment = inventoryDebugFragment;
			}

			// Token: 0x06000071 RID: 113 RVA: 0x000033C6 File Offset: 0x000015C6
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddDiagnosticFragment(this._inventoryDebugFragment);
				return builder.Build();
			}

			// Token: 0x04000050 RID: 80
			public readonly InventoryDebugFragment _inventoryDebugFragment;
		}
	}
}
