using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000034 RID: 52
	public class MarkPlantablesTutorialStepSpec : ComponentSpec, IEquatable<MarkPlantablesTutorialStepSpec>
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000512E File Offset: 0x0000332E
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(MarkPlantablesTutorialStepSpec);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000513A File Offset: 0x0000333A
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00005142 File Offset: 0x00003342
		[Serialize]
		public string TemplateName { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000514B File Offset: 0x0000334B
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00005153 File Offset: 0x00003353
		[Serialize]
		public int RequiredAmount { get; set; }

		// Token: 0x0600016D RID: 365 RVA: 0x0000515C File Offset: 0x0000335C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MarkPlantablesTutorialStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000051A8 File Offset: 0x000033A8
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

		// Token: 0x0600016F RID: 367 RVA: 0x0000520B File Offset: 0x0000340B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MarkPlantablesTutorialStepSpec left, MarkPlantablesTutorialStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005217 File Offset: 0x00003417
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MarkPlantablesTutorialStepSpec left, MarkPlantablesTutorialStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000522B File Offset: 0x0000342B
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TemplateName>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<RequiredAmount>k__BackingField);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005261 File Offset: 0x00003461
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MarkPlantablesTutorialStepSpec);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005270 File Offset: 0x00003470
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MarkPlantablesTutorialStepSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<TemplateName>k__BackingField, other.<TemplateName>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<RequiredAmount>k__BackingField, other.<RequiredAmount>k__BackingField));
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000052C4 File Offset: 0x000034C4
		[CompilerGenerated]
		protected MarkPlantablesTutorialStepSpec([Nullable(1)] MarkPlantablesTutorialStepSpec original) : base(original)
		{
			this.TemplateName = original.<TemplateName>k__BackingField;
			this.RequiredAmount = original.<RequiredAmount>k__BackingField;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000239D File Offset: 0x0000059D
		public MarkPlantablesTutorialStepSpec()
		{
		}
	}
}
