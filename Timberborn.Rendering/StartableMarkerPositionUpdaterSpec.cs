using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Rendering
{
	// Token: 0x02000022 RID: 34
	[NullableContext(1)]
	[Nullable(0)]
	public class StartableMarkerPositionUpdaterSpec : ComponentSpec, IEquatable<StartableMarkerPositionUpdaterSpec>
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000045A5 File Offset: 0x000027A5
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(StartableMarkerPositionUpdaterSpec);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000045B4 File Offset: 0x000027B4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("StartableMarkerPositionUpdaterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00002CC0 File Offset: 0x00000EC0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004600 File Offset: 0x00002800
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(StartableMarkerPositionUpdaterSpec left, StartableMarkerPositionUpdaterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000460C File Offset: 0x0000280C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(StartableMarkerPositionUpdaterSpec left, StartableMarkerPositionUpdaterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00002CE9 File Offset: 0x00000EE9
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004620 File Offset: 0x00002820
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as StartableMarkerPositionUpdaterSpec);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00002675 File Offset: 0x00000875
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00002CFF File Offset: 0x00000EFF
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(StartableMarkerPositionUpdaterSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00002D16 File Offset: 0x00000F16
		[CompilerGenerated]
		protected StartableMarkerPositionUpdaterSpec(StartableMarkerPositionUpdaterSpec original) : base(original)
		{
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000026F5 File Offset: 0x000008F5
		public StartableMarkerPositionUpdaterSpec()
		{
		}
	}
}
