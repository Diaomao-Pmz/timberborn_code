using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine.Audio;

namespace Timberborn.SoundSystem
{
	// Token: 0x0200000B RID: 11
	public class AudioMixerGroupRetrieverSpec : ComponentSpec, IEquatable<AudioMixerGroupRetrieverSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002347 File Offset: 0x00000547
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(AudioMixerGroupRetrieverSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002353 File Offset: 0x00000553
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000235B File Offset: 0x0000055B
		[Serialize]
		public AssetRef<AudioMixer> AudioMixer { get; set; }

		// Token: 0x0600001C RID: 28 RVA: 0x00002364 File Offset: 0x00000564
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AudioMixerGroupRetrieverSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023B0 File Offset: 0x000005B0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("AudioMixer = ");
			builder.Append(this.AudioMixer);
			return true;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023E1 File Offset: 0x000005E1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(AudioMixerGroupRetrieverSpec left, AudioMixerGroupRetrieverSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023ED File Offset: 0x000005ED
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(AudioMixerGroupRetrieverSpec left, AudioMixerGroupRetrieverSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002401 File Offset: 0x00000601
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<AssetRef<UnityEngine.Audio.AudioMixer>>.Default.GetHashCode(this.<AudioMixer>k__BackingField);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002420 File Offset: 0x00000620
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AudioMixerGroupRetrieverSpec);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000242E File Offset: 0x0000062E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002437 File Offset: 0x00000637
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(AudioMixerGroupRetrieverSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AssetRef<UnityEngine.Audio.AudioMixer>>.Default.Equals(this.<AudioMixer>k__BackingField, other.<AudioMixer>k__BackingField));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002468 File Offset: 0x00000668
		[CompilerGenerated]
		protected AudioMixerGroupRetrieverSpec([Nullable(1)] AudioMixerGroupRetrieverSpec original) : base(original)
		{
			this.AudioMixer = original.<AudioMixer>k__BackingField;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000247D File Offset: 0x0000067D
		public AudioMixerGroupRetrieverSpec()
		{
		}
	}
}
