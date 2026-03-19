using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x0200002A RID: 42
	[NullableContext(1)]
	[Nullable(0)]
	public class NoPowerStatusAlertDisablerSpec : ComponentSpec, IEquatable<NoPowerStatusAlertDisablerSpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004313 File Offset: 0x00002513
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(NoPowerStatusAlertDisablerSpec);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004320 File Offset: 0x00002520
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NoPowerStatusAlertDisablerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000039EC File Offset: 0x00001BEC
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000436C File Offset: 0x0000256C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(NoPowerStatusAlertDisablerSpec left, NoPowerStatusAlertDisablerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004378 File Offset: 0x00002578
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(NoPowerStatusAlertDisablerSpec left, NoPowerStatusAlertDisablerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003A15 File Offset: 0x00001C15
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000438C File Offset: 0x0000258C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NoPowerStatusAlertDisablerSpec);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003027 File Offset: 0x00001227
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003A2B File Offset: 0x00001C2B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(NoPowerStatusAlertDisablerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003A42 File Offset: 0x00001C42
		[CompilerGenerated]
		protected NoPowerStatusAlertDisablerSpec(NoPowerStatusAlertDisablerSpec original) : base(original)
		{
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003076 File Offset: 0x00001276
		public NoPowerStatusAlertDisablerSpec()
		{
		}
	}
}
