using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.LocalizationSerialization;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000046 RID: 70
	public class SpeakerSoundSpec : ComponentSpec, IEquatable<SpeakerSoundSpec>
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00008594 File Offset: 0x00006794
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(SpeakerSoundSpec);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x000085A0 File Offset: 0x000067A0
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x000085A8 File Offset: 0x000067A8
		[Serialize]
		public string SoundId { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x000085B1 File Offset: 0x000067B1
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x000085B9 File Offset: 0x000067B9
		[Serialize("DisplayNameLocKey")]
		public LocalizedText DisplayName { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x000085C2 File Offset: 0x000067C2
		// (set) Token: 0x060002FA RID: 762 RVA: 0x000085CA File Offset: 0x000067CA
		[Serialize]
		private string DisplayNameLocKey { get; set; }

		// Token: 0x060002FB RID: 763 RVA: 0x000085D4 File Offset: 0x000067D4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SpeakerSoundSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00008620 File Offset: 0x00006820
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("SoundId = ");
			builder.Append(this.SoundId);
			builder.Append(", DisplayName = ");
			builder.Append(this.DisplayName);
			return true;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00008675 File Offset: 0x00006875
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SpeakerSoundSpec left, SpeakerSoundSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00008681 File Offset: 0x00006881
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SpeakerSoundSpec left, SpeakerSoundSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00008698 File Offset: 0x00006898
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SoundId>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayNameLocKey>k__BackingField);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000086F0 File Offset: 0x000068F0
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SpeakerSoundSpec);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00008700 File Offset: 0x00006900
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SpeakerSoundSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<SoundId>k__BackingField, other.<SoundId>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayNameLocKey>k__BackingField, other.<DisplayNameLocKey>k__BackingField));
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000876C File Offset: 0x0000696C
		[CompilerGenerated]
		protected SpeakerSoundSpec([Nullable(1)] SpeakerSoundSpec original) : base(original)
		{
			this.SoundId = original.<SoundId>k__BackingField;
			this.DisplayName = original.<DisplayName>k__BackingField;
			this.DisplayNameLocKey = original.<DisplayNameLocKey>k__BackingField;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00002778 File Offset: 0x00000978
		public SpeakerSoundSpec()
		{
		}
	}
}
