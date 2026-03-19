using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x0200001B RID: 27
	[NullableContext(1)]
	[Nullable(0)]
	public class WaterSourceDisablerSpec : ComponentSpec, IEquatable<WaterSourceDisablerSpec>
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003686 File Offset: 0x00001886
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WaterSourceDisablerSpec);
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003694 File Offset: 0x00001894
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterSourceDisablerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000022B8 File Offset: 0x000004B8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000036E0 File Offset: 0x000018E0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterSourceDisablerSpec left, WaterSourceDisablerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000036EC File Offset: 0x000018EC
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterSourceDisablerSpec left, WaterSourceDisablerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000022E1 File Offset: 0x000004E1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003700 File Offset: 0x00001900
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterSourceDisablerSpec);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000022F7 File Offset: 0x000004F7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00002300 File Offset: 0x00000500
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterSourceDisablerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00002317 File Offset: 0x00000517
		[CompilerGenerated]
		protected WaterSourceDisablerSpec(WaterSourceDisablerSpec original) : base(original)
		{
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00002320 File Offset: 0x00000520
		public WaterSourceDisablerSpec()
		{
		}
	}
}
