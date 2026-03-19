using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterWorkshops
{
	// Token: 0x0200000C RID: 12
	[NullableContext(1)]
	[Nullable(0)]
	public class ManufactoryWaterContaminationProducerSpec : ComponentSpec, IEquatable<ManufactoryWaterContaminationProducerSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000024B1 File Offset: 0x000006B1
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ManufactoryWaterContaminationProducerSpec);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000024C0 File Offset: 0x000006C0
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ManufactoryWaterContaminationProducerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002214 File Offset: 0x00000414
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000250C File Offset: 0x0000070C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ManufactoryWaterContaminationProducerSpec left, ManufactoryWaterContaminationProducerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002518 File Offset: 0x00000718
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ManufactoryWaterContaminationProducerSpec left, ManufactoryWaterContaminationProducerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000223D File Offset: 0x0000043D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000252C File Offset: 0x0000072C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ManufactoryWaterContaminationProducerSpec);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002253 File Offset: 0x00000453
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000225C File Offset: 0x0000045C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ManufactoryWaterContaminationProducerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002273 File Offset: 0x00000473
		[CompilerGenerated]
		protected ManufactoryWaterContaminationProducerSpec(ManufactoryWaterContaminationProducerSpec original) : base(original)
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000227C File Offset: 0x0000047C
		public ManufactoryWaterContaminationProducerSpec()
		{
		}
	}
}
