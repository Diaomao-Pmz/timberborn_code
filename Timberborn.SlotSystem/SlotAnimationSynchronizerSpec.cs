using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.SlotSystem
{
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	[Nullable(0)]
	public class SlotAnimationSynchronizerSpec : ComponentSpec, IEquatable<SlotAnimationSynchronizerSpec>
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003206 File Offset: 0x00001406
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SlotAnimationSynchronizerSpec);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003212 File Offset: 0x00001412
		// (set) Token: 0x0600008A RID: 138 RVA: 0x0000321A File Offset: 0x0000141A
		[Serialize]
		public float MaxTimeOffset { get; set; }

		// Token: 0x0600008B RID: 139 RVA: 0x00003224 File Offset: 0x00001424
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SlotAnimationSynchronizerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003270 File Offset: 0x00001470
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MaxTimeOffset = ");
			builder.Append(this.MaxTimeOffset.ToString());
			return true;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000032BA File Offset: 0x000014BA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SlotAnimationSynchronizerSpec left, SlotAnimationSynchronizerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000032C6 File Offset: 0x000014C6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SlotAnimationSynchronizerSpec left, SlotAnimationSynchronizerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000032DA File Offset: 0x000014DA
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxTimeOffset>k__BackingField);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000032F9 File Offset: 0x000014F9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SlotAnimationSynchronizerSpec);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002363 File Offset: 0x00000563
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003307 File Offset: 0x00001507
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SlotAnimationSynchronizerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<MaxTimeOffset>k__BackingField, other.<MaxTimeOffset>k__BackingField));
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003338 File Offset: 0x00001538
		[CompilerGenerated]
		protected SlotAnimationSynchronizerSpec(SlotAnimationSynchronizerSpec original) : base(original)
		{
			this.MaxTimeOffset = original.<MaxTimeOffset>k__BackingField;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000238C File Offset: 0x0000058C
		public SlotAnimationSynchronizerSpec()
		{
		}
	}
}
