using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.TemplateSystem;
using Timberborn.Yielding;

namespace Timberborn.NaturalResources
{
	// Token: 0x0200000D RID: 13
	[NullableContext(1)]
	[Nullable(0)]
	public class NaturalResourceSpec : ComponentSpec, IOrderableYielder, IEquatable<NaturalResourceSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000246C File Offset: 0x0000066C
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(NaturalResourceSpec);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002478 File Offset: 0x00000678
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002480 File Offset: 0x00000680
		[Serialize]
		public int Order { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002489 File Offset: 0x00000689
		public bool UsableWithCurrentFeatureToggles
		{
			get
			{
				return base.GetSpec<TemplateSpec>().UsableWithCurrentFeatureToggles;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002498 File Offset: 0x00000698
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NaturalResourceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024E4 File Offset: 0x000006E4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Order = ");
			builder.Append(this.Order.ToString());
			builder.Append(", UsableWithCurrentFeatureToggles = ");
			builder.Append(this.UsableWithCurrentFeatureToggles.ToString());
			return true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002555 File Offset: 0x00000755
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(NaturalResourceSpec left, NaturalResourceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002561 File Offset: 0x00000761
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(NaturalResourceSpec left, NaturalResourceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002575 File Offset: 0x00000775
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Order>k__BackingField);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002594 File Offset: 0x00000794
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NaturalResourceSpec);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025A2 File Offset: 0x000007A2
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025AB File Offset: 0x000007AB
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(NaturalResourceSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Order>k__BackingField, other.<Order>k__BackingField));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025DC File Offset: 0x000007DC
		[CompilerGenerated]
		protected NaturalResourceSpec(NaturalResourceSpec original) : base(original)
		{
			this.Order = original.<Order>k__BackingField;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025F1 File Offset: 0x000007F1
		public NaturalResourceSpec()
		{
		}
	}
}
