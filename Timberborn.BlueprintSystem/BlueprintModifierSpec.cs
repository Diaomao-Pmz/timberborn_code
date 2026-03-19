using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x02000018 RID: 24
	public class BlueprintModifierSpec : IEquatable<BlueprintModifierSpec>
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000037B4 File Offset: 0x000019B4
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BlueprintModifierSpec);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000037C0 File Offset: 0x000019C0
		// (set) Token: 0x0600008B RID: 139 RVA: 0x000037C8 File Offset: 0x000019C8
		[Serialize]
		public AssetRef<BlueprintAsset> Original { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000037D1 File Offset: 0x000019D1
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000037D9 File Offset: 0x000019D9
		[Serialize]
		public AssetRef<BlueprintAsset> Modifier { get; set; }

		// Token: 0x0600008E RID: 142 RVA: 0x000037E4 File Offset: 0x000019E4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlueprintModifierSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003830 File Offset: 0x00001A30
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Original = ");
			builder.Append(this.Original);
			builder.Append(", Modifier = ");
			builder.Append(this.Modifier);
			return true;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000386A File Offset: 0x00001A6A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlueprintModifierSpec left, BlueprintModifierSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003876 File Offset: 0x00001A76
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlueprintModifierSpec left, BlueprintModifierSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000388A File Offset: 0x00001A8A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<AssetRef<BlueprintAsset>>.Default.GetHashCode(this.<Original>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<BlueprintAsset>>.Default.GetHashCode(this.<Modifier>k__BackingField);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000038CA File Offset: 0x00001ACA
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlueprintModifierSpec);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000038D8 File Offset: 0x00001AD8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlueprintModifierSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<AssetRef<BlueprintAsset>>.Default.Equals(this.<Original>k__BackingField, other.<Original>k__BackingField) && EqualityComparer<AssetRef<BlueprintAsset>>.Default.Equals(this.<Modifier>k__BackingField, other.<Modifier>k__BackingField));
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003939 File Offset: 0x00001B39
		[CompilerGenerated]
		protected BlueprintModifierSpec([Nullable(1)] BlueprintModifierSpec original)
		{
			this.Original = original.<Original>k__BackingField;
			this.Modifier = original.<Modifier>k__BackingField;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000020F8 File Offset: 0x000002F8
		public BlueprintModifierSpec()
		{
		}
	}
}
