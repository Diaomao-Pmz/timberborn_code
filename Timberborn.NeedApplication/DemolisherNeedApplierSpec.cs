using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NeedApplication
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public class DemolisherNeedApplierSpec : ComponentSpec, IEquatable<DemolisherNeedApplierSpec>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000297C File Offset: 0x00000B7C
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DemolisherNeedApplierSpec);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002988 File Offset: 0x00000B88
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DemolisherNeedApplierSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000029D4 File Offset: 0x00000BD4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029DD File Offset: 0x00000BDD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DemolisherNeedApplierSpec left, DemolisherNeedApplierSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000029E9 File Offset: 0x00000BE9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DemolisherNeedApplierSpec left, DemolisherNeedApplierSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000029FD File Offset: 0x00000BFD
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A05 File Offset: 0x00000C05
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DemolisherNeedApplierSpec);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002519 File Offset: 0x00000719
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A13 File Offset: 0x00000C13
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DemolisherNeedApplierSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A2A File Offset: 0x00000C2A
		[CompilerGenerated]
		protected DemolisherNeedApplierSpec(DemolisherNeedApplierSpec original) : base(original)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002599 File Offset: 0x00000799
		public DemolisherNeedApplierSpec()
		{
		}
	}
}
