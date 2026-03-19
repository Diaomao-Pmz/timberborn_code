using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WorkerOutfitSystem
{
	// Token: 0x02000013 RID: 19
	public class WorkplaceWorkerOutfitSpec : ComponentSpec, IEquatable<WorkplaceWorkerOutfitSpec>
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003106 File Offset: 0x00001306
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WorkplaceWorkerOutfitSpec);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003112 File Offset: 0x00001312
		// (set) Token: 0x0600006E RID: 110 RVA: 0x0000311A File Offset: 0x0000131A
		[Serialize]
		public string WorkerOutfit { get; set; }

		// Token: 0x0600006F RID: 111 RVA: 0x00003124 File Offset: 0x00001324
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WorkplaceWorkerOutfitSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003170 File Offset: 0x00001370
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("WorkerOutfit = ");
			builder.Append(this.WorkerOutfit);
			return true;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000031A1 File Offset: 0x000013A1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WorkplaceWorkerOutfitSpec left, WorkplaceWorkerOutfitSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000031AD File Offset: 0x000013AD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WorkplaceWorkerOutfitSpec left, WorkplaceWorkerOutfitSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000031C1 File Offset: 0x000013C1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WorkerOutfit>k__BackingField);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000031E0 File Offset: 0x000013E0
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WorkplaceWorkerOutfitSpec);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000027AB File Offset: 0x000009AB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000031EE File Offset: 0x000013EE
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WorkplaceWorkerOutfitSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<WorkerOutfit>k__BackingField, other.<WorkerOutfit>k__BackingField));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000321F File Offset: 0x0000141F
		[CompilerGenerated]
		protected WorkplaceWorkerOutfitSpec([Nullable(1)] WorkplaceWorkerOutfitSpec original) : base(original)
		{
			this.WorkerOutfit = original.<WorkerOutfit>k__BackingField;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000027FA File Offset: 0x000009FA
		public WorkplaceWorkerOutfitSpec()
		{
		}
	}
}
