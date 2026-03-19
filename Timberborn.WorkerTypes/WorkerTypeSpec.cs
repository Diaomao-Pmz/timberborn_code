using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.LocalizationSerialization;

namespace Timberborn.WorkerTypes
{
	// Token: 0x02000009 RID: 9
	public class WorkerTypeSpec : ComponentSpec, IEquatable<WorkerTypeSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021D8 File Offset: 0x000003D8
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WorkerTypeSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021E4 File Offset: 0x000003E4
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000021EC File Offset: 0x000003EC
		[Serialize]
		public string Id { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021F5 File Offset: 0x000003F5
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000021FD File Offset: 0x000003FD
		[Serialize]
		public ImmutableArray<string> BackwardCompatibleIds { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002206 File Offset: 0x00000406
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000220E File Offset: 0x0000040E
		[Serialize("DisplayNameLocKey")]
		public LocalizedText DisplayName { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002217 File Offset: 0x00000417
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000221F File Offset: 0x0000041F
		[Serialize("WorkerOnlyTextLocKey")]
		public LocalizedText WorkerOnlyText { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002228 File Offset: 0x00000428
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002230 File Offset: 0x00000430
		[Serialize]
		public bool IgnoresWorkingHours { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002239 File Offset: 0x00000439
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002241 File Offset: 0x00000441
		[Serialize]
		private string DisplayNameLocKey { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000224A File Offset: 0x0000044A
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002252 File Offset: 0x00000452
		[Serialize]
		private string WorkerOnlyTextLocKey { get; set; }

		// Token: 0x0600001C RID: 28 RVA: 0x0000225C File Offset: 0x0000045C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WorkerTypeSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022A8 File Offset: 0x000004A8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", BackwardCompatibleIds = ");
			builder.Append(this.BackwardCompatibleIds.ToString());
			builder.Append(", DisplayName = ");
			builder.Append(this.DisplayName);
			builder.Append(", WorkerOnlyText = ");
			builder.Append(this.WorkerOnlyText);
			builder.Append(", IgnoresWorkingHours = ");
			builder.Append(this.IgnoresWorkingHours.ToString());
			return true;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002364 File Offset: 0x00000564
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WorkerTypeSpec left, WorkerTypeSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002370 File Offset: 0x00000570
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WorkerTypeSpec left, WorkerTypeSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002384 File Offset: 0x00000584
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<BackwardCompatibleIds>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<WorkerOnlyText>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IgnoresWorkingHours>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayNameLocKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WorkerOnlyTextLocKey>k__BackingField);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002438 File Offset: 0x00000638
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WorkerTypeSpec);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002446 File Offset: 0x00000646
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002450 File Offset: 0x00000650
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WorkerTypeSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<BackwardCompatibleIds>k__BackingField, other.<BackwardCompatibleIds>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<WorkerOnlyText>k__BackingField, other.<WorkerOnlyText>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IgnoresWorkingHours>k__BackingField, other.<IgnoresWorkingHours>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayNameLocKey>k__BackingField, other.<DisplayNameLocKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<WorkerOnlyTextLocKey>k__BackingField, other.<WorkerOnlyTextLocKey>k__BackingField));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002528 File Offset: 0x00000728
		[CompilerGenerated]
		protected WorkerTypeSpec([Nullable(1)] WorkerTypeSpec original) : base(original)
		{
			this.Id = original.<Id>k__BackingField;
			this.BackwardCompatibleIds = original.<BackwardCompatibleIds>k__BackingField;
			this.DisplayName = original.<DisplayName>k__BackingField;
			this.WorkerOnlyText = original.<WorkerOnlyText>k__BackingField;
			this.IgnoresWorkingHours = original.<IgnoresWorkingHours>k__BackingField;
			this.DisplayNameLocKey = original.<DisplayNameLocKey>k__BackingField;
			this.WorkerOnlyTextLocKey = original.<WorkerOnlyTextLocKey>k__BackingField;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002590 File Offset: 0x00000790
		public WorkerTypeSpec()
		{
		}
	}
}
