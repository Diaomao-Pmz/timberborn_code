using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Timberborn.LocalizationSerialization
{
	// Token: 0x02000008 RID: 8
	public class LocalizedText : IEquatable<LocalizedText>
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002119 File Offset: 0x00000319
		public LocalizedText(string Value)
		{
			this.Value = Value;
			base..ctor();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002128 File Offset: 0x00000328
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(LocalizedText);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002134 File Offset: 0x00000334
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000213C File Offset: 0x0000033C
		public string Value { get; set; }

		// Token: 0x0600000D RID: 13 RVA: 0x00002148 File Offset: 0x00000348
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("LocalizedText");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002194 File Offset: 0x00000394
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Value = ");
			builder.Append(this.Value);
			return true;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021B5 File Offset: 0x000003B5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(LocalizedText left, LocalizedText right)
		{
			return !(left == right);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021C1 File Offset: 0x000003C1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(LocalizedText left, LocalizedText right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021D5 File Offset: 0x000003D5
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Value>k__BackingField);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021FE File Offset: 0x000003FE
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as LocalizedText);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000220C File Offset: 0x0000040C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(LocalizedText other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Value>k__BackingField, other.<Value>k__BackingField));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000224A File Offset: 0x0000044A
		[CompilerGenerated]
		protected LocalizedText([Nullable(1)] LocalizedText original)
		{
			this.Value = original.<Value>k__BackingField;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000225E File Offset: 0x0000045E
		[CompilerGenerated]
		public void Deconstruct(out string Value)
		{
			Value = this.Value;
		}
	}
}
