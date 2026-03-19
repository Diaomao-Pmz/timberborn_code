using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.RecoveredGoodSystem
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	public class RecoveredGoodStackDisintegrationSpec : ComponentSpec, IEquatable<RecoveredGoodStackDisintegrationSpec>
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000030F6 File Offset: 0x000012F6
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(RecoveredGoodStackDisintegrationSpec);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003102 File Offset: 0x00001302
		// (set) Token: 0x06000073 RID: 115 RVA: 0x0000310A File Offset: 0x0000130A
		[Serialize]
		public float DaysToDisintegrate { get; set; }

		// Token: 0x06000074 RID: 116 RVA: 0x00003114 File Offset: 0x00001314
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RecoveredGoodStackDisintegrationSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003160 File Offset: 0x00001360
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DaysToDisintegrate = ");
			builder.Append(this.DaysToDisintegrate.ToString());
			return true;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000031AA File Offset: 0x000013AA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RecoveredGoodStackDisintegrationSpec left, RecoveredGoodStackDisintegrationSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000031B6 File Offset: 0x000013B6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RecoveredGoodStackDisintegrationSpec left, RecoveredGoodStackDisintegrationSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000031CA File Offset: 0x000013CA
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DaysToDisintegrate>k__BackingField);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000031E9 File Offset: 0x000013E9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RecoveredGoodStackDisintegrationSpec);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002F69 File Offset: 0x00001169
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000031F7 File Offset: 0x000013F7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RecoveredGoodStackDisintegrationSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<DaysToDisintegrate>k__BackingField, other.<DaysToDisintegrate>k__BackingField));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003228 File Offset: 0x00001428
		[CompilerGenerated]
		protected RecoveredGoodStackDisintegrationSpec(RecoveredGoodStackDisintegrationSpec original) : base(original)
		{
			this.DaysToDisintegrate = original.<DaysToDisintegrate>k__BackingField;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002FE9 File Offset: 0x000011E9
		public RecoveredGoodStackDisintegrationSpec()
		{
		}
	}
}
