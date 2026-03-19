using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200002F RID: 47
	[NullableContext(1)]
	[Nullable(0)]
	public class PopulationCounterSpec : ComponentSpec, IEquatable<PopulationCounterSpec>
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000065E2 File Offset: 0x000047E2
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(PopulationCounterSpec);
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000065F0 File Offset: 0x000047F0
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PopulationCounterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00002710 File Offset: 0x00000910
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000663C File Offset: 0x0000483C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PopulationCounterSpec left, PopulationCounterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006648 File Offset: 0x00004848
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PopulationCounterSpec left, PopulationCounterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00002739 File Offset: 0x00000939
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000665C File Offset: 0x0000485C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PopulationCounterSpec);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00002758 File Offset: 0x00000958
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PopulationCounterSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000276F File Offset: 0x0000096F
		[CompilerGenerated]
		protected PopulationCounterSpec(PopulationCounterSpec original) : base(original)
		{
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00002778 File Offset: 0x00000978
		public PopulationCounterSpec()
		{
		}
	}
}
