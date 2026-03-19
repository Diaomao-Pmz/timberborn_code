using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.StockpileVisualization
{
	// Token: 0x0200001E RID: 30
	public class StockpilePlaneVisualizerSpec : ComponentSpec, IEquatable<StockpilePlaneVisualizerSpec>
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000045BB File Offset: 0x000027BB
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(StockpilePlaneVisualizerSpec);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000045C7 File Offset: 0x000027C7
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000045CF File Offset: 0x000027CF
		[Serialize]
		public ImmutableArray<StockpilePlaneVisualization> StockpilePlaneVisualizations { get; set; }

		// Token: 0x060000E0 RID: 224 RVA: 0x000045D8 File Offset: 0x000027D8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("StockpilePlaneVisualizerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004624 File Offset: 0x00002824
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("StockpilePlaneVisualizations = ");
			builder.Append(this.StockpilePlaneVisualizations.ToString());
			return true;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000466E File Offset: 0x0000286E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(StockpilePlaneVisualizerSpec left, StockpilePlaneVisualizerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000467A File Offset: 0x0000287A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(StockpilePlaneVisualizerSpec left, StockpilePlaneVisualizerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000468E File Offset: 0x0000288E
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<StockpilePlaneVisualization>>.Default.GetHashCode(this.<StockpilePlaneVisualizations>k__BackingField);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000046AD File Offset: 0x000028AD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as StockpilePlaneVisualizerSpec);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003481 File Offset: 0x00001681
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000046BB File Offset: 0x000028BB
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(StockpilePlaneVisualizerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<StockpilePlaneVisualization>>.Default.Equals(this.<StockpilePlaneVisualizations>k__BackingField, other.<StockpilePlaneVisualizations>k__BackingField));
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000046EC File Offset: 0x000028EC
		[CompilerGenerated]
		protected StockpilePlaneVisualizerSpec([Nullable(1)] StockpilePlaneVisualizerSpec original) : base(original)
		{
			this.StockpilePlaneVisualizations = original.<StockpilePlaneVisualizations>k__BackingField;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000035F0 File Offset: 0x000017F0
		public StockpilePlaneVisualizerSpec()
		{
		}
	}
}
