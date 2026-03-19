using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000020 RID: 32
	[NullableContext(1)]
	[Nullable(0)]
	public class SluiceSpec : ComponentSpec, IEquatable<SluiceSpec>
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00004446 File Offset: 0x00002646
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SluiceSpec);
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004454 File Offset: 0x00002654
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SluiceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00002F68 File Offset: 0x00001168
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000044A0 File Offset: 0x000026A0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SluiceSpec left, SluiceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000044AC File Offset: 0x000026AC
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SluiceSpec left, SluiceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00002F91 File Offset: 0x00001191
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000044C0 File Offset: 0x000026C0
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SluiceSpec);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00002B9B File Offset: 0x00000D9B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00002FA7 File Offset: 0x000011A7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SluiceSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00002FBE File Offset: 0x000011BE
		[CompilerGenerated]
		protected SluiceSpec(SluiceSpec original) : base(original)
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00002CBC File Offset: 0x00000EBC
		public SluiceSpec()
		{
		}
	}
}
