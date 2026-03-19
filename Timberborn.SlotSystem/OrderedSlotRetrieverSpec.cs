using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;
using Timberborn.BlueprintSystem;

namespace Timberborn.SlotSystem
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	[UsedImplicitly]
	public class OrderedSlotRetrieverSpec : ComponentSpec, ICustomSlotRetriever, IEquatable<OrderedSlotRetrieverSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002394 File Offset: 0x00000594
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(OrderedSlotRetrieverSpec);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023A0 File Offset: 0x000005A0
		[NullableContext(0)]
		public bool TryGetUnassignedSlot(IEnumerable<ISlot> slots, out ISlot slot)
		{
			foreach (ISlot slot2 in slots)
			{
				if (!slot2.AssignedEnterer)
				{
					slot = slot2;
					return true;
				}
			}
			slot = null;
			return false;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000023FC File Offset: 0x000005FC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("OrderedSlotRetrieverSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002324 File Offset: 0x00000524
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002448 File Offset: 0x00000648
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(OrderedSlotRetrieverSpec left, OrderedSlotRetrieverSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002454 File Offset: 0x00000654
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(OrderedSlotRetrieverSpec left, OrderedSlotRetrieverSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000234D File Offset: 0x0000054D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002468 File Offset: 0x00000668
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as OrderedSlotRetrieverSpec);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002363 File Offset: 0x00000563
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000236C File Offset: 0x0000056C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(OrderedSlotRetrieverSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002383 File Offset: 0x00000583
		[CompilerGenerated]
		protected OrderedSlotRetrieverSpec(OrderedSlotRetrieverSpec original) : base(original)
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000238C File Offset: 0x0000058C
		public OrderedSlotRetrieverSpec()
		{
		}
	}
}
