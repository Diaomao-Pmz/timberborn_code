using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BotsUI
{
	// Token: 0x0200000D RID: 13
	public class BotSelectionSoundSpec : ComponentSpec, IEquatable<BotSelectionSoundSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002540 File Offset: 0x00000740
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BotSelectionSoundSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000254C File Offset: 0x0000074C
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002554 File Offset: 0x00000754
		[Serialize]
		public string SoundNameKey { get; set; }

		// Token: 0x0600002D RID: 45 RVA: 0x00002560 File Offset: 0x00000760
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BotSelectionSoundSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000025AC File Offset: 0x000007AC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("SoundNameKey = ");
			builder.Append(this.SoundNameKey);
			return true;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025DD File Offset: 0x000007DD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BotSelectionSoundSpec left, BotSelectionSoundSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000025E9 File Offset: 0x000007E9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BotSelectionSoundSpec left, BotSelectionSoundSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000025FD File Offset: 0x000007FD
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SoundNameKey>k__BackingField);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000261C File Offset: 0x0000081C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BotSelectionSoundSpec);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000262A File Offset: 0x0000082A
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002633 File Offset: 0x00000833
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BotSelectionSoundSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<SoundNameKey>k__BackingField, other.<SoundNameKey>k__BackingField));
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002664 File Offset: 0x00000864
		[CompilerGenerated]
		protected BotSelectionSoundSpec([Nullable(1)] BotSelectionSoundSpec original) : base(original)
		{
			this.SoundNameKey = original.<SoundNameKey>k__BackingField;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002679 File Offset: 0x00000879
		public BotSelectionSoundSpec()
		{
		}
	}
}
