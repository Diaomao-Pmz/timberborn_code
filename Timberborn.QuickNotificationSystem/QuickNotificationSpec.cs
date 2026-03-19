using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.QuickNotificationSystem
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	public class QuickNotificationSpec : ComponentSpec, IEquatable<QuickNotificationSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000237B File Offset: 0x0000057B
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(QuickNotificationSpec);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002387 File Offset: 0x00000587
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000238F File Offset: 0x0000058F
		[Serialize]
		public float Duration { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002398 File Offset: 0x00000598
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000023A0 File Offset: 0x000005A0
		[Serialize]
		public float ExtendedDuration { get; set; }

		// Token: 0x0600001A RID: 26 RVA: 0x000023AC File Offset: 0x000005AC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("QuickNotificationSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023F8 File Offset: 0x000005F8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Duration = ");
			builder.Append(this.Duration.ToString());
			builder.Append(", ExtendedDuration = ");
			builder.Append(this.ExtendedDuration.ToString());
			return true;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002469 File Offset: 0x00000669
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(QuickNotificationSpec left, QuickNotificationSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002475 File Offset: 0x00000675
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(QuickNotificationSpec left, QuickNotificationSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002489 File Offset: 0x00000689
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Duration>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ExtendedDuration>k__BackingField);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024BF File Offset: 0x000006BF
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as QuickNotificationSpec);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024CD File Offset: 0x000006CD
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024D8 File Offset: 0x000006D8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(QuickNotificationSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<Duration>k__BackingField, other.<Duration>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ExtendedDuration>k__BackingField, other.<ExtendedDuration>k__BackingField));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000252C File Offset: 0x0000072C
		[CompilerGenerated]
		protected QuickNotificationSpec(QuickNotificationSpec original) : base(original)
		{
			this.Duration = original.<Duration>k__BackingField;
			this.ExtendedDuration = original.<ExtendedDuration>k__BackingField;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000254D File Offset: 0x0000074D
		public QuickNotificationSpec()
		{
		}
	}
}
