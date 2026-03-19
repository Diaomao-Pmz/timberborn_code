using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.GameWaterSourceSystem
{
	// Token: 0x0200000C RID: 12
	[NullableContext(1)]
	[Nullable(0)]
	public class UndergroundWaterSourceDrillSpec : ComponentSpec, IEquatable<UndergroundWaterSourceDrillSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000244E File Offset: 0x0000064E
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(UndergroundWaterSourceDrillSpec);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000245C File Offset: 0x0000065C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UndergroundWaterSourceDrillSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024A8 File Offset: 0x000006A8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024B1 File Offset: 0x000006B1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(UndergroundWaterSourceDrillSpec left, UndergroundWaterSourceDrillSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024BD File Offset: 0x000006BD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(UndergroundWaterSourceDrillSpec left, UndergroundWaterSourceDrillSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024D1 File Offset: 0x000006D1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000024D9 File Offset: 0x000006D9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as UndergroundWaterSourceDrillSpec);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000024E7 File Offset: 0x000006E7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024F0 File Offset: 0x000006F0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(UndergroundWaterSourceDrillSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002507 File Offset: 0x00000707
		[CompilerGenerated]
		protected UndergroundWaterSourceDrillSpec(UndergroundWaterSourceDrillSpec original) : base(original)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002510 File Offset: 0x00000710
		public UndergroundWaterSourceDrillSpec()
		{
		}
	}
}
