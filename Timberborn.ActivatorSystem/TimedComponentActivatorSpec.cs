using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ActivatorSystem
{
	// Token: 0x0200000E RID: 14
	public class TimedComponentActivatorSpec : ComponentSpec, IEquatable<TimedComponentActivatorSpec>
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002EFC File Offset: 0x000010FC
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(TimedComponentActivatorSpec);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002F08 File Offset: 0x00001108
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002F10 File Offset: 0x00001110
		[Serialize]
		public bool IsOptionallyActivable { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002F19 File Offset: 0x00001119
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002F21 File Offset: 0x00001121
		[Serialize]
		public int CyclesUntilCountdownActivation { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002F2A File Offset: 0x0000112A
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002F32 File Offset: 0x00001132
		[Serialize]
		public float DaysUntilActivation { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002F3B File Offset: 0x0000113B
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002F43 File Offset: 0x00001143
		[Serialize]
		public string ProgressBarActiveLabelLocKey { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002F4C File Offset: 0x0000114C
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002F54 File Offset: 0x00001154
		[Serialize]
		public string ProgressBarNotActiveLabelLocKey { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002F5D File Offset: 0x0000115D
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002F65 File Offset: 0x00001165
		[Serialize]
		public bool IsHazardousActivator { get; set; }

		// Token: 0x06000073 RID: 115 RVA: 0x00002F70 File Offset: 0x00001170
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TimedComponentActivatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002FBC File Offset: 0x000011BC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("IsOptionallyActivable = ");
			builder.Append(this.IsOptionallyActivable.ToString());
			builder.Append(", CyclesUntilCountdownActivation = ");
			builder.Append(this.CyclesUntilCountdownActivation.ToString());
			builder.Append(", DaysUntilActivation = ");
			builder.Append(this.DaysUntilActivation.ToString());
			builder.Append(", ProgressBarActiveLabelLocKey = ");
			builder.Append(this.ProgressBarActiveLabelLocKey);
			builder.Append(", ProgressBarNotActiveLabelLocKey = ");
			builder.Append(this.ProgressBarNotActiveLabelLocKey);
			builder.Append(", IsHazardousActivator = ");
			builder.Append(this.IsHazardousActivator.ToString());
			return true;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000030AD File Offset: 0x000012AD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TimedComponentActivatorSpec left, TimedComponentActivatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000030B9 File Offset: 0x000012B9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TimedComponentActivatorSpec left, TimedComponentActivatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000030D0 File Offset: 0x000012D0
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((base.GetHashCode() * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsOptionallyActivable>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<CyclesUntilCountdownActivation>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DaysUntilActivation>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ProgressBarActiveLabelLocKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ProgressBarNotActiveLabelLocKey>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsHazardousActivator>k__BackingField);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000316D File Offset: 0x0000136D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TimedComponentActivatorSpec);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000023EE File Offset: 0x000005EE
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000317C File Offset: 0x0000137C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TimedComponentActivatorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<bool>.Default.Equals(this.<IsOptionallyActivable>k__BackingField, other.<IsOptionallyActivable>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<CyclesUntilCountdownActivation>k__BackingField, other.<CyclesUntilCountdownActivation>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DaysUntilActivation>k__BackingField, other.<DaysUntilActivation>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ProgressBarActiveLabelLocKey>k__BackingField, other.<ProgressBarActiveLabelLocKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ProgressBarNotActiveLabelLocKey>k__BackingField, other.<ProgressBarNotActiveLabelLocKey>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsHazardousActivator>k__BackingField, other.<IsHazardousActivator>k__BackingField));
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003238 File Offset: 0x00001438
		[CompilerGenerated]
		protected TimedComponentActivatorSpec([Nullable(1)] TimedComponentActivatorSpec original) : base(original)
		{
			this.IsOptionallyActivable = original.<IsOptionallyActivable>k__BackingField;
			this.CyclesUntilCountdownActivation = original.<CyclesUntilCountdownActivation>k__BackingField;
			this.DaysUntilActivation = original.<DaysUntilActivation>k__BackingField;
			this.ProgressBarActiveLabelLocKey = original.<ProgressBarActiveLabelLocKey>k__BackingField;
			this.ProgressBarNotActiveLabelLocKey = original.<ProgressBarNotActiveLabelLocKey>k__BackingField;
			this.IsHazardousActivator = original.<IsHazardousActivator>k__BackingField;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002491 File Offset: 0x00000691
		public TimedComponentActivatorSpec()
		{
		}
	}
}
