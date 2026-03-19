using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BlockSystemNavigation
{
	// Token: 0x0200000B RID: 11
	public class BlockObjectNavMeshEdgeGroupSpec : IEquatable<BlockObjectNavMeshEdgeGroupSpec>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000024E1 File Offset: 0x000006E1
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BlockObjectNavMeshEdgeGroupSpec);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000024ED File Offset: 0x000006ED
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000024F5 File Offset: 0x000006F5
		[Serialize]
		public float Cost { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000024FE File Offset: 0x000006FE
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002506 File Offset: 0x00000706
		[Serialize]
		public BlockObjectNavMeshGroup Group { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000250F File Offset: 0x0000070F
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002517 File Offset: 0x00000717
		[Serialize]
		public bool IsPath { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002520 File Offset: 0x00000720
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002528 File Offset: 0x00000728
		[Serialize]
		public ImmutableArray<BlockObjectNavMeshEdgeSpec> AddedEdges { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002531 File Offset: 0x00000731
		public bool UseGroup
		{
			get
			{
				return this.Group.UseGroup;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000253E File Offset: 0x0000073E
		public string GroupName
		{
			get
			{
				return this.Group.GroupName;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000254C File Offset: 0x0000074C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockObjectNavMeshEdgeGroupSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002598 File Offset: 0x00000798
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Cost = ");
			builder.Append(this.Cost.ToString());
			builder.Append(", Group = ");
			builder.Append(this.Group);
			builder.Append(", IsPath = ");
			builder.Append(this.IsPath.ToString());
			builder.Append(", AddedEdges = ");
			builder.Append(this.AddedEdges.ToString());
			builder.Append(", UseGroup = ");
			builder.Append(this.UseGroup.ToString());
			builder.Append(", GroupName = ");
			builder.Append(this.GroupName);
			return true;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002679 File Offset: 0x00000879
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockObjectNavMeshEdgeGroupSpec left, BlockObjectNavMeshEdgeGroupSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002685 File Offset: 0x00000885
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockObjectNavMeshEdgeGroupSpec left, BlockObjectNavMeshEdgeGroupSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000269C File Offset: 0x0000089C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Cost>k__BackingField)) * -1521134295 + EqualityComparer<BlockObjectNavMeshGroup>.Default.GetHashCode(this.<Group>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsPath>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<BlockObjectNavMeshEdgeSpec>>.Default.GetHashCode(this.<AddedEdges>k__BackingField);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002715 File Offset: 0x00000915
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockObjectNavMeshEdgeGroupSpec);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002724 File Offset: 0x00000924
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockObjectNavMeshEdgeGroupSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<float>.Default.Equals(this.<Cost>k__BackingField, other.<Cost>k__BackingField) && EqualityComparer<BlockObjectNavMeshGroup>.Default.Equals(this.<Group>k__BackingField, other.<Group>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsPath>k__BackingField, other.<IsPath>k__BackingField) && EqualityComparer<ImmutableArray<BlockObjectNavMeshEdgeSpec>>.Default.Equals(this.<AddedEdges>k__BackingField, other.<AddedEdges>k__BackingField));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000027B5 File Offset: 0x000009B5
		[CompilerGenerated]
		protected BlockObjectNavMeshEdgeGroupSpec([Nullable(1)] BlockObjectNavMeshEdgeGroupSpec original)
		{
			this.Cost = original.<Cost>k__BackingField;
			this.Group = original.<Group>k__BackingField;
			this.IsPath = original.<IsPath>k__BackingField;
			this.AddedEdges = original.<AddedEdges>k__BackingField;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000020F6 File Offset: 0x000002F6
		public BlockObjectNavMeshEdgeGroupSpec()
		{
		}
	}
}
