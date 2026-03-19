using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class BlockObjectDeletionToolSpec : ComponentSpec, IEquatable<BlockObjectDeletionToolSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000026E8 File Offset: 0x000008E8
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BlockObjectDeletionToolSpec);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000026F4 File Offset: 0x000008F4
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000026FC File Offset: 0x000008FC
		[Serialize]
		public Color DeletedObjectHighlightColor { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002705 File Offset: 0x00000905
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000270D File Offset: 0x0000090D
		[Serialize]
		public Color DeletedAreaTileColor { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002716 File Offset: 0x00000916
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000271E File Offset: 0x0000091E
		[Serialize]
		public Color DeletedAreaSideColor { get; set; }

		// Token: 0x06000026 RID: 38 RVA: 0x00002728 File Offset: 0x00000928
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockObjectDeletionToolSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002774 File Offset: 0x00000974
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DeletedObjectHighlightColor = ");
			builder.Append(this.DeletedObjectHighlightColor.ToString());
			builder.Append(", DeletedAreaTileColor = ");
			builder.Append(this.DeletedAreaTileColor.ToString());
			builder.Append(", DeletedAreaSideColor = ");
			builder.Append(this.DeletedAreaSideColor.ToString());
			return true;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000280C File Offset: 0x00000A0C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockObjectDeletionToolSpec left, BlockObjectDeletionToolSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002818 File Offset: 0x00000A18
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockObjectDeletionToolSpec left, BlockObjectDeletionToolSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000282C File Offset: 0x00000A2C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<DeletedObjectHighlightColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<DeletedAreaTileColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<DeletedAreaSideColor>k__BackingField);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002884 File Offset: 0x00000A84
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockObjectDeletionToolSpec);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002892 File Offset: 0x00000A92
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000289C File Offset: 0x00000A9C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockObjectDeletionToolSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<DeletedObjectHighlightColor>k__BackingField, other.<DeletedObjectHighlightColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<DeletedAreaTileColor>k__BackingField, other.<DeletedAreaTileColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<DeletedAreaSideColor>k__BackingField, other.<DeletedAreaSideColor>k__BackingField));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002908 File Offset: 0x00000B08
		[CompilerGenerated]
		protected BlockObjectDeletionToolSpec(BlockObjectDeletionToolSpec original) : base(original)
		{
			this.DeletedObjectHighlightColor = original.<DeletedObjectHighlightColor>k__BackingField;
			this.DeletedAreaTileColor = original.<DeletedAreaTileColor>k__BackingField;
			this.DeletedAreaSideColor = original.<DeletedAreaSideColor>k__BackingField;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002935 File Offset: 0x00000B35
		public BlockObjectDeletionToolSpec()
		{
		}
	}
}
