using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.RangedEffectBuildingUI
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class RangedEffectBuildingColorsSpec : ComponentSpec, IEquatable<RangedEffectBuildingColorsSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002440 File Offset: 0x00000640
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(RangedEffectBuildingColorsSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000244C File Offset: 0x0000064C
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002454 File Offset: 0x00000654
		[Serialize]
		public Color BuildingRangeTile { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000245D File Offset: 0x0000065D
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002465 File Offset: 0x00000665
		[Serialize]
		public Color BuildingRangeObject { get; set; }

		// Token: 0x06000025 RID: 37 RVA: 0x00002470 File Offset: 0x00000670
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RangedEffectBuildingColorsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024BC File Offset: 0x000006BC
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("BuildingRangeTile = ");
			builder.Append(this.BuildingRangeTile.ToString());
			builder.Append(", BuildingRangeObject = ");
			builder.Append(this.BuildingRangeObject.ToString());
			return true;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000252D File Offset: 0x0000072D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RangedEffectBuildingColorsSpec left, RangedEffectBuildingColorsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002539 File Offset: 0x00000739
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RangedEffectBuildingColorsSpec left, RangedEffectBuildingColorsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000254D File Offset: 0x0000074D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<BuildingRangeTile>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<BuildingRangeObject>k__BackingField);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002583 File Offset: 0x00000783
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RangedEffectBuildingColorsSpec);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002591 File Offset: 0x00000791
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000259C File Offset: 0x0000079C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RangedEffectBuildingColorsSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<BuildingRangeTile>k__BackingField, other.<BuildingRangeTile>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<BuildingRangeObject>k__BackingField, other.<BuildingRangeObject>k__BackingField));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000025F0 File Offset: 0x000007F0
		[CompilerGenerated]
		protected RangedEffectBuildingColorsSpec(RangedEffectBuildingColorsSpec original) : base(original)
		{
			this.BuildingRangeTile = original.<BuildingRangeTile>k__BackingField;
			this.BuildingRangeObject = original.<BuildingRangeObject>k__BackingField;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002611 File Offset: 0x00000811
		public RangedEffectBuildingColorsSpec()
		{
		}
	}
}
