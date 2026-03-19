using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x02000018 RID: 24
	public class TimbermeshAnimatorControllerSpec : ComponentSpec, IEquatable<TimbermeshAnimatorControllerSpec>
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000038AA File Offset: 0x00001AAA
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(TimbermeshAnimatorControllerSpec);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000038B6 File Offset: 0x00001AB6
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000038BE File Offset: 0x00001ABE
		[Serialize]
		public ImmutableArray<string> BoolParameters { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000038C7 File Offset: 0x00001AC7
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000038CF File Offset: 0x00001ACF
		[Serialize]
		public ImmutableArray<string> FloatParameters { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000038D8 File Offset: 0x00001AD8
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x000038E0 File Offset: 0x00001AE0
		[Serialize]
		public ImmutableArray<AnimatorState> AnimatorStates { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000038EC File Offset: 0x00001AEC
		public IEnumerable<string> UsedBoolParameters
		{
			get
			{
				return (from c in this.AnimatorStates.SelectMany((AnimatorState s) => s.Conditions)
				select c.ParameterName).Distinct<string>();
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003951 File Offset: 0x00001B51
		public IEnumerable<string> UsedFloatParameters
		{
			get
			{
				return (from s in this.AnimatorStates
				select s.SpeedModifier).Distinct<string>();
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003982 File Offset: 0x00001B82
		public IEnumerable<string> AnimationNames
		{
			get
			{
				return (from s in this.AnimatorStates
				select s.AnimationName).Distinct<string>();
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000039B4 File Offset: 0x00001BB4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TimbermeshAnimatorControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003A00 File Offset: 0x00001C00
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("BoolParameters = ");
			builder.Append(this.BoolParameters.ToString());
			builder.Append(", FloatParameters = ");
			builder.Append(this.FloatParameters.ToString());
			builder.Append(", AnimatorStates = ");
			builder.Append(this.AnimatorStates.ToString());
			builder.Append(", UsedBoolParameters = ");
			builder.Append(this.UsedBoolParameters);
			builder.Append(", UsedFloatParameters = ");
			builder.Append(this.UsedFloatParameters);
			builder.Append(", AnimationNames = ");
			builder.Append(this.AnimationNames);
			return true;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003AE3 File Offset: 0x00001CE3
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TimbermeshAnimatorControllerSpec left, TimbermeshAnimatorControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003AEF File Offset: 0x00001CEF
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TimbermeshAnimatorControllerSpec left, TimbermeshAnimatorControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003B04 File Offset: 0x00001D04
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<BoolParameters>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<FloatParameters>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<AnimatorState>>.Default.GetHashCode(this.<AnimatorStates>k__BackingField);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003B5C File Offset: 0x00001D5C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TimbermeshAnimatorControllerSpec);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003B6A File Offset: 0x00001D6A
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003B74 File Offset: 0x00001D74
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TimbermeshAnimatorControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<BoolParameters>k__BackingField, other.<BoolParameters>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<FloatParameters>k__BackingField, other.<FloatParameters>k__BackingField) && EqualityComparer<ImmutableArray<AnimatorState>>.Default.Equals(this.<AnimatorStates>k__BackingField, other.<AnimatorStates>k__BackingField));
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003BE0 File Offset: 0x00001DE0
		[CompilerGenerated]
		protected TimbermeshAnimatorControllerSpec([Nullable(1)] TimbermeshAnimatorControllerSpec original) : base(original)
		{
			this.BoolParameters = original.<BoolParameters>k__BackingField;
			this.FloatParameters = original.<FloatParameters>k__BackingField;
			this.AnimatorStates = original.<AnimatorStates>k__BackingField;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003C0D File Offset: 0x00001E0D
		public TimbermeshAnimatorControllerSpec()
		{
		}
	}
}
