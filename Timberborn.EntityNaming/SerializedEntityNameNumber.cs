using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Timberborn.EntityNaming
{
	// Token: 0x02000014 RID: 20
	public class SerializedEntityNameNumber : IEquatable<SerializedEntityNameNumber>
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00002E09 File Offset: 0x00001009
		public SerializedEntityNameNumber(string Group, int NextNumber)
		{
			this.Group = Group;
			this.NextNumber = NextNumber;
			base..ctor();
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002E1F File Offset: 0x0000101F
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(SerializedEntityNameNumber);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002E2B File Offset: 0x0000102B
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002E33 File Offset: 0x00001033
		public string Group { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002E3C File Offset: 0x0000103C
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002E44 File Offset: 0x00001044
		public int NextNumber { get; set; }

		// Token: 0x0600006E RID: 110 RVA: 0x00002E50 File Offset: 0x00001050
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SerializedEntityNameNumber");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002E9C File Offset: 0x0000109C
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Group = ");
			builder.Append(this.Group);
			builder.Append(", NextNumber = ");
			builder.Append(this.NextNumber.ToString());
			return true;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002EEF File Offset: 0x000010EF
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SerializedEntityNameNumber left, SerializedEntityNameNumber right)
		{
			return !(left == right);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002EFB File Offset: 0x000010FB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SerializedEntityNameNumber left, SerializedEntityNameNumber right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002F0F File Offset: 0x0000110F
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Group>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<NextNumber>k__BackingField);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002F4F File Offset: 0x0000114F
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SerializedEntityNameNumber);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002F60 File Offset: 0x00001160
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SerializedEntityNameNumber other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Group>k__BackingField, other.<Group>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<NextNumber>k__BackingField, other.<NextNumber>k__BackingField));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002FC1 File Offset: 0x000011C1
		[CompilerGenerated]
		protected SerializedEntityNameNumber([Nullable(1)] SerializedEntityNameNumber original)
		{
			this.Group = original.<Group>k__BackingField;
			this.NextNumber = original.<NextNumber>k__BackingField;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002FE1 File Offset: 0x000011E1
		[CompilerGenerated]
		public void Deconstruct(out string Group, out int NextNumber)
		{
			Group = this.Group;
			NextNumber = this.NextNumber;
		}
	}
}
