using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000052 RID: 82
	[NullableContext(1)]
	[Nullable(0)]
	public class TimerSpec : ComponentSpec, IEquatable<TimerSpec>
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00009825 File Offset: 0x00007A25
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(TimerSpec);
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00009834 File Offset: 0x00007A34
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TimerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00002710 File Offset: 0x00000910
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00009880 File Offset: 0x00007A80
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TimerSpec left, TimerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000988C File Offset: 0x00007A8C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TimerSpec left, TimerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00002739 File Offset: 0x00000939
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000098A0 File Offset: 0x00007AA0
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TimerSpec);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00002758 File Offset: 0x00000958
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TimerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000276F File Offset: 0x0000096F
		[CompilerGenerated]
		protected TimerSpec(TimerSpec original) : base(original)
		{
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00002778 File Offset: 0x00000978
		public TimerSpec()
		{
		}
	}
}
