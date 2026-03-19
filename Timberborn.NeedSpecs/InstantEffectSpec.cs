using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NeedSpecs
{
	// Token: 0x0200000B RID: 11
	public class InstantEffectSpec : IEquatable<InstantEffectSpec>
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002844 File Offset: 0x00000A44
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(InstantEffectSpec);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002850 File Offset: 0x00000A50
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002858 File Offset: 0x00000A58
		[Serialize]
		public string NeedId { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002861 File Offset: 0x00000A61
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002869 File Offset: 0x00000A69
		[Serialize]
		public float Points { get; set; }

		// Token: 0x06000044 RID: 68 RVA: 0x00002874 File Offset: 0x00000A74
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("InstantEffectSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000028C0 File Offset: 0x00000AC0
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("NeedId = ");
			builder.Append(this.NeedId);
			builder.Append(", Points = ");
			builder.Append(this.Points.ToString());
			return true;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002913 File Offset: 0x00000B13
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(InstantEffectSpec left, InstantEffectSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000291F File Offset: 0x00000B1F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(InstantEffectSpec left, InstantEffectSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002933 File Offset: 0x00000B33
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NeedId>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Points>k__BackingField);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002973 File Offset: 0x00000B73
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as InstantEffectSpec);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002984 File Offset: 0x00000B84
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(InstantEffectSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<NeedId>k__BackingField, other.<NeedId>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<Points>k__BackingField, other.<Points>k__BackingField));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000029E5 File Offset: 0x00000BE5
		[CompilerGenerated]
		protected InstantEffectSpec([Nullable(1)] InstantEffectSpec original)
		{
			this.NeedId = original.<NeedId>k__BackingField;
			this.Points = original.<Points>k__BackingField;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000020F6 File Offset: 0x000002F6
		public InstantEffectSpec()
		{
		}
	}
}
