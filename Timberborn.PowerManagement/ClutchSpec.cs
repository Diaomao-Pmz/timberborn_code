using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.PowerManagement
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public class ClutchSpec : ComponentSpec, IEquatable<ClutchSpec>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002701 File Offset: 0x00000901
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ClutchSpec);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002710 File Offset: 0x00000910
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ClutchSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000275C File Offset: 0x0000095C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002765 File Offset: 0x00000965
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ClutchSpec left, ClutchSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002771 File Offset: 0x00000971
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ClutchSpec left, ClutchSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002785 File Offset: 0x00000985
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000278D File Offset: 0x0000098D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ClutchSpec);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002679 File Offset: 0x00000879
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000279B File Offset: 0x0000099B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ClutchSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000027B2 File Offset: 0x000009B2
		[CompilerGenerated]
		protected ClutchSpec(ClutchSpec original) : base(original)
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000026F9 File Offset: 0x000008F9
		public ClutchSpec()
		{
		}
	}
}
