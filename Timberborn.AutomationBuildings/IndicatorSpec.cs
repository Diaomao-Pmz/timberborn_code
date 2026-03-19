using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200001F RID: 31
	[NullableContext(1)]
	[Nullable(0)]
	public class IndicatorSpec : ComponentSpec, IEquatable<IndicatorSpec>
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00004B81 File Offset: 0x00002D81
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(IndicatorSpec);
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004B90 File Offset: 0x00002D90
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("IndicatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00002710 File Offset: 0x00000910
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004BDC File Offset: 0x00002DDC
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(IndicatorSpec left, IndicatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004BE8 File Offset: 0x00002DE8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(IndicatorSpec left, IndicatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00002739 File Offset: 0x00000939
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004BFC File Offset: 0x00002DFC
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IndicatorSpec);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00002758 File Offset: 0x00000958
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(IndicatorSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000276F File Offset: 0x0000096F
		[CompilerGenerated]
		protected IndicatorSpec(IndicatorSpec original) : base(original)
		{
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00002778 File Offset: 0x00000978
		public IndicatorSpec()
		{
		}
	}
}
