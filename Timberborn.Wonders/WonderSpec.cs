using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Wonders
{
	// Token: 0x02000021 RID: 33
	[NullableContext(1)]
	[Nullable(0)]
	public class WonderSpec : ComponentSpec, IEquatable<WonderSpec>
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003825 File Offset: 0x00001A25
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WonderSpec);
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003834 File Offset: 0x00001A34
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WonderSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003880 File Offset: 0x00001A80
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003889 File Offset: 0x00001A89
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WonderSpec left, WonderSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003895 File Offset: 0x00001A95
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WonderSpec left, WonderSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000038A9 File Offset: 0x00001AA9
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000038B1 File Offset: 0x00001AB1
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WonderSpec);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000029FB File Offset: 0x00000BFB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000038BF File Offset: 0x00001ABF
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WonderSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000038D6 File Offset: 0x00001AD6
		[CompilerGenerated]
		protected WonderSpec(WonderSpec original) : base(original)
		{
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00002A4A File Offset: 0x00000C4A
		public WonderSpec()
		{
		}
	}
}
