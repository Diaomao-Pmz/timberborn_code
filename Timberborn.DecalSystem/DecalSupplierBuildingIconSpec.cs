using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.DecalSystem
{
	// Token: 0x02000010 RID: 16
	public class DecalSupplierBuildingIconSpec : ComponentSpec, IEquatable<DecalSupplierBuildingIconSpec>
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002BB4 File Offset: 0x00000DB4
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DecalSupplierBuildingIconSpec);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002BC0 File Offset: 0x00000DC0
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002BC8 File Offset: 0x00000DC8
		[Serialize]
		public string IconRendererName { get; set; }

		// Token: 0x06000053 RID: 83 RVA: 0x00002BD4 File Offset: 0x00000DD4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DecalSupplierBuildingIconSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002C20 File Offset: 0x00000E20
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("IconRendererName = ");
			builder.Append(this.IconRendererName);
			return true;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002C51 File Offset: 0x00000E51
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DecalSupplierBuildingIconSpec left, DecalSupplierBuildingIconSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002C5D File Offset: 0x00000E5D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DecalSupplierBuildingIconSpec left, DecalSupplierBuildingIconSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002C71 File Offset: 0x00000E71
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<IconRendererName>k__BackingField);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002C90 File Offset: 0x00000E90
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DecalSupplierBuildingIconSpec);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002806 File Offset: 0x00000A06
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002C9E File Offset: 0x00000E9E
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DecalSupplierBuildingIconSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<IconRendererName>k__BackingField, other.<IconRendererName>k__BackingField));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002CCF File Offset: 0x00000ECF
		[CompilerGenerated]
		protected DecalSupplierBuildingIconSpec([Nullable(1)] DecalSupplierBuildingIconSpec original) : base(original)
		{
			this.IconRendererName = original.<IconRendererName>k__BackingField;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000028A9 File Offset: 0x00000AA9
		public DecalSupplierBuildingIconSpec()
		{
		}
	}
}
