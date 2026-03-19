using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.LocalizationSerialization;

namespace Timberborn.NeedSpecs
{
	// Token: 0x0200000C RID: 12
	public class NeedGroupSpec : ComponentSpec, IEquatable<NeedGroupSpec>
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002A05 File Offset: 0x00000C05
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(NeedGroupSpec);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002A11 File Offset: 0x00000C11
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002A19 File Offset: 0x00000C19
		[Serialize]
		public string Id { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002A22 File Offset: 0x00000C22
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002A2A File Offset: 0x00000C2A
		[Serialize]
		public int Order { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002A33 File Offset: 0x00000C33
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002A3B File Offset: 0x00000C3B
		[Serialize("DisplayNameLocKey")]
		public LocalizedText DisplayName { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002A44 File Offset: 0x00000C44
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002A4C File Offset: 0x00000C4C
		[Serialize]
		private string DisplayNameLocKey { get; set; }

		// Token: 0x06000057 RID: 87 RVA: 0x00002A58 File Offset: 0x00000C58
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NeedGroupSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002AA4 File Offset: 0x00000CA4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", Order = ");
			builder.Append(this.Order.ToString());
			builder.Append(", DisplayName = ");
			builder.Append(this.DisplayName);
			return true;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002B20 File Offset: 0x00000D20
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(NeedGroupSpec left, NeedGroupSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002B2C File Offset: 0x00000D2C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(NeedGroupSpec left, NeedGroupSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002B40 File Offset: 0x00000D40
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Order>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayNameLocKey>k__BackingField);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002BAF File Offset: 0x00000DAF
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NeedGroupSpec);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000271B File Offset: 0x0000091B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002BC0 File Offset: 0x00000DC0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(NeedGroupSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Order>k__BackingField, other.<Order>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayNameLocKey>k__BackingField, other.<DisplayNameLocKey>k__BackingField));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002C44 File Offset: 0x00000E44
		[CompilerGenerated]
		protected NeedGroupSpec([Nullable(1)] NeedGroupSpec original) : base(original)
		{
			this.Id = original.<Id>k__BackingField;
			this.Order = original.<Order>k__BackingField;
			this.DisplayName = original.<DisplayName>k__BackingField;
			this.DisplayNameLocKey = original.<DisplayNameLocKey>k__BackingField;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000283C File Offset: 0x00000A3C
		public NeedGroupSpec()
		{
		}
	}
}
