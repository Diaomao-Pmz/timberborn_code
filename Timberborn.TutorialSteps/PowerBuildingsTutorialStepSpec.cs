using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000043 RID: 67
	public class PowerBuildingsTutorialStepSpec : ComponentSpec, IEquatable<PowerBuildingsTutorialStepSpec>
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00005B63 File Offset: 0x00003D63
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(PowerBuildingsTutorialStepSpec);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00005B6F File Offset: 0x00003D6F
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00005B77 File Offset: 0x00003D77
		[Serialize]
		public string TemplateName { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00005B80 File Offset: 0x00003D80
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00005B88 File Offset: 0x00003D88
		[Serialize]
		public int RequiredAmount { get; set; }

		// Token: 0x060001C8 RID: 456 RVA: 0x00005B94 File Offset: 0x00003D94
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PowerBuildingsTutorialStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00005BE0 File Offset: 0x00003DE0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("TemplateName = ");
			builder.Append(this.TemplateName);
			builder.Append(", RequiredAmount = ");
			builder.Append(this.RequiredAmount.ToString());
			return true;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00005C43 File Offset: 0x00003E43
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PowerBuildingsTutorialStepSpec left, PowerBuildingsTutorialStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00005C4F File Offset: 0x00003E4F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PowerBuildingsTutorialStepSpec left, PowerBuildingsTutorialStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005C63 File Offset: 0x00003E63
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TemplateName>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<RequiredAmount>k__BackingField);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005C99 File Offset: 0x00003E99
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PowerBuildingsTutorialStepSpec);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005CA8 File Offset: 0x00003EA8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PowerBuildingsTutorialStepSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<TemplateName>k__BackingField, other.<TemplateName>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<RequiredAmount>k__BackingField, other.<RequiredAmount>k__BackingField));
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005CFC File Offset: 0x00003EFC
		[CompilerGenerated]
		protected PowerBuildingsTutorialStepSpec([Nullable(1)] PowerBuildingsTutorialStepSpec original) : base(original)
		{
			this.TemplateName = original.<TemplateName>k__BackingField;
			this.RequiredAmount = original.<RequiredAmount>k__BackingField;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000239D File Offset: 0x0000059D
		public PowerBuildingsTutorialStepSpec()
		{
		}
	}
}
