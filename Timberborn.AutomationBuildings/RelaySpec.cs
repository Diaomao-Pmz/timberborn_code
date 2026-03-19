using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000035 RID: 53
	[NullableContext(1)]
	[Nullable(0)]
	public class RelaySpec : ComponentSpec, IEquatable<RelaySpec>
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00006EB8 File Offset: 0x000050B8
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(RelaySpec);
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00006EC4 File Offset: 0x000050C4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RelaySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00002710 File Offset: 0x00000910
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00006F10 File Offset: 0x00005110
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RelaySpec left, RelaySpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00006F1C File Offset: 0x0000511C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RelaySpec left, RelaySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00002739 File Offset: 0x00000939
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00006F30 File Offset: 0x00005130
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RelaySpec);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00002758 File Offset: 0x00000958
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RelaySpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000276F File Offset: 0x0000096F
		[CompilerGenerated]
		protected RelaySpec(RelaySpec original) : base(original)
		{
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00002778 File Offset: 0x00000978
		public RelaySpec()
		{
		}
	}
}
