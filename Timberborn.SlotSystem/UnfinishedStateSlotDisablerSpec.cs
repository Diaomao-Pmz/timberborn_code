using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.SlotSystem
{
	// Token: 0x02000024 RID: 36
	public class UnfinishedStateSlotDisablerSpec : ComponentSpec, IEquatable<UnfinishedStateSlotDisablerSpec>
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000435E File Offset: 0x0000255E
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(UnfinishedStateSlotDisablerSpec);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000436A File Offset: 0x0000256A
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00004372 File Offset: 0x00002572
		[Serialize]
		public string SlotKeyword { get; set; }

		// Token: 0x06000101 RID: 257 RVA: 0x0000437C File Offset: 0x0000257C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UnfinishedStateSlotDisablerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000043C8 File Offset: 0x000025C8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("SlotKeyword = ");
			builder.Append(this.SlotKeyword);
			return true;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000043F9 File Offset: 0x000025F9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(UnfinishedStateSlotDisablerSpec left, UnfinishedStateSlotDisablerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004405 File Offset: 0x00002605
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(UnfinishedStateSlotDisablerSpec left, UnfinishedStateSlotDisablerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004419 File Offset: 0x00002619
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SlotKeyword>k__BackingField);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004438 File Offset: 0x00002638
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as UnfinishedStateSlotDisablerSpec);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00002363 File Offset: 0x00000563
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004446 File Offset: 0x00002646
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(UnfinishedStateSlotDisablerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<SlotKeyword>k__BackingField, other.<SlotKeyword>k__BackingField));
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004477 File Offset: 0x00002677
		[CompilerGenerated]
		protected UnfinishedStateSlotDisablerSpec([Nullable(1)] UnfinishedStateSlotDisablerSpec original) : base(original)
		{
			this.SlotKeyword = original.<SlotKeyword>k__BackingField;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000238C File Offset: 0x0000058C
		public UnfinishedStateSlotDisablerSpec()
		{
		}
	}
}
