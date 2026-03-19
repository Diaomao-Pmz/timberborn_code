using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x02000010 RID: 16
	public class RecipeModel : IEquatable<RecipeModel>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002DED File Offset: 0x00000FED
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(RecipeModel);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002DF9 File Offset: 0x00000FF9
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002E01 File Offset: 0x00001001
		[Serialize]
		public string RecipeId { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002E0A File Offset: 0x0000100A
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002E12 File Offset: 0x00001012
		[Serialize]
		public string ModelName { get; set; }

		// Token: 0x0600006E RID: 110 RVA: 0x00002E1C File Offset: 0x0000101C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RecipeModel");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002E68 File Offset: 0x00001068
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("RecipeId = ");
			builder.Append(this.RecipeId);
			builder.Append(", ModelName = ");
			builder.Append(this.ModelName);
			return true;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002EA2 File Offset: 0x000010A2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RecipeModel left, RecipeModel right)
		{
			return !(left == right);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002EAE File Offset: 0x000010AE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RecipeModel left, RecipeModel right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002EC2 File Offset: 0x000010C2
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RecipeId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ModelName>k__BackingField);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002F02 File Offset: 0x00001102
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RecipeModel);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002F10 File Offset: 0x00001110
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RecipeModel other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<RecipeId>k__BackingField, other.<RecipeId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ModelName>k__BackingField, other.<ModelName>k__BackingField));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002F71 File Offset: 0x00001171
		[CompilerGenerated]
		protected RecipeModel([Nullable(1)] RecipeModel original)
		{
			this.RecipeId = original.<RecipeId>k__BackingField;
			this.ModelName = original.<ModelName>k__BackingField;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000020F8 File Offset: 0x000002F8
		public RecipeModel()
		{
		}
	}
}
