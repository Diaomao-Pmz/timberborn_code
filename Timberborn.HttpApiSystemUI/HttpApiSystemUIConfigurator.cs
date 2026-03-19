using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;

namespace Timberborn.HttpApiSystemUI
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	public class HttpApiSystemUIConfigurator : Configurator
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000028D4 File Offset: 0x00000AD4
		public override void Configure()
		{
			base.Bind<HttpApiFragment>().AsSingleton();
			base.Bind<HttpAdapterFragment>().AsSingleton();
			base.Bind<HttpLeverFragment>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<HttpApiSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000009 RID: 9
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000026 RID: 38 RVA: 0x00002913 File Offset: 0x00000B13
			public EntityPanelModuleProvider(HttpApiFragment httpApiFragment, HttpAdapterFragment httpAdapterFragment, HttpLeverFragment httpLeverFragment)
			{
				this._httpApiFragment = httpApiFragment;
				this._httpAdapterFragment = httpAdapterFragment;
				this._httpLeverFragment = httpLeverFragment;
			}

			// Token: 0x06000027 RID: 39 RVA: 0x00002930 File Offset: 0x00000B30
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddBottomFragment(this._httpAdapterFragment, 200);
				builder.AddBottomFragment(this._httpLeverFragment, 200);
				builder.AddBottomFragment(this._httpApiFragment, 300);
				return builder.Build();
			}

			// Token: 0x0400002D RID: 45
			public readonly HttpApiFragment _httpApiFragment;

			// Token: 0x0400002E RID: 46
			public readonly HttpAdapterFragment _httpAdapterFragment;

			// Token: 0x0400002F RID: 47
			public readonly HttpLeverFragment _httpLeverFragment;
		}
	}
}
