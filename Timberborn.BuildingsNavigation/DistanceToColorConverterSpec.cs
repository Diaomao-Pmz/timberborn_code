using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.CoreUI;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x02000015 RID: 21
	public class DistanceToColorConverterSpec : ComponentSpec, IEquatable<DistanceToColorConverterSpec>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000034C7 File Offset: 0x000016C7
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DistanceToColorConverterSpec);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000034D3 File Offset: 0x000016D3
		// (set) Token: 0x0600007C RID: 124 RVA: 0x000034DB File Offset: 0x000016DB
		[Serialize]
		public ImmutableArray<GradientPointSpec> DistanceGradient { get; set; }

		// Token: 0x0600007D RID: 125 RVA: 0x000034E4 File Offset: 0x000016E4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DistanceToColorConverterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003530 File Offset: 0x00001730
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DistanceGradient = ");
			builder.Append(this.DistanceGradient.ToString());
			return true;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000357A File Offset: 0x0000177A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DistanceToColorConverterSpec left, DistanceToColorConverterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003586 File Offset: 0x00001786
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DistanceToColorConverterSpec left, DistanceToColorConverterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000359A File Offset: 0x0000179A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<GradientPointSpec>>.Default.GetHashCode(this.<DistanceGradient>k__BackingField);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000035B9 File Offset: 0x000017B9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DistanceToColorConverterSpec);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000028AB File Offset: 0x00000AAB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000035C7 File Offset: 0x000017C7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DistanceToColorConverterSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<GradientPointSpec>>.Default.Equals(this.<DistanceGradient>k__BackingField, other.<DistanceGradient>k__BackingField));
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000035F8 File Offset: 0x000017F8
		[CompilerGenerated]
		protected DistanceToColorConverterSpec([Nullable(1)] DistanceToColorConverterSpec original) : base(original)
		{
			this.DistanceGradient = original.<DistanceGradient>k__BackingField;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002929 File Offset: 0x00000B29
		public DistanceToColorConverterSpec()
		{
		}
	}
}
