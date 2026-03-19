using System;
using Bindito.Core;

namespace Timberborn.TutorialSystemUI
{
	// Token: 0x02000014 RID: 20
	[Context("Game")]
	public class TutorialSystemUIConfigurator : Configurator
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00002FA4 File Offset: 0x000011A4
		public override void Configure()
		{
			base.Bind<TutorialPanels>().AsSingleton();
			base.Bind<TutorialPanelFactory>().AsSingleton();
			base.Bind<DisableTutorialButtonInitializer>().AsSingleton();
			base.Bind<TutorialPanelBlinker>().AsSingleton();
			base.Bind<TutorialStepViewFactory>().AsSingleton();
		}
	}
}
