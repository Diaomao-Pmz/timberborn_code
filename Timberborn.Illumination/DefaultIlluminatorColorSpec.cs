using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Illumination
{
	// Token: 0x0200000C RID: 12
	public class DefaultIlluminatorColorSpec : ComponentSpec, IEquatable<DefaultIlluminatorColorSpec>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002774 File Offset: 0x00000974
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DefaultIlluminatorColorSpec);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002780 File Offset: 0x00000980
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002788 File Offset: 0x00000988
		[Serialize]
		public string ColorId { get; set; }

		// Token: 0x06000043 RID: 67 RVA: 0x00002794 File Offset: 0x00000994
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DefaultIlluminatorColorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000027E0 File Offset: 0x000009E0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ColorId = ");
			builder.Append(this.ColorId);
			return true;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002811 File Offset: 0x00000A11
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DefaultIlluminatorColorSpec left, DefaultIlluminatorColorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000281D File Offset: 0x00000A1D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DefaultIlluminatorColorSpec left, DefaultIlluminatorColorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002831 File Offset: 0x00000A31
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ColorId>k__BackingField);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002850 File Offset: 0x00000A50
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DefaultIlluminatorColorSpec);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002267 File Offset: 0x00000467
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000285E File Offset: 0x00000A5E
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DefaultIlluminatorColorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<ColorId>k__BackingField, other.<ColorId>k__BackingField));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000288F File Offset: 0x00000A8F
		[CompilerGenerated]
		protected DefaultIlluminatorColorSpec([Nullable(1)] DefaultIlluminatorColorSpec original) : base(original)
		{
			this.ColorId = original.<ColorId>k__BackingField;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002290 File Offset: 0x00000490
		public DefaultIlluminatorColorSpec()
		{
		}
	}
}
