using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.SkySystem
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	public class DayStageCycleSpec : ComponentSpec, IEquatable<DayStageCycleSpec>
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000027DE File Offset: 0x000009DE
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DayStageCycleSpec);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000027EA File Offset: 0x000009EA
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000027F2 File Offset: 0x000009F2
		[Serialize]
		public float SunriseSunsetLengthInHours { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000027FB File Offset: 0x000009FB
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002803 File Offset: 0x00000A03
		[Serialize]
		public float TransitionLengthInHours { get; set; }

		// Token: 0x06000030 RID: 48 RVA: 0x0000280C File Offset: 0x00000A0C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DayStageCycleSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002858 File Offset: 0x00000A58
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("SunriseSunsetLengthInHours = ");
			builder.Append(this.SunriseSunsetLengthInHours.ToString());
			builder.Append(", TransitionLengthInHours = ");
			builder.Append(this.TransitionLengthInHours.ToString());
			return true;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000028C9 File Offset: 0x00000AC9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DayStageCycleSpec left, DayStageCycleSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028D5 File Offset: 0x00000AD5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DayStageCycleSpec left, DayStageCycleSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028E9 File Offset: 0x00000AE9
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<SunriseSunsetLengthInHours>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<TransitionLengthInHours>k__BackingField);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000291F File Offset: 0x00000B1F
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DayStageCycleSpec);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000292D File Offset: 0x00000B2D
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002938 File Offset: 0x00000B38
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DayStageCycleSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<SunriseSunsetLengthInHours>k__BackingField, other.<SunriseSunsetLengthInHours>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<TransitionLengthInHours>k__BackingField, other.<TransitionLengthInHours>k__BackingField));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000298C File Offset: 0x00000B8C
		[CompilerGenerated]
		protected DayStageCycleSpec(DayStageCycleSpec original) : base(original)
		{
			this.SunriseSunsetLengthInHours = original.<SunriseSunsetLengthInHours>k__BackingField;
			this.TransitionLengthInHours = original.<TransitionLengthInHours>k__BackingField;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000029AD File Offset: 0x00000BAD
		public DayStageCycleSpec()
		{
		}
	}
}
