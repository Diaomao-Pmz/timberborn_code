using System;
using Bindito.Core;

namespace Timberborn.MapEditorSceneLoading
{
	// Token: 0x02000008 RID: 8
	[Context("MainMenu")]
	[Context("MapEditor")]
	public class MapEditorSceneLoadingConfigurator : Configurator
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000021C4 File Offset: 0x000003C4
		public override void Configure()
		{
			base.Bind<MapEditorSceneLoader>().AsTransient();
		}
	}
}
