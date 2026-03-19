using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.PowerGeneration
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	public class AdjustableStrengthPowerGeneratorSpec : ComponentSpec, IEquatable<AdjustableStrengthPowerGeneratorSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000219A File Offset: 0x0000039A
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(AdjustableStrengthPowerGeneratorSpec);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021A8 File Offset: 0x000003A8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AdjustableStrengthPowerGeneratorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021F4 File Offset: 0x000003F4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021FD File Offset: 0x000003FD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(AdjustableStrengthPowerGeneratorSpec left, AdjustableStrengthPowerGeneratorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002209 File Offset: 0x00000409
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(AdjustableStrengthPowerGeneratorSpec left, AdjustableStrengthPowerGeneratorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000221D File Offset: 0x0000041D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002225 File Offset: 0x00000425
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AdjustableStrengthPowerGeneratorSpec);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002233 File Offset: 0x00000433
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000223C File Offset: 0x0000043C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(AdjustableStrengthPowerGeneratorSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002253 File Offset: 0x00000453
		[CompilerGenerated]
		protected AdjustableStrengthPowerGeneratorSpec(AdjustableStrengthPowerGeneratorSpec original) : base(original)
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000225C File Offset: 0x0000045C
		public AdjustableStrengthPowerGeneratorSpec()
		{
		}
	}
}
