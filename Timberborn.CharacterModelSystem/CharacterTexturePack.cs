using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.CharacterModelSystem
{
	// Token: 0x0200000F RID: 15
	public class CharacterTexturePack : IEquatable<CharacterTexturePack>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000029D6 File Offset: 0x00000BD6
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(CharacterTexturePack);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000029E2 File Offset: 0x00000BE2
		// (set) Token: 0x06000053 RID: 83 RVA: 0x000029EA File Offset: 0x00000BEA
		[Serialize]
		public string DiffuseTexture { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000029F3 File Offset: 0x00000BF3
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000029FB File Offset: 0x00000BFB
		[Serialize]
		public string EmissionTexture { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002A04 File Offset: 0x00000C04
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002A0C File Offset: 0x00000C0C
		[Serialize]
		public string NormalTexture { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002A15 File Offset: 0x00000C15
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002A1D File Offset: 0x00000C1D
		[Serialize]
		public string DisplacementTexture { get; set; }

		// Token: 0x0600005A RID: 90 RVA: 0x00002A28 File Offset: 0x00000C28
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CharacterTexturePack");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002A74 File Offset: 0x00000C74
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("DiffuseTexture = ");
			builder.Append(this.DiffuseTexture);
			builder.Append(", EmissionTexture = ");
			builder.Append(this.EmissionTexture);
			builder.Append(", NormalTexture = ");
			builder.Append(this.NormalTexture);
			builder.Append(", DisplacementTexture = ");
			builder.Append(this.DisplacementTexture);
			return true;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002AEB File Offset: 0x00000CEB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CharacterTexturePack left, CharacterTexturePack right)
		{
			return !(left == right);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002AF7 File Offset: 0x00000CF7
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CharacterTexturePack left, CharacterTexturePack right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002B0C File Offset: 0x00000D0C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DiffuseTexture>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<EmissionTexture>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NormalTexture>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplacementTexture>k__BackingField);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002B85 File Offset: 0x00000D85
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CharacterTexturePack);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002B94 File Offset: 0x00000D94
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CharacterTexturePack other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<DiffuseTexture>k__BackingField, other.<DiffuseTexture>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<EmissionTexture>k__BackingField, other.<EmissionTexture>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<NormalTexture>k__BackingField, other.<NormalTexture>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplacementTexture>k__BackingField, other.<DisplacementTexture>k__BackingField));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002C25 File Offset: 0x00000E25
		[CompilerGenerated]
		protected CharacterTexturePack([Nullable(1)] CharacterTexturePack original)
		{
			this.DiffuseTexture = original.<DiffuseTexture>k__BackingField;
			this.EmissionTexture = original.<EmissionTexture>k__BackingField;
			this.NormalTexture = original.<NormalTexture>k__BackingField;
			this.DisplacementTexture = original.<DisplacementTexture>k__BackingField;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000020F8 File Offset: 0x000002F8
		public CharacterTexturePack()
		{
		}
	}
}
