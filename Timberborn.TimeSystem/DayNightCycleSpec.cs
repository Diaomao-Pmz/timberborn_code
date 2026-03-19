using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TimeSystem
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public class DayNightCycleSpec : ComponentSpec, IEquatable<DayNightCycleSpec>
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002868 File Offset: 0x00000A68
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DayNightCycleSpec);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002874 File Offset: 0x00000A74
		// (set) Token: 0x06000045 RID: 69 RVA: 0x0000287C File Offset: 0x00000A7C
		[Serialize]
		public int HoursPassedOnNewGame { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002885 File Offset: 0x00000A85
		// (set) Token: 0x06000047 RID: 71 RVA: 0x0000288D File Offset: 0x00000A8D
		[Serialize]
		public int ConfiguredDayLengthInTicks { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002896 File Offset: 0x00000A96
		// (set) Token: 0x06000049 RID: 73 RVA: 0x0000289E File Offset: 0x00000A9E
		[Serialize]
		public int ConfiguredDaytimeLengthInHours { get; set; }

		// Token: 0x0600004A RID: 74 RVA: 0x000028A8 File Offset: 0x00000AA8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DayNightCycleSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000028F4 File Offset: 0x00000AF4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("HoursPassedOnNewGame = ");
			builder.Append(this.HoursPassedOnNewGame.ToString());
			builder.Append(", ConfiguredDayLengthInTicks = ");
			builder.Append(this.ConfiguredDayLengthInTicks.ToString());
			builder.Append(", ConfiguredDaytimeLengthInHours = ");
			builder.Append(this.ConfiguredDaytimeLengthInHours.ToString());
			return true;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000298C File Offset: 0x00000B8C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DayNightCycleSpec left, DayNightCycleSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002998 File Offset: 0x00000B98
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DayNightCycleSpec left, DayNightCycleSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000029AC File Offset: 0x00000BAC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<HoursPassedOnNewGame>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ConfiguredDayLengthInTicks>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ConfiguredDaytimeLengthInHours>k__BackingField);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002A04 File Offset: 0x00000C04
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DayNightCycleSpec);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000231F File Offset: 0x0000051F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002A14 File Offset: 0x00000C14
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DayNightCycleSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<HoursPassedOnNewGame>k__BackingField, other.<HoursPassedOnNewGame>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ConfiguredDayLengthInTicks>k__BackingField, other.<ConfiguredDayLengthInTicks>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ConfiguredDaytimeLengthInHours>k__BackingField, other.<ConfiguredDaytimeLengthInHours>k__BackingField));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A80 File Offset: 0x00000C80
		[CompilerGenerated]
		protected DayNightCycleSpec(DayNightCycleSpec original) : base(original)
		{
			this.HoursPassedOnNewGame = original.<HoursPassedOnNewGame>k__BackingField;
			this.ConfiguredDayLengthInTicks = original.<ConfiguredDayLengthInTicks>k__BackingField;
			this.ConfiguredDaytimeLengthInHours = original.<ConfiguredDaytimeLengthInHours>k__BackingField;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000239D File Offset: 0x0000059D
		public DayNightCycleSpec()
		{
		}
	}
}
