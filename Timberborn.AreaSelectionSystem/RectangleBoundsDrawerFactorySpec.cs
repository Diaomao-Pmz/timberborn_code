using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.AreaSelectionSystem
{
	// Token: 0x02000024 RID: 36
	public class RectangleBoundsDrawerFactorySpec : ComponentSpec, IEquatable<RectangleBoundsDrawerFactorySpec>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003BF2 File Offset: 0x00001DF2
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(RectangleBoundsDrawerFactorySpec);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003BFE File Offset: 0x00001DFE
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003C06 File Offset: 0x00001E06
		[Serialize]
		public AssetRef<Mesh> BlockSideMesh0010 { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003C0F File Offset: 0x00001E0F
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00003C17 File Offset: 0x00001E17
		[Serialize]
		public AssetRef<Mesh> BlockSideMesh0011 { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003C20 File Offset: 0x00001E20
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00003C28 File Offset: 0x00001E28
		[Serialize]
		public AssetRef<Mesh> BlockSideMesh0111 { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003C31 File Offset: 0x00001E31
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00003C39 File Offset: 0x00001E39
		[Serialize]
		public AssetRef<Mesh> BlockSideMesh1010 { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003C42 File Offset: 0x00001E42
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003C4A File Offset: 0x00001E4A
		[Serialize]
		public AssetRef<Mesh> BlockSideMesh1111 { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003C53 File Offset: 0x00001E53
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003C5B File Offset: 0x00001E5B
		[Serialize]
		public AssetRef<Material> BlockSideMaterial { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003C64 File Offset: 0x00001E64
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00003C6C File Offset: 0x00001E6C
		[Serialize]
		public AssetRef<Mesh> BlockBottomMesh { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003C75 File Offset: 0x00001E75
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003C7D File Offset: 0x00001E7D
		[Serialize]
		public AssetRef<Material> BlockBottomMaterial { get; set; }

		// Token: 0x060000A8 RID: 168 RVA: 0x00003C88 File Offset: 0x00001E88
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RectangleBoundsDrawerFactorySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003CD4 File Offset: 0x00001ED4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("BlockSideMesh0010 = ");
			builder.Append(this.BlockSideMesh0010);
			builder.Append(", BlockSideMesh0011 = ");
			builder.Append(this.BlockSideMesh0011);
			builder.Append(", BlockSideMesh0111 = ");
			builder.Append(this.BlockSideMesh0111);
			builder.Append(", BlockSideMesh1010 = ");
			builder.Append(this.BlockSideMesh1010);
			builder.Append(", BlockSideMesh1111 = ");
			builder.Append(this.BlockSideMesh1111);
			builder.Append(", BlockSideMaterial = ");
			builder.Append(this.BlockSideMaterial);
			builder.Append(", BlockBottomMesh = ");
			builder.Append(this.BlockBottomMesh);
			builder.Append(", BlockBottomMaterial = ");
			builder.Append(this.BlockBottomMaterial);
			return true;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003DBF File Offset: 0x00001FBF
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RectangleBoundsDrawerFactorySpec left, RectangleBoundsDrawerFactorySpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003DCB File Offset: 0x00001FCB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RectangleBoundsDrawerFactorySpec left, RectangleBoundsDrawerFactorySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003DE0 File Offset: 0x00001FE0
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((((base.GetHashCode() * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<BlockSideMesh0010>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<BlockSideMesh0011>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<BlockSideMesh0111>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<BlockSideMesh1010>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<BlockSideMesh1111>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Material>>.Default.GetHashCode(this.<BlockSideMaterial>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<BlockBottomMesh>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Material>>.Default.GetHashCode(this.<BlockBottomMaterial>k__BackingField);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003EAB File Offset: 0x000020AB
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RectangleBoundsDrawerFactorySpec);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000336D File Offset: 0x0000156D
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003EBC File Offset: 0x000020BC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RectangleBoundsDrawerFactorySpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<BlockSideMesh0010>k__BackingField, other.<BlockSideMesh0010>k__BackingField) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<BlockSideMesh0011>k__BackingField, other.<BlockSideMesh0011>k__BackingField) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<BlockSideMesh0111>k__BackingField, other.<BlockSideMesh0111>k__BackingField) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<BlockSideMesh1010>k__BackingField, other.<BlockSideMesh1010>k__BackingField) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<BlockSideMesh1111>k__BackingField, other.<BlockSideMesh1111>k__BackingField) && EqualityComparer<AssetRef<Material>>.Default.Equals(this.<BlockSideMaterial>k__BackingField, other.<BlockSideMaterial>k__BackingField) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<BlockBottomMesh>k__BackingField, other.<BlockBottomMesh>k__BackingField) && EqualityComparer<AssetRef<Material>>.Default.Equals(this.<BlockBottomMaterial>k__BackingField, other.<BlockBottomMaterial>k__BackingField));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003FAC File Offset: 0x000021AC
		[CompilerGenerated]
		protected RectangleBoundsDrawerFactorySpec([Nullable(1)] RectangleBoundsDrawerFactorySpec original) : base(original)
		{
			this.BlockSideMesh0010 = original.<BlockSideMesh0010>k__BackingField;
			this.BlockSideMesh0011 = original.<BlockSideMesh0011>k__BackingField;
			this.BlockSideMesh0111 = original.<BlockSideMesh0111>k__BackingField;
			this.BlockSideMesh1010 = original.<BlockSideMesh1010>k__BackingField;
			this.BlockSideMesh1111 = original.<BlockSideMesh1111>k__BackingField;
			this.BlockSideMaterial = original.<BlockSideMaterial>k__BackingField;
			this.BlockBottomMesh = original.<BlockBottomMesh>k__BackingField;
			this.BlockBottomMaterial = original.<BlockBottomMaterial>k__BackingField;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000033ED File Offset: 0x000015ED
		public RectangleBoundsDrawerFactorySpec()
		{
		}
	}
}
