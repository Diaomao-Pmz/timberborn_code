using System;
using System.Collections.Immutable;
using System.Linq;

namespace Timberborn.Versioning
{
	// Token: 0x02000005 RID: 5
	public readonly struct Version
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002133 File Offset: 0x00000333
		public string Numeric { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000213B File Offset: 0x0000033B
		public string Full { get; }

		// Token: 0x0600000C RID: 12 RVA: 0x00002143 File Offset: 0x00000343
		public Version(string numeric, string full, bool isExperimental, ImmutableArray<int> subNumbers)
		{
			this.Numeric = numeric;
			this.Full = full;
			this._isExperimental = isExperimental;
			this._subNumbers = subNumbers;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002164 File Offset: 0x00000364
		public static Version Create(string version)
		{
			string text = Version.ExtractVersionNumber(version);
			ImmutableArray<int> subNumbers = text.Split('.', StringSplitOptions.None).Select(new Func<string, int>(int.Parse)).ToImmutableArray<int>();
			return new Version(text, version, Version.GetExperimental(version), subNumbers);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021A4 File Offset: 0x000003A4
		public string Formatted
		{
			get
			{
				return "v" + this.Full;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021B6 File Offset: 0x000003B6
		public string NumericWithBranch
		{
			get
			{
				if (!this._isExperimental)
				{
					return this.Numeric;
				}
				return string.Format("{0}-{1}", this.Numeric, Version.ExperimentalSymbol);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021E1 File Offset: 0x000003E1
		public bool IsDevelopmentVersion
		{
			get
			{
				return this.Numeric == "0";
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021F3 File Offset: 0x000003F3
		public override string ToString()
		{
			return this.Numeric;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021FC File Offset: 0x000003FC
		public bool IsEqualOrHigherThan(Version other, int? depth = null)
		{
			int num = (depth != null) ? Math.Min(depth.Value, this._subNumbers.Length) : this._subNumbers.Length;
			for (int i = 0; i < num; i++)
			{
				if (i >= other._subNumbers.Length)
				{
					return true;
				}
				if (this._subNumbers[i] > other._subNumbers[i])
				{
					return true;
				}
				if (this._subNumbers[i] < other._subNumbers[i])
				{
					return false;
				}
			}
			int num2 = (depth != null) ? Math.Min(depth.Value, other._subNumbers.Length) : other._subNumbers.Length;
			for (int j = num; j < num2; j++)
			{
				if (other._subNumbers[j] > 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022DD File Offset: 0x000004DD
		public bool IsFromSameBranch(Version other)
		{
			return this._isExperimental == other._isExperimental;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022ED File Offset: 0x000004ED
		public static string ExtractVersionNumber(string version)
		{
			if (!Version.VersionIsInOldFormat(version))
			{
				return version.Split('-', StringSplitOptions.None)[0];
			}
			return "0.0.0.0";
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002308 File Offset: 0x00000508
		public static bool VersionIsInOldFormat(string version)
		{
			return version[0] == 'v';
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002318 File Offset: 0x00000518
		public static bool GetExperimental(string version)
		{
			string[] array = version.Split('-', StringSplitOptions.None);
			return !Version.VersionIsInOldFormat(version) && array.Length > 2 && array[2].StartsWith(Version.ExperimentalSymbol);
		}

		// Token: 0x04000008 RID: 8
		public static readonly char ExperimentalSymbol = 'x';

		// Token: 0x0400000B RID: 11
		public readonly bool _isExperimental;

		// Token: 0x0400000C RID: 12
		public readonly ImmutableArray<int> _subNumbers;
	}
}
