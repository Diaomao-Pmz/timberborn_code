using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.GameSceneLoading
{
	// Token: 0x0200000A RID: 10
	public class GameTipSpec : ComponentSpec, IEquatable<GameTipSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002284 File Offset: 0x00000484
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(GameTipSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002290 File Offset: 0x00000490
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002298 File Offset: 0x00000498
		[Serialize]
		public ImmutableArray<string> Tips { get; set; }

		// Token: 0x0600001A RID: 26 RVA: 0x000022A4 File Offset: 0x000004A4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GameTipSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022F0 File Offset: 0x000004F0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Tips = ");
			builder.Append(this.Tips.ToString());
			return true;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000233A File Offset: 0x0000053A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GameTipSpec left, GameTipSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002346 File Offset: 0x00000546
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GameTipSpec left, GameTipSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000235A File Offset: 0x0000055A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<Tips>k__BackingField);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002379 File Offset: 0x00000579
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GameTipSpec);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002387 File Offset: 0x00000587
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002390 File Offset: 0x00000590
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GameTipSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<Tips>k__BackingField, other.<Tips>k__BackingField));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023C1 File Offset: 0x000005C1
		[CompilerGenerated]
		protected GameTipSpec([Nullable(1)] GameTipSpec original) : base(original)
		{
			this.Tips = original.<Tips>k__BackingField;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023D6 File Offset: 0x000005D6
		public GameTipSpec()
		{
		}
	}
}
