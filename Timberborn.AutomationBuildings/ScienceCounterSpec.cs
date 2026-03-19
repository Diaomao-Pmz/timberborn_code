using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200003D RID: 61
	[NullableContext(1)]
	[Nullable(0)]
	public class ScienceCounterSpec : ComponentSpec, IEquatable<ScienceCounterSpec>
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000799D File Offset: 0x00005B9D
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ScienceCounterSpec);
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000079AC File Offset: 0x00005BAC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ScienceCounterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00002710 File Offset: 0x00000910
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000079F8 File Offset: 0x00005BF8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ScienceCounterSpec left, ScienceCounterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00007A04 File Offset: 0x00005C04
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ScienceCounterSpec left, ScienceCounterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00002739 File Offset: 0x00000939
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00007A18 File Offset: 0x00005C18
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ScienceCounterSpec);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00002758 File Offset: 0x00000958
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ScienceCounterSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000276F File Offset: 0x0000096F
		[CompilerGenerated]
		protected ScienceCounterSpec(ScienceCounterSpec original) : base(original)
		{
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00002778 File Offset: 0x00000978
		public ScienceCounterSpec()
		{
		}
	}
}
