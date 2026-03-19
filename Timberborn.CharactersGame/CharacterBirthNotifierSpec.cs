using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.CharactersGame
{
	// Token: 0x02000008 RID: 8
	public class CharacterBirthNotifierSpec : ComponentSpec, IEquatable<CharacterBirthNotifierSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002151 File Offset: 0x00000351
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(CharacterBirthNotifierSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000215D File Offset: 0x0000035D
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002165 File Offset: 0x00000365
		[Serialize]
		public string NotificationLocKey { get; set; }

		// Token: 0x0600000D RID: 13 RVA: 0x00002170 File Offset: 0x00000370
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CharacterBirthNotifierSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021BC File Offset: 0x000003BC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("NotificationLocKey = ");
			builder.Append(this.NotificationLocKey);
			return true;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021ED File Offset: 0x000003ED
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CharacterBirthNotifierSpec left, CharacterBirthNotifierSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021F9 File Offset: 0x000003F9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CharacterBirthNotifierSpec left, CharacterBirthNotifierSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000220D File Offset: 0x0000040D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NotificationLocKey>k__BackingField);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000222C File Offset: 0x0000042C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CharacterBirthNotifierSpec);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000223A File Offset: 0x0000043A
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002243 File Offset: 0x00000443
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CharacterBirthNotifierSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<NotificationLocKey>k__BackingField, other.<NotificationLocKey>k__BackingField));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002274 File Offset: 0x00000474
		[CompilerGenerated]
		protected CharacterBirthNotifierSpec([Nullable(1)] CharacterBirthNotifierSpec original) : base(original)
		{
			this.NotificationLocKey = original.<NotificationLocKey>k__BackingField;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002289 File Offset: 0x00000489
		public CharacterBirthNotifierSpec()
		{
		}
	}
}
