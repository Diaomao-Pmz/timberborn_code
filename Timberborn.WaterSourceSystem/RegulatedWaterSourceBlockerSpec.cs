using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000010 RID: 16
	[NullableContext(1)]
	[Nullable(0)]
	public class RegulatedWaterSourceBlockerSpec : ComponentSpec, IEquatable<RegulatedWaterSourceBlockerSpec>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000028A8 File Offset: 0x00000AA8
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(RegulatedWaterSourceBlockerSpec);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000028B4 File Offset: 0x00000AB4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RegulatedWaterSourceBlockerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000022B8 File Offset: 0x000004B8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002900 File Offset: 0x00000B00
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RegulatedWaterSourceBlockerSpec left, RegulatedWaterSourceBlockerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000290C File Offset: 0x00000B0C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RegulatedWaterSourceBlockerSpec left, RegulatedWaterSourceBlockerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000022E1 File Offset: 0x000004E1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002920 File Offset: 0x00000B20
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RegulatedWaterSourceBlockerSpec);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000022F7 File Offset: 0x000004F7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002300 File Offset: 0x00000500
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RegulatedWaterSourceBlockerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002317 File Offset: 0x00000517
		[CompilerGenerated]
		protected RegulatedWaterSourceBlockerSpec(RegulatedWaterSourceBlockerSpec original) : base(original)
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002320 File Offset: 0x00000520
		public RegulatedWaterSourceBlockerSpec()
		{
		}
	}
}
