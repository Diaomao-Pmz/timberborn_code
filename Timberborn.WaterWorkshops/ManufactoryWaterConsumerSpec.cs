using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterWorkshops
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	public class ManufactoryWaterConsumerSpec : ComponentSpec, IEquatable<ManufactoryWaterConsumerSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021BA File Offset: 0x000003BA
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ManufactoryWaterConsumerSpec);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021C8 File Offset: 0x000003C8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ManufactoryWaterConsumerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002214 File Offset: 0x00000414
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000221D File Offset: 0x0000041D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ManufactoryWaterConsumerSpec left, ManufactoryWaterConsumerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002229 File Offset: 0x00000429
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ManufactoryWaterConsumerSpec left, ManufactoryWaterConsumerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000223D File Offset: 0x0000043D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002245 File Offset: 0x00000445
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ManufactoryWaterConsumerSpec);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002253 File Offset: 0x00000453
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000225C File Offset: 0x0000045C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ManufactoryWaterConsumerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002273 File Offset: 0x00000473
		[CompilerGenerated]
		protected ManufactoryWaterConsumerSpec(ManufactoryWaterConsumerSpec original) : base(original)
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000227C File Offset: 0x0000047C
		public ManufactoryWaterConsumerSpec()
		{
		}
	}
}
