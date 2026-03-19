using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000020 RID: 32
	public class ChangePausedStateStepSpec : ComponentSpec, IEquatable<ChangePausedStateStepSpec>
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003F06 File Offset: 0x00002106
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ChangePausedStateStepSpec);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00003F12 File Offset: 0x00002112
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00003F1A File Offset: 0x0000211A
		[Serialize]
		public bool ShouldBePaused { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003F23 File Offset: 0x00002123
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00003F2B File Offset: 0x0000212B
		[Serialize]
		public string TemplateName { get; set; }

		// Token: 0x060000DF RID: 223 RVA: 0x00003F34 File Offset: 0x00002134
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ChangePausedStateStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003F80 File Offset: 0x00002180
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ShouldBePaused = ");
			builder.Append(this.ShouldBePaused.ToString());
			builder.Append(", TemplateName = ");
			builder.Append(this.TemplateName);
			return true;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003FE3 File Offset: 0x000021E3
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ChangePausedStateStepSpec left, ChangePausedStateStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003FEF File Offset: 0x000021EF
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ChangePausedStateStepSpec left, ChangePausedStateStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004003 File Offset: 0x00002203
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ShouldBePaused>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TemplateName>k__BackingField);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004039 File Offset: 0x00002239
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ChangePausedStateStepSpec);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004048 File Offset: 0x00002248
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ChangePausedStateStepSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<bool>.Default.Equals(this.<ShouldBePaused>k__BackingField, other.<ShouldBePaused>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TemplateName>k__BackingField, other.<TemplateName>k__BackingField));
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000409C File Offset: 0x0000229C
		[CompilerGenerated]
		protected ChangePausedStateStepSpec([Nullable(1)] ChangePausedStateStepSpec original) : base(original)
		{
			this.ShouldBePaused = original.<ShouldBePaused>k__BackingField;
			this.TemplateName = original.<TemplateName>k__BackingField;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000239D File Offset: 0x0000059D
		public ChangePausedStateStepSpec()
		{
		}
	}
}
