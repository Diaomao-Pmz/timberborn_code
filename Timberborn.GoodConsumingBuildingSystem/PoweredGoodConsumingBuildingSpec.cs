using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.GoodConsumingBuildingSystem
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	public class PoweredGoodConsumingBuildingSpec : ComponentSpec, IEquatable<PoweredGoodConsumingBuildingSpec>
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002EEA File Offset: 0x000010EA
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(PoweredGoodConsumingBuildingSpec);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002EF8 File Offset: 0x000010F8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PoweredGoodConsumingBuildingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002F44 File Offset: 0x00001144
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002F4D File Offset: 0x0000114D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PoweredGoodConsumingBuildingSpec left, PoweredGoodConsumingBuildingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002F59 File Offset: 0x00001159
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PoweredGoodConsumingBuildingSpec left, PoweredGoodConsumingBuildingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002F6D File Offset: 0x0000116D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002F75 File Offset: 0x00001175
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PoweredGoodConsumingBuildingSpec);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000223F File Offset: 0x0000043F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002F83 File Offset: 0x00001183
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PoweredGoodConsumingBuildingSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002F9A File Offset: 0x0000119A
		[CompilerGenerated]
		protected PoweredGoodConsumingBuildingSpec(PoweredGoodConsumingBuildingSpec original) : base(original)
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000022BD File Offset: 0x000004BD
		public PoweredGoodConsumingBuildingSpec()
		{
		}
	}
}
