using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Particles
{
	// Token: 0x02000007 RID: 7
	public class AnimationParticle : IEquatable<AnimationParticle>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(AnimationParticle);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210C File Offset: 0x0000030C
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002114 File Offset: 0x00000314
		[Serialize]
		public string AnimationName { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000211D File Offset: 0x0000031D
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002125 File Offset: 0x00000325
		[Serialize]
		public string ParticlesAttachmentId { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000212E File Offset: 0x0000032E
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002136 File Offset: 0x00000336
		[Serialize]
		public ImmutableArray<float> TriggerTimes { get; set; }

		// Token: 0x0600000E RID: 14 RVA: 0x00002140 File Offset: 0x00000340
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AnimationParticle");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000218C File Offset: 0x0000038C
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("AnimationName = ");
			builder.Append(this.AnimationName);
			builder.Append(", ParticlesAttachmentId = ");
			builder.Append(this.ParticlesAttachmentId);
			builder.Append(", TriggerTimes = ");
			builder.Append(this.TriggerTimes.ToString());
			return true;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021F8 File Offset: 0x000003F8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(AnimationParticle left, AnimationParticle right)
		{
			return !(left == right);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002204 File Offset: 0x00000404
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(AnimationParticle left, AnimationParticle right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002218 File Offset: 0x00000418
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AnimationName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ParticlesAttachmentId>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<float>>.Default.GetHashCode(this.<TriggerTimes>k__BackingField);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000227A File Offset: 0x0000047A
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AnimationParticle);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002288 File Offset: 0x00000488
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(AnimationParticle other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<AnimationName>k__BackingField, other.<AnimationName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ParticlesAttachmentId>k__BackingField, other.<ParticlesAttachmentId>k__BackingField) && EqualityComparer<ImmutableArray<float>>.Default.Equals(this.<TriggerTimes>k__BackingField, other.<TriggerTimes>k__BackingField));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002301 File Offset: 0x00000501
		[CompilerGenerated]
		protected AnimationParticle([Nullable(1)] AnimationParticle original)
		{
			this.AnimationName = original.<AnimationName>k__BackingField;
			this.ParticlesAttachmentId = original.<ParticlesAttachmentId>k__BackingField;
			this.TriggerTimes = original.<TriggerTimes>k__BackingField;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000020F8 File Offset: 0x000002F8
		public AnimationParticle()
		{
		}
	}
}
