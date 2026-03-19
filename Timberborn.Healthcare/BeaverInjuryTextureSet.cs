using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Healthcare
{
	// Token: 0x02000007 RID: 7
	public class BeaverInjuryTextureSet : IEquatable<BeaverInjuryTextureSet>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BeaverInjuryTextureSet);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210A File Offset: 0x0000030A
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002112 File Offset: 0x00000312
		[Serialize]
		public string DiffusePath { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000211B File Offset: 0x0000031B
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002123 File Offset: 0x00000323
		[Serialize]
		public string NormalMapPath { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000212C File Offset: 0x0000032C
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002134 File Offset: 0x00000334
		[Serialize]
		public string DisplacementPath { get; set; }

		// Token: 0x0600000E RID: 14 RVA: 0x00002140 File Offset: 0x00000340
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BeaverInjuryTextureSet");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000218C File Offset: 0x0000038C
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("DiffusePath = ");
			builder.Append(this.DiffusePath);
			builder.Append(", NormalMapPath = ");
			builder.Append(this.NormalMapPath);
			builder.Append(", DisplacementPath = ");
			builder.Append(this.DisplacementPath);
			return true;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021EA File Offset: 0x000003EA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BeaverInjuryTextureSet left, BeaverInjuryTextureSet right)
		{
			return !(left == right);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021F6 File Offset: 0x000003F6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BeaverInjuryTextureSet left, BeaverInjuryTextureSet right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000220C File Offset: 0x0000040C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DiffusePath>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NormalMapPath>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplacementPath>k__BackingField);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000226E File Offset: 0x0000046E
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BeaverInjuryTextureSet);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000227C File Offset: 0x0000047C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BeaverInjuryTextureSet other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<DiffusePath>k__BackingField, other.<DiffusePath>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<NormalMapPath>k__BackingField, other.<NormalMapPath>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplacementPath>k__BackingField, other.<DisplacementPath>k__BackingField));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022F5 File Offset: 0x000004F5
		[CompilerGenerated]
		protected BeaverInjuryTextureSet([Nullable(1)] BeaverInjuryTextureSet original)
		{
			this.DiffusePath = original.<DiffusePath>k__BackingField;
			this.NormalMapPath = original.<NormalMapPath>k__BackingField;
			this.DisplacementPath = original.<DisplacementPath>k__BackingField;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000020F6 File Offset: 0x000002F6
		public BeaverInjuryTextureSet()
		{
		}
	}
}
