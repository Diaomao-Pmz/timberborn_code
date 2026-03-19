using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TerrainPhysics
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	public class TerrainPhysicsBlockObjectValidatorSpec : ComponentSpec, IEquatable<TerrainPhysicsBlockObjectValidatorSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000029A6 File Offset: 0x00000BA6
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(TerrainPhysicsBlockObjectValidatorSpec);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000029B4 File Offset: 0x00000BB4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TerrainPhysicsBlockObjectValidatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002A00 File Offset: 0x00000C00
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002A09 File Offset: 0x00000C09
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TerrainPhysicsBlockObjectValidatorSpec left, TerrainPhysicsBlockObjectValidatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A15 File Offset: 0x00000C15
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TerrainPhysicsBlockObjectValidatorSpec left, TerrainPhysicsBlockObjectValidatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002A29 File Offset: 0x00000C29
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002A31 File Offset: 0x00000C31
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TerrainPhysicsBlockObjectValidatorSpec);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A3F File Offset: 0x00000C3F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A48 File Offset: 0x00000C48
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TerrainPhysicsBlockObjectValidatorSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A5F File Offset: 0x00000C5F
		[CompilerGenerated]
		protected TerrainPhysicsBlockObjectValidatorSpec(TerrainPhysicsBlockObjectValidatorSpec original) : base(original)
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A68 File Offset: 0x00000C68
		public TerrainPhysicsBlockObjectValidatorSpec()
		{
		}
	}
}
