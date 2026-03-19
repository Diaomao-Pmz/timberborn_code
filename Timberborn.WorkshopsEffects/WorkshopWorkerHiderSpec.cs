using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x0200001A RID: 26
	[NullableContext(1)]
	[Nullable(0)]
	public class WorkshopWorkerHiderSpec : ComponentSpec, IEquatable<WorkshopWorkerHiderSpec>
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000036FB File Offset: 0x000018FB
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WorkshopWorkerHiderSpec);
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003708 File Offset: 0x00001908
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WorkshopWorkerHiderSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000325C File Offset: 0x0000145C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003754 File Offset: 0x00001954
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WorkshopWorkerHiderSpec left, WorkshopWorkerHiderSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003760 File Offset: 0x00001960
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WorkshopWorkerHiderSpec left, WorkshopWorkerHiderSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003285 File Offset: 0x00001485
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003774 File Offset: 0x00001974
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WorkshopWorkerHiderSpec);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000023D3 File Offset: 0x000005D3
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000329B File Offset: 0x0000149B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WorkshopWorkerHiderSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000032B2 File Offset: 0x000014B2
		[CompilerGenerated]
		protected WorkshopWorkerHiderSpec(WorkshopWorkerHiderSpec original) : base(original)
		{
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00002422 File Offset: 0x00000622
		public WorkshopWorkerHiderSpec()
		{
		}
	}
}
