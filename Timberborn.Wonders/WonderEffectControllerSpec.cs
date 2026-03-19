using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.NeedSpecs;

namespace Timberborn.Wonders
{
	// Token: 0x02000016 RID: 22
	public class WonderEffectControllerSpec : ComponentSpec, IEquatable<WonderEffectControllerSpec>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002C8B File Offset: 0x00000E8B
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WonderEffectControllerSpec);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002C97 File Offset: 0x00000E97
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002C9F File Offset: 0x00000E9F
		[Serialize]
		public ImmutableArray<ContinuousEffectSpec> Effects { get; set; }

		// Token: 0x06000069 RID: 105 RVA: 0x00002CA8 File Offset: 0x00000EA8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WonderEffectControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002CF4 File Offset: 0x00000EF4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Effects = ");
			builder.Append(this.Effects.ToString());
			return true;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002D3E File Offset: 0x00000F3E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WonderEffectControllerSpec left, WonderEffectControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002D4A File Offset: 0x00000F4A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WonderEffectControllerSpec left, WonderEffectControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002D5E File Offset: 0x00000F5E
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<ContinuousEffectSpec>>.Default.GetHashCode(this.<Effects>k__BackingField);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002D7D File Offset: 0x00000F7D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WonderEffectControllerSpec);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000029FB File Offset: 0x00000BFB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002D8B File Offset: 0x00000F8B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WonderEffectControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<ContinuousEffectSpec>>.Default.Equals(this.<Effects>k__BackingField, other.<Effects>k__BackingField));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002DBC File Offset: 0x00000FBC
		[CompilerGenerated]
		protected WonderEffectControllerSpec([Nullable(1)] WonderEffectControllerSpec original) : base(original)
		{
			this.Effects = original.<Effects>k__BackingField;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002A4A File Offset: 0x00000C4A
		public WonderEffectControllerSpec()
		{
		}
	}
}
