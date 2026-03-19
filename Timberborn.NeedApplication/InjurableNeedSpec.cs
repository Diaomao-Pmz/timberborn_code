using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NeedApplication
{
	// Token: 0x02000010 RID: 16
	[NullableContext(1)]
	[Nullable(0)]
	public class InjurableNeedSpec : ComponentSpec, IEquatable<InjurableNeedSpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002C1C File Offset: 0x00000E1C
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(InjurableNeedSpec);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002C28 File Offset: 0x00000E28
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("InjurableNeedSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000029D4 File Offset: 0x00000BD4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002C74 File Offset: 0x00000E74
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(InjurableNeedSpec left, InjurableNeedSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002C80 File Offset: 0x00000E80
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(InjurableNeedSpec left, InjurableNeedSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000029FD File Offset: 0x00000BFD
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002C94 File Offset: 0x00000E94
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as InjurableNeedSpec);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002519 File Offset: 0x00000719
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002A13 File Offset: 0x00000C13
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(InjurableNeedSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002A2A File Offset: 0x00000C2A
		[CompilerGenerated]
		protected InjurableNeedSpec(InjurableNeedSpec original) : base(original)
		{
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002599 File Offset: 0x00000799
		public InjurableNeedSpec()
		{
		}
	}
}
