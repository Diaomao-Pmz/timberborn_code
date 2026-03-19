using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WonderPlanes
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	public class PlaneLauncherSpec : ComponentSpec, IEquatable<PlaneLauncherSpec>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003445 File Offset: 0x00001645
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(PlaneLauncherSpec);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003454 File Offset: 0x00001654
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PlaneLauncherSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000034A0 File Offset: 0x000016A0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000034A9 File Offset: 0x000016A9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PlaneLauncherSpec left, PlaneLauncherSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000034B5 File Offset: 0x000016B5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PlaneLauncherSpec left, PlaneLauncherSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000034C9 File Offset: 0x000016C9
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000034D1 File Offset: 0x000016D1
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlaneLauncherSpec);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002AA6 File Offset: 0x00000CA6
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000034DF File Offset: 0x000016DF
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PlaneLauncherSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000034F6 File Offset: 0x000016F6
		[CompilerGenerated]
		protected PlaneLauncherSpec(PlaneLauncherSpec original) : base(original)
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002AF5 File Offset: 0x00000CF5
		public PlaneLauncherSpec()
		{
		}
	}
}
