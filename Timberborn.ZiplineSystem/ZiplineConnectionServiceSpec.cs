using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x02000018 RID: 24
	[NullableContext(1)]
	[Nullable(0)]
	public class ZiplineConnectionServiceSpec : ComponentSpec, IEquatable<ZiplineConnectionServiceSpec>
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003DBF File Offset: 0x00001FBF
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ZiplineConnectionServiceSpec);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003DCB File Offset: 0x00001FCB
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003DD3 File Offset: 0x00001FD3
		[Serialize]
		public int MaxCableInclination { get; set; }

		// Token: 0x060000B1 RID: 177 RVA: 0x00003DDC File Offset: 0x00001FDC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ZiplineConnectionServiceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003E28 File Offset: 0x00002028
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MaxCableInclination = ");
			builder.Append(this.MaxCableInclination.ToString());
			return true;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003E72 File Offset: 0x00002072
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ZiplineConnectionServiceSpec left, ZiplineConnectionServiceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003E7E File Offset: 0x0000207E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ZiplineConnectionServiceSpec left, ZiplineConnectionServiceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003E92 File Offset: 0x00002092
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxCableInclination>k__BackingField);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003EB1 File Offset: 0x000020B1
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ZiplineConnectionServiceSpec);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00002363 File Offset: 0x00000563
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003EBF File Offset: 0x000020BF
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ZiplineConnectionServiceSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<MaxCableInclination>k__BackingField, other.<MaxCableInclination>k__BackingField));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003EF0 File Offset: 0x000020F0
		[CompilerGenerated]
		protected ZiplineConnectionServiceSpec(ZiplineConnectionServiceSpec original) : base(original)
		{
			this.MaxCableInclination = original.<MaxCableInclination>k__BackingField;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000238C File Offset: 0x0000058C
		public ZiplineConnectionServiceSpec()
		{
		}
	}
}
