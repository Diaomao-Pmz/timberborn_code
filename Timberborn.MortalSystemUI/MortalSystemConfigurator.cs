using System;
using Bindito.Core;
using Timberborn.WellbeingUI;

namespace Timberborn.MortalSystemUI
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class MortalSystemConfigurator : Configurator
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000211F File Offset: 0x0000031F
		public override void Configure()
		{
			base.MultiBind<INeedEffectDescriber>().To<LethalNeedEffectDescriber>().AsSingleton();
		}
	}
}
