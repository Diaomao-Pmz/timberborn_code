using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x0200001F RID: 31
	public class PathMeshDrawerFactorySpec : ComponentSpec, IEquatable<PathMeshDrawerFactorySpec>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000041BE File Offset: 0x000023BE
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(PathMeshDrawerFactorySpec);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000041CA File Offset: 0x000023CA
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000041D2 File Offset: 0x000023D2
		[Serialize]
		public ImmutableArray<AssetRef<Mesh>> RegularModelVariants { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000041DB File Offset: 0x000023DB
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000041E3 File Offset: 0x000023E3
		[Serialize]
		public ImmutableArray<AssetRef<Mesh>> StairsModelVariants { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000041EC File Offset: 0x000023EC
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000041F4 File Offset: 0x000023F4
		[Serialize]
		public AssetRef<Material> Material { get; set; }

		// Token: 0x060000C8 RID: 200 RVA: 0x00004200 File Offset: 0x00002400
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PathMeshDrawerFactorySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000424C File Offset: 0x0000244C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("RegularModelVariants = ");
			builder.Append(this.RegularModelVariants.ToString());
			builder.Append(", StairsModelVariants = ");
			builder.Append(this.StairsModelVariants.ToString());
			builder.Append(", Material = ");
			builder.Append(this.Material);
			return true;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000042D6 File Offset: 0x000024D6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PathMeshDrawerFactorySpec left, PathMeshDrawerFactorySpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000042E2 File Offset: 0x000024E2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PathMeshDrawerFactorySpec left, PathMeshDrawerFactorySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000042F8 File Offset: 0x000024F8
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<AssetRef<Mesh>>>.Default.GetHashCode(this.<RegularModelVariants>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<AssetRef<Mesh>>>.Default.GetHashCode(this.<StairsModelVariants>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<UnityEngine.Material>>.Default.GetHashCode(this.<Material>k__BackingField);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004350 File Offset: 0x00002550
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PathMeshDrawerFactorySpec);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000028AB File Offset: 0x00000AAB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004360 File Offset: 0x00002560
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PathMeshDrawerFactorySpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<AssetRef<Mesh>>>.Default.Equals(this.<RegularModelVariants>k__BackingField, other.<RegularModelVariants>k__BackingField) && EqualityComparer<ImmutableArray<AssetRef<Mesh>>>.Default.Equals(this.<StairsModelVariants>k__BackingField, other.<StairsModelVariants>k__BackingField) && EqualityComparer<AssetRef<UnityEngine.Material>>.Default.Equals(this.<Material>k__BackingField, other.<Material>k__BackingField));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000043CC File Offset: 0x000025CC
		[CompilerGenerated]
		protected PathMeshDrawerFactorySpec([Nullable(1)] PathMeshDrawerFactorySpec original) : base(original)
		{
			this.RegularModelVariants = original.<RegularModelVariants>k__BackingField;
			this.StairsModelVariants = original.<StairsModelVariants>k__BackingField;
			this.Material = original.<Material>k__BackingField;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00002929 File Offset: 0x00000B29
		public PathMeshDrawerFactorySpec()
		{
		}
	}
}
