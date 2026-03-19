using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x02000015 RID: 21
	public class WorkshopParticleControllerSpec : ComponentSpec, IEquatable<WorkshopParticleControllerSpec>
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000335C File Offset: 0x0000155C
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WorkshopParticleControllerSpec);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003368 File Offset: 0x00001568
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00003370 File Offset: 0x00001570
		[Serialize]
		public ImmutableArray<string> AttachmentIds { get; set; }

		// Token: 0x0600009E RID: 158 RVA: 0x0000337C File Offset: 0x0000157C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WorkshopParticleControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000033C8 File Offset: 0x000015C8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("AttachmentIds = ");
			builder.Append(this.AttachmentIds.ToString());
			return true;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003412 File Offset: 0x00001612
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WorkshopParticleControllerSpec left, WorkshopParticleControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000341E File Offset: 0x0000161E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WorkshopParticleControllerSpec left, WorkshopParticleControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003432 File Offset: 0x00001632
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<AttachmentIds>k__BackingField);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003451 File Offset: 0x00001651
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WorkshopParticleControllerSpec);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000023D3 File Offset: 0x000005D3
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000345F File Offset: 0x0000165F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WorkshopParticleControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<AttachmentIds>k__BackingField, other.<AttachmentIds>k__BackingField));
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003490 File Offset: 0x00001690
		[CompilerGenerated]
		protected WorkshopParticleControllerSpec([Nullable(1)] WorkshopParticleControllerSpec original) : base(original)
		{
			this.AttachmentIds = original.<AttachmentIds>k__BackingField;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00002422 File Offset: 0x00000622
		public WorkshopParticleControllerSpec()
		{
		}
	}
}
