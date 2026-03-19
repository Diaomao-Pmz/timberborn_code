using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.CameraSystem
{
	// Token: 0x02000019 RID: 25
	[NullableContext(1)]
	[Nullable(0)]
	public class FloatLimitsSpec : IEquatable<FloatLimitsSpec>
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000046CC File Offset: 0x000028CC
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(FloatLimitsSpec);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000046D8 File Offset: 0x000028D8
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000046E0 File Offset: 0x000028E0
		[Serialize]
		public float Min { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000046E9 File Offset: 0x000028E9
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000046F1 File Offset: 0x000028F1
		[Serialize]
		public float Max { get; set; }

		// Token: 0x060000E2 RID: 226 RVA: 0x000046FC File Offset: 0x000028FC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FloatLimitsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004748 File Offset: 0x00002948
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Min = ");
			builder.Append(this.Min.ToString());
			builder.Append(", Max = ");
			builder.Append(this.Max.ToString());
			return true;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000047A9 File Offset: 0x000029A9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FloatLimitsSpec left, FloatLimitsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000047B5 File Offset: 0x000029B5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FloatLimitsSpec left, FloatLimitsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000047C9 File Offset: 0x000029C9
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Min>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Max>k__BackingField);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004809 File Offset: 0x00002A09
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FloatLimitsSpec);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004818 File Offset: 0x00002A18
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FloatLimitsSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<float>.Default.Equals(this.<Min>k__BackingField, other.<Min>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<Max>k__BackingField, other.<Max>k__BackingField));
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004879 File Offset: 0x00002A79
		[CompilerGenerated]
		protected FloatLimitsSpec(FloatLimitsSpec original)
		{
			this.Min = original.<Min>k__BackingField;
			this.Max = original.<Max>k__BackingField;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000020F8 File Offset: 0x000002F8
		public FloatLimitsSpec()
		{
		}
	}
}
