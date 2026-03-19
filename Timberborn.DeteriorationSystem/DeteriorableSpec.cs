using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.DeteriorationSystem
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	public class DeteriorableSpec : ComponentSpec, IEquatable<DeteriorableSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000224D File Offset: 0x0000044D
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DeteriorableSpec);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002259 File Offset: 0x00000459
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002261 File Offset: 0x00000461
		[Serialize]
		public int DeteriorationInDays { get; set; }

		// Token: 0x06000013 RID: 19 RVA: 0x0000226C File Offset: 0x0000046C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DeteriorableSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022B8 File Offset: 0x000004B8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DeteriorationInDays = ");
			builder.Append(this.DeteriorationInDays.ToString());
			return true;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002302 File Offset: 0x00000502
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DeteriorableSpec left, DeteriorableSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000230E File Offset: 0x0000050E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DeteriorableSpec left, DeteriorableSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002322 File Offset: 0x00000522
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<DeteriorationInDays>k__BackingField);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002341 File Offset: 0x00000541
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DeteriorableSpec);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000234F File Offset: 0x0000054F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002358 File Offset: 0x00000558
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DeteriorableSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<DeteriorationInDays>k__BackingField, other.<DeteriorationInDays>k__BackingField));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002389 File Offset: 0x00000589
		[CompilerGenerated]
		protected DeteriorableSpec(DeteriorableSpec original) : base(original)
		{
			this.DeteriorationInDays = original.<DeteriorationInDays>k__BackingField;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000239E File Offset: 0x0000059E
		public DeteriorableSpec()
		{
		}
	}
}
