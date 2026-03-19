using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ConstructionMode
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public class ConstructionModeToolGroupSpec : ComponentSpec, IEquatable<ConstructionModeToolGroupSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000024F3 File Offset: 0x000006F3
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ConstructionModeToolGroupSpec);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002500 File Offset: 0x00000700
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ConstructionModeToolGroupSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000254C File Offset: 0x0000074C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002555 File Offset: 0x00000755
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ConstructionModeToolGroupSpec left, ConstructionModeToolGroupSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002561 File Offset: 0x00000761
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ConstructionModeToolGroupSpec left, ConstructionModeToolGroupSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002575 File Offset: 0x00000775
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000257D File Offset: 0x0000077D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ConstructionModeToolGroupSpec);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000258B File Offset: 0x0000078B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002594 File Offset: 0x00000794
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ConstructionModeToolGroupSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000025AB File Offset: 0x000007AB
		[CompilerGenerated]
		protected ConstructionModeToolGroupSpec(ConstructionModeToolGroupSpec original) : base(original)
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025B4 File Offset: 0x000007B4
		public ConstructionModeToolGroupSpec()
		{
		}
	}
}
