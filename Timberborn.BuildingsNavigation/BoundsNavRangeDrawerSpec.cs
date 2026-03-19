using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x0200000B RID: 11
	public class BoundsNavRangeDrawerSpec : ComponentSpec, IEquatable<BoundsNavRangeDrawerSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000276A File Offset: 0x0000096A
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BoundsNavRangeDrawerSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002776 File Offset: 0x00000976
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000277E File Offset: 0x0000097E
		[Serialize]
		public ImmutableArray<AssetRef<Mesh>> TileMeshes { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002787 File Offset: 0x00000987
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000278F File Offset: 0x0000098F
		[Serialize]
		public AssetRef<Material> Material { get; set; }

		// Token: 0x06000026 RID: 38 RVA: 0x00002798 File Offset: 0x00000998
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BoundsNavRangeDrawerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027E4 File Offset: 0x000009E4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("TileMeshes = ");
			builder.Append(this.TileMeshes.ToString());
			builder.Append(", Material = ");
			builder.Append(this.Material);
			return true;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002847 File Offset: 0x00000A47
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BoundsNavRangeDrawerSpec left, BoundsNavRangeDrawerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002853 File Offset: 0x00000A53
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BoundsNavRangeDrawerSpec left, BoundsNavRangeDrawerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002867 File Offset: 0x00000A67
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<AssetRef<Mesh>>>.Default.GetHashCode(this.<TileMeshes>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<UnityEngine.Material>>.Default.GetHashCode(this.<Material>k__BackingField);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000289D File Offset: 0x00000A9D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BoundsNavRangeDrawerSpec);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000028AB File Offset: 0x00000AAB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000028B4 File Offset: 0x00000AB4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BoundsNavRangeDrawerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<AssetRef<Mesh>>>.Default.Equals(this.<TileMeshes>k__BackingField, other.<TileMeshes>k__BackingField) && EqualityComparer<AssetRef<UnityEngine.Material>>.Default.Equals(this.<Material>k__BackingField, other.<Material>k__BackingField));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002908 File Offset: 0x00000B08
		[CompilerGenerated]
		protected BoundsNavRangeDrawerSpec([Nullable(1)] BoundsNavRangeDrawerSpec original) : base(original)
		{
			this.TileMeshes = original.<TileMeshes>k__BackingField;
			this.Material = original.<Material>k__BackingField;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002929 File Offset: 0x00000B29
		public BoundsNavRangeDrawerSpec()
		{
		}
	}
}
