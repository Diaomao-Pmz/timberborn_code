using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Characters
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	public class GameSpeedThrottlerSpec : ComponentSpec, IEquatable<GameSpeedThrottlerSpec>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000268C File Offset: 0x0000088C
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(GameSpeedThrottlerSpec);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002698 File Offset: 0x00000898
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000026A0 File Offset: 0x000008A0
		[Serialize]
		public int MinPopulation { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000026A9 File Offset: 0x000008A9
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000026B1 File Offset: 0x000008B1
		[Serialize]
		public int MaxPopulation { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000026BA File Offset: 0x000008BA
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000026C2 File Offset: 0x000008C2
		[Serialize]
		public float MinGameSpeedScale { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000026CB File Offset: 0x000008CB
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000026D3 File Offset: 0x000008D3
		[Serialize]
		public float MaxGameSpeedScale { get; set; }

		// Token: 0x06000041 RID: 65 RVA: 0x000026DC File Offset: 0x000008DC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GameSpeedThrottlerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002728 File Offset: 0x00000928
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MinPopulation = ");
			builder.Append(this.MinPopulation.ToString());
			builder.Append(", MaxPopulation = ");
			builder.Append(this.MaxPopulation.ToString());
			builder.Append(", MinGameSpeedScale = ");
			builder.Append(this.MinGameSpeedScale.ToString());
			builder.Append(", MaxGameSpeedScale = ");
			builder.Append(this.MaxGameSpeedScale.ToString());
			return true;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000027E7 File Offset: 0x000009E7
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GameSpeedThrottlerSpec left, GameSpeedThrottlerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000027F3 File Offset: 0x000009F3
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GameSpeedThrottlerSpec left, GameSpeedThrottlerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002808 File Offset: 0x00000A08
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MinPopulation>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxPopulation>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinGameSpeedScale>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxGameSpeedScale>k__BackingField);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002877 File Offset: 0x00000A77
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GameSpeedThrottlerSpec);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002885 File Offset: 0x00000A85
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002890 File Offset: 0x00000A90
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GameSpeedThrottlerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<MinPopulation>k__BackingField, other.<MinPopulation>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MaxPopulation>k__BackingField, other.<MaxPopulation>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MinGameSpeedScale>k__BackingField, other.<MinGameSpeedScale>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxGameSpeedScale>k__BackingField, other.<MaxGameSpeedScale>k__BackingField));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002914 File Offset: 0x00000B14
		[CompilerGenerated]
		protected GameSpeedThrottlerSpec(GameSpeedThrottlerSpec original) : base(original)
		{
			this.MinPopulation = original.<MinPopulation>k__BackingField;
			this.MaxPopulation = original.<MaxPopulation>k__BackingField;
			this.MinGameSpeedScale = original.<MinGameSpeedScale>k__BackingField;
			this.MaxGameSpeedScale = original.<MaxGameSpeedScale>k__BackingField;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000294D File Offset: 0x00000B4D
		public GameSpeedThrottlerSpec()
		{
		}
	}
}
