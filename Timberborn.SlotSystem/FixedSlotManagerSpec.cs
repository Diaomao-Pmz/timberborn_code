using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.SlotSystem
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	public class FixedSlotManagerSpec : ComponentSpec, IEquatable<FixedSlotManagerSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022CC File Offset: 0x000004CC
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(FixedSlotManagerSpec);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022D8 File Offset: 0x000004D8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FixedSlotManagerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002324 File Offset: 0x00000524
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000232D File Offset: 0x0000052D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FixedSlotManagerSpec left, FixedSlotManagerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002339 File Offset: 0x00000539
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FixedSlotManagerSpec left, FixedSlotManagerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000234D File Offset: 0x0000054D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002355 File Offset: 0x00000555
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FixedSlotManagerSpec);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002363 File Offset: 0x00000563
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000236C File Offset: 0x0000056C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FixedSlotManagerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002383 File Offset: 0x00000583
		[CompilerGenerated]
		protected FixedSlotManagerSpec(FixedSlotManagerSpec original) : base(original)
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000238C File Offset: 0x0000058C
		public FixedSlotManagerSpec()
		{
		}
	}
}
