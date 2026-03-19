using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BuildingDoorsteps
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class DoorstepSpawnDisablerSpec : ComponentSpec, IEquatable<DoorstepSpawnDisablerSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000225C File Offset: 0x0000045C
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DoorstepSpawnDisablerSpec);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002268 File Offset: 0x00000468
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DoorstepSpawnDisablerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022B4 File Offset: 0x000004B4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022BD File Offset: 0x000004BD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DoorstepSpawnDisablerSpec left, DoorstepSpawnDisablerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022C9 File Offset: 0x000004C9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DoorstepSpawnDisablerSpec left, DoorstepSpawnDisablerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022DD File Offset: 0x000004DD
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022E5 File Offset: 0x000004E5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DoorstepSpawnDisablerSpec);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022F3 File Offset: 0x000004F3
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022FC File Offset: 0x000004FC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DoorstepSpawnDisablerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002313 File Offset: 0x00000513
		[CompilerGenerated]
		protected DoorstepSpawnDisablerSpec(DoorstepSpawnDisablerSpec original) : base(original)
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000231C File Offset: 0x0000051C
		public DoorstepSpawnDisablerSpec()
		{
		}
	}
}
