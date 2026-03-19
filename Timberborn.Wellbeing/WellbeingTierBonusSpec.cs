using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Wellbeing
{
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	[Nullable(0)]
	public class WellbeingTierBonusSpec : IEquatable<WellbeingTierBonusSpec>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002837 File Offset: 0x00000A37
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WellbeingTierBonusSpec);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002843 File Offset: 0x00000A43
		// (set) Token: 0x06000046 RID: 70 RVA: 0x0000284B File Offset: 0x00000A4B
		[Serialize]
		public int Wellbeing { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002854 File Offset: 0x00000A54
		// (set) Token: 0x06000048 RID: 72 RVA: 0x0000285C File Offset: 0x00000A5C
		[Serialize]
		public float Multiplier { get; set; }

		// Token: 0x06000049 RID: 73 RVA: 0x00002868 File Offset: 0x00000A68
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WellbeingTierBonusSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000028B4 File Offset: 0x00000AB4
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Wellbeing = ");
			builder.Append(this.Wellbeing.ToString());
			builder.Append(", Multiplier = ");
			builder.Append(this.Multiplier.ToString());
			return true;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002915 File Offset: 0x00000B15
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WellbeingTierBonusSpec left, WellbeingTierBonusSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002921 File Offset: 0x00000B21
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WellbeingTierBonusSpec left, WellbeingTierBonusSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002935 File Offset: 0x00000B35
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Wellbeing>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Multiplier>k__BackingField);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002975 File Offset: 0x00000B75
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WellbeingTierBonusSpec);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002984 File Offset: 0x00000B84
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WellbeingTierBonusSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<Wellbeing>k__BackingField, other.<Wellbeing>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<Multiplier>k__BackingField, other.<Multiplier>k__BackingField));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000029E5 File Offset: 0x00000BE5
		[CompilerGenerated]
		protected WellbeingTierBonusSpec(WellbeingTierBonusSpec original)
		{
			this.Wellbeing = original.<Wellbeing>k__BackingField;
			this.Multiplier = original.<Multiplier>k__BackingField;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000020F8 File Offset: 0x000002F8
		public WellbeingTierBonusSpec()
		{
		}
	}
}
