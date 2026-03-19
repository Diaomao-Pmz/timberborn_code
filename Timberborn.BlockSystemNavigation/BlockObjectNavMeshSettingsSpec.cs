using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BlockSystemNavigation
{
	// Token: 0x0200000F RID: 15
	public class BlockObjectNavMeshSettingsSpec : ComponentSpec, IEquatable<BlockObjectNavMeshSettingsSpec>
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002C94 File Offset: 0x00000E94
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BlockObjectNavMeshSettingsSpec);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002CA0 File Offset: 0x00000EA0
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002CA8 File Offset: 0x00000EA8
		[Serialize]
		public bool NoAutoWalls { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002CB1 File Offset: 0x00000EB1
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002CB9 File Offset: 0x00000EB9
		[Serialize]
		public bool GenerateFloorsOnStackable { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002CC2 File Offset: 0x00000EC2
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002CCA File Offset: 0x00000ECA
		[Serialize]
		public ImmutableArray<BlockObjectNavMeshEdgeGroupSpec> EdgeGroups { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002CD3 File Offset: 0x00000ED3
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002CDB File Offset: 0x00000EDB
		[Serialize]
		public ImmutableArray<BlockObjectNavMeshUnblockedCoordinatesSpec> UnblockedCoordinates { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002CE4 File Offset: 0x00000EE4
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002CEC File Offset: 0x00000EEC
		[Serialize]
		public ImmutableArray<BlockObjectNavMeshBlockedEdgeSpec> BlockedEdges { get; set; }

		// Token: 0x0600006F RID: 111 RVA: 0x00002CF8 File Offset: 0x00000EF8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockObjectNavMeshSettingsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002D44 File Offset: 0x00000F44
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("NoAutoWalls = ");
			builder.Append(this.NoAutoWalls.ToString());
			builder.Append(", GenerateFloorsOnStackable = ");
			builder.Append(this.GenerateFloorsOnStackable.ToString());
			builder.Append(", EdgeGroups = ");
			builder.Append(this.EdgeGroups.ToString());
			builder.Append(", UnblockedCoordinates = ");
			builder.Append(this.UnblockedCoordinates.ToString());
			builder.Append(", BlockedEdges = ");
			builder.Append(this.BlockedEdges.ToString());
			return true;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002E2A File Offset: 0x0000102A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockObjectNavMeshSettingsSpec left, BlockObjectNavMeshSettingsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002E36 File Offset: 0x00001036
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockObjectNavMeshSettingsSpec left, BlockObjectNavMeshSettingsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002E4C File Offset: 0x0000104C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((base.GetHashCode() * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<NoAutoWalls>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<GenerateFloorsOnStackable>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<BlockObjectNavMeshEdgeGroupSpec>>.Default.GetHashCode(this.<EdgeGroups>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<BlockObjectNavMeshUnblockedCoordinatesSpec>>.Default.GetHashCode(this.<UnblockedCoordinates>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<BlockObjectNavMeshBlockedEdgeSpec>>.Default.GetHashCode(this.<BlockedEdges>k__BackingField);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002ED2 File Offset: 0x000010D2
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockObjectNavMeshSettingsSpec);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002273 File Offset: 0x00000473
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002EE0 File Offset: 0x000010E0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockObjectNavMeshSettingsSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<bool>.Default.Equals(this.<NoAutoWalls>k__BackingField, other.<NoAutoWalls>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<GenerateFloorsOnStackable>k__BackingField, other.<GenerateFloorsOnStackable>k__BackingField) && EqualityComparer<ImmutableArray<BlockObjectNavMeshEdgeGroupSpec>>.Default.Equals(this.<EdgeGroups>k__BackingField, other.<EdgeGroups>k__BackingField) && EqualityComparer<ImmutableArray<BlockObjectNavMeshUnblockedCoordinatesSpec>>.Default.Equals(this.<UnblockedCoordinates>k__BackingField, other.<UnblockedCoordinates>k__BackingField) && EqualityComparer<ImmutableArray<BlockObjectNavMeshBlockedEdgeSpec>>.Default.Equals(this.<BlockedEdges>k__BackingField, other.<BlockedEdges>k__BackingField));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002F80 File Offset: 0x00001180
		[CompilerGenerated]
		protected BlockObjectNavMeshSettingsSpec([Nullable(1)] BlockObjectNavMeshSettingsSpec original) : base(original)
		{
			this.NoAutoWalls = original.<NoAutoWalls>k__BackingField;
			this.GenerateFloorsOnStackable = original.<GenerateFloorsOnStackable>k__BackingField;
			this.EdgeGroups = original.<EdgeGroups>k__BackingField;
			this.UnblockedCoordinates = original.<UnblockedCoordinates>k__BackingField;
			this.BlockedEdges = original.<BlockedEdges>k__BackingField;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000229C File Offset: 0x0000049C
		public BlockObjectNavMeshSettingsSpec()
		{
		}
	}
}
