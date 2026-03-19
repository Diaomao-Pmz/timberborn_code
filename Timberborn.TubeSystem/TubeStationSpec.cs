using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TubeSystem
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	public class TubeStationSpec : ComponentSpec, IEquatable<TubeStationSpec>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002D43 File Offset: 0x00000F43
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(TubeStationSpec);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002D50 File Offset: 0x00000F50
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TubeStationSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002CE4 File Offset: 0x00000EE4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002D9C File Offset: 0x00000F9C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TubeStationSpec left, TubeStationSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002DA8 File Offset: 0x00000FA8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TubeStationSpec left, TubeStationSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002D0D File Offset: 0x00000F0D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002DBC File Offset: 0x00000FBC
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TubeStationSpec);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002A3E File Offset: 0x00000C3E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002D23 File Offset: 0x00000F23
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TubeStationSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002D3A File Offset: 0x00000F3A
		[CompilerGenerated]
		protected TubeStationSpec(TubeStationSpec original) : base(original)
		{
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002A8D File Offset: 0x00000C8D
		public TubeStationSpec()
		{
		}
	}
}
