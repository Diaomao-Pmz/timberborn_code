using System;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;
using Timberborn.BlueprintSystem;
using Timberborn.Planting;

namespace Timberborn.Cutting
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	[UsedImplicitly]
	public class CuttableYieldGoodIdProviderSpec : ComponentSpec, IPlantableGoodIdProvider, IEquatable<CuttableYieldGoodIdProviderSpec>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002615 File Offset: 0x00000815
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(CuttableYieldGoodIdProviderSpec);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002621 File Offset: 0x00000821
		[NullableContext(0)]
		public string GetGoodId()
		{
			return base.GetSpec<CuttableSpec>().Yielder.Yield.Id;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002638 File Offset: 0x00000838
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CuttableYieldGoodIdProviderSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002684 File Offset: 0x00000884
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000268D File Offset: 0x0000088D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CuttableYieldGoodIdProviderSpec left, CuttableYieldGoodIdProviderSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002699 File Offset: 0x00000899
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CuttableYieldGoodIdProviderSpec left, CuttableYieldGoodIdProviderSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000026AD File Offset: 0x000008AD
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000026B5 File Offset: 0x000008B5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CuttableYieldGoodIdProviderSpec);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000256A File Offset: 0x0000076A
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000026C3 File Offset: 0x000008C3
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CuttableYieldGoodIdProviderSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000026DA File Offset: 0x000008DA
		[CompilerGenerated]
		protected CuttableYieldGoodIdProviderSpec(CuttableYieldGoodIdProviderSpec original) : base(original)
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000260D File Offset: 0x0000080D
		public CuttableYieldGoodIdProviderSpec()
		{
		}
	}
}
