using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000022 RID: 34
	[NullableContext(1)]
	[Nullable(0)]
	public class WaterSourceRegulatorSpec : ComponentSpec, IEquatable<WaterSourceRegulatorSpec>
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00003CA9 File Offset: 0x00001EA9
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WaterSourceRegulatorSpec);
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00003CB8 File Offset: 0x00001EB8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterSourceRegulatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000022B8 File Offset: 0x000004B8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003D04 File Offset: 0x00001F04
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterSourceRegulatorSpec left, WaterSourceRegulatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00003D10 File Offset: 0x00001F10
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterSourceRegulatorSpec left, WaterSourceRegulatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000022E1 File Offset: 0x000004E1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00003D24 File Offset: 0x00001F24
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterSourceRegulatorSpec);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000022F7 File Offset: 0x000004F7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00002300 File Offset: 0x00000500
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterSourceRegulatorSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00002317 File Offset: 0x00000517
		[CompilerGenerated]
		protected WaterSourceRegulatorSpec(WaterSourceRegulatorSpec original) : base(original)
		{
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00002320 File Offset: 0x00000520
		public WaterSourceRegulatorSpec()
		{
		}
	}
}
