using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.PlantingEffects
{
	// Token: 0x0200000A RID: 10
	public class PlantingParticleControllerSpec : ComponentSpec, IEquatable<PlantingParticleControllerSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022F5 File Offset: 0x000004F5
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(PlantingParticleControllerSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002301 File Offset: 0x00000501
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002309 File Offset: 0x00000509
		[Serialize]
		public string ParticlesAttachmentId { get; set; }

		// Token: 0x0600001B RID: 27 RVA: 0x00002314 File Offset: 0x00000514
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PlantingParticleControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002360 File Offset: 0x00000560
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ParticlesAttachmentId = ");
			builder.Append(this.ParticlesAttachmentId);
			return true;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002391 File Offset: 0x00000591
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PlantingParticleControllerSpec left, PlantingParticleControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000239D File Offset: 0x0000059D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PlantingParticleControllerSpec left, PlantingParticleControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023B1 File Offset: 0x000005B1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ParticlesAttachmentId>k__BackingField);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023D0 File Offset: 0x000005D0
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlantingParticleControllerSpec);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023DE File Offset: 0x000005DE
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023E7 File Offset: 0x000005E7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PlantingParticleControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<ParticlesAttachmentId>k__BackingField, other.<ParticlesAttachmentId>k__BackingField));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002418 File Offset: 0x00000618
		[CompilerGenerated]
		protected PlantingParticleControllerSpec([Nullable(1)] PlantingParticleControllerSpec original) : base(original)
		{
			this.ParticlesAttachmentId = original.<ParticlesAttachmentId>k__BackingField;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000242D File Offset: 0x0000062D
		public PlantingParticleControllerSpec()
		{
		}
	}
}
