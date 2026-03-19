using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.MechanicalSystemHighlighting
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	public class MechanicalNodeHighlighterSpec : ComponentSpec, IEquatable<MechanicalNodeHighlighterSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002602 File Offset: 0x00000802
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(MechanicalNodeHighlighterSpec);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000260E File Offset: 0x0000080E
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002616 File Offset: 0x00000816
		[Serialize]
		public Color HighlightColor { get; set; }

		// Token: 0x06000021 RID: 33 RVA: 0x00002620 File Offset: 0x00000820
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MechanicalNodeHighlighterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000266C File Offset: 0x0000086C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("HighlightColor = ");
			builder.Append(this.HighlightColor.ToString());
			return true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026B6 File Offset: 0x000008B6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MechanicalNodeHighlighterSpec left, MechanicalNodeHighlighterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026C2 File Offset: 0x000008C2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MechanicalNodeHighlighterSpec left, MechanicalNodeHighlighterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026D6 File Offset: 0x000008D6
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<HighlightColor>k__BackingField);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026F5 File Offset: 0x000008F5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MechanicalNodeHighlighterSpec);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002703 File Offset: 0x00000903
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000270C File Offset: 0x0000090C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MechanicalNodeHighlighterSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<HighlightColor>k__BackingField, other.<HighlightColor>k__BackingField));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000273D File Offset: 0x0000093D
		[CompilerGenerated]
		protected MechanicalNodeHighlighterSpec(MechanicalNodeHighlighterSpec original) : base(original)
		{
			this.HighlightColor = original.<HighlightColor>k__BackingField;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002752 File Offset: 0x00000952
		public MechanicalNodeHighlighterSpec()
		{
		}
	}
}
