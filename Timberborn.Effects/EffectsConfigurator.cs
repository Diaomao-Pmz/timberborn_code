using System;
using Bindito.Core;

namespace Timberborn.Effects
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	public class EffectsConfigurator : Configurator
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002404 File Offset: 0x00000604
		public override void Configure()
		{
			base.Bind<GoodEffectDescriber>().AsSingleton();
			base.Bind<ContinuousEffectValueSerializer>().AsSingleton();
			base.Bind<EffectDescriber>().AsSingleton();
		}
	}
}
