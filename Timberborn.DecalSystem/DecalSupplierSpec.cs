using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.DecalSystem
{
	// Token: 0x02000011 RID: 17
	public class DecalSupplierSpec : ComponentSpec, IEquatable<DecalSupplierSpec>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002CE4 File Offset: 0x00000EE4
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DecalSupplierSpec);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002CF0 File Offset: 0x00000EF0
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002CF8 File Offset: 0x00000EF8
		[Serialize]
		public string Category { get; set; }

		// Token: 0x06000061 RID: 97 RVA: 0x00002D04 File Offset: 0x00000F04
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DecalSupplierSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002D50 File Offset: 0x00000F50
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Category = ");
			builder.Append(this.Category);
			return true;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002D81 File Offset: 0x00000F81
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DecalSupplierSpec left, DecalSupplierSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002D8D File Offset: 0x00000F8D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DecalSupplierSpec left, DecalSupplierSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002DA1 File Offset: 0x00000FA1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Category>k__BackingField);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002DC0 File Offset: 0x00000FC0
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DecalSupplierSpec);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002806 File Offset: 0x00000A06
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002DCE File Offset: 0x00000FCE
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DecalSupplierSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Category>k__BackingField, other.<Category>k__BackingField));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002DFF File Offset: 0x00000FFF
		[CompilerGenerated]
		protected DecalSupplierSpec([Nullable(1)] DecalSupplierSpec original) : base(original)
		{
			this.Category = original.<Category>k__BackingField;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000028A9 File Offset: 0x00000AA9
		public DecalSupplierSpec()
		{
		}
	}
}
