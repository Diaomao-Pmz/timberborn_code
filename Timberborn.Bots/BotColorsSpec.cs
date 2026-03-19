using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Bots
{
	// Token: 0x02000009 RID: 9
	public class BotColorsSpec : ComponentSpec, IEquatable<BotColorsSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002160 File Offset: 0x00000360
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BotColorsSpec);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000216C File Offset: 0x0000036C
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002174 File Offset: 0x00000374
		[Serialize]
		public string BotColorId { get; set; }

		// Token: 0x0600000F RID: 15 RVA: 0x00002180 File Offset: 0x00000380
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BotColorsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021CC File Offset: 0x000003CC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("BotColorId = ");
			builder.Append(this.BotColorId);
			return true;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021FD File Offset: 0x000003FD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BotColorsSpec left, BotColorsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002209 File Offset: 0x00000409
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BotColorsSpec left, BotColorsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000221D File Offset: 0x0000041D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BotColorId>k__BackingField);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000223C File Offset: 0x0000043C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BotColorsSpec);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000224A File Offset: 0x0000044A
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002253 File Offset: 0x00000453
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BotColorsSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<BotColorId>k__BackingField, other.<BotColorId>k__BackingField));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002284 File Offset: 0x00000484
		[CompilerGenerated]
		protected BotColorsSpec([Nullable(1)] BotColorsSpec original) : base(original)
		{
			this.BotColorId = original.<BotColorId>k__BackingField;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002299 File Offset: 0x00000499
		public BotColorsSpec()
		{
		}
	}
}
