using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Terraforming
{
	// Token: 0x02000010 RID: 16
	[NullableContext(1)]
	[Nullable(0)]
	public class GroundRaiserSpec : ComponentSpec, IEquatable<GroundRaiserSpec>
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003124 File Offset: 0x00001324
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(GroundRaiserSpec);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003130 File Offset: 0x00001330
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GroundRaiserSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000317C File Offset: 0x0000137C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003185 File Offset: 0x00001385
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GroundRaiserSpec left, GroundRaiserSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003191 File Offset: 0x00001391
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GroundRaiserSpec left, GroundRaiserSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000031A5 File Offset: 0x000013A5
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000031AD File Offset: 0x000013AD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GroundRaiserSpec);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000262F File Offset: 0x0000082F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000031BB File Offset: 0x000013BB
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GroundRaiserSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000031D2 File Offset: 0x000013D2
		[CompilerGenerated]
		protected GroundRaiserSpec(GroundRaiserSpec original) : base(original)
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000026AD File Offset: 0x000008AD
		public GroundRaiserSpec()
		{
		}
	}
}
