using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x0200002D RID: 45
	[NullableContext(1)]
	[Nullable(0)]
	public class HttpLeverSpec : ComponentSpec, IEquatable<HttpLeverSpec>
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000058C0 File Offset: 0x00003AC0
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(HttpLeverSpec);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000058CC File Offset: 0x00003ACC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("HttpLeverSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000038E0 File Offset: 0x00001AE0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005918 File Offset: 0x00003B18
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(HttpLeverSpec left, HttpLeverSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005924 File Offset: 0x00003B24
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(HttpLeverSpec left, HttpLeverSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003909 File Offset: 0x00001B09
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005938 File Offset: 0x00003B38
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as HttpLeverSpec);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000391F File Offset: 0x00001B1F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003928 File Offset: 0x00001B28
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(HttpLeverSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000393F File Offset: 0x00001B3F
		[CompilerGenerated]
		protected HttpLeverSpec(HttpLeverSpec original) : base(original)
		{
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003948 File Offset: 0x00001B48
		public HttpLeverSpec()
		{
		}
	}
}
