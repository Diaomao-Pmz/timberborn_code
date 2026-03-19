using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.LifeSystem
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	public class LifeServiceSpec : ComponentSpec, IEquatable<LifeServiceSpec>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000022F2 File Offset: 0x000004F2
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(LifeServiceSpec);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000022FE File Offset: 0x000004FE
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002306 File Offset: 0x00000506
		[Serialize]
		public int AverageLifespan { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000230F File Offset: 0x0000050F
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002317 File Offset: 0x00000517
		[Serialize]
		public int DaysOfChildhood { get; set; }

		// Token: 0x06000021 RID: 33 RVA: 0x00002320 File Offset: 0x00000520
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("LifeServiceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000236C File Offset: 0x0000056C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("AverageLifespan = ");
			builder.Append(this.AverageLifespan.ToString());
			builder.Append(", DaysOfChildhood = ");
			builder.Append(this.DaysOfChildhood.ToString());
			return true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023DD File Offset: 0x000005DD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(LifeServiceSpec left, LifeServiceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023E9 File Offset: 0x000005E9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(LifeServiceSpec left, LifeServiceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023FD File Offset: 0x000005FD
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<AverageLifespan>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<DaysOfChildhood>k__BackingField);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002433 File Offset: 0x00000633
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as LifeServiceSpec);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002441 File Offset: 0x00000641
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000244C File Offset: 0x0000064C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(LifeServiceSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<AverageLifespan>k__BackingField, other.<AverageLifespan>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<DaysOfChildhood>k__BackingField, other.<DaysOfChildhood>k__BackingField));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000024A0 File Offset: 0x000006A0
		[CompilerGenerated]
		protected LifeServiceSpec(LifeServiceSpec original) : base(original)
		{
			this.AverageLifespan = original.<AverageLifespan>k__BackingField;
			this.DaysOfChildhood = original.<DaysOfChildhood>k__BackingField;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024C1 File Offset: 0x000006C1
		public LifeServiceSpec()
		{
		}
	}
}
