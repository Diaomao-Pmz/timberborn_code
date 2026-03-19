using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Ruins
{
	// Token: 0x02000014 RID: 20
	[NullableContext(1)]
	[Nullable(0)]
	public class ScavengerSpec : ComponentSpec, IEquatable<ScavengerSpec>
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003162 File Offset: 0x00001362
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ScavengerSpec);
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003170 File Offset: 0x00001370
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ScavengerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000031BC File Offset: 0x000013BC
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000031C5 File Offset: 0x000013C5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ScavengerSpec left, ScavengerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000031D1 File Offset: 0x000013D1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ScavengerSpec left, ScavengerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000031E5 File Offset: 0x000013E5
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000031ED File Offset: 0x000013ED
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ScavengerSpec);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000268E File Offset: 0x0000088E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000031FB File Offset: 0x000013FB
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ScavengerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003212 File Offset: 0x00001412
		[CompilerGenerated]
		protected ScavengerSpec(ScavengerSpec original) : base(original)
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002731 File Offset: 0x00000931
		public ScavengerSpec()
		{
		}
	}
}
