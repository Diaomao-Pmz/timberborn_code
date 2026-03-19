using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.StartingLocationSystem
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public class StartingLocationSpec : ComponentSpec, IEquatable<StartingLocationSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000239A File Offset: 0x0000059A
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(StartingLocationSpec);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023A8 File Offset: 0x000005A8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("StartingLocationSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023F4 File Offset: 0x000005F4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023FD File Offset: 0x000005FD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(StartingLocationSpec left, StartingLocationSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002409 File Offset: 0x00000609
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(StartingLocationSpec left, StartingLocationSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000241D File Offset: 0x0000061D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002425 File Offset: 0x00000625
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as StartingLocationSpec);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002433 File Offset: 0x00000633
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000243C File Offset: 0x0000063C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(StartingLocationSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002453 File Offset: 0x00000653
		[CompilerGenerated]
		protected StartingLocationSpec(StartingLocationSpec original) : base(original)
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000245C File Offset: 0x0000065C
		public StartingLocationSpec()
		{
		}
	}
}
