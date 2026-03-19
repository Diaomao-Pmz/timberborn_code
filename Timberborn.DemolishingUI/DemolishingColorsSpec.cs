using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.DemolishingUI
{
	// Token: 0x0200000E RID: 14
	[NullableContext(1)]
	[Nullable(0)]
	public class DemolishingColorsSpec : ComponentSpec, IEquatable<DemolishingColorsSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002D3D File Offset: 0x00000F3D
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DemolishingColorsSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002D49 File Offset: 0x00000F49
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002D51 File Offset: 0x00000F51
		[Serialize]
		public Color DeletedObjectHighlightColor { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002D5A File Offset: 0x00000F5A
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002D62 File Offset: 0x00000F62
		[Serialize]
		public Color DeletedAreaTileColor { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002D6B File Offset: 0x00000F6B
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002D73 File Offset: 0x00000F73
		[Serialize]
		public Color DeletedAreaSideColor { get; set; }

		// Token: 0x06000049 RID: 73 RVA: 0x00002D7C File Offset: 0x00000F7C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DemolishingColorsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002DC8 File Offset: 0x00000FC8
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

		// Token: 0x0600004B RID: 75 RVA: 0x00002E60 File Offset: 0x00001060
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DemolishingColorsSpec left, DemolishingColorsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E6C File Offset: 0x0000106C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DemolishingColorsSpec left, DemolishingColorsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E80 File Offset: 0x00001080
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<DeletedObjectHighlightColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<DeletedAreaTileColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<DeletedAreaSideColor>k__BackingField);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002ED8 File Offset: 0x000010D8
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DemolishingColorsSpec);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000027DD File Offset: 0x000009DD
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002EE8 File Offset: 0x000010E8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DemolishingColorsSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<DeletedObjectHighlightColor>k__BackingField, other.<DeletedObjectHighlightColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<DeletedAreaTileColor>k__BackingField, other.<DeletedAreaTileColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<DeletedAreaSideColor>k__BackingField, other.<DeletedAreaSideColor>k__BackingField));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002F54 File Offset: 0x00001154
		[CompilerGenerated]
		protected DemolishingColorsSpec(DemolishingColorsSpec original) : base(original)
		{
			this.DeletedObjectHighlightColor = original.<DeletedObjectHighlightColor>k__BackingField;
			this.DeletedAreaTileColor = original.<DeletedAreaTileColor>k__BackingField;
			this.DeletedAreaSideColor = original.<DeletedAreaSideColor>k__BackingField;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000285D File Offset: 0x00000A5D
		public DemolishingColorsSpec()
		{
		}
	}
}
