using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x02000015 RID: 21
	public class BlockObjectToolGroupSpec : ComponentSpec, IEquatable<BlockObjectToolGroupSpec>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002F83 File Offset: 0x00001183
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BlockObjectToolGroupSpec);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002F8F File Offset: 0x0000118F
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002F97 File Offset: 0x00001197
		[Serialize]
		public string Id { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002FA0 File Offset: 0x000011A0
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002FA8 File Offset: 0x000011A8
		[Serialize]
		public int Order { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002FB1 File Offset: 0x000011B1
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002FB9 File Offset: 0x000011B9
		[Serialize]
		public string NameLocKey { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002FC2 File Offset: 0x000011C2
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002FCA File Offset: 0x000011CA
		[Serialize]
		public AssetRef<Sprite> Icon { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002FD3 File Offset: 0x000011D3
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002FDB File Offset: 0x000011DB
		[Serialize]
		public bool FallbackGroup { get; set; }

		// Token: 0x06000063 RID: 99 RVA: 0x00002FE4 File Offset: 0x000011E4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockObjectToolGroupSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003030 File Offset: 0x00001230
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", Order = ");
			builder.Append(this.Order.ToString());
			builder.Append(", NameLocKey = ");
			builder.Append(this.NameLocKey);
			builder.Append(", Icon = ");
			builder.Append(this.Icon);
			builder.Append(", FallbackGroup = ");
			builder.Append(this.FallbackGroup.ToString());
			return true;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000030EC File Offset: 0x000012EC
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockObjectToolGroupSpec left, BlockObjectToolGroupSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000030F8 File Offset: 0x000012F8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockObjectToolGroupSpec left, BlockObjectToolGroupSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000310C File Offset: 0x0000130C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Order>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NameLocKey>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Sprite>>.Default.GetHashCode(this.<Icon>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<FallbackGroup>k__BackingField);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003192 File Offset: 0x00001392
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockObjectToolGroupSpec);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002892 File Offset: 0x00000A92
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000031A0 File Offset: 0x000013A0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockObjectToolGroupSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Order>k__BackingField, other.<Order>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<NameLocKey>k__BackingField, other.<NameLocKey>k__BackingField) && EqualityComparer<AssetRef<Sprite>>.Default.Equals(this.<Icon>k__BackingField, other.<Icon>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<FallbackGroup>k__BackingField, other.<FallbackGroup>k__BackingField));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003240 File Offset: 0x00001440
		[CompilerGenerated]
		protected BlockObjectToolGroupSpec([Nullable(1)] BlockObjectToolGroupSpec original) : base(original)
		{
			this.Id = original.<Id>k__BackingField;
			this.Order = original.<Order>k__BackingField;
			this.NameLocKey = original.<NameLocKey>k__BackingField;
			this.Icon = original.<Icon>k__BackingField;
			this.FallbackGroup = original.<FallbackGroup>k__BackingField;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002935 File Offset: 0x00000B35
		public BlockObjectToolGroupSpec()
		{
		}
	}
}
