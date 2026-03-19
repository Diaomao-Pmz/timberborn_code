using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000018 RID: 24
	[NullableContext(1)]
	[Nullable(0)]
	public class ImpermeableFloorSpec : ComponentSpec, IEquatable<ImpermeableFloorSpec>
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003B7A File Offset: 0x00001D7A
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ImpermeableFloorSpec);
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003B88 File Offset: 0x00001D88
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ImpermeableFloorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00002F68 File Offset: 0x00001168
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003BD4 File Offset: 0x00001DD4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ImpermeableFloorSpec left, ImpermeableFloorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003BE0 File Offset: 0x00001DE0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ImpermeableFloorSpec left, ImpermeableFloorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00002F91 File Offset: 0x00001191
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003BF4 File Offset: 0x00001DF4
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ImpermeableFloorSpec);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00002B9B File Offset: 0x00000D9B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00002FA7 File Offset: 0x000011A7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ImpermeableFloorSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00002FBE File Offset: 0x000011BE
		[CompilerGenerated]
		protected ImpermeableFloorSpec(ImpermeableFloorSpec original) : base(original)
		{
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00002CBC File Offset: 0x00000EBC
		public ImpermeableFloorSpec()
		{
		}
	}
}
