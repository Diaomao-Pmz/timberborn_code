using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.Buildings
{
	// Token: 0x0200001B RID: 27
	public class BuildingTerrainCutoutSpec : ComponentSpec, IEquatable<BuildingTerrainCutoutSpec>
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00003CF7 File Offset: 0x00001EF7
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BuildingTerrainCutoutSpec);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003D03 File Offset: 0x00001F03
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00003D0B File Offset: 0x00001F0B
		[Serialize]
		public ImmutableArray<Vector3Int> CutoutTiles { get; set; }

		// Token: 0x060000DE RID: 222 RVA: 0x00003D14 File Offset: 0x00001F14
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BuildingTerrainCutoutSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003D60 File Offset: 0x00001F60
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("CutoutTiles = ");
			builder.Append(this.CutoutTiles.ToString());
			return true;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003DAA File Offset: 0x00001FAA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BuildingTerrainCutoutSpec left, BuildingTerrainCutoutSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003DB6 File Offset: 0x00001FB6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BuildingTerrainCutoutSpec left, BuildingTerrainCutoutSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003DCA File Offset: 0x00001FCA
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<Vector3Int>>.Default.GetHashCode(this.<CutoutTiles>k__BackingField);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003DE9 File Offset: 0x00001FE9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BuildingTerrainCutoutSpec);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000023ED File Offset: 0x000005ED
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003DF7 File Offset: 0x00001FF7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BuildingTerrainCutoutSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<Vector3Int>>.Default.Equals(this.<CutoutTiles>k__BackingField, other.<CutoutTiles>k__BackingField));
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003E28 File Offset: 0x00002028
		[CompilerGenerated]
		protected BuildingTerrainCutoutSpec([Nullable(1)] BuildingTerrainCutoutSpec original) : base(original)
		{
			this.CutoutTiles = original.<CutoutTiles>k__BackingField;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000246D File Offset: 0x0000066D
		public BuildingTerrainCutoutSpec()
		{
		}
	}
}
