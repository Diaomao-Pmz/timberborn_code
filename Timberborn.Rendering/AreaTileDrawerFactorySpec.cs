using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x02000009 RID: 9
	public class AreaTileDrawerFactorySpec : ComponentSpec, IEquatable<AreaTileDrawerFactorySpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000253F File Offset: 0x0000073F
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(AreaTileDrawerFactorySpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000254B File Offset: 0x0000074B
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002553 File Offset: 0x00000753
		[Serialize]
		public AssetRef<Mesh> TileMesh { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000255C File Offset: 0x0000075C
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002564 File Offset: 0x00000764
		[Serialize]
		public AssetRef<Material> TileMaterial { get; set; }

		// Token: 0x0600001B RID: 27 RVA: 0x00002570 File Offset: 0x00000770
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AreaTileDrawerFactorySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025BC File Offset: 0x000007BC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("TileMesh = ");
			builder.Append(this.TileMesh);
			builder.Append(", TileMaterial = ");
			builder.Append(this.TileMaterial);
			return true;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002611 File Offset: 0x00000811
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(AreaTileDrawerFactorySpec left, AreaTileDrawerFactorySpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000261D File Offset: 0x0000081D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(AreaTileDrawerFactorySpec left, AreaTileDrawerFactorySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002631 File Offset: 0x00000831
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<TileMesh>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Material>>.Default.GetHashCode(this.<TileMaterial>k__BackingField);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002667 File Offset: 0x00000867
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AreaTileDrawerFactorySpec);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002675 File Offset: 0x00000875
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002680 File Offset: 0x00000880
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(AreaTileDrawerFactorySpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<TileMesh>k__BackingField, other.<TileMesh>k__BackingField) && EqualityComparer<AssetRef<Material>>.Default.Equals(this.<TileMaterial>k__BackingField, other.<TileMaterial>k__BackingField));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026D4 File Offset: 0x000008D4
		[CompilerGenerated]
		protected AreaTileDrawerFactorySpec([Nullable(1)] AreaTileDrawerFactorySpec original) : base(original)
		{
			this.TileMesh = original.<TileMesh>k__BackingField;
			this.TileMaterial = original.<TileMaterial>k__BackingField;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026F5 File Offset: 0x000008F5
		public AreaTileDrawerFactorySpec()
		{
		}
	}
}
