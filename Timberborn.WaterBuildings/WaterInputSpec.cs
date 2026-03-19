using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000035 RID: 53
	public class WaterInputSpec : ComponentSpec, IEquatable<WaterInputSpec>
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000268 RID: 616 RVA: 0x000077BB File Offset: 0x000059BB
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WaterInputSpec);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000077C7 File Offset: 0x000059C7
		// (set) Token: 0x0600026A RID: 618 RVA: 0x000077CF File Offset: 0x000059CF
		[Serialize]
		public Vector3Int WaterInputCoordinates { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000077D8 File Offset: 0x000059D8
		// (set) Token: 0x0600026C RID: 620 RVA: 0x000077E0 File Offset: 0x000059E0
		[Serialize]
		public int MaxDepth { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600026D RID: 621 RVA: 0x000077E9 File Offset: 0x000059E9
		// (set) Token: 0x0600026E RID: 622 RVA: 0x000077F1 File Offset: 0x000059F1
		[Serialize]
		public string PipeSegmentPrefabPath { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000077FA File Offset: 0x000059FA
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00007802 File Offset: 0x00005A02
		[Serialize]
		public string PipeParentName { get; set; }

		// Token: 0x06000271 RID: 625 RVA: 0x0000780C File Offset: 0x00005A0C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterInputSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00007858 File Offset: 0x00005A58
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("WaterInputCoordinates = ");
			builder.Append(this.WaterInputCoordinates.ToString());
			builder.Append(", MaxDepth = ");
			builder.Append(this.MaxDepth.ToString());
			builder.Append(", PipeSegmentPrefabPath = ");
			builder.Append(this.PipeSegmentPrefabPath);
			builder.Append(", PipeParentName = ");
			builder.Append(this.PipeParentName);
			return true;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000078FB File Offset: 0x00005AFB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterInputSpec left, WaterInputSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00007907 File Offset: 0x00005B07
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterInputSpec left, WaterInputSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000791C File Offset: 0x00005B1C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<WaterInputCoordinates>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxDepth>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PipeSegmentPrefabPath>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PipeParentName>k__BackingField);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000798B File Offset: 0x00005B8B
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterInputSpec);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00002B9B File Offset: 0x00000D9B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000799C File Offset: 0x00005B9C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterInputSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Vector3Int>.Default.Equals(this.<WaterInputCoordinates>k__BackingField, other.<WaterInputCoordinates>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MaxDepth>k__BackingField, other.<MaxDepth>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<PipeSegmentPrefabPath>k__BackingField, other.<PipeSegmentPrefabPath>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<PipeParentName>k__BackingField, other.<PipeParentName>k__BackingField));
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00007A20 File Offset: 0x00005C20
		[CompilerGenerated]
		protected WaterInputSpec([Nullable(1)] WaterInputSpec original) : base(original)
		{
			this.WaterInputCoordinates = original.<WaterInputCoordinates>k__BackingField;
			this.MaxDepth = original.<MaxDepth>k__BackingField;
			this.PipeSegmentPrefabPath = original.<PipeSegmentPrefabPath>k__BackingField;
			this.PipeParentName = original.<PipeParentName>k__BackingField;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00002CBC File Offset: 0x00000EBC
		public WaterInputSpec()
		{
		}
	}
}
