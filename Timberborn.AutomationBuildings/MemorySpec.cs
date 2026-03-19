using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000028 RID: 40
	[NullableContext(1)]
	[Nullable(0)]
	public class MemorySpec : ComponentSpec, IEquatable<MemorySpec>
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00005955 File Offset: 0x00003B55
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(MemorySpec);
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005964 File Offset: 0x00003B64
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MemorySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00002710 File Offset: 0x00000910
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000059B0 File Offset: 0x00003BB0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MemorySpec left, MemorySpec right)
		{
			return !(left == right);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000059BC File Offset: 0x00003BBC
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MemorySpec left, MemorySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00002739 File Offset: 0x00000939
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000059D0 File Offset: 0x00003BD0
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MemorySpec);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00002758 File Offset: 0x00000958
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MemorySpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000276F File Offset: 0x0000096F
		[CompilerGenerated]
		protected MemorySpec(MemorySpec original) : base(original)
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00002778 File Offset: 0x00000978
		public MemorySpec()
		{
		}
	}
}
