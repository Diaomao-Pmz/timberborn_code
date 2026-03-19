using System;
using Bindito.Core;

namespace Timberborn.FactionValidators
{
	// Token: 0x0200000D RID: 13
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class FactionValidatorsConfigurator : Configurator
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002428 File Offset: 0x00000628
		public override void Configure()
		{
			base.Bind<FactionSpecValidationService>().AsSingleton();
			base.MultiBind<IFactionSpecValidator>().To<FactionSpecGoodsValidator>().AsSingleton();
			base.MultiBind<IFactionSpecValidator>().To<FactionSpecMaterialsValidator>().AsSingleton();
			base.MultiBind<IFactionSpecValidator>().To<FactionSpecNeedsValidator>().AsSingleton();
			base.MultiBind<IFactionSpecValidator>().To<FactionSpecTemplateValidator>().AsSingleton();
		}
	}
}
