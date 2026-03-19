using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.UnityEngineSpecs
{
	// Token: 0x02000007 RID: 7
	public class AnimationCurveSpec : IEquatable<AnimationCurveSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(AnimationCurveSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210A File Offset: 0x0000030A
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002112 File Offset: 0x00000312
		[Serialize]
		public ImmutableArray<AnimationKeyframeSpec> Keys { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000211B File Offset: 0x0000031B
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002123 File Offset: 0x00000323
		[Serialize]
		public WrapMode PreWrapMode { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000212C File Offset: 0x0000032C
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002134 File Offset: 0x00000334
		[Serialize]
		public WrapMode PostWrapMode { get; set; }

		// Token: 0x0600000E RID: 14 RVA: 0x00002140 File Offset: 0x00000340
		public static AnimationCurveSpec FromAnimationCurve(AnimationCurve curve)
		{
			return new AnimationCurveSpec
			{
				Keys = curve.keys.Select(new Func<Keyframe, AnimationKeyframeSpec>(AnimationKeyframeSpec.FromKeyframe)).ToImmutableArray<AnimationKeyframeSpec>(),
				PreWrapMode = curve.preWrapMode,
				PostWrapMode = curve.postWrapMode
			};
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000218C File Offset: 0x0000038C
		public AnimationCurve ToAnimationCurve()
		{
			return new AnimationCurve((from key in this.Keys
			select key.ToKeyframe()).ToArray<Keyframe>())
			{
				preWrapMode = this.PreWrapMode,
				postWrapMode = this.PostWrapMode
			};
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021E8 File Offset: 0x000003E8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AnimationCurveSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002234 File Offset: 0x00000434
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Keys = ");
			builder.Append(this.Keys.ToString());
			builder.Append(", PreWrapMode = ");
			builder.Append(this.PreWrapMode.ToString());
			builder.Append(", PostWrapMode = ");
			builder.Append(this.PostWrapMode.ToString());
			return true;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022BC File Offset: 0x000004BC
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(AnimationCurveSpec left, AnimationCurveSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022C8 File Offset: 0x000004C8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(AnimationCurveSpec left, AnimationCurveSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022DC File Offset: 0x000004DC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ImmutableArray<AnimationKeyframeSpec>>.Default.GetHashCode(this.<Keys>k__BackingField)) * -1521134295 + EqualityComparer<WrapMode>.Default.GetHashCode(this.<PreWrapMode>k__BackingField)) * -1521134295 + EqualityComparer<WrapMode>.Default.GetHashCode(this.<PostWrapMode>k__BackingField);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000233E File Offset: 0x0000053E
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AnimationCurveSpec);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000234C File Offset: 0x0000054C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(AnimationCurveSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ImmutableArray<AnimationKeyframeSpec>>.Default.Equals(this.<Keys>k__BackingField, other.<Keys>k__BackingField) && EqualityComparer<WrapMode>.Default.Equals(this.<PreWrapMode>k__BackingField, other.<PreWrapMode>k__BackingField) && EqualityComparer<WrapMode>.Default.Equals(this.<PostWrapMode>k__BackingField, other.<PostWrapMode>k__BackingField));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023C5 File Offset: 0x000005C5
		[CompilerGenerated]
		protected AnimationCurveSpec([Nullable(1)] AnimationCurveSpec original)
		{
			this.Keys = original.<Keys>k__BackingField;
			this.PreWrapMode = original.<PreWrapMode>k__BackingField;
			this.PostWrapMode = original.<PostWrapMode>k__BackingField;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000020F6 File Offset: 0x000002F6
		public AnimationCurveSpec()
		{
		}
	}
}
