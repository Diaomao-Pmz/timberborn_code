using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.Effects;

namespace Timberborn.NeedApplication
{
	// Token: 0x02000014 RID: 20
	public class NeedApplierEffectSpec : IEquatable<NeedApplierEffectSpec>
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002D5E File Offset: 0x00000F5E
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(NeedApplierEffectSpec);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002D6A File Offset: 0x00000F6A
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002D72 File Offset: 0x00000F72
		[Serialize]
		public string NeedId { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002D7B File Offset: 0x00000F7B
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002D83 File Offset: 0x00000F83
		[Serialize]
		public float Points { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002D8C File Offset: 0x00000F8C
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002D94 File Offset: 0x00000F94
		[Serialize]
		public EffectProbability Probability { get; set; }

		// Token: 0x06000070 RID: 112 RVA: 0x00002D9D File Offset: 0x00000F9D
		public InstantEffect ToInstantEffect()
		{
			return new InstantEffect(this.NeedId, this.Points, 1);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002DB4 File Offset: 0x00000FB4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NeedApplierEffectSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002E00 File Offset: 0x00001000
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("NeedId = ");
			builder.Append(this.NeedId);
			builder.Append(", Points = ");
			builder.Append(this.Points.ToString());
			builder.Append(", Probability = ");
			builder.Append(this.Probability.ToString());
			return true;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002E7A File Offset: 0x0000107A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(NeedApplierEffectSpec left, NeedApplierEffectSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002E86 File Offset: 0x00001086
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(NeedApplierEffectSpec left, NeedApplierEffectSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002E9C File Offset: 0x0000109C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NeedId>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Points>k__BackingField)) * -1521134295 + EqualityComparer<EffectProbability>.Default.GetHashCode(this.<Probability>k__BackingField);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002EFE File Offset: 0x000010FE
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NeedApplierEffectSpec);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002F0C File Offset: 0x0000110C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(NeedApplierEffectSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<NeedId>k__BackingField, other.<NeedId>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<Points>k__BackingField, other.<Points>k__BackingField) && EqualityComparer<EffectProbability>.Default.Equals(this.<Probability>k__BackingField, other.<Probability>k__BackingField));
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002F85 File Offset: 0x00001185
		[CompilerGenerated]
		protected NeedApplierEffectSpec([Nullable(1)] NeedApplierEffectSpec original)
		{
			this.NeedId = original.<NeedId>k__BackingField;
			this.Points = original.<Points>k__BackingField;
			this.Probability = original.<Probability>k__BackingField;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000020F8 File Offset: 0x000002F8
		public NeedApplierEffectSpec()
		{
		}
	}
}
