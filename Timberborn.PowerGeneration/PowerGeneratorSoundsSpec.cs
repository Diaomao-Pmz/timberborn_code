using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.PowerGeneration
{
	// Token: 0x0200000D RID: 13
	[NullableContext(1)]
	[Nullable(0)]
	public class PowerGeneratorSoundsSpec : ComponentSpec, IEquatable<PowerGeneratorSoundsSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002529 File Offset: 0x00000729
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(PowerGeneratorSoundsSpec);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002538 File Offset: 0x00000738
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PowerGeneratorSoundsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000021F4 File Offset: 0x000003F4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002584 File Offset: 0x00000784
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PowerGeneratorSoundsSpec left, PowerGeneratorSoundsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002590 File Offset: 0x00000790
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PowerGeneratorSoundsSpec left, PowerGeneratorSoundsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000221D File Offset: 0x0000041D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000025A4 File Offset: 0x000007A4
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PowerGeneratorSoundsSpec);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002233 File Offset: 0x00000433
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000223C File Offset: 0x0000043C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PowerGeneratorSoundsSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002253 File Offset: 0x00000453
		[CompilerGenerated]
		protected PowerGeneratorSoundsSpec(PowerGeneratorSoundsSpec original) : base(original)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000225C File Offset: 0x0000045C
		public PowerGeneratorSoundsSpec()
		{
		}
	}
}
