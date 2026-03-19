using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.SlotSystem
{
	// Token: 0x02000013 RID: 19
	public class PrioritySlotRetrieverSpec : ComponentSpec, IEquatable<PrioritySlotRetrieverSpec>
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002FD4 File Offset: 0x000011D4
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(PrioritySlotRetrieverSpec);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002FE0 File Offset: 0x000011E0
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002FE8 File Offset: 0x000011E8
		[Serialize]
		public ImmutableArray<string> PrioritySlotNames { get; set; }

		// Token: 0x06000078 RID: 120 RVA: 0x00002FF4 File Offset: 0x000011F4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PrioritySlotRetrieverSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003040 File Offset: 0x00001240
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("PrioritySlotNames = ");
			builder.Append(this.PrioritySlotNames.ToString());
			return true;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000308A File Offset: 0x0000128A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PrioritySlotRetrieverSpec left, PrioritySlotRetrieverSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003096 File Offset: 0x00001296
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PrioritySlotRetrieverSpec left, PrioritySlotRetrieverSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000030AA File Offset: 0x000012AA
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<PrioritySlotNames>k__BackingField);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000030C9 File Offset: 0x000012C9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PrioritySlotRetrieverSpec);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002363 File Offset: 0x00000563
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000030D7 File Offset: 0x000012D7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PrioritySlotRetrieverSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<PrioritySlotNames>k__BackingField, other.<PrioritySlotNames>k__BackingField));
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003108 File Offset: 0x00001308
		[CompilerGenerated]
		protected PrioritySlotRetrieverSpec([Nullable(1)] PrioritySlotRetrieverSpec original) : base(original)
		{
			this.PrioritySlotNames = original.<PrioritySlotNames>k__BackingField;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000238C File Offset: 0x0000058C
		public PrioritySlotRetrieverSpec()
		{
		}
	}
}
