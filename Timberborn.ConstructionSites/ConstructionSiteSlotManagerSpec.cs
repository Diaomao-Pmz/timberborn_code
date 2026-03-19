using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ConstructionSites
{
	// Token: 0x02000020 RID: 32
	[NullableContext(1)]
	[Nullable(0)]
	public class ConstructionSiteSlotManagerSpec : ComponentSpec, IEquatable<ConstructionSiteSlotManagerSpec>
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004184 File Offset: 0x00002384
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ConstructionSiteSlotManagerSpec);
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004190 File Offset: 0x00002390
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ConstructionSiteSlotManagerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000041DC File Offset: 0x000023DC
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000041E5 File Offset: 0x000023E5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ConstructionSiteSlotManagerSpec left, ConstructionSiteSlotManagerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000041F1 File Offset: 0x000023F1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ConstructionSiteSlotManagerSpec left, ConstructionSiteSlotManagerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004205 File Offset: 0x00002405
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000420D File Offset: 0x0000240D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ConstructionSiteSlotManagerSpec);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000345F File Offset: 0x0000165F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000421B File Offset: 0x0000241B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ConstructionSiteSlotManagerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004232 File Offset: 0x00002432
		[CompilerGenerated]
		protected ConstructionSiteSlotManagerSpec(ConstructionSiteSlotManagerSpec original) : base(original)
		{
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003DBD File Offset: 0x00001FBD
		public ConstructionSiteSlotManagerSpec()
		{
		}
	}
}
