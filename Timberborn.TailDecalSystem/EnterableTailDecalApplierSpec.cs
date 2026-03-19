using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TailDecalSystem
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	public class EnterableTailDecalApplierSpec : ComponentSpec, IEquatable<EnterableTailDecalApplierSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021FE File Offset: 0x000003FE
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(EnterableTailDecalApplierSpec);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000220C File Offset: 0x0000040C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EnterableTailDecalApplierSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002258 File Offset: 0x00000458
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002261 File Offset: 0x00000461
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(EnterableTailDecalApplierSpec left, EnterableTailDecalApplierSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000226D File Offset: 0x0000046D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(EnterableTailDecalApplierSpec left, EnterableTailDecalApplierSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002281 File Offset: 0x00000481
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002289 File Offset: 0x00000489
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EnterableTailDecalApplierSpec);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002297 File Offset: 0x00000497
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022A0 File Offset: 0x000004A0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(EnterableTailDecalApplierSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022B7 File Offset: 0x000004B7
		[CompilerGenerated]
		protected EnterableTailDecalApplierSpec(EnterableTailDecalApplierSpec original) : base(original)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022C0 File Offset: 0x000004C0
		public EnterableTailDecalApplierSpec()
		{
		}
	}
}
