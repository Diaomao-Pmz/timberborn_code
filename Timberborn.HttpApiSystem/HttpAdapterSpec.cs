using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x02000016 RID: 22
	[NullableContext(1)]
	[Nullable(0)]
	public class HttpAdapterSpec : ComponentSpec, IEquatable<HttpAdapterSpec>
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003886 File Offset: 0x00001A86
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(HttpAdapterSpec);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003894 File Offset: 0x00001A94
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("HttpAdapterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000038E0 File Offset: 0x00001AE0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000038E9 File Offset: 0x00001AE9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(HttpAdapterSpec left, HttpAdapterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000038F5 File Offset: 0x00001AF5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(HttpAdapterSpec left, HttpAdapterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003909 File Offset: 0x00001B09
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003911 File Offset: 0x00001B11
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as HttpAdapterSpec);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000391F File Offset: 0x00001B1F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003928 File Offset: 0x00001B28
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(HttpAdapterSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000393F File Offset: 0x00001B3F
		[CompilerGenerated]
		protected HttpAdapterSpec(HttpAdapterSpec original) : base(original)
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003948 File Offset: 0x00001B48
		public HttpAdapterSpec()
		{
		}
	}
}
