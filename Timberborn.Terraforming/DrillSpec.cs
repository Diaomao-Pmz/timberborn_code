using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.Terraforming
{
	// Token: 0x0200000E RID: 14
	public class DrillSpec : ComponentSpec, IEquatable<DrillSpec>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002F29 File Offset: 0x00001129
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DrillSpec);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002F35 File Offset: 0x00001135
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002F3D File Offset: 0x0000113D
		[Serialize]
		public ImmutableArray<Vector3Int> DrillableCoordinates { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002F46 File Offset: 0x00001146
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002F4E File Offset: 0x0000114E
		[Serialize]
		public string RemovalEffectPath { get; set; }

		// Token: 0x06000069 RID: 105 RVA: 0x00002F58 File Offset: 0x00001158
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DrillSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002FA4 File Offset: 0x000011A4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DrillableCoordinates = ");
			builder.Append(this.DrillableCoordinates.ToString());
			builder.Append(", RemovalEffectPath = ");
			builder.Append(this.RemovalEffectPath);
			return true;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003007 File Offset: 0x00001207
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DrillSpec left, DrillSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003013 File Offset: 0x00001213
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DrillSpec left, DrillSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003027 File Offset: 0x00001227
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<Vector3Int>>.Default.GetHashCode(this.<DrillableCoordinates>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RemovalEffectPath>k__BackingField);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000305D File Offset: 0x0000125D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DrillSpec);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000262F File Offset: 0x0000082F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000306C File Offset: 0x0000126C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DrillSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<Vector3Int>>.Default.Equals(this.<DrillableCoordinates>k__BackingField, other.<DrillableCoordinates>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RemovalEffectPath>k__BackingField, other.<RemovalEffectPath>k__BackingField));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000030C0 File Offset: 0x000012C0
		[CompilerGenerated]
		protected DrillSpec([Nullable(1)] DrillSpec original) : base(original)
		{
			this.DrillableCoordinates = original.<DrillableCoordinates>k__BackingField;
			this.RemovalEffectPath = original.<RemovalEffectPath>k__BackingField;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000026AD File Offset: 0x000008AD
		public DrillSpec()
		{
		}
	}
}
