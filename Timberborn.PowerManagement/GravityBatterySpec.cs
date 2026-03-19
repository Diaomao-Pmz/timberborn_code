using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.PowerManagement
{
	// Token: 0x0200000D RID: 13
	[NullableContext(1)]
	[Nullable(0)]
	public class GravityBatterySpec : ComponentSpec, IEquatable<GravityBatterySpec>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000028A3 File Offset: 0x00000AA3
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(GravityBatterySpec);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000028AF File Offset: 0x00000AAF
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000028B7 File Offset: 0x00000AB7
		[Serialize]
		public int CapacityPerTile { get; set; }

		// Token: 0x0600004B RID: 75 RVA: 0x000028C0 File Offset: 0x00000AC0
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GravityBatterySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000290C File Offset: 0x00000B0C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("CapacityPerTile = ");
			builder.Append(this.CapacityPerTile.ToString());
			return true;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002956 File Offset: 0x00000B56
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GravityBatterySpec left, GravityBatterySpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002962 File Offset: 0x00000B62
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GravityBatterySpec left, GravityBatterySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002976 File Offset: 0x00000B76
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<CapacityPerTile>k__BackingField);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002995 File Offset: 0x00000B95
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GravityBatterySpec);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002679 File Offset: 0x00000879
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000029A3 File Offset: 0x00000BA3
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GravityBatterySpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<CapacityPerTile>k__BackingField, other.<CapacityPerTile>k__BackingField));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000029D4 File Offset: 0x00000BD4
		[CompilerGenerated]
		protected GravityBatterySpec(GravityBatterySpec original) : base(original)
		{
			this.CapacityPerTile = original.<CapacityPerTile>k__BackingField;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000026F9 File Offset: 0x000008F9
		public GravityBatterySpec()
		{
		}
	}
}
