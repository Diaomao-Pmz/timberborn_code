using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000026 RID: 38
	[NullableContext(1)]
	[Nullable(0)]
	public class StreamGaugeSpec : ComponentSpec, IEquatable<StreamGaugeSpec>
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00004FF5 File Offset: 0x000031F5
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(StreamGaugeSpec);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00005001 File Offset: 0x00003201
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00005009 File Offset: 0x00003209
		[Serialize]
		public float MaxWaterLevel { get; set; }

		// Token: 0x0600018C RID: 396 RVA: 0x00005014 File Offset: 0x00003214
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("StreamGaugeSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005060 File Offset: 0x00003260
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MaxWaterLevel = ");
			builder.Append(this.MaxWaterLevel.ToString());
			return true;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000050AA File Offset: 0x000032AA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(StreamGaugeSpec left, StreamGaugeSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000050B6 File Offset: 0x000032B6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(StreamGaugeSpec left, StreamGaugeSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000050CA File Offset: 0x000032CA
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxWaterLevel>k__BackingField);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000050E9 File Offset: 0x000032E9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as StreamGaugeSpec);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00002B9B File Offset: 0x00000D9B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000050F7 File Offset: 0x000032F7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(StreamGaugeSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<MaxWaterLevel>k__BackingField, other.<MaxWaterLevel>k__BackingField));
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005128 File Offset: 0x00003328
		[CompilerGenerated]
		protected StreamGaugeSpec(StreamGaugeSpec original) : base(original)
		{
			this.MaxWaterLevel = original.<MaxWaterLevel>k__BackingField;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00002CBC File Offset: 0x00000EBC
		public StreamGaugeSpec()
		{
		}
	}
}
