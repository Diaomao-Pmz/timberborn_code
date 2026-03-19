using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NaturalResourcesContamination
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class ContaminatedNaturalResourceSpec : ComponentSpec, IEquatable<ContaminatedNaturalResourceSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000023DE File Offset: 0x000005DE
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ContaminatedNaturalResourceSpec);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023EC File Offset: 0x000005EC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ContaminatedNaturalResourceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002438 File Offset: 0x00000638
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002441 File Offset: 0x00000641
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ContaminatedNaturalResourceSpec left, ContaminatedNaturalResourceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000244D File Offset: 0x0000064D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ContaminatedNaturalResourceSpec left, ContaminatedNaturalResourceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002461 File Offset: 0x00000661
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002469 File Offset: 0x00000669
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ContaminatedNaturalResourceSpec);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002477 File Offset: 0x00000677
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002480 File Offset: 0x00000680
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ContaminatedNaturalResourceSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002497 File Offset: 0x00000697
		[CompilerGenerated]
		protected ContaminatedNaturalResourceSpec(ContaminatedNaturalResourceSpec original) : base(original)
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024A0 File Offset: 0x000006A0
		public ContaminatedNaturalResourceSpec()
		{
		}
	}
}
