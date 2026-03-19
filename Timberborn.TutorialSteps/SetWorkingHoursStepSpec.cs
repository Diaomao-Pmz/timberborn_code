using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000051 RID: 81
	[NullableContext(1)]
	[Nullable(0)]
	public class SetWorkingHoursStepSpec : ComponentSpec, IEquatable<SetWorkingHoursStepSpec>
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000229 RID: 553 RVA: 0x000069C9 File Offset: 0x00004BC9
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SetWorkingHoursStepSpec);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600022A RID: 554 RVA: 0x000069D5 File Offset: 0x00004BD5
		// (set) Token: 0x0600022B RID: 555 RVA: 0x000069DD File Offset: 0x00004BDD
		[Serialize]
		public int TargetWorkingHours { get; set; }

		// Token: 0x0600022C RID: 556 RVA: 0x000069E8 File Offset: 0x00004BE8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SetWorkingHoursStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00006A34 File Offset: 0x00004C34
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("TargetWorkingHours = ");
			builder.Append(this.TargetWorkingHours.ToString());
			return true;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00006A7E File Offset: 0x00004C7E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SetWorkingHoursStepSpec left, SetWorkingHoursStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00006A8A File Offset: 0x00004C8A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SetWorkingHoursStepSpec left, SetWorkingHoursStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00006A9E File Offset: 0x00004C9E
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<TargetWorkingHours>k__BackingField);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00006ABD File Offset: 0x00004CBD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SetWorkingHoursStepSpec);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00006ACB File Offset: 0x00004CCB
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SetWorkingHoursStepSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<TargetWorkingHours>k__BackingField, other.<TargetWorkingHours>k__BackingField));
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00006AFC File Offset: 0x00004CFC
		[CompilerGenerated]
		protected SetWorkingHoursStepSpec(SetWorkingHoursStepSpec original) : base(original)
		{
			this.TargetWorkingHours = original.<TargetWorkingHours>k__BackingField;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000239D File Offset: 0x0000059D
		public SetWorkingHoursStepSpec()
		{
		}
	}
}
