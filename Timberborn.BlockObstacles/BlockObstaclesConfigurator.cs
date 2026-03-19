using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.BlockObstacles
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	public class BlockObstaclesConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public override void Configure()
		{
			base.Bind<LayeredBlockObstacleVisualizer>().AsTransient();
			base.Bind<BlockOccupier>().AsTransient();
			base.Bind<LayeredBlockObstacle>().AsTransient();
			base.Bind<BlockOccupationLayerFactory>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BlockObstaclesConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000215A File Offset: 0x0000035A
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<LayeredBlockObstacleSpec, LayeredBlockObstacle>();
			builder.AddDecorator<LayeredBlockObstacleVisualizerSpec, LayeredBlockObstacleVisualizer>();
			builder.AddDecorator<BlockOccupierSpec, BlockOccupier>();
			return builder.Build();
		}
	}
}
