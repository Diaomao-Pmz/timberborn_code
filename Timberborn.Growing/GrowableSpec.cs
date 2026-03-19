using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Growing
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	public class GrowableSpec : ComponentSpec, IEquatable<GrowableSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002419 File Offset: 0x00000619
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(GrowableSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002425 File Offset: 0x00000625
		// (set) Token: 0x06000021 RID: 33 RVA: 0x0000242D File Offset: 0x0000062D
		[Serialize]
		public float GrowthTimeInDays { get; set; }

		// Token: 0x06000022 RID: 34 RVA: 0x00002438 File Offset: 0x00000638
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GrowableSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002484 File Offset: 0x00000684
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("GrowthTimeInDays = ");
			builder.Append(this.GrowthTimeInDays.ToString());
			return true;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024CE File Offset: 0x000006CE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GrowableSpec left, GrowableSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024DA File Offset: 0x000006DA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GrowableSpec left, GrowableSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024EE File Offset: 0x000006EE
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<GrowthTimeInDays>k__BackingField);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000250D File Offset: 0x0000070D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GrowableSpec);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000251B File Offset: 0x0000071B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002524 File Offset: 0x00000724
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GrowableSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<GrowthTimeInDays>k__BackingField, other.<GrowthTimeInDays>k__BackingField));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002555 File Offset: 0x00000755
		[CompilerGenerated]
		protected GrowableSpec(GrowableSpec original) : base(original)
		{
			this.GrowthTimeInDays = original.<GrowthTimeInDays>k__BackingField;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000256A File Offset: 0x0000076A
		public GrowableSpec()
		{
		}
	}
}
