using System;
using Bindito.Core;

namespace Timberborn.ErrorReporting
{
	// Token: 0x02000006 RID: 6
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class ErrorReportingConfigurator : Configurator
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002556 File Offset: 0x00000756
		public override void Configure()
		{
			base.Bind<WorldDataClearer>().AsSingleton();
			base.Bind<ILoadingIssueService>().To<LoadingIssueService>().AsSingleton();
		}
	}
}
