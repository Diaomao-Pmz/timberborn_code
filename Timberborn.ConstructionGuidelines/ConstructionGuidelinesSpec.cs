using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ConstructionGuidelines
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	public class ConstructionGuidelinesSpec : ComponentSpec, IEquatable<ConstructionGuidelinesSpec>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000303F File Offset: 0x0000123F
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ConstructionGuidelinesSpec);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000304B File Offset: 0x0000124B
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00003053 File Offset: 0x00001253
		[Serialize]
		public int Radius { get; set; }

		// Token: 0x06000040 RID: 64 RVA: 0x0000305C File Offset: 0x0000125C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ConstructionGuidelinesSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000030A8 File Offset: 0x000012A8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Radius = ");
			builder.Append(this.Radius.ToString());
			return true;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000030F2 File Offset: 0x000012F2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ConstructionGuidelinesSpec left, ConstructionGuidelinesSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000030FE File Offset: 0x000012FE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ConstructionGuidelinesSpec left, ConstructionGuidelinesSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003112 File Offset: 0x00001312
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Radius>k__BackingField);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003131 File Offset: 0x00001331
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ConstructionGuidelinesSpec);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000313F File Offset: 0x0000133F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003148 File Offset: 0x00001348
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ConstructionGuidelinesSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Radius>k__BackingField, other.<Radius>k__BackingField));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003179 File Offset: 0x00001379
		[CompilerGenerated]
		protected ConstructionGuidelinesSpec(ConstructionGuidelinesSpec original) : base(original)
		{
			this.Radius = original.<Radius>k__BackingField;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000318E File Offset: 0x0000138E
		public ConstructionGuidelinesSpec()
		{
		}
	}
}
