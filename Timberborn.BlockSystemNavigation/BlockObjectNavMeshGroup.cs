using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BlockSystemNavigation
{
	// Token: 0x0200000D RID: 13
	public class BlockObjectNavMeshGroup : IEquatable<BlockObjectNavMeshGroup>
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002A35 File Offset: 0x00000C35
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BlockObjectNavMeshGroup);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002A41 File Offset: 0x00000C41
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002A49 File Offset: 0x00000C49
		[Serialize]
		public bool UseGroup { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002A52 File Offset: 0x00000C52
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002A5A File Offset: 0x00000C5A
		[Serialize]
		public string GroupName { get; set; }

		// Token: 0x06000058 RID: 88 RVA: 0x00002A64 File Offset: 0x00000C64
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockObjectNavMeshGroup");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002AB0 File Offset: 0x00000CB0
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("UseGroup = ");
			builder.Append(this.UseGroup.ToString());
			builder.Append(", GroupName = ");
			builder.Append(this.GroupName);
			return true;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002B03 File Offset: 0x00000D03
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockObjectNavMeshGroup left, BlockObjectNavMeshGroup right)
		{
			return !(left == right);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002B0F File Offset: 0x00000D0F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockObjectNavMeshGroup left, BlockObjectNavMeshGroup right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002B23 File Offset: 0x00000D23
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<UseGroup>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GroupName>k__BackingField);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002B63 File Offset: 0x00000D63
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockObjectNavMeshGroup);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002B74 File Offset: 0x00000D74
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockObjectNavMeshGroup other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<bool>.Default.Equals(this.<UseGroup>k__BackingField, other.<UseGroup>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<GroupName>k__BackingField, other.<GroupName>k__BackingField));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002BD5 File Offset: 0x00000DD5
		[CompilerGenerated]
		protected BlockObjectNavMeshGroup([Nullable(1)] BlockObjectNavMeshGroup original)
		{
			this.UseGroup = original.<UseGroup>k__BackingField;
			this.GroupName = original.<GroupName>k__BackingField;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000020F6 File Offset: 0x000002F6
		public BlockObjectNavMeshGroup()
		{
		}
	}
}
