using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200002C RID: 44
	public class PinnedLeverModified : IEquatable<PinnedLeverModified>
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x00005C22 File Offset: 0x00003E22
		public PinnedLeverModified(Lever Lever)
		{
			this.Lever = Lever;
			base..ctor();
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00005C31 File Offset: 0x00003E31
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(PinnedLeverModified);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00005C3D File Offset: 0x00003E3D
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00005C45 File Offset: 0x00003E45
		public Lever Lever { get; set; }

		// Token: 0x060001D9 RID: 473 RVA: 0x00005C50 File Offset: 0x00003E50
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PinnedLeverModified");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00005C9C File Offset: 0x00003E9C
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Lever = ");
			builder.Append(this.Lever);
			return true;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00005CBD File Offset: 0x00003EBD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PinnedLeverModified left, PinnedLeverModified right)
		{
			return !(left == right);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00005CC9 File Offset: 0x00003EC9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PinnedLeverModified left, PinnedLeverModified right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00005CDD File Offset: 0x00003EDD
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Lever>.Default.GetHashCode(this.<Lever>k__BackingField);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00005D06 File Offset: 0x00003F06
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PinnedLeverModified);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00005D14 File Offset: 0x00003F14
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PinnedLeverModified other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Lever>.Default.Equals(this.<Lever>k__BackingField, other.<Lever>k__BackingField));
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00005D52 File Offset: 0x00003F52
		[CompilerGenerated]
		protected PinnedLeverModified([Nullable(1)] PinnedLeverModified original)
		{
			this.Lever = original.<Lever>k__BackingField;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00005D66 File Offset: 0x00003F66
		[CompilerGenerated]
		public void Deconstruct(out Lever Lever)
		{
			Lever = this.Lever;
		}
	}
}
