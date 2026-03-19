using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Terraforming
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	public class Slope : ComponentSpec, IEquatable<Slope>
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000332C File Offset: 0x0000152C
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(Slope);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003338 File Offset: 0x00001538
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Slope");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000317C File Offset: 0x0000137C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003384 File Offset: 0x00001584
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(Slope left, Slope right)
		{
			return !(left == right);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003390 File Offset: 0x00001590
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(Slope left, Slope right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000031A5 File Offset: 0x000013A5
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000033A4 File Offset: 0x000015A4
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Slope);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000262F File Offset: 0x0000082F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000031BB File Offset: 0x000013BB
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(Slope other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000031D2 File Offset: 0x000013D2
		[CompilerGenerated]
		protected Slope(Slope original) : base(original)
		{
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000026AD File Offset: 0x000008AD
		public Slope()
		{
		}
	}
}
