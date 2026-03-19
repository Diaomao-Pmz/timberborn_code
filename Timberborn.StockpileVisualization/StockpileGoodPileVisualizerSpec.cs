using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.StockpileVisualization
{
	// Token: 0x02000019 RID: 25
	public class StockpileGoodPileVisualizerSpec : ComponentSpec, IEquatable<StockpileGoodPileVisualizerSpec>
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003E7F File Offset: 0x0000207F
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(StockpileGoodPileVisualizerSpec);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003E8B File Offset: 0x0000208B
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003E93 File Offset: 0x00002093
		[Serialize]
		public Vector3 CenterOffset { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003E9C File Offset: 0x0000209C
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003EA4 File Offset: 0x000020A4
		[Serialize]
		public ImmutableArray<string> GoodPileVisualizations { get; set; }

		// Token: 0x060000B1 RID: 177 RVA: 0x00003EB0 File Offset: 0x000020B0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("StockpileGoodPileVisualizerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003EFC File Offset: 0x000020FC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("CenterOffset = ");
			builder.Append(this.CenterOffset.ToString());
			builder.Append(", GoodPileVisualizations = ");
			builder.Append(this.GoodPileVisualizations.ToString());
			return true;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003F6D File Offset: 0x0000216D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(StockpileGoodPileVisualizerSpec left, StockpileGoodPileVisualizerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003F79 File Offset: 0x00002179
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(StockpileGoodPileVisualizerSpec left, StockpileGoodPileVisualizerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003F8D File Offset: 0x0000218D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<CenterOffset>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<GoodPileVisualizations>k__BackingField);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003FC3 File Offset: 0x000021C3
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as StockpileGoodPileVisualizerSpec);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003481 File Offset: 0x00001681
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003FD4 File Offset: 0x000021D4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(StockpileGoodPileVisualizerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Vector3>.Default.Equals(this.<CenterOffset>k__BackingField, other.<CenterOffset>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<GoodPileVisualizations>k__BackingField, other.<GoodPileVisualizations>k__BackingField));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004028 File Offset: 0x00002228
		[CompilerGenerated]
		protected StockpileGoodPileVisualizerSpec([Nullable(1)] StockpileGoodPileVisualizerSpec original) : base(original)
		{
			this.CenterOffset = original.<CenterOffset>k__BackingField;
			this.GoodPileVisualizations = original.<GoodPileVisualizations>k__BackingField;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000035F0 File Offset: 0x000017F0
		public StockpileGoodPileVisualizerSpec()
		{
		}
	}
}
