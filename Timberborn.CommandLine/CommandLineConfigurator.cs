using System;
using Bindito.Core;

namespace Timberborn.CommandLine
{
	// Token: 0x02000005 RID: 5
	[Context("Bootstrapper")]
	public class CommandLineConfigurator : Configurator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000215E File Offset: 0x0000035E
		public override void Configure()
		{
			base.Bind<ICommandLineArguments>().ToProvider(new Func<ICommandLineArguments>(CommandLineArguments.CreateWithCommandLineArgs)).AsSingleton().AsExported();
		}
	}
}
