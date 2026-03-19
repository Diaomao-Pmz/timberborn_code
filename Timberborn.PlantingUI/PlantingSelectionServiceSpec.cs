using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.PlantingUI
{
	// Token: 0x0200001A RID: 26
	[NullableContext(1)]
	[Nullable(0)]
	public class PlantingSelectionServiceSpec : ComponentSpec, IEquatable<PlantingSelectionServiceSpec>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000037E0 File Offset: 0x000019E0
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(PlantingSelectionServiceSpec);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000037EC File Offset: 0x000019EC
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000037F4 File Offset: 0x000019F4
		[Serialize]
		public Color PlantingToolTile { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000037FD File Offset: 0x000019FD
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003805 File Offset: 0x00001A05
		[Serialize]
		public Color ToolActionTile { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000380E File Offset: 0x00001A0E
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003816 File Offset: 0x00001A16
		[Serialize]
		public Color ToolNoActionTile { get; set; }

		// Token: 0x06000093 RID: 147 RVA: 0x00003820 File Offset: 0x00001A20
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PlantingSelectionServiceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000386C File Offset: 0x00001A6C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("PlantingToolTile = ");
			builder.Append(this.PlantingToolTile.ToString());
			builder.Append(", ToolActionTile = ");
			builder.Append(this.ToolActionTile.ToString());
			builder.Append(", ToolNoActionTile = ");
			builder.Append(this.ToolNoActionTile.ToString());
			return true;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003904 File Offset: 0x00001B04
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PlantingSelectionServiceSpec left, PlantingSelectionServiceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003910 File Offset: 0x00001B10
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PlantingSelectionServiceSpec left, PlantingSelectionServiceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003924 File Offset: 0x00001B24
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<PlantingToolTile>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ToolActionTile>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ToolNoActionTile>k__BackingField);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000397C File Offset: 0x00001B7C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlantingSelectionServiceSpec);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000027CE File Offset: 0x000009CE
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000398C File Offset: 0x00001B8C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PlantingSelectionServiceSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<PlantingToolTile>k__BackingField, other.<PlantingToolTile>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ToolActionTile>k__BackingField, other.<ToolActionTile>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ToolNoActionTile>k__BackingField, other.<ToolNoActionTile>k__BackingField));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000039F8 File Offset: 0x00001BF8
		[CompilerGenerated]
		protected PlantingSelectionServiceSpec(PlantingSelectionServiceSpec original) : base(original)
		{
			this.PlantingToolTile = original.<PlantingToolTile>k__BackingField;
			this.ToolActionTile = original.<ToolActionTile>k__BackingField;
			this.ToolNoActionTile = original.<ToolNoActionTile>k__BackingField;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000281D File Offset: 0x00000A1D
		public PlantingSelectionServiceSpec()
		{
		}
	}
}
