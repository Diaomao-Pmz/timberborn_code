using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.CharacterModelSystem
{
	// Token: 0x0200000B RID: 11
	public class CharacterModelSpec : ComponentSpec, IEquatable<CharacterModelSpec>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002724 File Offset: 0x00000924
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(CharacterModelSpec);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002730 File Offset: 0x00000930
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002738 File Offset: 0x00000938
		[Serialize]
		public string ModelName { get; set; }

		// Token: 0x0600003D RID: 61 RVA: 0x00002744 File Offset: 0x00000944
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CharacterModelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002790 File Offset: 0x00000990
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ModelName = ");
			builder.Append(this.ModelName);
			return true;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000027C1 File Offset: 0x000009C1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CharacterModelSpec left, CharacterModelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000027CD File Offset: 0x000009CD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CharacterModelSpec left, CharacterModelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000027E1 File Offset: 0x000009E1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ModelName>k__BackingField);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002800 File Offset: 0x00000A00
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CharacterModelSpec);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000280E File Offset: 0x00000A0E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002817 File Offset: 0x00000A17
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CharacterModelSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<ModelName>k__BackingField, other.<ModelName>k__BackingField));
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002848 File Offset: 0x00000A48
		[CompilerGenerated]
		protected CharacterModelSpec([Nullable(1)] CharacterModelSpec original) : base(original)
		{
			this.ModelName = original.<ModelName>k__BackingField;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000285D File Offset: 0x00000A5D
		public CharacterModelSpec()
		{
		}
	}
}
