using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000012 RID: 18
	public class FloodgateAnimationControllerSpec : ComponentSpec, IEquatable<FloodgateAnimationControllerSpec>
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000366E File Offset: 0x0000186E
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(FloodgateAnimationControllerSpec);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000367A File Offset: 0x0000187A
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003682 File Offset: 0x00001882
		[Serialize]
		public string GateName { get; set; }

		// Token: 0x060000B1 RID: 177 RVA: 0x0000368C File Offset: 0x0000188C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FloodgateAnimationControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000036D8 File Offset: 0x000018D8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("GateName = ");
			builder.Append(this.GateName);
			return true;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003709 File Offset: 0x00001909
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FloodgateAnimationControllerSpec left, FloodgateAnimationControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003715 File Offset: 0x00001915
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FloodgateAnimationControllerSpec left, FloodgateAnimationControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003729 File Offset: 0x00001929
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GateName>k__BackingField);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003748 File Offset: 0x00001948
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FloodgateAnimationControllerSpec);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00002B9B File Offset: 0x00000D9B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003756 File Offset: 0x00001956
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FloodgateAnimationControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<GateName>k__BackingField, other.<GateName>k__BackingField));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003787 File Offset: 0x00001987
		[CompilerGenerated]
		protected FloodgateAnimationControllerSpec([Nullable(1)] FloodgateAnimationControllerSpec original) : base(original)
		{
			this.GateName = original.<GateName>k__BackingField;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00002CBC File Offset: 0x00000EBC
		public FloodgateAnimationControllerSpec()
		{
		}
	}
}
