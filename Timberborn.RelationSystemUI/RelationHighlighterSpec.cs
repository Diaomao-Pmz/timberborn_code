using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.RelationSystemUI
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	public class RelationHighlighterSpec : ComponentSpec, IEquatable<RelationHighlighterSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002270 File Offset: 0x00000470
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(RelationHighlighterSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000227C File Offset: 0x0000047C
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002284 File Offset: 0x00000484
		[Serialize]
		public Color RelationSelection { get; set; }

		// Token: 0x06000010 RID: 16 RVA: 0x00002290 File Offset: 0x00000490
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RelationHighlighterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022DC File Offset: 0x000004DC
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("RelationSelection = ");
			builder.Append(this.RelationSelection.ToString());
			return true;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002326 File Offset: 0x00000526
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RelationHighlighterSpec left, RelationHighlighterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002332 File Offset: 0x00000532
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RelationHighlighterSpec left, RelationHighlighterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002346 File Offset: 0x00000546
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<RelationSelection>k__BackingField);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002365 File Offset: 0x00000565
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RelationHighlighterSpec);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002373 File Offset: 0x00000573
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000237C File Offset: 0x0000057C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RelationHighlighterSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<RelationSelection>k__BackingField, other.<RelationSelection>k__BackingField));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023AD File Offset: 0x000005AD
		[CompilerGenerated]
		protected RelationHighlighterSpec(RelationHighlighterSpec original) : base(original)
		{
			this.RelationSelection = original.<RelationSelection>k__BackingField;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023C2 File Offset: 0x000005C2
		public RelationHighlighterSpec()
		{
		}
	}
}
