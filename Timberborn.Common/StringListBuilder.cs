using System;
using System.Text;

namespace Timberborn.Common
{
	// Token: 0x0200002F RID: 47
	public struct StringListBuilder
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x0000387E File Offset: 0x00001A7E
		public StringListBuilder(StringBuilder stringBuilder, string separator)
		{
			this._stringBuilder = stringBuilder;
			this._separator = separator;
			this._subsequent = false;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003895 File Offset: 0x00001A95
		public void BeginItem()
		{
			if (this._subsequent)
			{
				this._stringBuilder.Append(this._separator);
				return;
			}
			this._subsequent = true;
		}

		// Token: 0x04000046 RID: 70
		public readonly StringBuilder _stringBuilder;

		// Token: 0x04000047 RID: 71
		public readonly string _separator;

		// Token: 0x04000048 RID: 72
		public bool _subsequent;
	}
}
