using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x02000010 RID: 16
	public class InputBindingSpec : ComponentSpec, IEquatable<InputBindingSpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002E87 File Offset: 0x00001087
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(InputBindingSpec);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002E93 File Offset: 0x00001093
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002E9B File Offset: 0x0000109B
		[Serialize]
		public string Path { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002EA4 File Offset: 0x000010A4
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002EAC File Offset: 0x000010AC
		[Serialize]
		public InputModifiers InputModifiers { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002EB5 File Offset: 0x000010B5
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002EBD File Offset: 0x000010BD
		[Serialize]
		public bool Unchangeable { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002EC6 File Offset: 0x000010C6
		public bool IsDefined
		{
			get
			{
				return !string.IsNullOrEmpty(this.Path);
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002ED8 File Offset: 0x000010D8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("InputBindingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002F24 File Offset: 0x00001124
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Path = ");
			builder.Append(this.Path);
			builder.Append(", InputModifiers = ");
			builder.Append(this.InputModifiers.ToString());
			builder.Append(", Unchangeable = ");
			builder.Append(this.Unchangeable.ToString());
			builder.Append(", IsDefined = ");
			builder.Append(this.IsDefined.ToString());
			return true;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002FD5 File Offset: 0x000011D5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(InputBindingSpec left, InputBindingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002FE1 File Offset: 0x000011E1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(InputBindingSpec left, InputBindingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002FF8 File Offset: 0x000011F8
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Path>k__BackingField)) * -1521134295 + EqualityComparer<InputModifiers>.Default.GetHashCode(this.<InputModifiers>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Unchangeable>k__BackingField);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003050 File Offset: 0x00001250
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as InputBindingSpec);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000024D7 File Offset: 0x000006D7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003060 File Offset: 0x00001260
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(InputBindingSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Path>k__BackingField, other.<Path>k__BackingField) && EqualityComparer<InputModifiers>.Default.Equals(this.<InputModifiers>k__BackingField, other.<InputModifiers>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Unchangeable>k__BackingField, other.<Unchangeable>k__BackingField));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000030CC File Offset: 0x000012CC
		[CompilerGenerated]
		protected InputBindingSpec([Nullable(1)] InputBindingSpec original) : base(original)
		{
			this.Path = original.<Path>k__BackingField;
			this.InputModifiers = original.<InputModifiers>k__BackingField;
			this.Unchangeable = original.<Unchangeable>k__BackingField;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002500 File Offset: 0x00000700
		public InputBindingSpec()
		{
		}
	}
}
