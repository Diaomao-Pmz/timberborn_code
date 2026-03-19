using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.BlockSystemNavigation
{
	// Token: 0x02000010 RID: 16
	public class BlockObjectNavMeshUnblockedCoordinatesSpec : IEquatable<BlockObjectNavMeshUnblockedCoordinatesSpec>
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002FD0 File Offset: 0x000011D0
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BlockObjectNavMeshUnblockedCoordinatesSpec);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002FDC File Offset: 0x000011DC
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002FE4 File Offset: 0x000011E4
		[Serialize]
		public string Group { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002FED File Offset: 0x000011ED
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002FF5 File Offset: 0x000011F5
		[Serialize]
		public Vector3Int Coordinates { get; set; }

		// Token: 0x0600007F RID: 127 RVA: 0x00003000 File Offset: 0x00001200
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockObjectNavMeshUnblockedCoordinatesSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000304C File Offset: 0x0000124C
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Group = ");
			builder.Append(this.Group);
			builder.Append(", Coordinates = ");
			builder.Append(this.Coordinates.ToString());
			return true;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000309F File Offset: 0x0000129F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockObjectNavMeshUnblockedCoordinatesSpec left, BlockObjectNavMeshUnblockedCoordinatesSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000030AB File Offset: 0x000012AB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockObjectNavMeshUnblockedCoordinatesSpec left, BlockObjectNavMeshUnblockedCoordinatesSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000030BF File Offset: 0x000012BF
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Group>k__BackingField)) * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<Coordinates>k__BackingField);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000030FF File Offset: 0x000012FF
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockObjectNavMeshUnblockedCoordinatesSpec);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003110 File Offset: 0x00001310
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockObjectNavMeshUnblockedCoordinatesSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Group>k__BackingField, other.<Group>k__BackingField) && EqualityComparer<Vector3Int>.Default.Equals(this.<Coordinates>k__BackingField, other.<Coordinates>k__BackingField));
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003171 File Offset: 0x00001371
		[CompilerGenerated]
		protected BlockObjectNavMeshUnblockedCoordinatesSpec([Nullable(1)] BlockObjectNavMeshUnblockedCoordinatesSpec original)
		{
			this.Group = original.<Group>k__BackingField;
			this.Coordinates = original.<Coordinates>k__BackingField;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000020F6 File Offset: 0x000002F6
		public BlockObjectNavMeshUnblockedCoordinatesSpec()
		{
		}
	}
}
