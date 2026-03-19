using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Illumination
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	public class IlluminationPresetSpec : ComponentSpec, IEquatable<IlluminationPresetSpec>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002B16 File Offset: 0x00000D16
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(IlluminationPresetSpec);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002B22 File Offset: 0x00000D22
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002B2A File Offset: 0x00000D2A
		[Serialize]
		public int Order { get; set; }

		// Token: 0x06000064 RID: 100 RVA: 0x00002B34 File Offset: 0x00000D34
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("IlluminationPresetSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002B80 File Offset: 0x00000D80
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Order = ");
			builder.Append(this.Order.ToString());
			return true;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002BCA File Offset: 0x00000DCA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(IlluminationPresetSpec left, IlluminationPresetSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002BD6 File Offset: 0x00000DD6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(IlluminationPresetSpec left, IlluminationPresetSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002BEA File Offset: 0x00000DEA
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Order>k__BackingField);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002C09 File Offset: 0x00000E09
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IlluminationPresetSpec);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002267 File Offset: 0x00000467
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002C17 File Offset: 0x00000E17
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(IlluminationPresetSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Order>k__BackingField, other.<Order>k__BackingField));
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002C48 File Offset: 0x00000E48
		[CompilerGenerated]
		protected IlluminationPresetSpec(IlluminationPresetSpec original) : base(original)
		{
			this.Order = original.<Order>k__BackingField;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002290 File Offset: 0x00000490
		public IlluminationPresetSpec()
		{
		}
	}
}
