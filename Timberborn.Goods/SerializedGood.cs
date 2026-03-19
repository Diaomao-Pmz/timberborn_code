using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Timberborn.Goods
{
	// Token: 0x0200001C RID: 28
	public class SerializedGood : IEquatable<SerializedGood>
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00003B60 File Offset: 0x00001D60
		public SerializedGood(string Id)
		{
			this.Id = Id;
			base..ctor();
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003B6F File Offset: 0x00001D6F
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(SerializedGood);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003B7B File Offset: 0x00001D7B
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003B83 File Offset: 0x00001D83
		public string Id { get; set; }

		// Token: 0x060000C2 RID: 194 RVA: 0x00003B8C File Offset: 0x00001D8C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SerializedGood");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003BD8 File Offset: 0x00001DD8
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Id = ");
			builder.Append(this.Id);
			return true;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003BF9 File Offset: 0x00001DF9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SerializedGood left, SerializedGood right)
		{
			return !(left == right);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003C05 File Offset: 0x00001E05
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SerializedGood left, SerializedGood right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003C19 File Offset: 0x00001E19
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003C42 File Offset: 0x00001E42
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SerializedGood);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003C50 File Offset: 0x00001E50
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SerializedGood other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField));
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003C8E File Offset: 0x00001E8E
		[CompilerGenerated]
		protected SerializedGood([Nullable(1)] SerializedGood original)
		{
			this.Id = original.<Id>k__BackingField;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003CA2 File Offset: 0x00001EA2
		[CompilerGenerated]
		public void Deconstruct(out string Id)
		{
			Id = this.Id;
		}
	}
}
