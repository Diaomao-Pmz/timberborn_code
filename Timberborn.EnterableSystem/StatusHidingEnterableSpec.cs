using System;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;
using Timberborn.BlueprintSystem;

namespace Timberborn.EnterableSystem
{
	// Token: 0x0200001D RID: 29
	[NullableContext(1)]
	[Nullable(0)]
	[UsedImplicitly]
	public class StatusHidingEnterableSpec : ComponentSpec, IStatusHider, IEquatable<StatusHidingEnterableSpec>
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003955 File Offset: 0x00001B55
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(StatusHidingEnterableSpec);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003964 File Offset: 0x00001B64
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("StatusHidingEnterableSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000028B0 File Offset: 0x00000AB0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000039B0 File Offset: 0x00001BB0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(StatusHidingEnterableSpec left, StatusHidingEnterableSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000039BC File Offset: 0x00001BBC
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(StatusHidingEnterableSpec left, StatusHidingEnterableSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000028D9 File Offset: 0x00000AD9
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000039D0 File Offset: 0x00001BD0
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as StatusHidingEnterableSpec);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000028EF File Offset: 0x00000AEF
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(StatusHidingEnterableSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00002906 File Offset: 0x00000B06
		[CompilerGenerated]
		protected StatusHidingEnterableSpec(StatusHidingEnterableSpec original) : base(original)
		{
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000279E File Offset: 0x0000099E
		public StatusHidingEnterableSpec()
		{
		}
	}
}
