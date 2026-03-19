using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Wandering
{
	// Token: 0x02000007 RID: 7
	public class IdleAnimationVariantSpec : IEquatable<IdleAnimationVariantSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(IdleAnimationVariantSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210A File Offset: 0x0000030A
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002112 File Offset: 0x00000312
		[Serialize]
		public float Probability { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000211B File Offset: 0x0000031B
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002123 File Offset: 0x00000323
		[Serialize]
		public string Variant { get; set; }

		// Token: 0x0600000C RID: 12 RVA: 0x0000212C File Offset: 0x0000032C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("IdleAnimationVariantSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002178 File Offset: 0x00000378
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Probability = ");
			builder.Append(this.Probability.ToString());
			builder.Append(", Variant = ");
			builder.Append(this.Variant);
			return true;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021CB File Offset: 0x000003CB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(IdleAnimationVariantSpec left, IdleAnimationVariantSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021D7 File Offset: 0x000003D7
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(IdleAnimationVariantSpec left, IdleAnimationVariantSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021EB File Offset: 0x000003EB
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Probability>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Variant>k__BackingField);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000222B File Offset: 0x0000042B
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IdleAnimationVariantSpec);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000223C File Offset: 0x0000043C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(IdleAnimationVariantSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<float>.Default.Equals(this.<Probability>k__BackingField, other.<Probability>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Variant>k__BackingField, other.<Variant>k__BackingField));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000229D File Offset: 0x0000049D
		[CompilerGenerated]
		protected IdleAnimationVariantSpec([Nullable(1)] IdleAnimationVariantSpec original)
		{
			this.Probability = original.<Probability>k__BackingField;
			this.Variant = original.<Variant>k__BackingField;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000020F6 File Offset: 0x000002F6
		public IdleAnimationVariantSpec()
		{
		}
	}
}
