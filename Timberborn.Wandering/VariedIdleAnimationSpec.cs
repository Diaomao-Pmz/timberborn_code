using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Wandering
{
	// Token: 0x0200000D RID: 13
	public class VariedIdleAnimationSpec : ComponentSpec, IEquatable<VariedIdleAnimationSpec>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002651 File Offset: 0x00000851
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(VariedIdleAnimationSpec);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000265D File Offset: 0x0000085D
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002665 File Offset: 0x00000865
		[Serialize]
		public ImmutableArray<string> Variants { get; set; }

		// Token: 0x06000037 RID: 55 RVA: 0x00002670 File Offset: 0x00000870
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("VariedIdleAnimationSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000026BC File Offset: 0x000008BC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Variants = ");
			builder.Append(this.Variants.ToString());
			return true;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002706 File Offset: 0x00000906
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(VariedIdleAnimationSpec left, VariedIdleAnimationSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002712 File Offset: 0x00000912
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(VariedIdleAnimationSpec left, VariedIdleAnimationSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002726 File Offset: 0x00000926
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<Variants>k__BackingField);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002745 File Offset: 0x00000945
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as VariedIdleAnimationSpec);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000235F File Offset: 0x0000055F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002753 File Offset: 0x00000953
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(VariedIdleAnimationSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<Variants>k__BackingField, other.<Variants>k__BackingField));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002784 File Offset: 0x00000984
		[CompilerGenerated]
		protected VariedIdleAnimationSpec([Nullable(1)] VariedIdleAnimationSpec original) : base(original)
		{
			this.Variants = original.<Variants>k__BackingField;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002388 File Offset: 0x00000588
		public VariedIdleAnimationSpec()
		{
		}
	}
}
