using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.PowerGenerationUI
{
	// Token: 0x0200000D RID: 13
	public class PowerGeneratorParticleControllerSpec : ComponentSpec, IEquatable<PowerGeneratorParticleControllerSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002598 File Offset: 0x00000798
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(PowerGeneratorParticleControllerSpec);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000025A4 File Offset: 0x000007A4
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000025AC File Offset: 0x000007AC
		[Serialize]
		public ImmutableArray<string> AttachmentIds { get; set; }

		// Token: 0x0600002F RID: 47 RVA: 0x000025B8 File Offset: 0x000007B8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PowerGeneratorParticleControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002604 File Offset: 0x00000804
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

		// Token: 0x06000031 RID: 49 RVA: 0x0000264E File Offset: 0x0000084E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PowerGeneratorParticleControllerSpec left, PowerGeneratorParticleControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000265A File Offset: 0x0000085A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PowerGeneratorParticleControllerSpec left, PowerGeneratorParticleControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000266E File Offset: 0x0000086E
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<AttachmentIds>k__BackingField);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000268D File Offset: 0x0000088D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PowerGeneratorParticleControllerSpec);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000023DB File Offset: 0x000005DB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000269B File Offset: 0x0000089B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PowerGeneratorParticleControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<AttachmentIds>k__BackingField, other.<AttachmentIds>k__BackingField));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000026CC File Offset: 0x000008CC
		[CompilerGenerated]
		protected PowerGeneratorParticleControllerSpec([Nullable(1)] PowerGeneratorParticleControllerSpec original) : base(original)
		{
			this.AttachmentIds = original.<AttachmentIds>k__BackingField;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002404 File Offset: 0x00000604
		public PowerGeneratorParticleControllerSpec()
		{
		}
	}
}
