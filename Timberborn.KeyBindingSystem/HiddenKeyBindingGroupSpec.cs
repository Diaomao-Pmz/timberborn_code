using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public class HiddenKeyBindingGroupSpec : ComponentSpec, IEquatable<HiddenKeyBindingGroupSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002440 File Offset: 0x00000640
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(HiddenKeyBindingGroupSpec);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000244C File Offset: 0x0000064C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("HiddenKeyBindingGroupSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002498 File Offset: 0x00000698
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024A1 File Offset: 0x000006A1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(HiddenKeyBindingGroupSpec left, HiddenKeyBindingGroupSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024AD File Offset: 0x000006AD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(HiddenKeyBindingGroupSpec left, HiddenKeyBindingGroupSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024C1 File Offset: 0x000006C1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024C9 File Offset: 0x000006C9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as HiddenKeyBindingGroupSpec);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024D7 File Offset: 0x000006D7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024E0 File Offset: 0x000006E0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(HiddenKeyBindingGroupSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024F7 File Offset: 0x000006F7
		[CompilerGenerated]
		protected HiddenKeyBindingGroupSpec(HiddenKeyBindingGroupSpec original) : base(original)
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002500 File Offset: 0x00000700
		public HiddenKeyBindingGroupSpec()
		{
		}
	}
}
