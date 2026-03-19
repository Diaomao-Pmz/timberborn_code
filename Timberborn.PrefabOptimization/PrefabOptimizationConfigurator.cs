using System;
using Bindito.Core;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x02000020 RID: 32
	[Context("Game")]
	[Context("MapEditor")]
	public class PrefabOptimizationConfigurator : Configurator
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x0000508C File Offset: 0x0000328C
		public override void Configure()
		{
			base.Bind<IPrefabOptimizationChain>().ToProvider<PrefabOptimizationChainProvider>().AsSingleton();
			base.Bind<OptimizedPrefabInstantiator>().AsSingleton();
			base.Bind<TimbermeshPrefabOptimizer>().AsSingleton();
			base.Bind<VertexColorPrefabOptimizer>().AsSingleton();
			base.Bind<MergeMeshesByMaterialPrefabOptimizer>().AsSingleton();
			base.Bind<DestroyEmptyChildrenPrefabOptimizer>().AsSingleton();
			base.Bind<AutoAtlasingPrefabOptimizer>().AsSingleton();
			base.Bind<VerticalShapeBuilder>().AsSingleton();
			base.Bind<AutoAtlaser>().AsSingleton();
			base.Bind<MaterialPropertyProvider>().AsSingleton();
		}
	}
}
