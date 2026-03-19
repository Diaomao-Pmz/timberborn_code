using System;
using System.Text;

namespace Timberborn.Common
{
	// Token: 0x0200002D RID: 45
	public static class StringBuilderExtensions
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x0000372D File Offset: 0x0000192D
		public static string ToStringWithoutNewLineEnd(this StringBuilder stringBuilder)
		{
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				'\r',
				'\n'
			});
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000374A File Offset: 0x0000194A
		public static string ToStringWithoutNewLineEndAndClean(this StringBuilder stringBuilder)
		{
			string result = stringBuilder.ToStringWithoutNewLineEnd();
			stringBuilder.Clear();
			return result;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003759 File Offset: 0x00001959
		public static string ToStringAndClear(this StringBuilder stringBuilder)
		{
			string result = stringBuilder.ToString();
			stringBuilder.Clear();
			return result;
		}
	}
}
