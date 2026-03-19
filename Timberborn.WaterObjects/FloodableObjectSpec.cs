using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterObjects
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	public class FloodableObjectSpec : ComponentSpec, IEquatable<FloodableObjectSpec>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000284A File Offset: 0x00000A4A
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(FloodableObjectSpec);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002858 File Offset: 0x00000A58
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FloodableObjectSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002228 File Offset: 0x00000428
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000028A4 File Offset: 0x00000AA4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FloodableObjectSpec left, FloodableObjectSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000028B0 File Offset: 0x00000AB0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FloodableObjectSpec left, FloodableObjectSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002251 File Offset: 0x00000451
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000028C4 File Offset: 0x00000AC4
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FloodableObjectSpec);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002267 File Offset: 0x00000467
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002270 File Offset: 0x00000470
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FloodableObjectSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002287 File Offset: 0x00000487
		[CompilerGenerated]
		protected FloodableObjectSpec(FloodableObjectSpec original) : base(original)
		{
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002290 File Offset: 0x00000490
		public FloodableObjectSpec()
		{
		}
	}
}
