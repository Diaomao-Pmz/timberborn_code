using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Workshops
{
	// Token: 0x02000028 RID: 40
	[NullableContext(1)]
	[Nullable(0)]
	public class SimpleManufactoryBehaviorsSpec : ComponentSpec, IEquatable<SimpleManufactoryBehaviorsSpec>
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00005266 File Offset: 0x00003466
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SimpleManufactoryBehaviorsSpec);
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005274 File Offset: 0x00003474
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SimpleManufactoryBehaviorsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00003FF4 File Offset: 0x000021F4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000052C0 File Offset: 0x000034C0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SimpleManufactoryBehaviorsSpec left, SimpleManufactoryBehaviorsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000052CC File Offset: 0x000034CC
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SimpleManufactoryBehaviorsSpec left, SimpleManufactoryBehaviorsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000401D File Offset: 0x0000221D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000052E0 File Offset: 0x000034E0
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SimpleManufactoryBehaviorsSpec);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00003857 File Offset: 0x00001A57
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004033 File Offset: 0x00002233
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SimpleManufactoryBehaviorsSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000404A File Offset: 0x0000224A
		[CompilerGenerated]
		protected SimpleManufactoryBehaviorsSpec(SimpleManufactoryBehaviorsSpec original) : base(original)
		{
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000038A6 File Offset: 0x00001AA6
		public SimpleManufactoryBehaviorsSpec()
		{
		}
	}
}
