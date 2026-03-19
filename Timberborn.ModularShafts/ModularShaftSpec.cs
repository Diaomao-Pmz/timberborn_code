using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ModularShafts
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	public class ModularShaftSpec : ComponentSpec, IEquatable<ModularShaftSpec>
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000369E File Offset: 0x0000189E
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ModularShaftSpec);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000036AC File Offset: 0x000018AC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ModularShaftSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000036F8 File Offset: 0x000018F8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003701 File Offset: 0x00001901
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ModularShaftSpec left, ModularShaftSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000370D File Offset: 0x0000190D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ModularShaftSpec left, ModularShaftSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003721 File Offset: 0x00001921
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003729 File Offset: 0x00001929
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ModularShaftSpec);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002742 File Offset: 0x00000942
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003737 File Offset: 0x00001937
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ModularShaftSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000374E File Offset: 0x0000194E
		[CompilerGenerated]
		protected ModularShaftSpec(ModularShaftSpec original) : base(original)
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002791 File Offset: 0x00000991
		public ModularShaftSpec()
		{
		}
	}
}
