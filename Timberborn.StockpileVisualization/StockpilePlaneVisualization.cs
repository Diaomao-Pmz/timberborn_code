using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.StockpileVisualization
{
	// Token: 0x0200001A RID: 26
	public class StockpilePlaneVisualization : IEquatable<StockpilePlaneVisualization>
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004049 File Offset: 0x00002249
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(StockpilePlaneVisualization);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004055 File Offset: 0x00002255
		// (set) Token: 0x060000BE RID: 190 RVA: 0x0000405D File Offset: 0x0000225D
		[Serialize]
		public Vector3 CenterOffset { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004066 File Offset: 0x00002266
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000406E File Offset: 0x0000226E
		[Serialize]
		public Vector2 MovementRange { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004077 File Offset: 0x00002277
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x0000407F File Offset: 0x0000227F
		[Serialize]
		public string GoodVisualizationId { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004088 File Offset: 0x00002288
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00004090 File Offset: 0x00002290
		[Serialize]
		public string GoodVisualizationVariant { get; set; }

		// Token: 0x060000C5 RID: 197 RVA: 0x0000409C File Offset: 0x0000229C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("StockpilePlaneVisualization");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000040E8 File Offset: 0x000022E8
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("CenterOffset = ");
			builder.Append(this.CenterOffset.ToString());
			builder.Append(", MovementRange = ");
			builder.Append(this.MovementRange.ToString());
			builder.Append(", GoodVisualizationId = ");
			builder.Append(this.GoodVisualizationId);
			builder.Append(", GoodVisualizationVariant = ");
			builder.Append(this.GoodVisualizationVariant);
			return true;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000417B File Offset: 0x0000237B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(StockpilePlaneVisualization left, StockpilePlaneVisualization right)
		{
			return !(left == right);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004187 File Offset: 0x00002387
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(StockpilePlaneVisualization left, StockpilePlaneVisualization right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000419C File Offset: 0x0000239C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<CenterOffset>k__BackingField)) * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(this.<MovementRange>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GoodVisualizationId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GoodVisualizationVariant>k__BackingField);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004215 File Offset: 0x00002415
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as StockpilePlaneVisualization);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004224 File Offset: 0x00002424
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(StockpilePlaneVisualization other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Vector3>.Default.Equals(this.<CenterOffset>k__BackingField, other.<CenterOffset>k__BackingField) && EqualityComparer<Vector2>.Default.Equals(this.<MovementRange>k__BackingField, other.<MovementRange>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<GoodVisualizationId>k__BackingField, other.<GoodVisualizationId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<GoodVisualizationVariant>k__BackingField, other.<GoodVisualizationVariant>k__BackingField));
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000042B5 File Offset: 0x000024B5
		[CompilerGenerated]
		protected StockpilePlaneVisualization([Nullable(1)] StockpilePlaneVisualization original)
		{
			this.CenterOffset = original.<CenterOffset>k__BackingField;
			this.MovementRange = original.<MovementRange>k__BackingField;
			this.GoodVisualizationId = original.<GoodVisualizationId>k__BackingField;
			this.GoodVisualizationVariant = original.<GoodVisualizationVariant>k__BackingField;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000020F8 File Offset: 0x000002F8
		public StockpilePlaneVisualization()
		{
		}
	}
}
