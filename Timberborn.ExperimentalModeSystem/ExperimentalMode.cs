using System;
using Timberborn.CommandLine;
using Timberborn.SingletonSystem;

namespace Timberborn.ExperimentalModeSystem
{
	// Token: 0x02000004 RID: 4
	public class ExperimentalMode : ILoadableSingleton
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public bool IsExperimental { get; private set; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020CF File Offset: 0x000002CF
		public ExperimentalMode(ICommandLineArguments commandLineArguments)
		{
			this._commandLineArguments = commandLineArguments;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020DE File Offset: 0x000002DE
		public void Load()
		{
			if (this._commandLineArguments.Has(ExperimentalMode.ExperimentalModeKey))
			{
				this.EnableExperimentalMode();
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F8 File Offset: 0x000002F8
		public void EnableExperimentalMode()
		{
			this.IsExperimental = true;
		}

		// Token: 0x04000006 RID: 6
		public static readonly string ExperimentalModeKey = "experimental";

		// Token: 0x04000008 RID: 8
		public readonly ICommandLineArguments _commandLineArguments;
	}
}
