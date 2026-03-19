using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NaturalResourcesMoisture
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	public class FloodableNaturalResourceSpec : ComponentSpec, IEquatable<FloodableNaturalResourceSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021F9 File Offset: 0x000003F9
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(FloodableNaturalResourceSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002205 File Offset: 0x00000405
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000220D File Offset: 0x0000040D
		[Serialize]
		public int MinWaterHeight { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002216 File Offset: 0x00000416
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000221E File Offset: 0x0000041E
		[Serialize]
		public int MaxWaterHeight { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002227 File Offset: 0x00000427
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000222F File Offset: 0x0000042F
		[Serialize]
		public float DaysToDie { get; set; }

		// Token: 0x06000013 RID: 19 RVA: 0x00002238 File Offset: 0x00000438
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FloodableNaturalResourceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002284 File Offset: 0x00000484
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MinWaterHeight = ");
			builder.Append(this.MinWaterHeight.ToString());
			builder.Append(", MaxWaterHeight = ");
			builder.Append(this.MaxWaterHeight.ToString());
			builder.Append(", DaysToDie = ");
			builder.Append(this.DaysToDie.ToString());
			return true;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000231C File Offset: 0x0000051C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FloodableNaturalResourceSpec left, FloodableNaturalResourceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002328 File Offset: 0x00000528
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FloodableNaturalResourceSpec left, FloodableNaturalResourceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000233C File Offset: 0x0000053C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MinWaterHeight>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxWaterHeight>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DaysToDie>k__BackingField);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002394 File Offset: 0x00000594
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FloodableNaturalResourceSpec);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023A2 File Offset: 0x000005A2
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023AC File Offset: 0x000005AC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FloodableNaturalResourceSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<MinWaterHeight>k__BackingField, other.<MinWaterHeight>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MaxWaterHeight>k__BackingField, other.<MaxWaterHeight>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DaysToDie>k__BackingField, other.<DaysToDie>k__BackingField));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002418 File Offset: 0x00000618
		[CompilerGenerated]
		protected FloodableNaturalResourceSpec(FloodableNaturalResourceSpec original) : base(original)
		{
			this.MinWaterHeight = original.<MinWaterHeight>k__BackingField;
			this.MaxWaterHeight = original.<MaxWaterHeight>k__BackingField;
			this.DaysToDie = original.<DaysToDie>k__BackingField;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002445 File Offset: 0x00000645
		public FloodableNaturalResourceSpec()
		{
		}
	}
}
