using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.StockpileVisualization
{
	// Token: 0x02000017 RID: 23
	public class StockpileGoodColumnVisualizerSpec : ComponentSpec, IEquatable<StockpileGoodColumnVisualizerSpec>
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003A23 File Offset: 0x00001C23
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(StockpileGoodColumnVisualizerSpec);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003A2F File Offset: 0x00001C2F
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003A37 File Offset: 0x00001C37
		[Serialize]
		public Vector3 CenterOffset { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003A40 File Offset: 0x00001C40
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003A48 File Offset: 0x00001C48
		[Serialize]
		public string GoodVisualizationId { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003A51 File Offset: 0x00001C51
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00003A59 File Offset: 0x00001C59
		[Serialize]
		public string GoodVisualizationVariant { get; set; }

		// Token: 0x06000097 RID: 151 RVA: 0x00003A64 File Offset: 0x00001C64
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("StockpileGoodColumnVisualizerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003AB0 File Offset: 0x00001CB0
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
			builder.Append(", GoodVisualizationId = ");
			builder.Append(this.GoodVisualizationId);
			builder.Append(", GoodVisualizationVariant = ");
			builder.Append(this.GoodVisualizationVariant);
			return true;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003B2C File Offset: 0x00001D2C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(StockpileGoodColumnVisualizerSpec left, StockpileGoodColumnVisualizerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003B38 File Offset: 0x00001D38
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(StockpileGoodColumnVisualizerSpec left, StockpileGoodColumnVisualizerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003B4C File Offset: 0x00001D4C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<CenterOffset>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GoodVisualizationId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GoodVisualizationVariant>k__BackingField);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003BA4 File Offset: 0x00001DA4
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as StockpileGoodColumnVisualizerSpec);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003481 File Offset: 0x00001681
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003BB4 File Offset: 0x00001DB4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(StockpileGoodColumnVisualizerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Vector3>.Default.Equals(this.<CenterOffset>k__BackingField, other.<CenterOffset>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<GoodVisualizationId>k__BackingField, other.<GoodVisualizationId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<GoodVisualizationVariant>k__BackingField, other.<GoodVisualizationVariant>k__BackingField));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003C20 File Offset: 0x00001E20
		[CompilerGenerated]
		protected StockpileGoodColumnVisualizerSpec([Nullable(1)] StockpileGoodColumnVisualizerSpec original) : base(original)
		{
			this.CenterOffset = original.<CenterOffset>k__BackingField;
			this.GoodVisualizationId = original.<GoodVisualizationId>k__BackingField;
			this.GoodVisualizationVariant = original.<GoodVisualizationVariant>k__BackingField;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000035F0 File Offset: 0x000017F0
		public StockpileGoodColumnVisualizerSpec()
		{
		}
	}
}
