using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterObjects
{
	// Token: 0x0200000E RID: 14
	[NullableContext(1)]
	[Nullable(0)]
	public class FloodableObjectBlockerSpec : ComponentSpec, IEquatable<FloodableObjectBlockerSpec>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000027BB File Offset: 0x000009BB
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(FloodableObjectBlockerSpec);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000027C8 File Offset: 0x000009C8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FloodableObjectBlockerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002228 File Offset: 0x00000428
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002814 File Offset: 0x00000A14
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FloodableObjectBlockerSpec left, FloodableObjectBlockerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002820 File Offset: 0x00000A20
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FloodableObjectBlockerSpec left, FloodableObjectBlockerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002251 File Offset: 0x00000451
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002834 File Offset: 0x00000A34
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FloodableObjectBlockerSpec);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002267 File Offset: 0x00000467
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002270 File Offset: 0x00000470
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FloodableObjectBlockerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002287 File Offset: 0x00000487
		[CompilerGenerated]
		protected FloodableObjectBlockerSpec(FloodableObjectBlockerSpec original) : base(original)
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002290 File Offset: 0x00000490
		public FloodableObjectBlockerSpec()
		{
		}
	}
}
