using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.BlockObstacles
{
	// Token: 0x0200000D RID: 13
	[NullableContext(1)]
	[Nullable(0)]
	public class LayeredBlockObstacleSpec : ComponentSpec, IEquatable<LayeredBlockObstacleSpec>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002C68 File Offset: 0x00000E68
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(LayeredBlockObstacleSpec);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002C74 File Offset: 0x00000E74
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002C7C File Offset: 0x00000E7C
		[Serialize]
		public Vector2Int LayerSize { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002C85 File Offset: 0x00000E85
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002C8D File Offset: 0x00000E8D
		[Serialize]
		public Vector3 AnchorPosition { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002C96 File Offset: 0x00000E96
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002C9E File Offset: 0x00000E9E
		[Serialize]
		public int BlockCreationOffset { get; set; }

		// Token: 0x06000051 RID: 81 RVA: 0x00002CA8 File Offset: 0x00000EA8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("LayeredBlockObstacleSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002CF4 File Offset: 0x00000EF4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("LayerSize = ");
			builder.Append(this.LayerSize.ToString());
			builder.Append(", AnchorPosition = ");
			builder.Append(this.AnchorPosition.ToString());
			builder.Append(", BlockCreationOffset = ");
			builder.Append(this.BlockCreationOffset.ToString());
			return true;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002D8C File Offset: 0x00000F8C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(LayeredBlockObstacleSpec left, LayeredBlockObstacleSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D98 File Offset: 0x00000F98
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(LayeredBlockObstacleSpec left, LayeredBlockObstacleSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002DAC File Offset: 0x00000FAC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<Vector2Int>.Default.GetHashCode(this.<LayerSize>k__BackingField)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<AnchorPosition>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<BlockCreationOffset>k__BackingField);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002E04 File Offset: 0x00001004
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as LayeredBlockObstacleSpec);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002697 File Offset: 0x00000897
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002E14 File Offset: 0x00001014
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(LayeredBlockObstacleSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Vector2Int>.Default.Equals(this.<LayerSize>k__BackingField, other.<LayerSize>k__BackingField) && EqualityComparer<Vector3>.Default.Equals(this.<AnchorPosition>k__BackingField, other.<AnchorPosition>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<BlockCreationOffset>k__BackingField, other.<BlockCreationOffset>k__BackingField));
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002E80 File Offset: 0x00001080
		[CompilerGenerated]
		protected LayeredBlockObstacleSpec(LayeredBlockObstacleSpec original) : base(original)
		{
			this.LayerSize = original.<LayerSize>k__BackingField;
			this.AnchorPosition = original.<AnchorPosition>k__BackingField;
			this.BlockCreationOffset = original.<BlockCreationOffset>k__BackingField;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000026C0 File Offset: 0x000008C0
		public LayeredBlockObstacleSpec()
		{
		}
	}
}
