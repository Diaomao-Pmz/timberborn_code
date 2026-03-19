using System;
using Bindito.Core;

namespace Timberborn.TutorialSystem
{
	// Token: 0x0200001C RID: 28
	[Context("Game")]
	public class TutorialSystemConfigurator : Configurator
	{
		// Token: 0x06000092 RID: 146 RVA: 0x000032FC File Offset: 0x000014FC
		public override void Configure()
		{
			base.Bind<TutorialService>().AsSingleton();
			base.Bind<ITutorialService>().ToExisting<TutorialService>();
			base.Bind<ITutorialTriggers>().To<TutorialTriggers>().AsSingleton();
			base.Bind<TutorialStageService>().AsSingleton();
		}
	}
}
