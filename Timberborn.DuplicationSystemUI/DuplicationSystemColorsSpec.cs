using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.DuplicationSystemUI
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public class DuplicationSystemColorsSpec : ComponentSpec, IEquatable<DuplicationSystemColorsSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002687 File Offset: 0x00000887
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DuplicationSystemColorsSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002693 File Offset: 0x00000893
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000269B File Offset: 0x0000089B
		[Serialize]
		public Color SourceColor { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000026A4 File Offset: 0x000008A4
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000026AC File Offset: 0x000008AC
		[Serialize]
		public Color TargetColor { get; set; }

		// Token: 0x06000028 RID: 40 RVA: 0x000026B8 File Offset: 0x000008B8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DuplicationSystemColorsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002704 File Offset: 0x00000904
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("SourceColor = ");
			builder.Append(this.SourceColor.ToString());
			builder.Append(", TargetColor = ");
			builder.Append(this.TargetColor.ToString());
			return true;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002775 File Offset: 0x00000975
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DuplicationSystemColorsSpec left, DuplicationSystemColorsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002781 File Offset: 0x00000981
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DuplicationSystemColorsSpec left, DuplicationSystemColorsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002795 File Offset: 0x00000995
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<SourceColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<TargetColor>k__BackingField);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000027CB File Offset: 0x000009CB
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DuplicationSystemColorsSpec);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000027D9 File Offset: 0x000009D9
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027E4 File Offset: 0x000009E4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DuplicationSystemColorsSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<SourceColor>k__BackingField, other.<SourceColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<TargetColor>k__BackingField, other.<TargetColor>k__BackingField));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002838 File Offset: 0x00000A38
		[CompilerGenerated]
		protected DuplicationSystemColorsSpec(DuplicationSystemColorsSpec original) : base(original)
		{
			this.SourceColor = original.<SourceColor>k__BackingField;
			this.TargetColor = original.<TargetColor>k__BackingField;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002859 File Offset: 0x00000A59
		public DuplicationSystemColorsSpec()
		{
		}
	}
}
