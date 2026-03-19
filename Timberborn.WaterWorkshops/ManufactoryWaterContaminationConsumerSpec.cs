using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.WaterBuildings;

namespace Timberborn.WaterWorkshops
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	public class ManufactoryWaterContaminationConsumerSpec : ComponentSpec, IContaminatedWaterNeedingBuilding, IEquatable<ManufactoryWaterContaminationConsumerSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002336 File Offset: 0x00000536
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ManufactoryWaterContaminationConsumerSpec);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002344 File Offset: 0x00000544
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ManufactoryWaterContaminationConsumerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002214 File Offset: 0x00000414
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002390 File Offset: 0x00000590
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ManufactoryWaterContaminationConsumerSpec left, ManufactoryWaterContaminationConsumerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000239C File Offset: 0x0000059C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ManufactoryWaterContaminationConsumerSpec left, ManufactoryWaterContaminationConsumerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000223D File Offset: 0x0000043D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000023B0 File Offset: 0x000005B0
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ManufactoryWaterContaminationConsumerSpec);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002253 File Offset: 0x00000453
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000225C File Offset: 0x0000045C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ManufactoryWaterContaminationConsumerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002273 File Offset: 0x00000473
		[CompilerGenerated]
		protected ManufactoryWaterContaminationConsumerSpec(ManufactoryWaterContaminationConsumerSpec original) : base(original)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000227C File Offset: 0x0000047C
		public ManufactoryWaterContaminationConsumerSpec()
		{
		}
	}
}
