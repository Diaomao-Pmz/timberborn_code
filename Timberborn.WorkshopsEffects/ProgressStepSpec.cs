using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x0200000F RID: 15
	public class ProgressStepSpec : IEquatable<ProgressStepSpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002C22 File Offset: 0x00000E22
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ProgressStepSpec);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002C2E File Offset: 0x00000E2E
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002C36 File Offset: 0x00000E36
		[Serialize]
		public float Threshold { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002C3F File Offset: 0x00000E3F
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002C47 File Offset: 0x00000E47
		[Serialize]
		public ImmutableArray<string> ModelNames { get; set; }

		// Token: 0x0600005F RID: 95 RVA: 0x00002C50 File Offset: 0x00000E50
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ProgressStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002C9C File Offset: 0x00000E9C
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Threshold = ");
			builder.Append(this.Threshold.ToString());
			builder.Append(", ModelNames = ");
			builder.Append(this.ModelNames.ToString());
			return true;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002CFD File Offset: 0x00000EFD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ProgressStepSpec left, ProgressStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002D09 File Offset: 0x00000F09
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ProgressStepSpec left, ProgressStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002D1D File Offset: 0x00000F1D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Threshold>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<ModelNames>k__BackingField);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002D5D File Offset: 0x00000F5D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ProgressStepSpec);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002D6C File Offset: 0x00000F6C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ProgressStepSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<float>.Default.Equals(this.<Threshold>k__BackingField, other.<Threshold>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<ModelNames>k__BackingField, other.<ModelNames>k__BackingField));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002DCD File Offset: 0x00000FCD
		[CompilerGenerated]
		protected ProgressStepSpec([Nullable(1)] ProgressStepSpec original)
		{
			this.Threshold = original.<Threshold>k__BackingField;
			this.ModelNames = original.<ModelNames>k__BackingField;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000020F8 File Offset: 0x000002F8
		public ProgressStepSpec()
		{
		}
	}
}
