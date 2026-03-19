using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.CharacterModelSystem
{
	// Token: 0x02000011 RID: 17
	public class CharacterTextureSetterSpec : ComponentSpec, IEquatable<CharacterTextureSetterSpec>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002D90 File Offset: 0x00000F90
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(CharacterTextureSetterSpec);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002D9C File Offset: 0x00000F9C
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002DA4 File Offset: 0x00000FA4
		[Serialize]
		public ImmutableArray<CharacterTexturePack> TexturePacks { get; set; }

		// Token: 0x0600006C RID: 108 RVA: 0x00002DB0 File Offset: 0x00000FB0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CharacterTextureSetterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002DFC File Offset: 0x00000FFC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("TexturePacks = ");
			builder.Append(this.TexturePacks.ToString());
			return true;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002E46 File Offset: 0x00001046
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CharacterTextureSetterSpec left, CharacterTextureSetterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002E52 File Offset: 0x00001052
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CharacterTextureSetterSpec left, CharacterTextureSetterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002E66 File Offset: 0x00001066
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<CharacterTexturePack>>.Default.GetHashCode(this.<TexturePacks>k__BackingField);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002E85 File Offset: 0x00001085
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CharacterTextureSetterSpec);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000280E File Offset: 0x00000A0E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002E93 File Offset: 0x00001093
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CharacterTextureSetterSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<CharacterTexturePack>>.Default.Equals(this.<TexturePacks>k__BackingField, other.<TexturePacks>k__BackingField));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002EC4 File Offset: 0x000010C4
		[CompilerGenerated]
		protected CharacterTextureSetterSpec([Nullable(1)] CharacterTextureSetterSpec original) : base(original)
		{
			this.TexturePacks = original.<TexturePacks>k__BackingField;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000285D File Offset: 0x00000A5D
		public CharacterTextureSetterSpec()
		{
		}
	}
}
