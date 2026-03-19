using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000055 RID: 85
	[NullableContext(1)]
	[Nullable(0)]
	public class TutorialColorsSpec : ComponentSpec, IEquatable<TutorialColorsSpec>
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00006C8F File Offset: 0x00004E8F
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(TutorialColorsSpec);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00006C9B File Offset: 0x00004E9B
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00006CA3 File Offset: 0x00004EA3
		[Serialize]
		public Color TutorialBuildingHighlight { get; set; }

		// Token: 0x06000246 RID: 582 RVA: 0x00006CAC File Offset: 0x00004EAC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TutorialColorsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00006CF8 File Offset: 0x00004EF8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("TutorialBuildingHighlight = ");
			builder.Append(this.TutorialBuildingHighlight.ToString());
			return true;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00006D42 File Offset: 0x00004F42
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TutorialColorsSpec left, TutorialColorsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00006D4E File Offset: 0x00004F4E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TutorialColorsSpec left, TutorialColorsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00006D62 File Offset: 0x00004F62
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<TutorialBuildingHighlight>k__BackingField);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00006D81 File Offset: 0x00004F81
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TutorialColorsSpec);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00006D8F File Offset: 0x00004F8F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TutorialColorsSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<TutorialBuildingHighlight>k__BackingField, other.<TutorialBuildingHighlight>k__BackingField));
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00006DC0 File Offset: 0x00004FC0
		[CompilerGenerated]
		protected TutorialColorsSpec(TutorialColorsSpec original) : base(original)
		{
			this.TutorialBuildingHighlight = original.<TutorialBuildingHighlight>k__BackingField;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000239D File Offset: 0x0000059D
		public TutorialColorsSpec()
		{
		}
	}
}
