using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000025 RID: 37
	[NullableContext(1)]
	[Nullable(0)]
	public class LeverSpec : ComponentSpec, IEquatable<LeverSpec>
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000529E File Offset: 0x0000349E
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(LeverSpec);
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000052AC File Offset: 0x000034AC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("LeverSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00002710 File Offset: 0x00000910
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000052F8 File Offset: 0x000034F8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(LeverSpec left, LeverSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005304 File Offset: 0x00003504
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(LeverSpec left, LeverSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00002739 File Offset: 0x00000939
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005318 File Offset: 0x00003518
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as LeverSpec);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00002758 File Offset: 0x00000958
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(LeverSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000276F File Offset: 0x0000096F
		[CompilerGenerated]
		protected LeverSpec(LeverSpec original) : base(original)
		{
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00002778 File Offset: 0x00000978
		public LeverSpec()
		{
		}
	}
}
