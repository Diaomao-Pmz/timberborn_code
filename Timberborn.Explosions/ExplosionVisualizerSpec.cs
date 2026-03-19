using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.Explosions
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	public class ExplosionVisualizerSpec : ComponentSpec, IEquatable<ExplosionVisualizerSpec>
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000037A6 File Offset: 0x000019A6
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ExplosionVisualizerSpec);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000037B2 File Offset: 0x000019B2
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000037BA File Offset: 0x000019BA
		[Serialize]
		public Color ObjectHighlightColor { get; set; }

		// Token: 0x06000071 RID: 113 RVA: 0x000037C4 File Offset: 0x000019C4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ExplosionVisualizerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003810 File Offset: 0x00001A10
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ObjectHighlightColor = ");
			builder.Append(this.ObjectHighlightColor.ToString());
			return true;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000385A File Offset: 0x00001A5A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ExplosionVisualizerSpec left, ExplosionVisualizerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003866 File Offset: 0x00001A66
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ExplosionVisualizerSpec left, ExplosionVisualizerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000387A File Offset: 0x00001A7A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ObjectHighlightColor>k__BackingField);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003899 File Offset: 0x00001A99
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ExplosionVisualizerSpec);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000028CB File Offset: 0x00000ACB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000038A7 File Offset: 0x00001AA7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ExplosionVisualizerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<ObjectHighlightColor>k__BackingField, other.<ObjectHighlightColor>k__BackingField));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000038D8 File Offset: 0x00001AD8
		[CompilerGenerated]
		protected ExplosionVisualizerSpec(ExplosionVisualizerSpec original) : base(original)
		{
			this.ObjectHighlightColor = original.<ObjectHighlightColor>k__BackingField;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002949 File Offset: 0x00000B49
		public ExplosionVisualizerSpec()
		{
		}
	}
}
