using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000010 RID: 16
	[NullableContext(1)]
	[Nullable(0)]
	public class DetonatorSpec : ComponentSpec, IEquatable<DetonatorSpec>
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003236 File Offset: 0x00001436
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DetonatorSpec);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003244 File Offset: 0x00001444
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DetonatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002710 File Offset: 0x00000910
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003290 File Offset: 0x00001490
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DetonatorSpec left, DetonatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000329C File Offset: 0x0000149C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DetonatorSpec left, DetonatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002739 File Offset: 0x00000939
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000032B0 File Offset: 0x000014B0
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DetonatorSpec);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002758 File Offset: 0x00000958
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DetonatorSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000276F File Offset: 0x0000096F
		[CompilerGenerated]
		protected DetonatorSpec(DetonatorSpec original) : base(original)
		{
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002778 File Offset: 0x00000978
		public DetonatorSpec()
		{
		}
	}
}
