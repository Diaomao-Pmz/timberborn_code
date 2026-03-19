using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.ForestryUI
{
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	[Nullable(0)]
	public class TreeCuttingColorsSpec : ComponentSpec, IEquatable<TreeCuttingColorsSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002EE5 File Offset: 0x000010E5
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(TreeCuttingColorsSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002EF1 File Offset: 0x000010F1
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002EF9 File Offset: 0x000010F9
		[Serialize]
		public Color ToolActionTile { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002F02 File Offset: 0x00001102
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002F0A File Offset: 0x0000110A
		[Serialize]
		public Color ToolNoActionTile { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002F13 File Offset: 0x00001113
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002F1B File Offset: 0x0000111B
		[Serialize]
		public Color CuttingAreaTile { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002F24 File Offset: 0x00001124
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002F2C File Offset: 0x0000112C
		[Serialize]
		public Color CuttingAreaHighlight { get; set; }

		// Token: 0x06000060 RID: 96 RVA: 0x00002F38 File Offset: 0x00001138
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TreeCuttingColorsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002F84 File Offset: 0x00001184
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ToolActionTile = ");
			builder.Append(this.ToolActionTile.ToString());
			builder.Append(", ToolNoActionTile = ");
			builder.Append(this.ToolNoActionTile.ToString());
			builder.Append(", CuttingAreaTile = ");
			builder.Append(this.CuttingAreaTile.ToString());
			builder.Append(", CuttingAreaHighlight = ");
			builder.Append(this.CuttingAreaHighlight.ToString());
			return true;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003043 File Offset: 0x00001243
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TreeCuttingColorsSpec left, TreeCuttingColorsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000304F File Offset: 0x0000124F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TreeCuttingColorsSpec left, TreeCuttingColorsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003064 File Offset: 0x00001264
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ToolActionTile>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ToolNoActionTile>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<CuttingAreaTile>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<CuttingAreaHighlight>k__BackingField);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000030D3 File Offset: 0x000012D3
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TreeCuttingColorsSpec);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000030E1 File Offset: 0x000012E1
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000030EC File Offset: 0x000012EC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TreeCuttingColorsSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<ToolActionTile>k__BackingField, other.<ToolActionTile>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ToolNoActionTile>k__BackingField, other.<ToolNoActionTile>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<CuttingAreaTile>k__BackingField, other.<CuttingAreaTile>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<CuttingAreaHighlight>k__BackingField, other.<CuttingAreaHighlight>k__BackingField));
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003170 File Offset: 0x00001370
		[CompilerGenerated]
		protected TreeCuttingColorsSpec(TreeCuttingColorsSpec original) : base(original)
		{
			this.ToolActionTile = original.<ToolActionTile>k__BackingField;
			this.ToolNoActionTile = original.<ToolNoActionTile>k__BackingField;
			this.CuttingAreaTile = original.<CuttingAreaTile>k__BackingField;
			this.CuttingAreaHighlight = original.<CuttingAreaHighlight>k__BackingField;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000031A9 File Offset: 0x000013A9
		public TreeCuttingColorsSpec()
		{
		}
	}
}
