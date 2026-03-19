using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WorkerOutfitSystem
{
	// Token: 0x0200000A RID: 10
	public class WorkerOutfitAnimationAttachmentVisibilitySpec : ComponentSpec, IEquatable<WorkerOutfitAnimationAttachmentVisibilitySpec>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000026AB File Offset: 0x000008AB
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WorkerOutfitAnimationAttachmentVisibilitySpec);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000026B7 File Offset: 0x000008B7
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000026BF File Offset: 0x000008BF
		[Serialize]
		public ImmutableArray<WorkerOutfitAnimationAttachmentSpec> WorkerOutfitAnimationAttachments { get; set; }

		// Token: 0x06000028 RID: 40 RVA: 0x000026C8 File Offset: 0x000008C8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WorkerOutfitAnimationAttachmentVisibilitySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002714 File Offset: 0x00000914
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("WorkerOutfitAnimationAttachments = ");
			builder.Append(this.WorkerOutfitAnimationAttachments.ToString());
			return true;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000275E File Offset: 0x0000095E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WorkerOutfitAnimationAttachmentVisibilitySpec left, WorkerOutfitAnimationAttachmentVisibilitySpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000276A File Offset: 0x0000096A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WorkerOutfitAnimationAttachmentVisibilitySpec left, WorkerOutfitAnimationAttachmentVisibilitySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000277E File Offset: 0x0000097E
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<WorkerOutfitAnimationAttachmentSpec>>.Default.GetHashCode(this.<WorkerOutfitAnimationAttachments>k__BackingField);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000279D File Offset: 0x0000099D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WorkerOutfitAnimationAttachmentVisibilitySpec);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000027AB File Offset: 0x000009AB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027B4 File Offset: 0x000009B4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WorkerOutfitAnimationAttachmentVisibilitySpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<WorkerOutfitAnimationAttachmentSpec>>.Default.Equals(this.<WorkerOutfitAnimationAttachments>k__BackingField, other.<WorkerOutfitAnimationAttachments>k__BackingField));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000027E5 File Offset: 0x000009E5
		[CompilerGenerated]
		protected WorkerOutfitAnimationAttachmentVisibilitySpec([Nullable(1)] WorkerOutfitAnimationAttachmentVisibilitySpec original) : base(original)
		{
			this.WorkerOutfitAnimationAttachments = original.<WorkerOutfitAnimationAttachments>k__BackingField;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027FA File Offset: 0x000009FA
		public WorkerOutfitAnimationAttachmentVisibilitySpec()
		{
		}
	}
}
