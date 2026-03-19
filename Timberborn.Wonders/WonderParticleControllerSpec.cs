using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Wonders
{
	// Token: 0x0200001E RID: 30
	public class WonderParticleControllerSpec : ComponentSpec, IEquatable<WonderParticleControllerSpec>
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000352D File Offset: 0x0000172D
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WonderParticleControllerSpec);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003539 File Offset: 0x00001739
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003541 File Offset: 0x00001741
		[Serialize]
		public ImmutableArray<string> AttachmentIds { get; set; }

		// Token: 0x060000B5 RID: 181 RVA: 0x0000354C File Offset: 0x0000174C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WonderParticleControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003598 File Offset: 0x00001798
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

		// Token: 0x060000B7 RID: 183 RVA: 0x000035E2 File Offset: 0x000017E2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WonderParticleControllerSpec left, WonderParticleControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000035EE File Offset: 0x000017EE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WonderParticleControllerSpec left, WonderParticleControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003602 File Offset: 0x00001802
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<AttachmentIds>k__BackingField);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003621 File Offset: 0x00001821
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WonderParticleControllerSpec);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000029FB File Offset: 0x00000BFB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000362F File Offset: 0x0000182F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WonderParticleControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<AttachmentIds>k__BackingField, other.<AttachmentIds>k__BackingField));
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003660 File Offset: 0x00001860
		[CompilerGenerated]
		protected WonderParticleControllerSpec([Nullable(1)] WonderParticleControllerSpec original) : base(original)
		{
			this.AttachmentIds = original.<AttachmentIds>k__BackingField;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00002A4A File Offset: 0x00000C4A
		public WonderParticleControllerSpec()
		{
		}
	}
}
