using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.LocalizationSerialization;

namespace Timberborn.TutorialSystem
{
	// Token: 0x02000019 RID: 25
	public class TutorialStageSpec : ComponentSpec, IEquatable<TutorialStageSpec>
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000300F File Offset: 0x0000120F
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(TutorialStageSpec);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000301B File Offset: 0x0000121B
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00003023 File Offset: 0x00001223
		[Serialize]
		public string Id { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000302C File Offset: 0x0000122C
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003034 File Offset: 0x00001234
		[Serialize("IntroLocKey")]
		public LocalizedText Intro { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000303D File Offset: 0x0000123D
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003045 File Offset: 0x00001245
		[Serialize]
		public string IntroLocKey { get; set; }

		// Token: 0x06000079 RID: 121 RVA: 0x00003050 File Offset: 0x00001250
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TutorialStageSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000309C File Offset: 0x0000129C
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
			builder.Append(", Intro = ");
			builder.Append(this.Intro);
			builder.Append(", IntroLocKey = ");
			builder.Append(this.IntroLocKey);
			return true;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000310A File Offset: 0x0000130A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TutorialStageSpec left, TutorialStageSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003116 File Offset: 0x00001316
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TutorialStageSpec left, TutorialStageSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000312C File Offset: 0x0000132C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<Intro>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<IntroLocKey>k__BackingField);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003184 File Offset: 0x00001384
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TutorialStageSpec);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002197 File Offset: 0x00000397
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003194 File Offset: 0x00001394
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TutorialStageSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<Intro>k__BackingField, other.<Intro>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<IntroLocKey>k__BackingField, other.<IntroLocKey>k__BackingField));
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003200 File Offset: 0x00001400
		[CompilerGenerated]
		protected TutorialStageSpec([Nullable(1)] TutorialStageSpec original) : base(original)
		{
			this.Id = original.<Id>k__BackingField;
			this.Intro = original.<Intro>k__BackingField;
			this.IntroLocKey = original.<IntroLocKey>k__BackingField;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000021C0 File Offset: 0x000003C0
		public TutorialStageSpec()
		{
		}
	}
}
