using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.TerrainSystemRendering
{
	// Token: 0x02000017 RID: 23
	public class TerrainMeshManagerSpec : ComponentSpec, IEquatable<TerrainMeshManagerSpec>
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00004D8D File Offset: 0x00002F8D
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(TerrainMeshManagerSpec);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004D99 File Offset: 0x00002F99
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00004DA1 File Offset: 0x00002FA1
		[Serialize]
		public AssetRef<GameObject> TerrainTilePrefab { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004DAA File Offset: 0x00002FAA
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00004DB2 File Offset: 0x00002FB2
		[Serialize]
		public AssetRef<GameObject> LayerToolTopMeshPrefab { get; set; }

		// Token: 0x06000099 RID: 153 RVA: 0x00004DBC File Offset: 0x00002FBC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TerrainMeshManagerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004E08 File Offset: 0x00003008
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("TerrainTilePrefab = ");
			builder.Append(this.TerrainTilePrefab);
			builder.Append(", LayerToolTopMeshPrefab = ");
			builder.Append(this.LayerToolTopMeshPrefab);
			return true;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004E5D File Offset: 0x0000305D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TerrainMeshManagerSpec left, TerrainMeshManagerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004E69 File Offset: 0x00003069
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TerrainMeshManagerSpec left, TerrainMeshManagerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004E7D File Offset: 0x0000307D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<TerrainTilePrefab>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<LayerToolTopMeshPrefab>k__BackingField);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004EB3 File Offset: 0x000030B3
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TerrainMeshManagerSpec);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004021 File Offset: 0x00002221
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004EC4 File Offset: 0x000030C4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TerrainMeshManagerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<TerrainTilePrefab>k__BackingField, other.<TerrainTilePrefab>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<LayerToolTopMeshPrefab>k__BackingField, other.<LayerToolTopMeshPrefab>k__BackingField));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004F18 File Offset: 0x00003118
		[CompilerGenerated]
		protected TerrainMeshManagerSpec([Nullable(1)] TerrainMeshManagerSpec original) : base(original)
		{
			this.TerrainTilePrefab = original.<TerrainTilePrefab>k__BackingField;
			this.LayerToolTopMeshPrefab = original.<LayerToolTopMeshPrefab>k__BackingField;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000422C File Offset: 0x0000242C
		public TerrainMeshManagerSpec()
		{
		}
	}
}
