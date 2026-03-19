using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.EntityNaming
{
	// Token: 0x02000013 RID: 19
	public class NumberedEntityNamerSpec : ComponentSpec, IEquatable<NumberedEntityNamerSpec>
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002BDE File Offset: 0x00000DDE
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(NumberedEntityNamerSpec);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002BEA File Offset: 0x00000DEA
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002BF2 File Offset: 0x00000DF2
		[Serialize]
		public string FormatLocKey { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002BFB File Offset: 0x00000DFB
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002C03 File Offset: 0x00000E03
		[Serialize]
		public string NumberingGroup { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002C0C File Offset: 0x00000E0C
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002C14 File Offset: 0x00000E14
		[Serialize]
		public bool IsPersistent { get; set; }

		// Token: 0x0600005D RID: 93 RVA: 0x00002C20 File Offset: 0x00000E20
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NumberedEntityNamerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002C6C File Offset: 0x00000E6C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("FormatLocKey = ");
			builder.Append(this.FormatLocKey);
			builder.Append(", NumberingGroup = ");
			builder.Append(this.NumberingGroup);
			builder.Append(", IsPersistent = ");
			builder.Append(this.IsPersistent.ToString());
			return true;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002CE8 File Offset: 0x00000EE8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(NumberedEntityNamerSpec left, NumberedEntityNamerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002CF4 File Offset: 0x00000EF4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(NumberedEntityNamerSpec left, NumberedEntityNamerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D08 File Offset: 0x00000F08
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FormatLocKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NumberingGroup>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsPersistent>k__BackingField);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002D60 File Offset: 0x00000F60
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NumberedEntityNamerSpec);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000280B File Offset: 0x00000A0B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002D70 File Offset: 0x00000F70
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(NumberedEntityNamerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<FormatLocKey>k__BackingField, other.<FormatLocKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<NumberingGroup>k__BackingField, other.<NumberingGroup>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsPersistent>k__BackingField, other.<IsPersistent>k__BackingField));
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002DDC File Offset: 0x00000FDC
		[CompilerGenerated]
		protected NumberedEntityNamerSpec([Nullable(1)] NumberedEntityNamerSpec original) : base(original)
		{
			this.FormatLocKey = original.<FormatLocKey>k__BackingField;
			this.NumberingGroup = original.<NumberingGroup>k__BackingField;
			this.IsPersistent = original.<IsPersistent>k__BackingField;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000285A File Offset: 0x00000A5A
		public NumberedEntityNamerSpec()
		{
		}
	}
}
