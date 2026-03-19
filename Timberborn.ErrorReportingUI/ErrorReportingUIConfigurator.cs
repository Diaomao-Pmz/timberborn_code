using System;
using Bindito.Core;

namespace Timberborn.ErrorReportingUI
{
	// Token: 0x0200000A RID: 10
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class ErrorReportingUIConfigurator : Configurator
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000028D3 File Offset: 0x00000AD3
		public override void Configure()
		{
			base.Bind<CrashBox>().AsSingleton();
			base.Bind<LoadingIssuePanel>().AsSingleton();
		}
	}
}
