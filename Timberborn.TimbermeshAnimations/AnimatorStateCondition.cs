using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x0200000B RID: 11
	public class AnimatorStateCondition : IEquatable<AnimatorStateCondition>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000026B1 File Offset: 0x000008B1
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(AnimatorStateCondition);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000026BD File Offset: 0x000008BD
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000026C5 File Offset: 0x000008C5
		[Serialize]
		public string ParameterName { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000026CE File Offset: 0x000008CE
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000026D6 File Offset: 0x000008D6
		[Serialize]
		public bool MustBeTrue { get; set; }

		// Token: 0x0600002F RID: 47 RVA: 0x000026E0 File Offset: 0x000008E0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AnimatorStateCondition");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000272C File Offset: 0x0000092C
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("ParameterName = ");
			builder.Append(this.ParameterName);
			builder.Append(", MustBeTrue = ");
			builder.Append(this.MustBeTrue.ToString());
			return true;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000277F File Offset: 0x0000097F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(AnimatorStateCondition left, AnimatorStateCondition right)
		{
			return !(left == right);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000278B File Offset: 0x0000098B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(AnimatorStateCondition left, AnimatorStateCondition right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000279F File Offset: 0x0000099F
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ParameterName>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<MustBeTrue>k__BackingField);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027DF File Offset: 0x000009DF
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AnimatorStateCondition);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027F0 File Offset: 0x000009F0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(AnimatorStateCondition other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ParameterName>k__BackingField, other.<ParameterName>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<MustBeTrue>k__BackingField, other.<MustBeTrue>k__BackingField));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002851 File Offset: 0x00000A51
		[CompilerGenerated]
		protected AnimatorStateCondition([Nullable(1)] AnimatorStateCondition original)
		{
			this.ParameterName = original.<ParameterName>k__BackingField;
			this.MustBeTrue = original.<MustBeTrue>k__BackingField;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000020F8 File Offset: 0x000002F8
		public AnimatorStateCondition()
		{
		}
	}
}
