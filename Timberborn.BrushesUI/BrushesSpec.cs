using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BrushesUI
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class BrushesSpec : ComponentSpec, IEquatable<BrushesSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002369 File Offset: 0x00000569
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BrushesSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002375 File Offset: 0x00000575
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000237D File Offset: 0x0000057D
		[Serialize]
		public int MaxBrushSize { get; set; }

		// Token: 0x06000016 RID: 22 RVA: 0x00002388 File Offset: 0x00000588
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BrushesSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023D4 File Offset: 0x000005D4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MaxBrushSize = ");
			builder.Append(this.MaxBrushSize.ToString());
			return true;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000241E File Offset: 0x0000061E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BrushesSpec left, BrushesSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000242A File Offset: 0x0000062A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BrushesSpec left, BrushesSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000243E File Offset: 0x0000063E
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxBrushSize>k__BackingField);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000245D File Offset: 0x0000065D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BrushesSpec);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000246B File Offset: 0x0000066B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002474 File Offset: 0x00000674
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BrushesSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<MaxBrushSize>k__BackingField, other.<MaxBrushSize>k__BackingField));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024A5 File Offset: 0x000006A5
		[CompilerGenerated]
		protected BrushesSpec(BrushesSpec original) : base(original)
		{
			this.MaxBrushSize = original.<MaxBrushSize>k__BackingField;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024BA File Offset: 0x000006BA
		public BrushesSpec()
		{
		}
	}
}
