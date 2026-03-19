using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x0200001E RID: 30
	public class BlockObjectTerrainCutoutSpec : ComponentSpec, IEquatable<BlockObjectTerrainCutoutSpec>
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004483 File Offset: 0x00002683
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BlockObjectTerrainCutoutSpec);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x0000448F File Offset: 0x0000268F
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00004497 File Offset: 0x00002697
		[Serialize]
		public ImmutableArray<Vector3Int> CutoutTiles { get; set; }

		// Token: 0x060000E7 RID: 231 RVA: 0x000044A0 File Offset: 0x000026A0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockObjectTerrainCutoutSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000044EC File Offset: 0x000026EC
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

		// Token: 0x060000E9 RID: 233 RVA: 0x00004536 File Offset: 0x00002736
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockObjectTerrainCutoutSpec left, BlockObjectTerrainCutoutSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004542 File Offset: 0x00002742
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockObjectTerrainCutoutSpec left, BlockObjectTerrainCutoutSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004556 File Offset: 0x00002756
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<Vector3Int>>.Default.GetHashCode(this.<CutoutTiles>k__BackingField);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004575 File Offset: 0x00002775
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockObjectTerrainCutoutSpec);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003CFB File Offset: 0x00001EFB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004583 File Offset: 0x00002783
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockObjectTerrainCutoutSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<Vector3Int>>.Default.Equals(this.<CutoutTiles>k__BackingField, other.<CutoutTiles>k__BackingField));
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000045B4 File Offset: 0x000027B4
		[CompilerGenerated]
		protected BlockObjectTerrainCutoutSpec([Nullable(1)] BlockObjectTerrainCutoutSpec original) : base(original)
		{
			this.CutoutTiles = original.<CutoutTiles>k__BackingField;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003E1C File Offset: 0x0000201C
		public BlockObjectTerrainCutoutSpec()
		{
		}
	}
}
