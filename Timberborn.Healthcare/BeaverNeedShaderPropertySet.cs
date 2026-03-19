using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Healthcare
{
	// Token: 0x0200000A RID: 10
	public class BeaverNeedShaderPropertySet : IEquatable<BeaverNeedShaderPropertySet>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000272A File Offset: 0x0000092A
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BeaverNeedShaderPropertySet);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002736 File Offset: 0x00000936
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000273E File Offset: 0x0000093E
		[Serialize]
		public string NeedId { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002747 File Offset: 0x00000947
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000274F File Offset: 0x0000094F
		[Serialize]
		public string PropertyName { get; set; }

		// Token: 0x06000039 RID: 57 RVA: 0x00002758 File Offset: 0x00000958
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BeaverNeedShaderPropertySet");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027A4 File Offset: 0x000009A4
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("NeedId = ");
			builder.Append(this.NeedId);
			builder.Append(", PropertyName = ");
			builder.Append(this.PropertyName);
			return true;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027DE File Offset: 0x000009DE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BeaverNeedShaderPropertySet left, BeaverNeedShaderPropertySet right)
		{
			return !(left == right);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000027EA File Offset: 0x000009EA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BeaverNeedShaderPropertySet left, BeaverNeedShaderPropertySet right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000027FE File Offset: 0x000009FE
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NeedId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PropertyName>k__BackingField);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000283E File Offset: 0x00000A3E
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BeaverNeedShaderPropertySet);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000284C File Offset: 0x00000A4C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BeaverNeedShaderPropertySet other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<NeedId>k__BackingField, other.<NeedId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<PropertyName>k__BackingField, other.<PropertyName>k__BackingField));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000028AD File Offset: 0x00000AAD
		[CompilerGenerated]
		protected BeaverNeedShaderPropertySet([Nullable(1)] BeaverNeedShaderPropertySet original)
		{
			this.NeedId = original.<NeedId>k__BackingField;
			this.PropertyName = original.<PropertyName>k__BackingField;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000020F6 File Offset: 0x000002F6
		public BeaverNeedShaderPropertySet()
		{
		}
	}
}
