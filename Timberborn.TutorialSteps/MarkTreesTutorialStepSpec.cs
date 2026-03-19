using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000037 RID: 55
	[NullableContext(1)]
	[Nullable(0)]
	public class MarkTreesTutorialStepSpec : ComponentSpec, IEquatable<MarkTreesTutorialStepSpec>
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600017F RID: 383 RVA: 0x000053B9 File Offset: 0x000035B9
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(MarkTreesTutorialStepSpec);
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000053C8 File Offset: 0x000035C8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MarkTreesTutorialStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000024A4 File Offset: 0x000006A4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005414 File Offset: 0x00003614
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MarkTreesTutorialStepSpec left, MarkTreesTutorialStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005420 File Offset: 0x00003620
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MarkTreesTutorialStepSpec left, MarkTreesTutorialStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000024CD File Offset: 0x000006CD
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005434 File Offset: 0x00003634
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MarkTreesTutorialStepSpec);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000024E3 File Offset: 0x000006E3
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MarkTreesTutorialStepSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000024FA File Offset: 0x000006FA
		[CompilerGenerated]
		protected MarkTreesTutorialStepSpec(MarkTreesTutorialStepSpec original) : base(original)
		{
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000239D File Offset: 0x0000059D
		public MarkTreesTutorialStepSpec()
		{
		}
	}
}
