using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.GoodConsumingBuildingSystem
{
	// Token: 0x0200000C RID: 12
	public class GoodConsumingBuildingSpec : ComponentSpec, IEquatable<GoodConsumingBuildingSpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002981 File Offset: 0x00000B81
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(GoodConsumingBuildingSpec);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000298D File Offset: 0x00000B8D
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002995 File Offset: 0x00000B95
		[Serialize]
		public int FullInventoryWorkHours { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000299E File Offset: 0x00000B9E
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000029A6 File Offset: 0x00000BA6
		[Serialize]
		public ImmutableArray<ConsumedGoodSpec> ConsumedGoods { get; set; }

		// Token: 0x06000046 RID: 70 RVA: 0x000029B0 File Offset: 0x00000BB0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GoodConsumingBuildingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000029FC File Offset: 0x00000BFC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("FullInventoryWorkHours = ");
			builder.Append(this.FullInventoryWorkHours.ToString());
			builder.Append(", ConsumedGoods = ");
			builder.Append(this.ConsumedGoods.ToString());
			return true;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A6D File Offset: 0x00000C6D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GoodConsumingBuildingSpec left, GoodConsumingBuildingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A79 File Offset: 0x00000C79
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GoodConsumingBuildingSpec left, GoodConsumingBuildingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002A8D File Offset: 0x00000C8D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<FullInventoryWorkHours>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<ConsumedGoodSpec>>.Default.GetHashCode(this.<ConsumedGoods>k__BackingField);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002AC3 File Offset: 0x00000CC3
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GoodConsumingBuildingSpec);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000223F File Offset: 0x0000043F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002AD4 File Offset: 0x00000CD4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GoodConsumingBuildingSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<FullInventoryWorkHours>k__BackingField, other.<FullInventoryWorkHours>k__BackingField) && EqualityComparer<ImmutableArray<ConsumedGoodSpec>>.Default.Equals(this.<ConsumedGoods>k__BackingField, other.<ConsumedGoods>k__BackingField));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B28 File Offset: 0x00000D28
		[CompilerGenerated]
		protected GoodConsumingBuildingSpec([Nullable(1)] GoodConsumingBuildingSpec original) : base(original)
		{
			this.FullInventoryWorkHours = original.<FullInventoryWorkHours>k__BackingField;
			this.ConsumedGoods = original.<ConsumedGoods>k__BackingField;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000022BD File Offset: 0x000004BD
		public GoodConsumingBuildingSpec()
		{
		}
	}
}
