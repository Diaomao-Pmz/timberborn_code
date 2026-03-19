using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x02000015 RID: 21
	public class WaterMeshSpec : ComponentSpec, IEquatable<WaterMeshSpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00004A74 File Offset: 0x00002C74
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WaterMeshSpec);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00004A80 File Offset: 0x00002C80
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00004A88 File Offset: 0x00002C88
		[Serialize]
		public AssetRef<GameObject> WaterTile { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004A91 File Offset: 0x00002C91
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00004A99 File Offset: 0x00002C99
		[Serialize]
		public AssetRef<Material> OpaqueMaterial { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00004AA2 File Offset: 0x00002CA2
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00004AAA File Offset: 0x00002CAA
		[Serialize]
		public AssetRef<Material> TransparentMaterial { get; set; }

		// Token: 0x06000082 RID: 130 RVA: 0x00004AB4 File Offset: 0x00002CB4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterMeshSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004B00 File Offset: 0x00002D00
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("WaterTile = ");
			builder.Append(this.WaterTile);
			builder.Append(", OpaqueMaterial = ");
			builder.Append(this.OpaqueMaterial);
			builder.Append(", TransparentMaterial = ");
			builder.Append(this.TransparentMaterial);
			return true;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004B6E File Offset: 0x00002D6E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterMeshSpec left, WaterMeshSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004B7A File Offset: 0x00002D7A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterMeshSpec left, WaterMeshSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004B90 File Offset: 0x00002D90
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<WaterTile>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Material>>.Default.GetHashCode(this.<OpaqueMaterial>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Material>>.Default.GetHashCode(this.<TransparentMaterial>k__BackingField);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004BE8 File Offset: 0x00002DE8
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterMeshSpec);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004BF6 File Offset: 0x00002DF6
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004C00 File Offset: 0x00002E00
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterMeshSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<WaterTile>k__BackingField, other.<WaterTile>k__BackingField) && EqualityComparer<AssetRef<Material>>.Default.Equals(this.<OpaqueMaterial>k__BackingField, other.<OpaqueMaterial>k__BackingField) && EqualityComparer<AssetRef<Material>>.Default.Equals(this.<TransparentMaterial>k__BackingField, other.<TransparentMaterial>k__BackingField));
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004C6C File Offset: 0x00002E6C
		[CompilerGenerated]
		protected WaterMeshSpec([Nullable(1)] WaterMeshSpec original) : base(original)
		{
			this.WaterTile = original.<WaterTile>k__BackingField;
			this.OpaqueMaterial = original.<OpaqueMaterial>k__BackingField;
			this.TransparentMaterial = original.<TransparentMaterial>k__BackingField;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004C99 File Offset: 0x00002E99
		public WaterMeshSpec()
		{
		}
	}
}
