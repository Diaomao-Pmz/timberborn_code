using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WalkingSystem
{
	// Token: 0x0200001E RID: 30
	[NullableContext(1)]
	[Nullable(0)]
	public class WalkerSpeedManagerSpec : ComponentSpec, IEquatable<WalkerSpeedManagerSpec>
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003C86 File Offset: 0x00001E86
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WalkerSpeedManagerSpec);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003C92 File Offset: 0x00001E92
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00003C9A File Offset: 0x00001E9A
		[Serialize]
		public float BaseWalkingSpeed { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003CA3 File Offset: 0x00001EA3
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00003CAB File Offset: 0x00001EAB
		[Serialize]
		public float BaseSlowedSpeed { get; set; }

		// Token: 0x060000B6 RID: 182 RVA: 0x00003CB4 File Offset: 0x00001EB4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WalkerSpeedManagerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003D00 File Offset: 0x00001F00
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("BaseWalkingSpeed = ");
			builder.Append(this.BaseWalkingSpeed.ToString());
			builder.Append(", BaseSlowedSpeed = ");
			builder.Append(this.BaseSlowedSpeed.ToString());
			return true;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003D71 File Offset: 0x00001F71
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WalkerSpeedManagerSpec left, WalkerSpeedManagerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003D7D File Offset: 0x00001F7D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WalkerSpeedManagerSpec left, WalkerSpeedManagerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003D91 File Offset: 0x00001F91
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<BaseWalkingSpeed>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<BaseSlowedSpeed>k__BackingField);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003DC7 File Offset: 0x00001FC7
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WalkerSpeedManagerSpec);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00002D79 File Offset: 0x00000F79
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003DD8 File Offset: 0x00001FD8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WalkerSpeedManagerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<BaseWalkingSpeed>k__BackingField, other.<BaseWalkingSpeed>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<BaseSlowedSpeed>k__BackingField, other.<BaseSlowedSpeed>k__BackingField));
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003E2C File Offset: 0x0000202C
		[CompilerGenerated]
		protected WalkerSpeedManagerSpec(WalkerSpeedManagerSpec original) : base(original)
		{
			this.BaseWalkingSpeed = original.<BaseWalkingSpeed>k__BackingField;
			this.BaseSlowedSpeed = original.<BaseSlowedSpeed>k__BackingField;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00002DF9 File Offset: 0x00000FF9
		public WalkerSpeedManagerSpec()
		{
		}
	}
}
