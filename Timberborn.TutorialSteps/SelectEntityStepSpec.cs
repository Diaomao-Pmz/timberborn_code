using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.LocalizationSerialization;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000047 RID: 71
	public class SelectEntityStepSpec : ComponentSpec, IEquatable<SelectEntityStepSpec>
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00005E0B File Offset: 0x0000400B
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(SelectEntityStepSpec);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00005E17 File Offset: 0x00004017
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00005E1F File Offset: 0x0000401F
		[Serialize]
		public ImmutableArray<string> TemplateNames { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00005E28 File Offset: 0x00004028
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00005E30 File Offset: 0x00004030
		[Serialize("DescriptionLocKey")]
		public LocalizedText Description { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00005E39 File Offset: 0x00004039
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00005E41 File Offset: 0x00004041
		[Serialize]
		private string DescriptionLocKey { get; set; }

		// Token: 0x060001E1 RID: 481 RVA: 0x00005E4C File Offset: 0x0000404C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SelectEntityStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00005E98 File Offset: 0x00004098
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("TemplateNames = ");
			builder.Append(this.TemplateNames.ToString());
			builder.Append(", Description = ");
			builder.Append(this.Description);
			return true;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00005EFB File Offset: 0x000040FB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SelectEntityStepSpec left, SelectEntityStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00005F07 File Offset: 0x00004107
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SelectEntityStepSpec left, SelectEntityStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00005F1C File Offset: 0x0000411C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<TemplateNames>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<Description>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DescriptionLocKey>k__BackingField);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00005F74 File Offset: 0x00004174
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SelectEntityStepSpec);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00005F84 File Offset: 0x00004184
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SelectEntityStepSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<TemplateNames>k__BackingField, other.<TemplateNames>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DescriptionLocKey>k__BackingField, other.<DescriptionLocKey>k__BackingField));
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00005FF0 File Offset: 0x000041F0
		[CompilerGenerated]
		protected SelectEntityStepSpec([Nullable(1)] SelectEntityStepSpec original) : base(original)
		{
			this.TemplateNames = original.<TemplateNames>k__BackingField;
			this.Description = original.<Description>k__BackingField;
			this.DescriptionLocKey = original.<DescriptionLocKey>k__BackingField;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000239D File Offset: 0x0000059D
		public SelectEntityStepSpec()
		{
		}
	}
}
