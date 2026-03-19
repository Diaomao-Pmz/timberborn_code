using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200005B RID: 91
	public class UnlockBuildingTutorialStepSpec : ComponentSpec, IEquatable<UnlockBuildingTutorialStepSpec>
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00007259 File Offset: 0x00005459
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(UnlockBuildingTutorialStepSpec);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00007265 File Offset: 0x00005465
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000726D File Offset: 0x0000546D
		[Serialize]
		public string TemplateName { get; set; }

		// Token: 0x06000265 RID: 613 RVA: 0x00007278 File Offset: 0x00005478
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UnlockBuildingTutorialStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000072C4 File Offset: 0x000054C4
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
			return true;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000072F5 File Offset: 0x000054F5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(UnlockBuildingTutorialStepSpec left, UnlockBuildingTutorialStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00007301 File Offset: 0x00005501
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(UnlockBuildingTutorialStepSpec left, UnlockBuildingTutorialStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00007315 File Offset: 0x00005515
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TemplateName>k__BackingField);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00007334 File Offset: 0x00005534
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as UnlockBuildingTutorialStepSpec);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00007342 File Offset: 0x00005542
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(UnlockBuildingTutorialStepSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<TemplateName>k__BackingField, other.<TemplateName>k__BackingField));
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00007373 File Offset: 0x00005573
		[CompilerGenerated]
		protected UnlockBuildingTutorialStepSpec([Nullable(1)] UnlockBuildingTutorialStepSpec original) : base(original)
		{
			this.TemplateName = original.<TemplateName>k__BackingField;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000239D File Offset: 0x0000059D
		public UnlockBuildingTutorialStepSpec()
		{
		}
	}
}
