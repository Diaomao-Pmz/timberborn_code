using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.FactionSystem
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	public class StartingFactionSpec : ComponentSpec, IEquatable<StartingFactionSpec>
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003091 File Offset: 0x00001291
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(StartingFactionSpec);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000030A0 File Offset: 0x000012A0
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("StartingFactionSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000030EC File Offset: 0x000012EC
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000030F5 File Offset: 0x000012F5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(StartingFactionSpec left, StartingFactionSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003101 File Offset: 0x00001301
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(StartingFactionSpec left, StartingFactionSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003115 File Offset: 0x00001315
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000311D File Offset: 0x0000131D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as StartingFactionSpec);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000028E5 File Offset: 0x00000AE5
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000312B File Offset: 0x0000132B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(StartingFactionSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003142 File Offset: 0x00001342
		[CompilerGenerated]
		protected StartingFactionSpec(StartingFactionSpec original) : base(original)
		{
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002D60 File Offset: 0x00000F60
		public StartingFactionSpec()
		{
		}
	}
}
