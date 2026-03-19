using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.CoreUI;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x02000026 RID: 38
	public class WaterOutputParticleColorsSpec : ComponentSpec, IEquatable<WaterOutputParticleColorsSpec>
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000054C4 File Offset: 0x000036C4
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WaterOutputParticleColorsSpec);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000054D0 File Offset: 0x000036D0
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x000054D8 File Offset: 0x000036D8
		[Serialize]
		public ImmutableArray<GradientPointSpec> WaterContaminationParticleGradient { get; set; }

		// Token: 0x060000E3 RID: 227 RVA: 0x000054E4 File Offset: 0x000036E4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterOutputParticleColorsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005530 File Offset: 0x00003730
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("WaterContaminationParticleGradient = ");
			builder.Append(this.WaterContaminationParticleGradient.ToString());
			return true;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000557A File Offset: 0x0000377A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterOutputParticleColorsSpec left, WaterOutputParticleColorsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005586 File Offset: 0x00003786
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterOutputParticleColorsSpec left, WaterOutputParticleColorsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000559A File Offset: 0x0000379A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<GradientPointSpec>>.Default.GetHashCode(this.<WaterContaminationParticleGradient>k__BackingField);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000055B9 File Offset: 0x000037B9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterOutputParticleColorsSpec);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000055C7 File Offset: 0x000037C7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000055D0 File Offset: 0x000037D0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterOutputParticleColorsSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<GradientPointSpec>>.Default.Equals(this.<WaterContaminationParticleGradient>k__BackingField, other.<WaterContaminationParticleGradient>k__BackingField));
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005601 File Offset: 0x00003801
		[CompilerGenerated]
		protected WaterOutputParticleColorsSpec([Nullable(1)] WaterOutputParticleColorsSpec original) : base(original)
		{
			this.WaterContaminationParticleGradient = original.<WaterContaminationParticleGradient>k__BackingField;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005616 File Offset: 0x00003816
		public WaterOutputParticleColorsSpec()
		{
		}
	}
}
