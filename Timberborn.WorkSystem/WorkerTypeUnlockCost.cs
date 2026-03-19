using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000019 RID: 25
	public class WorkerTypeUnlockCost : IEquatable<WorkerTypeUnlockCost>
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000033C8 File Offset: 0x000015C8
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WorkerTypeUnlockCost);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000033D4 File Offset: 0x000015D4
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000033DC File Offset: 0x000015DC
		[Serialize]
		public string WorkerType { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000033E5 File Offset: 0x000015E5
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000033ED File Offset: 0x000015ED
		[Serialize]
		public int ScienceCost { get; set; }

		// Token: 0x06000094 RID: 148 RVA: 0x000033F8 File Offset: 0x000015F8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WorkerTypeUnlockCost");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003444 File Offset: 0x00001644
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("WorkerType = ");
			builder.Append(this.WorkerType);
			builder.Append(", ScienceCost = ");
			builder.Append(this.ScienceCost.ToString());
			return true;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003497 File Offset: 0x00001697
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WorkerTypeUnlockCost left, WorkerTypeUnlockCost right)
		{
			return !(left == right);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000034A3 File Offset: 0x000016A3
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WorkerTypeUnlockCost left, WorkerTypeUnlockCost right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000034B7 File Offset: 0x000016B7
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WorkerType>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ScienceCost>k__BackingField);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000034F7 File Offset: 0x000016F7
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WorkerTypeUnlockCost);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003508 File Offset: 0x00001708
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WorkerTypeUnlockCost other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<WorkerType>k__BackingField, other.<WorkerType>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ScienceCost>k__BackingField, other.<ScienceCost>k__BackingField));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003569 File Offset: 0x00001769
		[CompilerGenerated]
		protected WorkerTypeUnlockCost([Nullable(1)] WorkerTypeUnlockCost original)
		{
			this.WorkerType = original.<WorkerType>k__BackingField;
			this.ScienceCost = original.<ScienceCost>k__BackingField;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000020F8 File Offset: 0x000002F8
		public WorkerTypeUnlockCost()
		{
		}
	}
}
