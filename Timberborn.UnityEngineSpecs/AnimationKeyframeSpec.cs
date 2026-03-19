using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.UnityEngineSpecs
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class AnimationKeyframeSpec : IEquatable<AnimationKeyframeSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002405 File Offset: 0x00000605
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(AnimationKeyframeSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002411 File Offset: 0x00000611
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002419 File Offset: 0x00000619
		[Serialize]
		public float Time { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002422 File Offset: 0x00000622
		// (set) Token: 0x06000021 RID: 33 RVA: 0x0000242A File Offset: 0x0000062A
		[Serialize]
		public float Value { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002433 File Offset: 0x00000633
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000243B File Offset: 0x0000063B
		[Serialize]
		public float InTangent { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002444 File Offset: 0x00000644
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000244C File Offset: 0x0000064C
		[Serialize]
		public float OutTangent { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002455 File Offset: 0x00000655
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000245D File Offset: 0x0000065D
		[Serialize]
		public int WeightedMode { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002466 File Offset: 0x00000666
		// (set) Token: 0x06000029 RID: 41 RVA: 0x0000246E File Offset: 0x0000066E
		[Serialize]
		public float InWeight { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002477 File Offset: 0x00000677
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000247F File Offset: 0x0000067F
		[Serialize]
		public float OutWeight { get; set; }

		// Token: 0x0600002C RID: 44 RVA: 0x00002488 File Offset: 0x00000688
		[NullableContext(0)]
		public static AnimationKeyframeSpec FromKeyframe(Keyframe keyframe)
		{
			return new AnimationKeyframeSpec
			{
				Time = keyframe.time,
				Value = keyframe.value,
				InTangent = keyframe.inTangent,
				OutTangent = keyframe.outTangent,
				WeightedMode = keyframe.weightedMode,
				InWeight = keyframe.inWeight,
				OutWeight = keyframe.outWeight
			};
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024F8 File Offset: 0x000006F8
		public Keyframe ToKeyframe()
		{
			Keyframe result = default(Keyframe);
			result.time = this.Time;
			result.value = this.Value;
			result.inTangent = this.InTangent;
			result.outTangent = this.OutTangent;
			result.weightedMode = this.WeightedMode;
			result.inWeight = this.InWeight;
			result.outWeight = this.OutWeight;
			return result;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000256C File Offset: 0x0000076C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AnimationKeyframeSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025B8 File Offset: 0x000007B8
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Time = ");
			builder.Append(this.Time.ToString());
			builder.Append(", Value = ");
			builder.Append(this.Value.ToString());
			builder.Append(", InTangent = ");
			builder.Append(this.InTangent.ToString());
			builder.Append(", OutTangent = ");
			builder.Append(this.OutTangent.ToString());
			builder.Append(", WeightedMode = ");
			builder.Append(this.WeightedMode.ToString());
			builder.Append(", InWeight = ");
			builder.Append(this.InWeight.ToString());
			builder.Append(", OutWeight = ");
			builder.Append(this.OutWeight.ToString());
			return true;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026DC File Offset: 0x000008DC
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(AnimationKeyframeSpec left, AnimationKeyframeSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026E8 File Offset: 0x000008E8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(AnimationKeyframeSpec left, AnimationKeyframeSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000026FC File Offset: 0x000008FC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Time>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Value>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<InTangent>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<OutTangent>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<WeightedMode>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<InWeight>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<OutWeight>k__BackingField);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000027BA File Offset: 0x000009BA
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AnimationKeyframeSpec);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027C8 File Offset: 0x000009C8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(AnimationKeyframeSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<float>.Default.Equals(this.<Time>k__BackingField, other.<Time>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<Value>k__BackingField, other.<Value>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<InTangent>k__BackingField, other.<InTangent>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<OutTangent>k__BackingField, other.<OutTangent>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<WeightedMode>k__BackingField, other.<WeightedMode>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<InWeight>k__BackingField, other.<InWeight>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<OutWeight>k__BackingField, other.<OutWeight>k__BackingField));
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000028B0 File Offset: 0x00000AB0
		[CompilerGenerated]
		protected AnimationKeyframeSpec(AnimationKeyframeSpec original)
		{
			this.Time = original.<Time>k__BackingField;
			this.Value = original.<Value>k__BackingField;
			this.InTangent = original.<InTangent>k__BackingField;
			this.OutTangent = original.<OutTangent>k__BackingField;
			this.WeightedMode = original.<WeightedMode>k__BackingField;
			this.InWeight = original.<InWeight>k__BackingField;
			this.OutWeight = original.<OutWeight>k__BackingField;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000020F6 File Offset: 0x000002F6
		public AnimationKeyframeSpec()
		{
		}
	}
}
