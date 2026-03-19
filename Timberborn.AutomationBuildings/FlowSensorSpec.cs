using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	public class FlowSensorSpec : ComponentSpec, IEquatable<FlowSensorSpec>
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003543 File Offset: 0x00001743
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(FlowSensorSpec);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000354F File Offset: 0x0000174F
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00003557 File Offset: 0x00001757
		[Serialize]
		public Vector3Int SensorCoordinates { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003560 File Offset: 0x00001760
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00003568 File Offset: 0x00001768
		[Serialize]
		public float MaxThreshold { get; set; }

		// Token: 0x060000A1 RID: 161 RVA: 0x00003574 File Offset: 0x00001774
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FlowSensorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000035C0 File Offset: 0x000017C0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("SensorCoordinates = ");
			builder.Append(this.SensorCoordinates.ToString());
			builder.Append(", MaxThreshold = ");
			builder.Append(this.MaxThreshold.ToString());
			return true;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003631 File Offset: 0x00001831
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FlowSensorSpec left, FlowSensorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000363D File Offset: 0x0000183D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FlowSensorSpec left, FlowSensorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003651 File Offset: 0x00001851
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<SensorCoordinates>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxThreshold>k__BackingField);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003687 File Offset: 0x00001887
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FlowSensorSpec);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003698 File Offset: 0x00001898
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FlowSensorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Vector3Int>.Default.Equals(this.<SensorCoordinates>k__BackingField, other.<SensorCoordinates>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxThreshold>k__BackingField, other.<MaxThreshold>k__BackingField));
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000036EC File Offset: 0x000018EC
		[CompilerGenerated]
		protected FlowSensorSpec(FlowSensorSpec original) : base(original)
		{
			this.SensorCoordinates = original.<SensorCoordinates>k__BackingField;
			this.MaxThreshold = original.<MaxThreshold>k__BackingField;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002778 File Offset: 0x00000978
		public FlowSensorSpec()
		{
		}
	}
}
