using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.EnterableSystem
{
	// Token: 0x02000016 RID: 22
	[NullableContext(1)]
	[Nullable(0)]
	public class EntererBoundsScalerSpec : ComponentSpec, IEquatable<EntererBoundsScalerSpec>
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000034BF File Offset: 0x000016BF
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(EntererBoundsScalerSpec);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000034CB File Offset: 0x000016CB
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000034D3 File Offset: 0x000016D3
		[Serialize]
		public float Scale { get; set; }

		// Token: 0x060000A4 RID: 164 RVA: 0x000034DC File Offset: 0x000016DC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EntererBoundsScalerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003528 File Offset: 0x00001728
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Scale = ");
			builder.Append(this.Scale.ToString());
			return true;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003572 File Offset: 0x00001772
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(EntererBoundsScalerSpec left, EntererBoundsScalerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000357E File Offset: 0x0000177E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(EntererBoundsScalerSpec left, EntererBoundsScalerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003592 File Offset: 0x00001792
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Scale>k__BackingField);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000035B1 File Offset: 0x000017B1
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EntererBoundsScalerSpec);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000035BF File Offset: 0x000017BF
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(EntererBoundsScalerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<Scale>k__BackingField, other.<Scale>k__BackingField));
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000035F0 File Offset: 0x000017F0
		[CompilerGenerated]
		protected EntererBoundsScalerSpec(EntererBoundsScalerSpec original) : base(original)
		{
			this.Scale = original.<Scale>k__BackingField;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000279E File Offset: 0x0000099E
		public EntererBoundsScalerSpec()
		{
		}
	}
}
