using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200003B RID: 59
	[NullableContext(1)]
	[Nullable(0)]
	public class OpenWellbeingPanelStepSpec : ComponentSpec, IEquatable<OpenWellbeingPanelStepSpec>
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000561B File Offset: 0x0000381B
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(OpenWellbeingPanelStepSpec);
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005628 File Offset: 0x00003828
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("OpenWellbeingPanelStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000024A4 File Offset: 0x000006A4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005674 File Offset: 0x00003874
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(OpenWellbeingPanelStepSpec left, OpenWellbeingPanelStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005680 File Offset: 0x00003880
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(OpenWellbeingPanelStepSpec left, OpenWellbeingPanelStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000024CD File Offset: 0x000006CD
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005694 File Offset: 0x00003894
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as OpenWellbeingPanelStepSpec);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000024E3 File Offset: 0x000006E3
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(OpenWellbeingPanelStepSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000024FA File Offset: 0x000006FA
		[CompilerGenerated]
		protected OpenWellbeingPanelStepSpec(OpenWellbeingPanelStepSpec original) : base(original)
		{
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000239D File Offset: 0x0000059D
		public OpenWellbeingPanelStepSpec()
		{
		}
	}
}
