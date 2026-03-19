using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.BlockSystemNavigation
{
	// Token: 0x0200000C RID: 12
	[NullableContext(1)]
	[Nullable(0)]
	public class BlockObjectNavMeshEdgeSpec : IEquatable<BlockObjectNavMeshEdgeSpec>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000027ED File Offset: 0x000009ED
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BlockObjectNavMeshEdgeSpec);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000027F9 File Offset: 0x000009F9
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002801 File Offset: 0x00000A01
		[Serialize]
		public Vector3Int Start { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000280A File Offset: 0x00000A0A
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002812 File Offset: 0x00000A12
		[Serialize]
		public Vector3Int End { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000281B File Offset: 0x00000A1B
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002823 File Offset: 0x00000A23
		[Serialize]
		public bool IsTwoWay { get; set; }

		// Token: 0x06000049 RID: 73 RVA: 0x0000282C File Offset: 0x00000A2C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockObjectNavMeshEdgeSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002878 File Offset: 0x00000A78
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Start = ");
			builder.Append(this.Start.ToString());
			builder.Append(", End = ");
			builder.Append(this.End.ToString());
			builder.Append(", IsTwoWay = ");
			builder.Append(this.IsTwoWay.ToString());
			return true;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002900 File Offset: 0x00000B00
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockObjectNavMeshEdgeSpec left, BlockObjectNavMeshEdgeSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000290C File Offset: 0x00000B0C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockObjectNavMeshEdgeSpec left, BlockObjectNavMeshEdgeSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002920 File Offset: 0x00000B20
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<Start>k__BackingField)) * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<End>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsTwoWay>k__BackingField);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002982 File Offset: 0x00000B82
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockObjectNavMeshEdgeSpec);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002990 File Offset: 0x00000B90
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockObjectNavMeshEdgeSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Vector3Int>.Default.Equals(this.<Start>k__BackingField, other.<Start>k__BackingField) && EqualityComparer<Vector3Int>.Default.Equals(this.<End>k__BackingField, other.<End>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsTwoWay>k__BackingField, other.<IsTwoWay>k__BackingField));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002A09 File Offset: 0x00000C09
		[CompilerGenerated]
		protected BlockObjectNavMeshEdgeSpec(BlockObjectNavMeshEdgeSpec original)
		{
			this.Start = original.<Start>k__BackingField;
			this.End = original.<End>k__BackingField;
			this.IsTwoWay = original.<IsTwoWay>k__BackingField;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000020F6 File Offset: 0x000002F6
		public BlockObjectNavMeshEdgeSpec()
		{
		}
	}
}
