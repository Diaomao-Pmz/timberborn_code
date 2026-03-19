using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TickSystem
{
	// Token: 0x0200001E RID: 30
	[NullableContext(1)]
	[Nullable(0)]
	public class TickTimeSpec : ComponentSpec, IEquatable<TickTimeSpec>
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002D01 File Offset: 0x00000F01
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(TickTimeSpec);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002D0D File Offset: 0x00000F0D
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002D15 File Offset: 0x00000F15
		[Serialize]
		public float TickIntervalInSeconds { get; set; }

		// Token: 0x06000074 RID: 116 RVA: 0x00002D20 File Offset: 0x00000F20
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TickTimeSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002D6C File Offset: 0x00000F6C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("TickIntervalInSeconds = ");
			builder.Append(this.TickIntervalInSeconds.ToString());
			return true;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002DB6 File Offset: 0x00000FB6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TickTimeSpec left, TickTimeSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002DC2 File Offset: 0x00000FC2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TickTimeSpec left, TickTimeSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002DD6 File Offset: 0x00000FD6
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<TickIntervalInSeconds>k__BackingField);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002DF5 File Offset: 0x00000FF5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TickTimeSpec);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002E03 File Offset: 0x00001003
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002E0C File Offset: 0x0000100C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TickTimeSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<TickIntervalInSeconds>k__BackingField, other.<TickIntervalInSeconds>k__BackingField));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002E3D File Offset: 0x0000103D
		[CompilerGenerated]
		protected TickTimeSpec(TickTimeSpec original) : base(original)
		{
			this.TickIntervalInSeconds = original.<TickIntervalInSeconds>k__BackingField;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002E52 File Offset: 0x00001052
		public TickTimeSpec()
		{
		}
	}
}
