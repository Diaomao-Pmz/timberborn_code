using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	public class WorkshopAnimationControllerSpec : ComponentSpec, IEquatable<WorkshopAnimationControllerSpec>
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003204 File Offset: 0x00001404
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WorkshopAnimationControllerSpec);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003210 File Offset: 0x00001410
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WorkshopAnimationControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000325C File Offset: 0x0000145C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003265 File Offset: 0x00001465
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WorkshopAnimationControllerSpec left, WorkshopAnimationControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003271 File Offset: 0x00001471
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WorkshopAnimationControllerSpec left, WorkshopAnimationControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003285 File Offset: 0x00001485
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000328D File Offset: 0x0000148D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WorkshopAnimationControllerSpec);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000023D3 File Offset: 0x000005D3
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000329B File Offset: 0x0000149B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WorkshopAnimationControllerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000032B2 File Offset: 0x000014B2
		[CompilerGenerated]
		protected WorkshopAnimationControllerSpec(WorkshopAnimationControllerSpec original) : base(original)
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002422 File Offset: 0x00000622
		public WorkshopAnimationControllerSpec()
		{
		}
	}
}
