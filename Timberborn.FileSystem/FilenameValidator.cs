using System;
using System.Linq;
using Timberborn.Common;

namespace Timberborn.FileSystem
{
	// Token: 0x02000005 RID: 5
	public class FilenameValidator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public bool NameIsInvalid(string name)
		{
			return string.IsNullOrWhiteSpace(name) || FilenameValidator.AllIllegalCharacters.FastAny(new Predicate<string>(name.Contains)) || FilenameValidator.SystemIllegalNames.Contains(name.ToUpper());
		}

		// Token: 0x0400000A RID: 10
		public static readonly string[] SystemIllegalCharacters = new string[]
		{
			"<",
			">",
			":",
			"\"",
			"/",
			"\\",
			"|",
			"?",
			"*"
		};

		// Token: 0x0400000B RID: 11
		public static readonly string[] SystemIllegalNames = new string[]
		{
			"AUX",
			"PRN",
			"NUL",
			"CON",
			"COM0",
			"COM1",
			"COM2",
			"COM3",
			"COM4",
			"COM5",
			"COM6",
			"COM7",
			"COM8",
			"COM9",
			"LPT0",
			"LPT1",
			"LPT2",
			"LPT3",
			"LPT4",
			"LPT5",
			"LPT6",
			"LPT7",
			"LPT8",
			"LPT9"
		};

		// Token: 0x0400000C RID: 12
		public static readonly string[] GameIllegalCharacters = new string[]
		{
			".",
			";",
			"#",
			"~",
			"`",
			"^",
			"$",
			"!",
			"%",
			"&",
			"@",
			"+",
			"="
		};

		// Token: 0x0400000D RID: 13
		public static readonly string[] AllIllegalCharacters = FilenameValidator.SystemIllegalCharacters.Concat(FilenameValidator.GameIllegalCharacters).ToArray<string>();
	}
}
