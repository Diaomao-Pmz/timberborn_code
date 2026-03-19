using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.UnderstructureSystem
{
	// Token: 0x02000009 RID: 9
	public class UnderstructureConstraintSpec : ComponentSpec, IEquatable<UnderstructureConstraintSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002421 File Offset: 0x00000621
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(UnderstructureConstraintSpec);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000242D File Offset: 0x0000062D
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002435 File Offset: 0x00000635
		[Serialize]
		public ImmutableArray<string> UnderstructureTemplateNames { get; set; }

		// Token: 0x06000022 RID: 34 RVA: 0x00002440 File Offset: 0x00000640
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UnderstructureConstraintSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000248C File Offset: 0x0000068C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("UnderstructureTemplateNames = ");
			builder.Append(this.UnderstructureTemplateNames.ToString());
			return true;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024D6 File Offset: 0x000006D6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(UnderstructureConstraintSpec left, UnderstructureConstraintSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024E2 File Offset: 0x000006E2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(UnderstructureConstraintSpec left, UnderstructureConstraintSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024F6 File Offset: 0x000006F6
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<UnderstructureTemplateNames>k__BackingField);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002515 File Offset: 0x00000715
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as UnderstructureConstraintSpec);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002523 File Offset: 0x00000723
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000252C File Offset: 0x0000072C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(UnderstructureConstraintSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<UnderstructureTemplateNames>k__BackingField, other.<UnderstructureTemplateNames>k__BackingField));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000255D File Offset: 0x0000075D
		[CompilerGenerated]
		protected UnderstructureConstraintSpec([Nullable(1)] UnderstructureConstraintSpec original) : base(original)
		{
			this.UnderstructureTemplateNames = original.<UnderstructureTemplateNames>k__BackingField;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002572 File Offset: 0x00000772
		public UnderstructureConstraintSpec()
		{
		}
	}
}
