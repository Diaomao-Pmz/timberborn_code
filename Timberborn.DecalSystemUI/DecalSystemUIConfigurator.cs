using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;

namespace Timberborn.DecalSystemUI
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	public class DecalSystemUIConfigurator : Configurator
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002604 File Offset: 0x00000804
		public override void Configure()
		{
			base.Bind<DecalSupplierFragment>().AsSingleton();
			base.Bind<DecalButtonFactory>().AsSingleton();
			base.Bind<DecalButtonContainer>().AsSingleton();
			base.Bind<FlippableDecalFragment>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<DecalSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000009 RID: 9
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000020 RID: 32 RVA: 0x0000265A File Offset: 0x0000085A
			public EntityPanelModuleProvider(DecalSupplierFragment decalSupplierFragment, FlippableDecalFragment flippableDecalFragment)
			{
				this._decalSupplierFragment = decalSupplierFragment;
				this._flippableDecalFragment = flippableDecalFragment;
			}

			// Token: 0x06000021 RID: 33 RVA: 0x00002670 File Offset: 0x00000870
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddMiddleFragment(this._decalSupplierFragment, 0);
				builder.AddMiddleFragment(this._flippableDecalFragment, 0);
				return builder.Build();
			}

			// Token: 0x0400001C RID: 28
			public readonly DecalSupplierFragment _decalSupplierFragment;

			// Token: 0x0400001D RID: 29
			public readonly FlippableDecalFragment _flippableDecalFragment;
		}
	}
}
