using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Demolishing
{
	// Token: 0x0200000E RID: 14
	public class DemolishableParticleControllerSpec : ComponentSpec, IEquatable<DemolishableParticleControllerSpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002993 File Offset: 0x00000B93
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DemolishableParticleControllerSpec);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000299F File Offset: 0x00000B9F
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000029A7 File Offset: 0x00000BA7
		[Serialize]
		public ImmutableArray<DemolishableParticle> DemolishableParticles { get; set; }

		// Token: 0x06000049 RID: 73 RVA: 0x000029B0 File Offset: 0x00000BB0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DemolishableParticleControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029FC File Offset: 0x00000BFC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DemolishableParticles = ");
			builder.Append(this.DemolishableParticles.ToString());
			return true;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002A46 File Offset: 0x00000C46
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DemolishableParticleControllerSpec left, DemolishableParticleControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002A52 File Offset: 0x00000C52
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DemolishableParticleControllerSpec left, DemolishableParticleControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A66 File Offset: 0x00000C66
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<DemolishableParticle>>.Default.GetHashCode(this.<DemolishableParticles>k__BackingField);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002A85 File Offset: 0x00000C85
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DemolishableParticleControllerSpec);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000025A7 File Offset: 0x000007A7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A93 File Offset: 0x00000C93
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DemolishableParticleControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<DemolishableParticle>>.Default.Equals(this.<DemolishableParticles>k__BackingField, other.<DemolishableParticles>k__BackingField));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002AC4 File Offset: 0x00000CC4
		[CompilerGenerated]
		protected DemolishableParticleControllerSpec([Nullable(1)] DemolishableParticleControllerSpec original) : base(original)
		{
			this.DemolishableParticles = original.<DemolishableParticles>k__BackingField;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000025D0 File Offset: 0x000007D0
		public DemolishableParticleControllerSpec()
		{
		}
	}
}
