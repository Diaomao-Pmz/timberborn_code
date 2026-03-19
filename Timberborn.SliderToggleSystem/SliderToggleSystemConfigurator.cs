using System;
using Bindito.Core;

namespace Timberborn.SliderToggleSystem
{
	// Token: 0x02000010 RID: 16
	[Context("Game")]
	[Context("MapEditor")]
	public class SliderToggleSystemConfigurator : Configurator
	{
		// Token: 0x0600003E RID: 62 RVA: 0x0000286B File Offset: 0x00000A6B
		public override void Configure()
		{
			base.Bind<SliderToggleFactory>().AsSingleton();
			base.Bind<SliderToggleButtonFactory>().AsSingleton();
		}
	}
}
