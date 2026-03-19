using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BlockObstacles
{
	// Token: 0x0200000F RID: 15
	public class LayeredBlockObstacleVisualizerSpec : ComponentSpec, IEquatable<LayeredBlockObstacleVisualizerSpec>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003093 File Offset: 0x00001293
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(LayeredBlockObstacleVisualizerSpec);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000309F File Offset: 0x0000129F
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000030A7 File Offset: 0x000012A7
		[Serialize]
		public string PositionTransformName { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000030B0 File Offset: 0x000012B0
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000030B8 File Offset: 0x000012B8
		[Serialize]
		public string ScaleTransformName { get; set; }

		// Token: 0x0600006B RID: 107 RVA: 0x000030C4 File Offset: 0x000012C4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("LayeredBlockObstacleVisualizerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003110 File Offset: 0x00001310
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("PositionTransformName = ");
			builder.Append(this.PositionTransformName);
			builder.Append(", ScaleTransformName = ");
			builder.Append(this.ScaleTransformName);
			return true;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003165 File Offset: 0x00001365
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(LayeredBlockObstacleVisualizerSpec left, LayeredBlockObstacleVisualizerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003171 File Offset: 0x00001371
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(LayeredBlockObstacleVisualizerSpec left, LayeredBlockObstacleVisualizerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003185 File Offset: 0x00001385
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PositionTransformName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ScaleTransformName>k__BackingField);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000031BB File Offset: 0x000013BB
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as LayeredBlockObstacleVisualizerSpec);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002697 File Offset: 0x00000897
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000031CC File Offset: 0x000013CC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(LayeredBlockObstacleVisualizerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<PositionTransformName>k__BackingField, other.<PositionTransformName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ScaleTransformName>k__BackingField, other.<ScaleTransformName>k__BackingField));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003220 File Offset: 0x00001420
		[CompilerGenerated]
		protected LayeredBlockObstacleVisualizerSpec([Nullable(1)] LayeredBlockObstacleVisualizerSpec original) : base(original)
		{
			this.PositionTransformName = original.<PositionTransformName>k__BackingField;
			this.ScaleTransformName = original.<ScaleTransformName>k__BackingField;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000026C0 File Offset: 0x000008C0
		public LayeredBlockObstacleVisualizerSpec()
		{
		}
	}
}
