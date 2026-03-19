using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200000E RID: 14
	[NullableContext(1)]
	[Nullable(0)]
	public class DepthSensorSpec : ComponentSpec, IEquatable<DepthSensorSpec>
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002ECB File Offset: 0x000010CB
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DepthSensorSpec);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002ED7 File Offset: 0x000010D7
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002EDF File Offset: 0x000010DF
		[Serialize]
		public Vector3Int SensorCoordinates { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002EE8 File Offset: 0x000010E8
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002EF0 File Offset: 0x000010F0
		[Serialize]
		public float SensorHeightOffset { get; set; }

		// Token: 0x06000068 RID: 104 RVA: 0x00002EFC File Offset: 0x000010FC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DepthSensorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002F48 File Offset: 0x00001148
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("SensorCoordinates = ");
			builder.Append(this.SensorCoordinates.ToString());
			builder.Append(", SensorHeightOffset = ");
			builder.Append(this.SensorHeightOffset.ToString());
			return true;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002FB9 File Offset: 0x000011B9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DepthSensorSpec left, DepthSensorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002FC5 File Offset: 0x000011C5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DepthSensorSpec left, DepthSensorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002FD9 File Offset: 0x000011D9
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<SensorCoordinates>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<SensorHeightOffset>k__BackingField);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000300F File Offset: 0x0000120F
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DepthSensorSpec);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003020 File Offset: 0x00001220
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DepthSensorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Vector3Int>.Default.Equals(this.<SensorCoordinates>k__BackingField, other.<SensorCoordinates>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<SensorHeightOffset>k__BackingField, other.<SensorHeightOffset>k__BackingField));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003074 File Offset: 0x00001274
		[CompilerGenerated]
		protected DepthSensorSpec(DepthSensorSpec original) : base(original)
		{
			this.SensorCoordinates = original.<SensorCoordinates>k__BackingField;
			this.SensorHeightOffset = original.<SensorHeightOffset>k__BackingField;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002778 File Offset: 0x00000978
		public DepthSensorSpec()
		{
		}
	}
}
