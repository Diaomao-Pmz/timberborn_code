using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.PowerGeneration
{
	// Token: 0x02000014 RID: 20
	[NullableContext(1)]
	[Nullable(0)]
	public class WindPoweredGeneratorSpec : ComponentSpec, IEquatable<WindPoweredGeneratorSpec>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002F29 File Offset: 0x00001129
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WindPoweredGeneratorSpec);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002F35 File Offset: 0x00001135
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00002F3D File Offset: 0x0000113D
		[Serialize]
		public float MinRequiredWindStrength { get; set; }

		// Token: 0x0600008F RID: 143 RVA: 0x00002F48 File Offset: 0x00001148
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WindPoweredGeneratorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002F94 File Offset: 0x00001194
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MinRequiredWindStrength = ");
			builder.Append(this.MinRequiredWindStrength.ToString());
			return true;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002FDE File Offset: 0x000011DE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WindPoweredGeneratorSpec left, WindPoweredGeneratorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002FEA File Offset: 0x000011EA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WindPoweredGeneratorSpec left, WindPoweredGeneratorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002FFE File Offset: 0x000011FE
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinRequiredWindStrength>k__BackingField);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000301D File Offset: 0x0000121D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WindPoweredGeneratorSpec);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002233 File Offset: 0x00000433
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000302B File Offset: 0x0000122B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WindPoweredGeneratorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<MinRequiredWindStrength>k__BackingField, other.<MinRequiredWindStrength>k__BackingField));
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000305C File Offset: 0x0000125C
		[CompilerGenerated]
		protected WindPoweredGeneratorSpec(WindPoweredGeneratorSpec original) : base(original)
		{
			this.MinRequiredWindStrength = original.<MinRequiredWindStrength>k__BackingField;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000225C File Offset: 0x0000045C
		public WindPoweredGeneratorSpec()
		{
		}
	}
}
