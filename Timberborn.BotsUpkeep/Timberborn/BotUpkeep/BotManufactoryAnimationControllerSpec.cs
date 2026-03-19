using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BotUpkeep
{
	// Token: 0x02000009 RID: 9
	public class BotManufactoryAnimationControllerSpec : ComponentSpec, IEquatable<BotManufactoryAnimationControllerSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002542 File Offset: 0x00000742
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BotManufactoryAnimationControllerSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000254E File Offset: 0x0000074E
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002556 File Offset: 0x00000756
		[Serialize]
		public float AssemblyDuration { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000255F File Offset: 0x0000075F
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002567 File Offset: 0x00000767
		[Serialize]
		public float RingRotationSpeed { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002570 File Offset: 0x00000770
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002578 File Offset: 0x00000778
		[Serialize]
		public float DrillRotationSpeed { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002581 File Offset: 0x00000781
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002589 File Offset: 0x00000789
		[Serialize]
		public string RingName { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002592 File Offset: 0x00000792
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000259A File Offset: 0x0000079A
		[Serialize]
		public string DrillName { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000025A3 File Offset: 0x000007A3
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000025AB File Offset: 0x000007AB
		[Serialize]
		public ImmutableArray<string> AttachmentIds { get; set; }

		// Token: 0x06000026 RID: 38 RVA: 0x000025B4 File Offset: 0x000007B4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BotManufactoryAnimationControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002600 File Offset: 0x00000800
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("AssemblyDuration = ");
			builder.Append(this.AssemblyDuration.ToString());
			builder.Append(", RingRotationSpeed = ");
			builder.Append(this.RingRotationSpeed.ToString());
			builder.Append(", DrillRotationSpeed = ");
			builder.Append(this.DrillRotationSpeed.ToString());
			builder.Append(", RingName = ");
			builder.Append(this.RingName);
			builder.Append(", DrillName = ");
			builder.Append(this.DrillName);
			builder.Append(", AttachmentIds = ");
			builder.Append(this.AttachmentIds.ToString());
			return true;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026F1 File Offset: 0x000008F1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BotManufactoryAnimationControllerSpec left, BotManufactoryAnimationControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026FD File Offset: 0x000008FD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BotManufactoryAnimationControllerSpec left, BotManufactoryAnimationControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002714 File Offset: 0x00000914
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<AssemblyDuration>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<RingRotationSpeed>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DrillRotationSpeed>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RingName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DrillName>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<AttachmentIds>k__BackingField);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000027B1 File Offset: 0x000009B1
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BotManufactoryAnimationControllerSpec);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000027BF File Offset: 0x000009BF
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000027C8 File Offset: 0x000009C8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BotManufactoryAnimationControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<AssemblyDuration>k__BackingField, other.<AssemblyDuration>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<RingRotationSpeed>k__BackingField, other.<RingRotationSpeed>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DrillRotationSpeed>k__BackingField, other.<DrillRotationSpeed>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RingName>k__BackingField, other.<RingName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DrillName>k__BackingField, other.<DrillName>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<AttachmentIds>k__BackingField, other.<AttachmentIds>k__BackingField));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002884 File Offset: 0x00000A84
		[CompilerGenerated]
		protected BotManufactoryAnimationControllerSpec([Nullable(1)] BotManufactoryAnimationControllerSpec original) : base(original)
		{
			this.AssemblyDuration = original.<AssemblyDuration>k__BackingField;
			this.RingRotationSpeed = original.<RingRotationSpeed>k__BackingField;
			this.DrillRotationSpeed = original.<DrillRotationSpeed>k__BackingField;
			this.RingName = original.<RingName>k__BackingField;
			this.DrillName = original.<DrillName>k__BackingField;
			this.AttachmentIds = original.<AttachmentIds>k__BackingField;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028E0 File Offset: 0x00000AE0
		public BotManufactoryAnimationControllerSpec()
		{
		}
	}
}
