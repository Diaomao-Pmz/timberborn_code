using System;
using Timberborn.CommandLine;
using Timberborn.SingletonSystem;

namespace Timberborn.ScreenSystem
{
	// Token: 0x02000004 RID: 4
	public class CommandLineScreenSettings : ILoadableSingleton
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public bool Uncapped { get; private set; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020CF File Offset: 0x000002CF
		public CommandLineScreenSettings(ICommandLineArguments commandLineArguments)
		{
			this._commandLineArguments = commandLineArguments;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020DE File Offset: 0x000002DE
		public void Load()
		{
			this.Uncapped = this._commandLineArguments.Has(CommandLineScreenSettings.UncappedCommandLineArgumentKey);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string UncappedCommandLineArgumentKey = "uncapped";

		// Token: 0x04000008 RID: 8
		public readonly ICommandLineArguments _commandLineArguments;
	}
}
