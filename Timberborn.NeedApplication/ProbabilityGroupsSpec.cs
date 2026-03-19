using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NeedApplication
{
	// Token: 0x02000015 RID: 21
	public class ProbabilityGroupsSpec : ComponentSpec, IEquatable<ProbabilityGroupsSpec>
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002FB1 File Offset: 0x000011B1
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ProbabilityGroupsSpec);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002FBD File Offset: 0x000011BD
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002FC5 File Offset: 0x000011C5
		[Serialize]
		public ImmutableArray<ProbabilityGroupSpec> Groups { get; set; }

		// Token: 0x0600007E RID: 126 RVA: 0x00002FD0 File Offset: 0x000011D0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ProbabilityGroupsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000301C File Offset: 0x0000121C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Groups = ");
			builder.Append(this.Groups.ToString());
			return true;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003066 File Offset: 0x00001266
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ProbabilityGroupsSpec left, ProbabilityGroupsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003072 File Offset: 0x00001272
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ProbabilityGroupsSpec left, ProbabilityGroupsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003086 File Offset: 0x00001286
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<ProbabilityGroupSpec>>.Default.GetHashCode(this.<Groups>k__BackingField);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000030A5 File Offset: 0x000012A5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ProbabilityGroupsSpec);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002519 File Offset: 0x00000719
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000030B3 File Offset: 0x000012B3
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ProbabilityGroupsSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<ProbabilityGroupSpec>>.Default.Equals(this.<Groups>k__BackingField, other.<Groups>k__BackingField));
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000030E4 File Offset: 0x000012E4
		[CompilerGenerated]
		protected ProbabilityGroupsSpec([Nullable(1)] ProbabilityGroupsSpec original) : base(original)
		{
			this.Groups = original.<Groups>k__BackingField;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002599 File Offset: 0x00000799
		public ProbabilityGroupsSpec()
		{
		}
	}
}
