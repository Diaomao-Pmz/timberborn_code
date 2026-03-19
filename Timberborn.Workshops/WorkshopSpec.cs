using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Workshops
{
	// Token: 0x0200002F RID: 47
	[NullableContext(1)]
	[Nullable(0)]
	public class WorkshopSpec : ComponentSpec, IEquatable<WorkshopSpec>
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000059F2 File Offset: 0x00003BF2
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WorkshopSpec);
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005A00 File Offset: 0x00003C00
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WorkshopSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00003FF4 File Offset: 0x000021F4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005A4C File Offset: 0x00003C4C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WorkshopSpec left, WorkshopSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005A58 File Offset: 0x00003C58
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WorkshopSpec left, WorkshopSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000401D File Offset: 0x0000221D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005A6C File Offset: 0x00003C6C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WorkshopSpec);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00003857 File Offset: 0x00001A57
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00004033 File Offset: 0x00002233
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WorkshopSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000404A File Offset: 0x0000224A
		[CompilerGenerated]
		protected WorkshopSpec(WorkshopSpec original) : base(original)
		{
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000038A6 File Offset: 0x00001AA6
		public WorkshopSpec()
		{
		}
	}
}
