using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x0200000C RID: 12
	[NullableContext(1)]
	[Nullable(0)]
	public class MechanicalBuildingSpec : ComponentSpec, IEquatable<MechanicalBuildingSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000024BC File Offset: 0x000006BC
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(MechanicalBuildingSpec);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024C8 File Offset: 0x000006C8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MechanicalBuildingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002514 File Offset: 0x00000714
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000251D File Offset: 0x0000071D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MechanicalBuildingSpec left, MechanicalBuildingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002529 File Offset: 0x00000729
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MechanicalBuildingSpec left, MechanicalBuildingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000253D File Offset: 0x0000073D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002545 File Offset: 0x00000745
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MechanicalBuildingSpec);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002553 File Offset: 0x00000753
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000255C File Offset: 0x0000075C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MechanicalBuildingSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002573 File Offset: 0x00000773
		[CompilerGenerated]
		protected MechanicalBuildingSpec(MechanicalBuildingSpec original) : base(original)
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000257C File Offset: 0x0000077C
		public MechanicalBuildingSpec()
		{
		}
	}
}
