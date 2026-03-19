using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.BuilderPrioritySystemUI
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	public class BuilderPriorityToolSpec : ComponentSpec, IEquatable<BuilderPriorityToolSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002BA8 File Offset: 0x00000DA8
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BuilderPriorityToolSpec);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002BB4 File Offset: 0x00000DB4
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002BBC File Offset: 0x00000DBC
		[Serialize]
		public Color PriorityHighlightColor { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002BC5 File Offset: 0x00000DC5
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002BCD File Offset: 0x00000DCD
		[Serialize]
		public Color PriorityActionColor { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002BD6 File Offset: 0x00000DD6
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002BDE File Offset: 0x00000DDE
		[Serialize]
		public Color PriorityTileColor { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002BE7 File Offset: 0x00000DE7
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002BEF File Offset: 0x00000DEF
		[Serialize]
		public Color PrioritySideColor { get; set; }

		// Token: 0x06000058 RID: 88 RVA: 0x00002BF8 File Offset: 0x00000DF8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BuilderPriorityToolSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002C44 File Offset: 0x00000E44
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("PriorityHighlightColor = ");
			builder.Append(this.PriorityHighlightColor.ToString());
			builder.Append(", PriorityActionColor = ");
			builder.Append(this.PriorityActionColor.ToString());
			builder.Append(", PriorityTileColor = ");
			builder.Append(this.PriorityTileColor.ToString());
			builder.Append(", PrioritySideColor = ");
			builder.Append(this.PrioritySideColor.ToString());
			return true;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002D03 File Offset: 0x00000F03
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BuilderPriorityToolSpec left, BuilderPriorityToolSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002D0F File Offset: 0x00000F0F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BuilderPriorityToolSpec left, BuilderPriorityToolSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002D24 File Offset: 0x00000F24
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<PriorityHighlightColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<PriorityActionColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<PriorityTileColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<PrioritySideColor>k__BackingField);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002D93 File Offset: 0x00000F93
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BuilderPriorityToolSpec);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002B77 File Offset: 0x00000D77
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002DA4 File Offset: 0x00000FA4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BuilderPriorityToolSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<PriorityHighlightColor>k__BackingField, other.<PriorityHighlightColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<PriorityActionColor>k__BackingField, other.<PriorityActionColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<PriorityTileColor>k__BackingField, other.<PriorityTileColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<PrioritySideColor>k__BackingField, other.<PrioritySideColor>k__BackingField));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002E28 File Offset: 0x00001028
		[CompilerGenerated]
		protected BuilderPriorityToolSpec(BuilderPriorityToolSpec original) : base(original)
		{
			this.PriorityHighlightColor = original.<PriorityHighlightColor>k__BackingField;
			this.PriorityActionColor = original.<PriorityActionColor>k__BackingField;
			this.PriorityTileColor = original.<PriorityTileColor>k__BackingField;
			this.PrioritySideColor = original.<PrioritySideColor>k__BackingField;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public BuilderPriorityToolSpec()
		{
		}
	}
}
