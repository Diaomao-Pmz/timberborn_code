using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WorkerOutfitSystem
{
	// Token: 0x02000008 RID: 8
	public class WorkerOutfitAnimationAttachmentSpec : IEquatable<WorkerOutfitAnimationAttachmentSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000022A1 File Offset: 0x000004A1
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WorkerOutfitAnimationAttachmentSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000022AD File Offset: 0x000004AD
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000022B5 File Offset: 0x000004B5
		[Serialize]
		public string WorkerOutfit { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000022BE File Offset: 0x000004BE
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000022C6 File Offset: 0x000004C6
		[Serialize]
		public ImmutableArray<string> AnimationNames { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022CF File Offset: 0x000004CF
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000022D7 File Offset: 0x000004D7
		[Serialize]
		public ImmutableArray<string> ShowWhenActive { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022E0 File Offset: 0x000004E0
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000022E8 File Offset: 0x000004E8
		[Serialize]
		public ImmutableArray<string> HideWhenActive { get; set; }

		// Token: 0x06000015 RID: 21 RVA: 0x000022F4 File Offset: 0x000004F4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WorkerOutfitAnimationAttachmentSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002340 File Offset: 0x00000540
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("WorkerOutfit = ");
			builder.Append(this.WorkerOutfit);
			builder.Append(", AnimationNames = ");
			builder.Append(this.AnimationNames.ToString());
			builder.Append(", ShowWhenActive = ");
			builder.Append(this.ShowWhenActive.ToString());
			builder.Append(", HideWhenActive = ");
			builder.Append(this.HideWhenActive.ToString());
			return true;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023E1 File Offset: 0x000005E1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WorkerOutfitAnimationAttachmentSpec left, WorkerOutfitAnimationAttachmentSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023ED File Offset: 0x000005ED
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WorkerOutfitAnimationAttachmentSpec left, WorkerOutfitAnimationAttachmentSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002404 File Offset: 0x00000604
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WorkerOutfit>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<AnimationNames>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<ShowWhenActive>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<HideWhenActive>k__BackingField);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000247D File Offset: 0x0000067D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WorkerOutfitAnimationAttachmentSpec);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000248C File Offset: 0x0000068C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WorkerOutfitAnimationAttachmentSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<WorkerOutfit>k__BackingField, other.<WorkerOutfit>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<AnimationNames>k__BackingField, other.<AnimationNames>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<ShowWhenActive>k__BackingField, other.<ShowWhenActive>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<HideWhenActive>k__BackingField, other.<HideWhenActive>k__BackingField));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000251D File Offset: 0x0000071D
		[CompilerGenerated]
		protected WorkerOutfitAnimationAttachmentSpec([Nullable(1)] WorkerOutfitAnimationAttachmentSpec original)
		{
			this.WorkerOutfit = original.<WorkerOutfit>k__BackingField;
			this.AnimationNames = original.<AnimationNames>k__BackingField;
			this.ShowWhenActive = original.<ShowWhenActive>k__BackingField;
			this.HideWhenActive = original.<HideWhenActive>k__BackingField;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000020F6 File Offset: 0x000002F6
		public WorkerOutfitAnimationAttachmentSpec()
		{
		}
	}
}
