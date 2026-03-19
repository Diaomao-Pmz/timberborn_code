using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BlockSystemUI
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	public class UndergroundDepthDescriberSpec : ComponentSpec, IEquatable<UndergroundDepthDescriberSpec>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002DB7 File Offset: 0x00000FB7
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(UndergroundDepthDescriberSpec);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002DC3 File Offset: 0x00000FC3
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002DCB File Offset: 0x00000FCB
		[Serialize]
		public int Depth { get; set; }

		// Token: 0x06000059 RID: 89 RVA: 0x00002DD4 File Offset: 0x00000FD4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UndergroundDepthDescriberSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002E20 File Offset: 0x00001020
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Depth = ");
			builder.Append(this.Depth.ToString());
			return true;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002E6A File Offset: 0x0000106A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(UndergroundDepthDescriberSpec left, UndergroundDepthDescriberSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002E76 File Offset: 0x00001076
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(UndergroundDepthDescriberSpec left, UndergroundDepthDescriberSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E8A File Offset: 0x0000108A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Depth>k__BackingField);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002EA9 File Offset: 0x000010A9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as UndergroundDepthDescriberSpec);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000025D3 File Offset: 0x000007D3
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002EB7 File Offset: 0x000010B7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(UndergroundDepthDescriberSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Depth>k__BackingField, other.<Depth>k__BackingField));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002EE8 File Offset: 0x000010E8
		[CompilerGenerated]
		protected UndergroundDepthDescriberSpec(UndergroundDepthDescriberSpec original) : base(original)
		{
			this.Depth = original.<Depth>k__BackingField;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000026F4 File Offset: 0x000008F4
		public UndergroundDepthDescriberSpec()
		{
		}
	}
}
