using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000009 RID: 9
	public class AccumulateScienceForBuildingStepSpec : ComponentSpec, IEquatable<AccumulateScienceForBuildingStepSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002266 File Offset: 0x00000466
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(AccumulateScienceForBuildingStepSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002272 File Offset: 0x00000472
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000227A File Offset: 0x0000047A
		[Serialize]
		public string TemplateName { get; set; }

		// Token: 0x06000011 RID: 17 RVA: 0x00002284 File Offset: 0x00000484
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AccumulateScienceForBuildingStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022D0 File Offset: 0x000004D0
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

		// Token: 0x06000013 RID: 19 RVA: 0x00002301 File Offset: 0x00000501
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(AccumulateScienceForBuildingStepSpec left, AccumulateScienceForBuildingStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000230D File Offset: 0x0000050D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(AccumulateScienceForBuildingStepSpec left, AccumulateScienceForBuildingStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002321 File Offset: 0x00000521
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TemplateName>k__BackingField);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002340 File Offset: 0x00000540
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AccumulateScienceForBuildingStepSpec);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002357 File Offset: 0x00000557
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(AccumulateScienceForBuildingStepSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<TemplateName>k__BackingField, other.<TemplateName>k__BackingField));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002388 File Offset: 0x00000588
		[CompilerGenerated]
		protected AccumulateScienceForBuildingStepSpec([Nullable(1)] AccumulateScienceForBuildingStepSpec original) : base(original)
		{
			this.TemplateName = original.<TemplateName>k__BackingField;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000239D File Offset: 0x0000059D
		public AccumulateScienceForBuildingStepSpec()
		{
		}
	}
}
