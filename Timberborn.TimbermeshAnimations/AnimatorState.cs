using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x0200000A RID: 10
	public class AnimatorState : IEquatable<AnimatorState>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002305 File Offset: 0x00000505
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(AnimatorState);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002311 File Offset: 0x00000511
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002319 File Offset: 0x00000519
		[Serialize]
		public string StateName { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002322 File Offset: 0x00000522
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000232A File Offset: 0x0000052A
		[Serialize]
		public string AnimationName { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002333 File Offset: 0x00000533
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000233B File Offset: 0x0000053B
		[Serialize]
		public float Speed { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002344 File Offset: 0x00000544
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000234C File Offset: 0x0000054C
		[Serialize]
		public string SpeedModifier { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002355 File Offset: 0x00000555
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000235D File Offset: 0x0000055D
		[Serialize]
		public bool Looped { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002366 File Offset: 0x00000566
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000236E File Offset: 0x0000056E
		[Serialize]
		public ImmutableArray<AnimatorStateCondition> Conditions { get; set; }

		// Token: 0x06000020 RID: 32 RVA: 0x00002378 File Offset: 0x00000578
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AnimatorState");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023C4 File Offset: 0x000005C4
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("StateName = ");
			builder.Append(this.StateName);
			builder.Append(", AnimationName = ");
			builder.Append(this.AnimationName);
			builder.Append(", Speed = ");
			builder.Append(this.Speed.ToString());
			builder.Append(", SpeedModifier = ");
			builder.Append(this.SpeedModifier);
			builder.Append(", Looped = ");
			builder.Append(this.Looped.ToString());
			builder.Append(", Conditions = ");
			builder.Append(this.Conditions.ToString());
			return true;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002497 File Offset: 0x00000697
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(AnimatorState left, AnimatorState right)
		{
			return !(left == right);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024A3 File Offset: 0x000006A3
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(AnimatorState left, AnimatorState right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024B8 File Offset: 0x000006B8
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<StateName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AnimationName>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Speed>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SpeedModifier>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Looped>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<AnimatorStateCondition>>.Default.GetHashCode(this.<Conditions>k__BackingField);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000255F File Offset: 0x0000075F
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AnimatorState);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002570 File Offset: 0x00000770
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(AnimatorState other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<StateName>k__BackingField, other.<StateName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AnimationName>k__BackingField, other.<AnimationName>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<Speed>k__BackingField, other.<Speed>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SpeedModifier>k__BackingField, other.<SpeedModifier>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Looped>k__BackingField, other.<Looped>k__BackingField) && EqualityComparer<ImmutableArray<AnimatorStateCondition>>.Default.Equals(this.<Conditions>k__BackingField, other.<Conditions>k__BackingField));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000263C File Offset: 0x0000083C
		[CompilerGenerated]
		protected AnimatorState([Nullable(1)] AnimatorState original)
		{
			this.StateName = original.<StateName>k__BackingField;
			this.AnimationName = original.<AnimationName>k__BackingField;
			this.Speed = original.<Speed>k__BackingField;
			this.SpeedModifier = original.<SpeedModifier>k__BackingField;
			this.Looped = original.<Looped>k__BackingField;
			this.Conditions = original.<Conditions>k__BackingField;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002697 File Offset: 0x00000897
		public AnimatorState()
		{
			this.Speed = 1f;
			this.Looped = true;
			base..ctor();
		}
	}
}
