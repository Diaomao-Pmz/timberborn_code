using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Wonders
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	public class WonderDeactivationTimerSpec : ComponentSpec, IEquatable<WonderDeactivationTimerSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000028F9 File Offset: 0x00000AF9
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WonderDeactivationTimerSpec);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002905 File Offset: 0x00000B05
		// (set) Token: 0x06000048 RID: 72 RVA: 0x0000290D File Offset: 0x00000B0D
		[Serialize]
		public float TimerDelayInHours { get; set; }

		// Token: 0x06000049 RID: 73 RVA: 0x00002918 File Offset: 0x00000B18
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WonderDeactivationTimerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002964 File Offset: 0x00000B64
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("TimerDelayInHours = ");
			builder.Append(this.TimerDelayInHours.ToString());
			return true;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000029AE File Offset: 0x00000BAE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WonderDeactivationTimerSpec left, WonderDeactivationTimerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000029BA File Offset: 0x00000BBA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WonderDeactivationTimerSpec left, WonderDeactivationTimerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000029CE File Offset: 0x00000BCE
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<TimerDelayInHours>k__BackingField);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000029ED File Offset: 0x00000BED
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WonderDeactivationTimerSpec);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029FB File Offset: 0x00000BFB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A04 File Offset: 0x00000C04
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WonderDeactivationTimerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<TimerDelayInHours>k__BackingField, other.<TimerDelayInHours>k__BackingField));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A35 File Offset: 0x00000C35
		[CompilerGenerated]
		protected WonderDeactivationTimerSpec(WonderDeactivationTimerSpec original) : base(original)
		{
			this.TimerDelayInHours = original.<TimerDelayInHours>k__BackingField;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A4A File Offset: 0x00000C4A
		public WonderDeactivationTimerSpec()
		{
		}
	}
}
