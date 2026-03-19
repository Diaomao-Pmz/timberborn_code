using System;
using System.Collections.Generic;
using System.Linq;

namespace Timberborn.CommandLine
{
	// Token: 0x02000004 RID: 4
	public class CommandLineArguments : ICommandLineArguments
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public CommandLineArguments(IEnumerable<string> arguments)
		{
			this._arguments = arguments.ToArray<string>();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D2 File Offset: 0x000002D2
		public static CommandLineArguments CreateWithCommandLineArgs()
		{
			return new CommandLineArguments(Environment.GetCommandLineArgs());
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020DE File Offset: 0x000002DE
		public bool Has(string key)
		{
			return this.IndexOfKey(key) >= this._arguments.GetLowerBound(0);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020F8 File Offset: 0x000002F8
		public int GetInt(string key)
		{
			return int.Parse(this.GetString(key));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002108 File Offset: 0x00000308
		public string GetString(string key)
		{
			if (!this.Has(key))
			{
				throw new ArgumentException("Argument " + key + " was not found.");
			}
			int num = this.IndexOfKey(key) + 1;
			return this._arguments[num];
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002146 File Offset: 0x00000346
		public int IndexOfKey(string key)
		{
			return Array.IndexOf<string>(this._arguments, "-" + key);
		}

		// Token: 0x04000006 RID: 6
		public readonly string[] _arguments;
	}
}
