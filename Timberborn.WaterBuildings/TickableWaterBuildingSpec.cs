using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000028 RID: 40
	[NullableContext(1)]
	[Nullable(0)]
	public class TickableWaterBuildingSpec : ComponentSpec, IEquatable<TickableWaterBuildingSpec>
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00005372 File Offset: 0x00003572
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(TickableWaterBuildingSpec);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000537E File Offset: 0x0000357E
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00005386 File Offset: 0x00003586
		[Serialize]
		public Vector3Int WaterCoordinates { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000538F File Offset: 0x0000358F
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00005397 File Offset: 0x00003597
		[Serialize]
		public float MinWaterHeight { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000053A0 File Offset: 0x000035A0
		// (set) Token: 0x060001AB RID: 427 RVA: 0x000053A8 File Offset: 0x000035A8
		[Serialize]
		public float ChangeRange { get; set; }

		// Token: 0x060001AC RID: 428 RVA: 0x000053B4 File Offset: 0x000035B4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TickableWaterBuildingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00005400 File Offset: 0x00003600
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("WaterCoordinates = ");
			builder.Append(this.WaterCoordinates.ToString());
			builder.Append(", MinWaterHeight = ");
			builder.Append(this.MinWaterHeight.ToString());
			builder.Append(", ChangeRange = ");
			builder.Append(this.ChangeRange.ToString());
			return true;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005498 File Offset: 0x00003698
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TickableWaterBuildingSpec left, TickableWaterBuildingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000054A4 File Offset: 0x000036A4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TickableWaterBuildingSpec left, TickableWaterBuildingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000054B8 File Offset: 0x000036B8
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<WaterCoordinates>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinWaterHeight>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ChangeRange>k__BackingField);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00005510 File Offset: 0x00003710
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TickableWaterBuildingSpec);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00002B9B File Offset: 0x00000D9B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00005520 File Offset: 0x00003720
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TickableWaterBuildingSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Vector3Int>.Default.Equals(this.<WaterCoordinates>k__BackingField, other.<WaterCoordinates>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MinWaterHeight>k__BackingField, other.<MinWaterHeight>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ChangeRange>k__BackingField, other.<ChangeRange>k__BackingField));
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000558C File Offset: 0x0000378C
		[CompilerGenerated]
		protected TickableWaterBuildingSpec(TickableWaterBuildingSpec original) : base(original)
		{
			this.WaterCoordinates = original.<WaterCoordinates>k__BackingField;
			this.MinWaterHeight = original.<MinWaterHeight>k__BackingField;
			this.ChangeRange = original.<ChangeRange>k__BackingField;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00002CBC File Offset: 0x00000EBC
		public TickableWaterBuildingSpec()
		{
		}
	}
}
