using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TerrainLevelValidation
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	public class ContinuousTerrainConstraintSpec : ComponentSpec, IEquatable<ContinuousTerrainConstraintSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000228B File Offset: 0x0000048B
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ContinuousTerrainConstraintSpec);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002298 File Offset: 0x00000498
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ContinuousTerrainConstraintSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022E4 File Offset: 0x000004E4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022ED File Offset: 0x000004ED
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ContinuousTerrainConstraintSpec left, ContinuousTerrainConstraintSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022F9 File Offset: 0x000004F9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ContinuousTerrainConstraintSpec left, ContinuousTerrainConstraintSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000230D File Offset: 0x0000050D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002315 File Offset: 0x00000515
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ContinuousTerrainConstraintSpec);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002323 File Offset: 0x00000523
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000232C File Offset: 0x0000052C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ContinuousTerrainConstraintSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002343 File Offset: 0x00000543
		[CompilerGenerated]
		protected ContinuousTerrainConstraintSpec(ContinuousTerrainConstraintSpec original) : base(original)
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000234C File Offset: 0x0000054C
		public ContinuousTerrainConstraintSpec()
		{
		}
	}
}
