using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.CoreSound
{
	// Token: 0x0200000B RID: 11
	public class CoreSoundSpec : ComponentSpec, IEquatable<CoreSoundSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002528 File Offset: 0x00000728
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(CoreSoundSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002534 File Offset: 0x00000734
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000253C File Offset: 0x0000073C
		[Serialize]
		public int MaxVerticalListenerPositionAboveGround { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002545 File Offset: 0x00000745
		// (set) Token: 0x06000029 RID: 41 RVA: 0x0000254D File Offset: 0x0000074D
		[Serialize]
		public float MinBuildingFadeDistance { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002556 File Offset: 0x00000756
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000255E File Offset: 0x0000075E
		[Serialize]
		public float MaxBuildingFadeDistance { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002567 File Offset: 0x00000767
		// (set) Token: 0x0600002D RID: 45 RVA: 0x0000256F File Offset: 0x0000076F
		[Serialize]
		public string WindAmbientKey { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002578 File Offset: 0x00000778
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002580 File Offset: 0x00000780
		[Serialize]
		public float MinAmbientFade { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002589 File Offset: 0x00000789
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002591 File Offset: 0x00000791
		[Serialize]
		public float MaxAmbientFade { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000259A File Offset: 0x0000079A
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000025A2 File Offset: 0x000007A2
		[Serialize]
		public float MinWindFade { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000025AB File Offset: 0x000007AB
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000025B3 File Offset: 0x000007B3
		[Serialize]
		public float MaxWindFade { get; set; }

		// Token: 0x06000036 RID: 54 RVA: 0x000025BC File Offset: 0x000007BC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CoreSoundSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002608 File Offset: 0x00000808
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MaxVerticalListenerPositionAboveGround = ");
			builder.Append(this.MaxVerticalListenerPositionAboveGround.ToString());
			builder.Append(", MinBuildingFadeDistance = ");
			builder.Append(this.MinBuildingFadeDistance.ToString());
			builder.Append(", MaxBuildingFadeDistance = ");
			builder.Append(this.MaxBuildingFadeDistance.ToString());
			builder.Append(", WindAmbientKey = ");
			builder.Append(this.WindAmbientKey);
			builder.Append(", MinAmbientFade = ");
			builder.Append(this.MinAmbientFade.ToString());
			builder.Append(", MaxAmbientFade = ");
			builder.Append(this.MaxAmbientFade.ToString());
			builder.Append(", MinWindFade = ");
			builder.Append(this.MinWindFade.ToString());
			builder.Append(", MaxWindFade = ");
			builder.Append(this.MaxWindFade.ToString());
			return true;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002755 File Offset: 0x00000955
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CoreSoundSpec left, CoreSoundSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002761 File Offset: 0x00000961
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CoreSoundSpec left, CoreSoundSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002778 File Offset: 0x00000978
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((((base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxVerticalListenerPositionAboveGround>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinBuildingFadeDistance>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxBuildingFadeDistance>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WindAmbientKey>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinAmbientFade>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxAmbientFade>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinWindFade>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxWindFade>k__BackingField);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002843 File Offset: 0x00000A43
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CoreSoundSpec);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000022DD File Offset: 0x000004DD
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002854 File Offset: 0x00000A54
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CoreSoundSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<MaxVerticalListenerPositionAboveGround>k__BackingField, other.<MaxVerticalListenerPositionAboveGround>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MinBuildingFadeDistance>k__BackingField, other.<MinBuildingFadeDistance>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxBuildingFadeDistance>k__BackingField, other.<MaxBuildingFadeDistance>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<WindAmbientKey>k__BackingField, other.<WindAmbientKey>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MinAmbientFade>k__BackingField, other.<MinAmbientFade>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxAmbientFade>k__BackingField, other.<MaxAmbientFade>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MinWindFade>k__BackingField, other.<MinWindFade>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxWindFade>k__BackingField, other.<MaxWindFade>k__BackingField));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002944 File Offset: 0x00000B44
		[CompilerGenerated]
		protected CoreSoundSpec([Nullable(1)] CoreSoundSpec original) : base(original)
		{
			this.MaxVerticalListenerPositionAboveGround = original.<MaxVerticalListenerPositionAboveGround>k__BackingField;
			this.MinBuildingFadeDistance = original.<MinBuildingFadeDistance>k__BackingField;
			this.MaxBuildingFadeDistance = original.<MaxBuildingFadeDistance>k__BackingField;
			this.WindAmbientKey = original.<WindAmbientKey>k__BackingField;
			this.MinAmbientFade = original.<MinAmbientFade>k__BackingField;
			this.MaxAmbientFade = original.<MaxAmbientFade>k__BackingField;
			this.MinWindFade = original.<MinWindFade>k__BackingField;
			this.MaxWindFade = original.<MaxWindFade>k__BackingField;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000235D File Offset: 0x0000055D
		public CoreSoundSpec()
		{
		}
	}
}
