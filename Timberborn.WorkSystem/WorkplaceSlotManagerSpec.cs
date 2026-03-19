using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000028 RID: 40
	[NullableContext(1)]
	[Nullable(0)]
	public class WorkplaceSlotManagerSpec : ComponentSpec, IEquatable<WorkplaceSlotManagerSpec>
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00004624 File Offset: 0x00002824
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WorkplaceSlotManagerSpec);
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004630 File Offset: 0x00002830
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WorkplaceSlotManagerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000036F0 File Offset: 0x000018F0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000467C File Offset: 0x0000287C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WorkplaceSlotManagerSpec left, WorkplaceSlotManagerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004688 File Offset: 0x00002888
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WorkplaceSlotManagerSpec left, WorkplaceSlotManagerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00003719 File Offset: 0x00001919
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000469C File Offset: 0x0000289C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WorkplaceSlotManagerSpec);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00002511 File Offset: 0x00000711
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000372F File Offset: 0x0000192F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WorkplaceSlotManagerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00003746 File Offset: 0x00001946
		[CompilerGenerated]
		protected WorkplaceSlotManagerSpec(WorkplaceSlotManagerSpec original) : base(original)
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00002591 File Offset: 0x00000791
		public WorkplaceSlotManagerSpec()
		{
		}
	}
}
