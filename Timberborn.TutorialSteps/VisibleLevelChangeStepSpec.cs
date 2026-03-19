using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000060 RID: 96
	[NullableContext(1)]
	[Nullable(0)]
	public class VisibleLevelChangeStepSpec : ComponentSpec, IEquatable<VisibleLevelChangeStepSpec>
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000767E File Offset: 0x0000587E
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(VisibleLevelChangeStepSpec);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000768A File Offset: 0x0000588A
		// (set) Token: 0x06000287 RID: 647 RVA: 0x00007692 File Offset: 0x00005892
		[Serialize]
		public VisibleLevelChangeType VisibleLevelChangeType { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000769B File Offset: 0x0000589B
		// (set) Token: 0x06000289 RID: 649 RVA: 0x000076A3 File Offset: 0x000058A3
		[Serialize]
		public bool ShowKeybindings { get; set; }

		// Token: 0x0600028A RID: 650 RVA: 0x000076AC File Offset: 0x000058AC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("VisibleLevelChangeStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000076F8 File Offset: 0x000058F8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("VisibleLevelChangeType = ");
			builder.Append(this.VisibleLevelChangeType.ToString());
			builder.Append(", ShowKeybindings = ");
			builder.Append(this.ShowKeybindings.ToString());
			return true;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00007769 File Offset: 0x00005969
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(VisibleLevelChangeStepSpec left, VisibleLevelChangeStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00007775 File Offset: 0x00005975
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(VisibleLevelChangeStepSpec left, VisibleLevelChangeStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00007789 File Offset: 0x00005989
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<VisibleLevelChangeType>.Default.GetHashCode(this.<VisibleLevelChangeType>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ShowKeybindings>k__BackingField);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000077BF File Offset: 0x000059BF
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as VisibleLevelChangeStepSpec);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000077D0 File Offset: 0x000059D0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(VisibleLevelChangeStepSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<VisibleLevelChangeType>.Default.Equals(this.<VisibleLevelChangeType>k__BackingField, other.<VisibleLevelChangeType>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<ShowKeybindings>k__BackingField, other.<ShowKeybindings>k__BackingField));
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00007824 File Offset: 0x00005A24
		[CompilerGenerated]
		protected VisibleLevelChangeStepSpec(VisibleLevelChangeStepSpec original) : base(original)
		{
			this.VisibleLevelChangeType = original.<VisibleLevelChangeType>k__BackingField;
			this.ShowKeybindings = original.<ShowKeybindings>k__BackingField;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000239D File Offset: 0x0000059D
		public VisibleLevelChangeStepSpec()
		{
		}
	}
}
