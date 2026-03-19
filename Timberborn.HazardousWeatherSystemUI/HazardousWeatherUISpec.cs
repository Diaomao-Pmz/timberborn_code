using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.HazardousWeatherSystemUI
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	public class HazardousWeatherUISpec : ComponentSpec, IEquatable<HazardousWeatherUISpec>
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002857 File Offset: 0x00000A57
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(HazardousWeatherUISpec);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002863 File Offset: 0x00000A63
		// (set) Token: 0x06000048 RID: 72 RVA: 0x0000286B File Offset: 0x00000A6B
		[Serialize]
		public int ApproachingNotificationDays { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002874 File Offset: 0x00000A74
		// (set) Token: 0x0600004A RID: 74 RVA: 0x0000287C File Offset: 0x00000A7C
		[Serialize]
		public float MaxDayProgressLeftToNotify { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002885 File Offset: 0x00000A85
		// (set) Token: 0x0600004C RID: 76 RVA: 0x0000288D File Offset: 0x00000A8D
		[Serialize]
		public float NotificationDuration { get; set; }

		// Token: 0x0600004D RID: 77 RVA: 0x00002898 File Offset: 0x00000A98
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("HazardousWeatherUISpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000028E4 File Offset: 0x00000AE4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ApproachingNotificationDays = ");
			builder.Append(this.ApproachingNotificationDays.ToString());
			builder.Append(", MaxDayProgressLeftToNotify = ");
			builder.Append(this.MaxDayProgressLeftToNotify.ToString());
			builder.Append(", NotificationDuration = ");
			builder.Append(this.NotificationDuration.ToString());
			return true;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000297C File Offset: 0x00000B7C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(HazardousWeatherUISpec left, HazardousWeatherUISpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002988 File Offset: 0x00000B88
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(HazardousWeatherUISpec left, HazardousWeatherUISpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000299C File Offset: 0x00000B9C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ApproachingNotificationDays>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxDayProgressLeftToNotify>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<NotificationDuration>k__BackingField);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000029F4 File Offset: 0x00000BF4
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as HazardousWeatherUISpec);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A02 File Offset: 0x00000C02
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002A0C File Offset: 0x00000C0C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(HazardousWeatherUISpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<ApproachingNotificationDays>k__BackingField, other.<ApproachingNotificationDays>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxDayProgressLeftToNotify>k__BackingField, other.<MaxDayProgressLeftToNotify>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<NotificationDuration>k__BackingField, other.<NotificationDuration>k__BackingField));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002A78 File Offset: 0x00000C78
		[CompilerGenerated]
		protected HazardousWeatherUISpec(HazardousWeatherUISpec original) : base(original)
		{
			this.ApproachingNotificationDays = original.<ApproachingNotificationDays>k__BackingField;
			this.MaxDayProgressLeftToNotify = original.<MaxDayProgressLeftToNotify>k__BackingField;
			this.NotificationDuration = original.<NotificationDuration>k__BackingField;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002AA5 File Offset: 0x00000CA5
		public HazardousWeatherUISpec()
		{
		}
	}
}
