using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.PowerGeneration
{
	// Token: 0x02000012 RID: 18
	public class WaterPoweredGeneratorSpec : ComponentSpec, IEquatable<WaterPoweredGeneratorSpec>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002C7D File Offset: 0x00000E7D
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WaterPoweredGeneratorSpec);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002C89 File Offset: 0x00000E89
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002C91 File Offset: 0x00000E91
		[Serialize]
		public ImmutableArray<Vector2Int> Blocks { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002C9A File Offset: 0x00000E9A
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002CA2 File Offset: 0x00000EA2
		[Serialize]
		public Vector2 ExpectedWaterDirection { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002CAB File Offset: 0x00000EAB
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002CB3 File Offset: 0x00000EB3
		[Serialize]
		public float MinRequiredOutflow { get; set; }

		// Token: 0x0600007C RID: 124 RVA: 0x00002CBC File Offset: 0x00000EBC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterPoweredGeneratorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002D08 File Offset: 0x00000F08
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Blocks = ");
			builder.Append(this.Blocks.ToString());
			builder.Append(", ExpectedWaterDirection = ");
			builder.Append(this.ExpectedWaterDirection.ToString());
			builder.Append(", MinRequiredOutflow = ");
			builder.Append(this.MinRequiredOutflow.ToString());
			return true;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002DA0 File Offset: 0x00000FA0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterPoweredGeneratorSpec left, WaterPoweredGeneratorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002DAC File Offset: 0x00000FAC
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterPoweredGeneratorSpec left, WaterPoweredGeneratorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002DC0 File Offset: 0x00000FC0
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<Vector2Int>>.Default.GetHashCode(this.<Blocks>k__BackingField)) * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(this.<ExpectedWaterDirection>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinRequiredOutflow>k__BackingField);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002E18 File Offset: 0x00001018
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterPoweredGeneratorSpec);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002233 File Offset: 0x00000433
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002E28 File Offset: 0x00001028
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterPoweredGeneratorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<Vector2Int>>.Default.Equals(this.<Blocks>k__BackingField, other.<Blocks>k__BackingField) && EqualityComparer<Vector2>.Default.Equals(this.<ExpectedWaterDirection>k__BackingField, other.<ExpectedWaterDirection>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MinRequiredOutflow>k__BackingField, other.<MinRequiredOutflow>k__BackingField));
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002E94 File Offset: 0x00001094
		[CompilerGenerated]
		protected WaterPoweredGeneratorSpec([Nullable(1)] WaterPoweredGeneratorSpec original) : base(original)
		{
			this.Blocks = original.<Blocks>k__BackingField;
			this.ExpectedWaterDirection = original.<ExpectedWaterDirection>k__BackingField;
			this.MinRequiredOutflow = original.<MinRequiredOutflow>k__BackingField;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000225C File Offset: 0x0000045C
		public WaterPoweredGeneratorSpec()
		{
		}
	}
}
