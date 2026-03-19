using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterWorkshops
{
	// Token: 0x0200000E RID: 14
	[NullableContext(1)]
	[Nullable(0)]
	public class ManufactoryWaterProducerSpec : ComponentSpec, IEquatable<ManufactoryWaterProducerSpec>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000262D File Offset: 0x0000082D
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ManufactoryWaterProducerSpec);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000263C File Offset: 0x0000083C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ManufactoryWaterProducerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002214 File Offset: 0x00000414
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002688 File Offset: 0x00000888
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ManufactoryWaterProducerSpec left, ManufactoryWaterProducerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002694 File Offset: 0x00000894
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ManufactoryWaterProducerSpec left, ManufactoryWaterProducerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000223D File Offset: 0x0000043D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000026A8 File Offset: 0x000008A8
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ManufactoryWaterProducerSpec);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002253 File Offset: 0x00000453
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000225C File Offset: 0x0000045C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ManufactoryWaterProducerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002273 File Offset: 0x00000473
		[CompilerGenerated]
		protected ManufactoryWaterProducerSpec(ManufactoryWaterProducerSpec original) : base(original)
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000227C File Offset: 0x0000047C
		public ManufactoryWaterProducerSpec()
		{
		}
	}
}
