using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x0200001D RID: 29
	[NullableContext(1)]
	[Nullable(0)]
	public class WaterSourceDischargerSpec : ComponentSpec, IEquatable<WaterSourceDischargerSpec>
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000373E File Offset: 0x0000193E
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WaterSourceDischargerSpec);
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000374C File Offset: 0x0000194C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterSourceDischargerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000022B8 File Offset: 0x000004B8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003798 File Offset: 0x00001998
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterSourceDischargerSpec left, WaterSourceDischargerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000037A4 File Offset: 0x000019A4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterSourceDischargerSpec left, WaterSourceDischargerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000022E1 File Offset: 0x000004E1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000037B8 File Offset: 0x000019B8
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterSourceDischargerSpec);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000022F7 File Offset: 0x000004F7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00002300 File Offset: 0x00000500
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterSourceDischargerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00002317 File Offset: 0x00000517
		[CompilerGenerated]
		protected WaterSourceDischargerSpec(WaterSourceDischargerSpec original) : base(original)
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00002320 File Offset: 0x00000520
		public WaterSourceDischargerSpec()
		{
		}
	}
}
