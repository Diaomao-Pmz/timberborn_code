using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000029 RID: 41
	public class WorkplaceSpec : ComponentSpec, IEquatable<WorkplaceSpec>
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000046B2 File Offset: 0x000028B2
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WorkplaceSpec);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000046BE File Offset: 0x000028BE
		// (set) Token: 0x06000132 RID: 306 RVA: 0x000046C6 File Offset: 0x000028C6
		[Serialize]
		public int MaxWorkers { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000046CF File Offset: 0x000028CF
		// (set) Token: 0x06000134 RID: 308 RVA: 0x000046D7 File Offset: 0x000028D7
		[Serialize]
		public int DefaultWorkers { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000046E0 File Offset: 0x000028E0
		// (set) Token: 0x06000136 RID: 310 RVA: 0x000046E8 File Offset: 0x000028E8
		[Serialize]
		public string DefaultWorkerType { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000046F1 File Offset: 0x000028F1
		// (set) Token: 0x06000138 RID: 312 RVA: 0x000046F9 File Offset: 0x000028F9
		[Serialize]
		public bool DisallowOtherWorkerTypes { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00004702 File Offset: 0x00002902
		// (set) Token: 0x0600013A RID: 314 RVA: 0x0000470A File Offset: 0x0000290A
		[Serialize]
		public ImmutableArray<WorkerTypeUnlockCost> WorkerTypeUnlockCosts { get; set; }

		// Token: 0x0600013B RID: 315 RVA: 0x00004714 File Offset: 0x00002914
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WorkplaceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004760 File Offset: 0x00002960
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MaxWorkers = ");
			builder.Append(this.MaxWorkers.ToString());
			builder.Append(", DefaultWorkers = ");
			builder.Append(this.DefaultWorkers.ToString());
			builder.Append(", DefaultWorkerType = ");
			builder.Append(this.DefaultWorkerType);
			builder.Append(", DisallowOtherWorkerTypes = ");
			builder.Append(this.DisallowOtherWorkerTypes.ToString());
			builder.Append(", WorkerTypeUnlockCosts = ");
			builder.Append(this.WorkerTypeUnlockCosts.ToString());
			return true;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004838 File Offset: 0x00002A38
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WorkplaceSpec left, WorkplaceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004844 File Offset: 0x00002A44
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WorkplaceSpec left, WorkplaceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004858 File Offset: 0x00002A58
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxWorkers>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<DefaultWorkers>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DefaultWorkerType>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<DisallowOtherWorkerTypes>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<WorkerTypeUnlockCost>>.Default.GetHashCode(this.<WorkerTypeUnlockCosts>k__BackingField);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000048DE File Offset: 0x00002ADE
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WorkplaceSpec);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00002511 File Offset: 0x00000711
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000048EC File Offset: 0x00002AEC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WorkplaceSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<MaxWorkers>k__BackingField, other.<MaxWorkers>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<DefaultWorkers>k__BackingField, other.<DefaultWorkers>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DefaultWorkerType>k__BackingField, other.<DefaultWorkerType>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<DisallowOtherWorkerTypes>k__BackingField, other.<DisallowOtherWorkerTypes>k__BackingField) && EqualityComparer<ImmutableArray<WorkerTypeUnlockCost>>.Default.Equals(this.<WorkerTypeUnlockCosts>k__BackingField, other.<WorkerTypeUnlockCosts>k__BackingField));
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000498C File Offset: 0x00002B8C
		[CompilerGenerated]
		protected WorkplaceSpec([Nullable(1)] WorkplaceSpec original) : base(original)
		{
			this.MaxWorkers = original.<MaxWorkers>k__BackingField;
			this.DefaultWorkers = original.<DefaultWorkers>k__BackingField;
			this.DefaultWorkerType = original.<DefaultWorkerType>k__BackingField;
			this.DisallowOtherWorkerTypes = original.<DisallowOtherWorkerTypes>k__BackingField;
			this.WorkerTypeUnlockCosts = original.<WorkerTypeUnlockCosts>k__BackingField;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000049DC File Offset: 0x00002BDC
		public WorkplaceSpec()
		{
			this.MaxWorkers = 1;
			this.DefaultWorkers = 1;
			this.DefaultWorkerType = "Beaver";
			base..ctor();
		}
	}
}
