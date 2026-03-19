using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Rendering
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	public class LightingEnablerSpec : ComponentSpec, IEquatable<LightingEnablerSpec>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002C66 File Offset: 0x00000E66
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(LightingEnablerSpec);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C74 File Offset: 0x00000E74
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("LightingEnablerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002CC0 File Offset: 0x00000EC0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002CC9 File Offset: 0x00000EC9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(LightingEnablerSpec left, LightingEnablerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002CD5 File Offset: 0x00000ED5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(LightingEnablerSpec left, LightingEnablerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002CE9 File Offset: 0x00000EE9
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002CF1 File Offset: 0x00000EF1
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as LightingEnablerSpec);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002675 File Offset: 0x00000875
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002CFF File Offset: 0x00000EFF
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(LightingEnablerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002D16 File Offset: 0x00000F16
		[CompilerGenerated]
		protected LightingEnablerSpec(LightingEnablerSpec original) : base(original)
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000026F5 File Offset: 0x000008F5
		public LightingEnablerSpec()
		{
		}
	}
}
