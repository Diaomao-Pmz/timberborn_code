using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.BlockSystemUI
{
	// Token: 0x0200000A RID: 10
	public class BlockObjectBoundsDrawerFactorySpec : ComponentSpec, IEquatable<BlockObjectBoundsDrawerFactorySpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000238D File Offset: 0x0000058D
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BlockObjectBoundsDrawerFactorySpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002399 File Offset: 0x00000599
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000023A1 File Offset: 0x000005A1
		[Serialize]
		public AssetRef<Mesh> BlockSideMesh0010 { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000023AA File Offset: 0x000005AA
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000023B2 File Offset: 0x000005B2
		[Serialize]
		public AssetRef<Mesh> BlockSideMesh0011 { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000023BB File Offset: 0x000005BB
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000023C3 File Offset: 0x000005C3
		[Serialize]
		public AssetRef<Mesh> BlockSideMesh0111 { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000023CC File Offset: 0x000005CC
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000023D4 File Offset: 0x000005D4
		[Serialize]
		public AssetRef<Mesh> BlockSideMesh1010 { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000023DD File Offset: 0x000005DD
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000023E5 File Offset: 0x000005E5
		[Serialize]
		public AssetRef<Mesh> BlockSideMesh1111 { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000023EE File Offset: 0x000005EE
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000023F6 File Offset: 0x000005F6
		[Serialize]
		public AssetRef<Material> Material { get; set; }

		// Token: 0x0600001D RID: 29 RVA: 0x00002400 File Offset: 0x00000600
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockObjectBoundsDrawerFactorySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000244C File Offset: 0x0000064C
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
			builder.Append(", Material = ");
			builder.Append(this.Material);
			return true;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002505 File Offset: 0x00000705
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockObjectBoundsDrawerFactorySpec left, BlockObjectBoundsDrawerFactorySpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002511 File Offset: 0x00000711
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockObjectBoundsDrawerFactorySpec left, BlockObjectBoundsDrawerFactorySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002528 File Offset: 0x00000728
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((base.GetHashCode() * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<BlockSideMesh0010>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<BlockSideMesh0011>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<BlockSideMesh0111>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<BlockSideMesh1010>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<BlockSideMesh1111>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<UnityEngine.Material>>.Default.GetHashCode(this.<Material>k__BackingField);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025C5 File Offset: 0x000007C5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockObjectBoundsDrawerFactorySpec);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025D3 File Offset: 0x000007D3
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025DC File Offset: 0x000007DC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockObjectBoundsDrawerFactorySpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<BlockSideMesh0010>k__BackingField, other.<BlockSideMesh0010>k__BackingField) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<BlockSideMesh0011>k__BackingField, other.<BlockSideMesh0011>k__BackingField) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<BlockSideMesh0111>k__BackingField, other.<BlockSideMesh0111>k__BackingField) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<BlockSideMesh1010>k__BackingField, other.<BlockSideMesh1010>k__BackingField) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<BlockSideMesh1111>k__BackingField, other.<BlockSideMesh1111>k__BackingField) && EqualityComparer<AssetRef<UnityEngine.Material>>.Default.Equals(this.<Material>k__BackingField, other.<Material>k__BackingField));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002698 File Offset: 0x00000898
		[CompilerGenerated]
		protected BlockObjectBoundsDrawerFactorySpec([Nullable(1)] BlockObjectBoundsDrawerFactorySpec original) : base(original)
		{
			this.BlockSideMesh0010 = original.<BlockSideMesh0010>k__BackingField;
			this.BlockSideMesh0011 = original.<BlockSideMesh0011>k__BackingField;
			this.BlockSideMesh0111 = original.<BlockSideMesh0111>k__BackingField;
			this.BlockSideMesh1010 = original.<BlockSideMesh1010>k__BackingField;
			this.BlockSideMesh1111 = original.<BlockSideMesh1111>k__BackingField;
			this.Material = original.<Material>k__BackingField;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026F4 File Offset: 0x000008F4
		public BlockObjectBoundsDrawerFactorySpec()
		{
		}
	}
}
