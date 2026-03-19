using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.BlockSystemNavigation
{
	// Token: 0x0200000A RID: 10
	public class BlockObjectNavMeshBlockedEdgeSpec : IEquatable<BlockObjectNavMeshBlockedEdgeSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000022A4 File Offset: 0x000004A4
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BlockObjectNavMeshBlockedEdgeSpec);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000022B0 File Offset: 0x000004B0
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000022B8 File Offset: 0x000004B8
		[Serialize]
		public string Group { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000022C1 File Offset: 0x000004C1
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000022C9 File Offset: 0x000004C9
		[Serialize]
		public Vector3Int Start { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000022D2 File Offset: 0x000004D2
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000022DA File Offset: 0x000004DA
		[Serialize]
		public Vector3Int End { get; set; }

		// Token: 0x06000023 RID: 35 RVA: 0x000022E4 File Offset: 0x000004E4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockObjectNavMeshBlockedEdgeSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002330 File Offset: 0x00000530
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Group = ");
			builder.Append(this.Group);
			builder.Append(", Start = ");
			builder.Append(this.Start.ToString());
			builder.Append(", End = ");
			builder.Append(this.End.ToString());
			return true;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023AA File Offset: 0x000005AA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockObjectNavMeshBlockedEdgeSpec left, BlockObjectNavMeshBlockedEdgeSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023B6 File Offset: 0x000005B6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockObjectNavMeshBlockedEdgeSpec left, BlockObjectNavMeshBlockedEdgeSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000023CC File Offset: 0x000005CC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Group>k__BackingField)) * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<Start>k__BackingField)) * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<End>k__BackingField);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000242E File Offset: 0x0000062E
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockObjectNavMeshBlockedEdgeSpec);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000243C File Offset: 0x0000063C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockObjectNavMeshBlockedEdgeSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Group>k__BackingField, other.<Group>k__BackingField) && EqualityComparer<Vector3Int>.Default.Equals(this.<Start>k__BackingField, other.<Start>k__BackingField) && EqualityComparer<Vector3Int>.Default.Equals(this.<End>k__BackingField, other.<End>k__BackingField));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024B5 File Offset: 0x000006B5
		[CompilerGenerated]
		protected BlockObjectNavMeshBlockedEdgeSpec([Nullable(1)] BlockObjectNavMeshBlockedEdgeSpec original)
		{
			this.Group = original.<Group>k__BackingField;
			this.Start = original.<Start>k__BackingField;
			this.End = original.<End>k__BackingField;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000020F6 File Offset: 0x000002F6
		public BlockObjectNavMeshBlockedEdgeSpec()
		{
		}
	}
}
