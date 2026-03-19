using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Particles
{
	// Token: 0x02000009 RID: 9
	public class AnimationParticlesTriggerSpec : ComponentSpec, IEquatable<AnimationParticlesTriggerSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000258A File Offset: 0x0000078A
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(AnimationParticlesTriggerSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002596 File Offset: 0x00000796
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000259E File Offset: 0x0000079E
		[Serialize]
		public ImmutableArray<AnimationParticle> AnimationParticles { get; set; }

		// Token: 0x06000023 RID: 35 RVA: 0x000025A8 File Offset: 0x000007A8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AnimationParticlesTriggerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025F4 File Offset: 0x000007F4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("AnimationParticles = ");
			builder.Append(this.AnimationParticles.ToString());
			return true;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000263E File Offset: 0x0000083E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(AnimationParticlesTriggerSpec left, AnimationParticlesTriggerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000264A File Offset: 0x0000084A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(AnimationParticlesTriggerSpec left, AnimationParticlesTriggerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000265E File Offset: 0x0000085E
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<AnimationParticle>>.Default.GetHashCode(this.<AnimationParticles>k__BackingField);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000267D File Offset: 0x0000087D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AnimationParticlesTriggerSpec);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000268B File Offset: 0x0000088B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002694 File Offset: 0x00000894
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(AnimationParticlesTriggerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<AnimationParticle>>.Default.Equals(this.<AnimationParticles>k__BackingField, other.<AnimationParticles>k__BackingField));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026C5 File Offset: 0x000008C5
		[CompilerGenerated]
		protected AnimationParticlesTriggerSpec([Nullable(1)] AnimationParticlesTriggerSpec original) : base(original)
		{
			this.AnimationParticles = original.<AnimationParticles>k__BackingField;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026DA File Offset: 0x000008DA
		public AnimationParticlesTriggerSpec()
		{
		}
	}
}
