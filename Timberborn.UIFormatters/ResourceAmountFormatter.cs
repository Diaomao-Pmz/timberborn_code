using System;
using Timberborn.Localization;

namespace Timberborn.UIFormatters
{
	// Token: 0x02000006 RID: 6
	public class ResourceAmountFormatter
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002352 File Offset: 0x00000552
		public ResourceAmountFormatter(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002361 File Offset: 0x00000561
		public string Format(string resourceName, int amount)
		{
			return this._loc.T<string, string>(ResourceAmountFormatter.ResourceNameAndAmountLocKey, resourceName, amount.ToString());
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000237B File Offset: 0x0000057B
		public string FormatPerHour(string resourceName, float amount)
		{
			return this._loc.T<string, string>(ResourceAmountFormatter.ResourceNameAndAmountPerHourLocKey, resourceName, amount.ToString("0.#"));
		}

		// Token: 0x0400000B RID: 11
		public static readonly string ResourceNameAndAmountLocKey = "Core.GoodNameAndAmount";

		// Token: 0x0400000C RID: 12
		public static readonly string ResourceNameAndAmountPerHourLocKey = "Core.GoodNameAndAmountPerHour";

		// Token: 0x0400000D RID: 13
		public readonly ILoc _loc;
	}
}
