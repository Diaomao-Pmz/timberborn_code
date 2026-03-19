using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x0200001A RID: 26
	[NullableContext(1)]
	[Nullable(0)]
	public class WorkplaceIlluminatorSpec : ComponentSpec, IEquatable<WorkplaceIlluminatorSpec>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000383B File Offset: 0x00001A3B
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WorkplaceIlluminatorSpec);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003848 File Offset: 0x00001A48
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WorkplaceIlluminatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003894 File Offset: 0x00001A94
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000389D File Offset: 0x00001A9D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WorkplaceIlluminatorSpec left, WorkplaceIlluminatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000038A9 File Offset: 0x00001AA9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WorkplaceIlluminatorSpec left, WorkplaceIlluminatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000038BD File Offset: 0x00001ABD
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000038C5 File Offset: 0x00001AC5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WorkplaceIlluminatorSpec);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000038D3 File Offset: 0x00001AD3
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000038DC File Offset: 0x00001ADC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WorkplaceIlluminatorSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000038F3 File Offset: 0x00001AF3
		[CompilerGenerated]
		protected WorkplaceIlluminatorSpec(WorkplaceIlluminatorSpec original) : base(original)
		{
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000038FC File Offset: 0x00001AFC
		public WorkplaceIlluminatorSpec()
		{
		}
	}
}
