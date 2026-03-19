using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.LocalizationSerialization;

namespace Timberborn.WorkSystem
{
	// Token: 0x0200000C RID: 12
	public class NeedPreventingWorkSpec : ComponentSpec, IEquatable<NeedPreventingWorkSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002402 File Offset: 0x00000602
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(NeedPreventingWorkSpec);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000240E File Offset: 0x0000060E
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002416 File Offset: 0x00000616
		[Serialize("WorkRefusalWarningLocKey")]
		public LocalizedText WorkRefusalWarning { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000241F File Offset: 0x0000061F
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002427 File Offset: 0x00000627
		[Serialize]
		private string WorkRefusalWarningLocKey { get; set; }

		// Token: 0x06000020 RID: 32 RVA: 0x00002430 File Offset: 0x00000630
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NeedPreventingWorkSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000247C File Offset: 0x0000067C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("WorkRefusalWarning = ");
			builder.Append(this.WorkRefusalWarning);
			return true;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024AD File Offset: 0x000006AD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(NeedPreventingWorkSpec left, NeedPreventingWorkSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024B9 File Offset: 0x000006B9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(NeedPreventingWorkSpec left, NeedPreventingWorkSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024CD File Offset: 0x000006CD
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<WorkRefusalWarning>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WorkRefusalWarningLocKey>k__BackingField);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002503 File Offset: 0x00000703
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NeedPreventingWorkSpec);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002511 File Offset: 0x00000711
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000251C File Offset: 0x0000071C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(NeedPreventingWorkSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<LocalizedText>.Default.Equals(this.<WorkRefusalWarning>k__BackingField, other.<WorkRefusalWarning>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<WorkRefusalWarningLocKey>k__BackingField, other.<WorkRefusalWarningLocKey>k__BackingField));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002570 File Offset: 0x00000770
		[CompilerGenerated]
		protected NeedPreventingWorkSpec([Nullable(1)] NeedPreventingWorkSpec original) : base(original)
		{
			this.WorkRefusalWarning = original.<WorkRefusalWarning>k__BackingField;
			this.WorkRefusalWarningLocKey = original.<WorkRefusalWarningLocKey>k__BackingField;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002591 File Offset: 0x00000791
		public NeedPreventingWorkSpec()
		{
		}
	}
}
