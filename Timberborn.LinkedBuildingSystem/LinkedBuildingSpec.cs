using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.LinkedBuildingSystem
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	public class LinkedBuildingSpec : ComponentSpec, IEquatable<LinkedBuildingSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022D6 File Offset: 0x000004D6
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(LinkedBuildingSpec);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022E4 File Offset: 0x000004E4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("LinkedBuildingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002330 File Offset: 0x00000530
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002339 File Offset: 0x00000539
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(LinkedBuildingSpec left, LinkedBuildingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002345 File Offset: 0x00000545
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(LinkedBuildingSpec left, LinkedBuildingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002359 File Offset: 0x00000559
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002361 File Offset: 0x00000561
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as LinkedBuildingSpec);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000236F File Offset: 0x0000056F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002378 File Offset: 0x00000578
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(LinkedBuildingSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000238F File Offset: 0x0000058F
		[CompilerGenerated]
		protected LinkedBuildingSpec(LinkedBuildingSpec original) : base(original)
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002398 File Offset: 0x00000598
		public LinkedBuildingSpec()
		{
		}
	}
}
