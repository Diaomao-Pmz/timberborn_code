using System;

namespace Timberborn.ErrorReporting
{
	// Token: 0x0200000B RID: 11
	public readonly struct LoadingIssueMessage : IEquatable<LoadingIssueMessage>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002937 File Offset: 0x00000B37
		public string MessageLocKey { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000293F File Offset: 0x00000B3F
		public string MessageParam { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002947 File Offset: 0x00000B47
		public bool ParamIsLocKey { get; }

		// Token: 0x06000030 RID: 48 RVA: 0x0000294F File Offset: 0x00000B4F
		public LoadingIssueMessage(string messageLocKey, string messageParam, bool paramIsLocKey)
		{
			this.MessageLocKey = messageLocKey;
			this.MessageParam = messageParam;
			this.ParamIsLocKey = paramIsLocKey;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002966 File Offset: 0x00000B66
		public bool Equals(LoadingIssueMessage other)
		{
			return this.MessageLocKey == other.MessageLocKey && this.MessageParam == other.MessageParam;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002990 File Offset: 0x00000B90
		public override bool Equals(object obj)
		{
			if (obj is LoadingIssueMessage)
			{
				LoadingIssueMessage other = (LoadingIssueMessage)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000029B5 File Offset: 0x00000BB5
		public override int GetHashCode()
		{
			return HashCode.Combine<string, string>(this.MessageLocKey, this.MessageParam);
		}
	}
}
