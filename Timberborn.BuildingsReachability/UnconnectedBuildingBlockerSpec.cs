using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BuildingsReachability
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	public class UnconnectedBuildingBlockerSpec : ComponentSpec, IEquatable<UnconnectedBuildingBlockerSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000027B4 File Offset: 0x000009B4
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(UnconnectedBuildingBlockerSpec);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000027C0 File Offset: 0x000009C0
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UnconnectedBuildingBlockerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000280C File Offset: 0x00000A0C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002815 File Offset: 0x00000A15
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(UnconnectedBuildingBlockerSpec left, UnconnectedBuildingBlockerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002821 File Offset: 0x00000A21
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(UnconnectedBuildingBlockerSpec left, UnconnectedBuildingBlockerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002835 File Offset: 0x00000A35
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000283D File Offset: 0x00000A3D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as UnconnectedBuildingBlockerSpec);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000284B File Offset: 0x00000A4B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002854 File Offset: 0x00000A54
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(UnconnectedBuildingBlockerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000286B File Offset: 0x00000A6B
		[CompilerGenerated]
		protected UnconnectedBuildingBlockerSpec(UnconnectedBuildingBlockerSpec original) : base(original)
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002874 File Offset: 0x00000A74
		public UnconnectedBuildingBlockerSpec()
		{
		}
	}
}
