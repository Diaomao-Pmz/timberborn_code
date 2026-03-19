using System;
using Bindito.Core;
using Timberborn.Goods;

namespace Timberborn.GoodsMapEditor
{
	// Token: 0x02000004 RID: 4
	[Context("MapEditor")]
	public class GoodsMapEditorConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public override void Configure()
		{
			base.Bind<IGoodFilter>().To<MapEditorGoodFilter>().AsSingleton();
		}
	}
}
