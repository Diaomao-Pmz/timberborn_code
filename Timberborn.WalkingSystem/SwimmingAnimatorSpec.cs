using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WalkingSystem
{
	// Token: 0x02000017 RID: 23
	[NullableContext(1)]
	[Nullable(0)]
	public class SwimmingAnimatorSpec : ComponentSpec, IEquatable<SwimmingAnimatorSpec>
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003314 File Offset: 0x00001514
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SwimmingAnimatorSpec);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003320 File Offset: 0x00001520
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003328 File Offset: 0x00001528
		[Serialize]
		public float LowerSwimmingDepthThreshold { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003331 File Offset: 0x00001531
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003339 File Offset: 0x00001539
		[Serialize]
		public float UpperSwimmingDepthThreshold { get; set; }

		// Token: 0x0600007C RID: 124 RVA: 0x00003344 File Offset: 0x00001544
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SwimmingAnimatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003390 File Offset: 0x00001590
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("LowerSwimmingDepthThreshold = ");
			builder.Append(this.LowerSwimmingDepthThreshold.ToString());
			builder.Append(", UpperSwimmingDepthThreshold = ");
			builder.Append(this.UpperSwimmingDepthThreshold.ToString());
			return true;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003401 File Offset: 0x00001601
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SwimmingAnimatorSpec left, SwimmingAnimatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000340D File Offset: 0x0000160D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SwimmingAnimatorSpec left, SwimmingAnimatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003421 File Offset: 0x00001621
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<LowerSwimmingDepthThreshold>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<UpperSwimmingDepthThreshold>k__BackingField);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003457 File Offset: 0x00001657
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SwimmingAnimatorSpec);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002D79 File Offset: 0x00000F79
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003468 File Offset: 0x00001668
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SwimmingAnimatorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<LowerSwimmingDepthThreshold>k__BackingField, other.<LowerSwimmingDepthThreshold>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<UpperSwimmingDepthThreshold>k__BackingField, other.<UpperSwimmingDepthThreshold>k__BackingField));
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000034BC File Offset: 0x000016BC
		[CompilerGenerated]
		protected SwimmingAnimatorSpec(SwimmingAnimatorSpec original) : base(original)
		{
			this.LowerSwimmingDepthThreshold = original.<LowerSwimmingDepthThreshold>k__BackingField;
			this.UpperSwimmingDepthThreshold = original.<UpperSwimmingDepthThreshold>k__BackingField;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002DF9 File Offset: 0x00000FF9
		public SwimmingAnimatorSpec()
		{
		}
	}
}
