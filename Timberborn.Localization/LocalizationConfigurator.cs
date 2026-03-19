using System;
using Bindito.Core;

namespace Timberborn.Localization
{
	// Token: 0x0200000A RID: 10
	[Context("Bootstrapper")]
	public class LocalizationConfigurator : Configurator
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000022A8 File Offset: 0x000004A8
		public override void Configure()
		{
			base.Bind<ILoc>().To<Loc>().AsSingleton().AsExported();
			base.Bind<ILocalizationService>().To<LocalizationService>().AsSingleton().AsExported();
			base.Bind<LocalizationLoader>().AsSingleton();
			base.Bind<LocalizationDisplayNames>().AsSingleton();
			base.Bind<ILocalizationCsvValidator>().To<LocalizationCsvValidator>().AsSingleton();
			base.Bind<NewLocalizationService>().AsSingleton();
			base.Bind<PanelTextSettingsUpdater>().AsSingleton();
		}
	}
}
