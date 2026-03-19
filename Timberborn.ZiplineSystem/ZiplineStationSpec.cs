using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x0200001A RID: 26
	[NullableContext(1)]
	[Nullable(0)]
	public class ZiplineStationSpec : ComponentSpec, IEquatable<ZiplineStationSpec>
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004002 File Offset: 0x00002202
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ZiplineStationSpec);
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004010 File Offset: 0x00002210
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ZiplineStationSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00002324 File Offset: 0x00000524
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000405C File Offset: 0x0000225C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ZiplineStationSpec left, ZiplineStationSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004068 File Offset: 0x00002268
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ZiplineStationSpec left, ZiplineStationSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000234D File Offset: 0x0000054D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000407C File Offset: 0x0000227C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ZiplineStationSpec);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00002363 File Offset: 0x00000563
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000236C File Offset: 0x0000056C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ZiplineStationSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00002383 File Offset: 0x00000583
		[CompilerGenerated]
		protected ZiplineStationSpec(ZiplineStationSpec original) : base(original)
		{
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000238C File Offset: 0x0000058C
		public ZiplineStationSpec()
		{
		}
	}
}
