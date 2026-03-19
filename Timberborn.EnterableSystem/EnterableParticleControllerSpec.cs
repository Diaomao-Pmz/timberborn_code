using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.EnterableSystem
{
	// Token: 0x0200000D RID: 13
	public class EnterableParticleControllerSpec : ComponentSpec, IEquatable<EnterableParticleControllerSpec>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000029E5 File Offset: 0x00000BE5
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(EnterableParticleControllerSpec);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000029F1 File Offset: 0x00000BF1
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000029F9 File Offset: 0x00000BF9
		[Serialize]
		public ImmutableArray<string> AttachmentIds { get; set; }

		// Token: 0x06000056 RID: 86 RVA: 0x00002A04 File Offset: 0x00000C04
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EnterableParticleControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002A50 File Offset: 0x00000C50
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

		// Token: 0x06000058 RID: 88 RVA: 0x00002A9A File Offset: 0x00000C9A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(EnterableParticleControllerSpec left, EnterableParticleControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002AA6 File Offset: 0x00000CA6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(EnterableParticleControllerSpec left, EnterableParticleControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002ABA File Offset: 0x00000CBA
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<AttachmentIds>k__BackingField);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002AD9 File Offset: 0x00000CD9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EnterableParticleControllerSpec);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002AE7 File Offset: 0x00000CE7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(EnterableParticleControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<AttachmentIds>k__BackingField, other.<AttachmentIds>k__BackingField));
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002B18 File Offset: 0x00000D18
		[CompilerGenerated]
		protected EnterableParticleControllerSpec([Nullable(1)] EnterableParticleControllerSpec original) : base(original)
		{
			this.AttachmentIds = original.<AttachmentIds>k__BackingField;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000279E File Offset: 0x0000099E
		public EnterableParticleControllerSpec()
		{
		}
	}
}
