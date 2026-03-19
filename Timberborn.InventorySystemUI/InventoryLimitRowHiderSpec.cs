using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.InventorySystemUI
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	public class InventoryLimitRowHiderSpec : ComponentSpec, IEquatable<InventoryLimitRowHiderSpec>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002E89 File Offset: 0x00001089
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(InventoryLimitRowHiderSpec);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E98 File Offset: 0x00001098
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("InventoryLimitRowHiderSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002EE4 File Offset: 0x000010E4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002EED File Offset: 0x000010ED
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(InventoryLimitRowHiderSpec left, InventoryLimitRowHiderSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002EF9 File Offset: 0x000010F9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(InventoryLimitRowHiderSpec left, InventoryLimitRowHiderSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002F0D File Offset: 0x0000110D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F15 File Offset: 0x00001115
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as InventoryLimitRowHiderSpec);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002F23 File Offset: 0x00001123
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002F2C File Offset: 0x0000112C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(InventoryLimitRowHiderSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002F43 File Offset: 0x00001143
		[CompilerGenerated]
		protected InventoryLimitRowHiderSpec(InventoryLimitRowHiderSpec original) : base(original)
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002F4C File Offset: 0x0000114C
		public InventoryLimitRowHiderSpec()
		{
		}
	}
}
