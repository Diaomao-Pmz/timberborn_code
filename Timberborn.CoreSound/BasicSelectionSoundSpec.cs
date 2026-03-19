using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.CoreSound
{
	// Token: 0x02000008 RID: 8
	public class BasicSelectionSoundSpec : ComponentSpec, IEquatable<BasicSelectionSoundSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021A8 File Offset: 0x000003A8
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BasicSelectionSoundSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021B4 File Offset: 0x000003B4
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000021BC File Offset: 0x000003BC
		[Serialize]
		public string SoundName { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021C5 File Offset: 0x000003C5
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000021CD File Offset: 0x000003CD
		[Serialize]
		public string AlternativeSoundName { get; set; }

		// Token: 0x06000011 RID: 17 RVA: 0x000021D8 File Offset: 0x000003D8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BasicSelectionSoundSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002224 File Offset: 0x00000424
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("SoundName = ");
			builder.Append(this.SoundName);
			builder.Append(", AlternativeSoundName = ");
			builder.Append(this.AlternativeSoundName);
			return true;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002279 File Offset: 0x00000479
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BasicSelectionSoundSpec left, BasicSelectionSoundSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002285 File Offset: 0x00000485
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BasicSelectionSoundSpec left, BasicSelectionSoundSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002299 File Offset: 0x00000499
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SoundName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AlternativeSoundName>k__BackingField);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022CF File Offset: 0x000004CF
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BasicSelectionSoundSpec);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022DD File Offset: 0x000004DD
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022E8 File Offset: 0x000004E8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BasicSelectionSoundSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<SoundName>k__BackingField, other.<SoundName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AlternativeSoundName>k__BackingField, other.<AlternativeSoundName>k__BackingField));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000233C File Offset: 0x0000053C
		[CompilerGenerated]
		protected BasicSelectionSoundSpec([Nullable(1)] BasicSelectionSoundSpec original) : base(original)
		{
			this.SoundName = original.<SoundName>k__BackingField;
			this.AlternativeSoundName = original.<AlternativeSoundName>k__BackingField;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000235D File Offset: 0x0000055D
		public BasicSelectionSoundSpec()
		{
		}
	}
}
