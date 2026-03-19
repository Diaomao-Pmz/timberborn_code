using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200000C RID: 12
	[NullableContext(1)]
	[Nullable(0)]
	public class BeaverBirthStepSpec : ComponentSpec, IEquatable<BeaverBirthStepSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000244A File Offset: 0x0000064A
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BeaverBirthStepSpec);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002458 File Offset: 0x00000658
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BeaverBirthStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024A4 File Offset: 0x000006A4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024AD File Offset: 0x000006AD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BeaverBirthStepSpec left, BeaverBirthStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024B9 File Offset: 0x000006B9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BeaverBirthStepSpec left, BeaverBirthStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024CD File Offset: 0x000006CD
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000024D5 File Offset: 0x000006D5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BeaverBirthStepSpec);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024E3 File Offset: 0x000006E3
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BeaverBirthStepSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024FA File Offset: 0x000006FA
		[CompilerGenerated]
		protected BeaverBirthStepSpec(BeaverBirthStepSpec original) : base(original)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000239D File Offset: 0x0000059D
		public BeaverBirthStepSpec()
		{
		}
	}
}
