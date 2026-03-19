using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200004E RID: 78
	[NullableContext(1)]
	[Nullable(0)]
	public class SetPauseStepSpec : ComponentSpec, IEquatable<SetPauseStepSpec>
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00006714 File Offset: 0x00004914
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SetPauseStepSpec);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00006720 File Offset: 0x00004920
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00006728 File Offset: 0x00004928
		[Serialize]
		public bool Pause { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00006731 File Offset: 0x00004931
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00006739 File Offset: 0x00004939
		[Serialize]
		public bool OnlyOnce { get; set; }

		// Token: 0x06000217 RID: 535 RVA: 0x00006744 File Offset: 0x00004944
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SetPauseStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00006790 File Offset: 0x00004990
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Pause = ");
			builder.Append(this.Pause.ToString());
			builder.Append(", OnlyOnce = ");
			builder.Append(this.OnlyOnce.ToString());
			return true;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00006801 File Offset: 0x00004A01
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SetPauseStepSpec left, SetPauseStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000680D File Offset: 0x00004A0D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SetPauseStepSpec left, SetPauseStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00006821 File Offset: 0x00004A21
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Pause>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<OnlyOnce>k__BackingField);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00006857 File Offset: 0x00004A57
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SetPauseStepSpec);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00006868 File Offset: 0x00004A68
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SetPauseStepSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<bool>.Default.Equals(this.<Pause>k__BackingField, other.<Pause>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<OnlyOnce>k__BackingField, other.<OnlyOnce>k__BackingField));
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000068BC File Offset: 0x00004ABC
		[CompilerGenerated]
		protected SetPauseStepSpec(SetPauseStepSpec original) : base(original)
		{
			this.Pause = original.<Pause>k__BackingField;
			this.OnlyOnce = original.<OnlyOnce>k__BackingField;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000239D File Offset: 0x0000059D
		public SetPauseStepSpec()
		{
		}
	}
}
