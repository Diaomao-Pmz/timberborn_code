using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;

namespace Timberborn.BlueprintUISystem
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	[Context("MapEditor")]
	public class BlueprintUISystemConfigurator : Configurator
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000023C5 File Offset: 0x000005C5
		public override void Configure()
		{
			base.Bind<BlueprintDebugFragment>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<BlueprintUISystemConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000008 RID: 8
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000013 RID: 19 RVA: 0x000023EC File Offset: 0x000005EC
			public EntityPanelModuleProvider(BlueprintDebugFragment blueprintDebugFragment)
			{
				this._blueprintDebugFragment = blueprintDebugFragment;
			}

			// Token: 0x06000014 RID: 20 RVA: 0x000023FB File Offset: 0x000005FB
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddDiagnosticFragment(this._blueprintDebugFragment);
				return builder.Build();
			}

			// Token: 0x04000011 RID: 17
			public readonly BlueprintDebugFragment _blueprintDebugFragment;
		}
	}
}
