using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000038 RID: 56
	public class WaterMoverParticleControllerSpec : ComponentSpec, IEquatable<WaterMoverParticleControllerSpec>
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00007D9E File Offset: 0x00005F9E
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WaterMoverParticleControllerSpec);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00007DAA File Offset: 0x00005FAA
		// (set) Token: 0x06000291 RID: 657 RVA: 0x00007DB2 File Offset: 0x00005FB2
		[Serialize]
		public ImmutableArray<string> AttachmentIds { get; set; }

		// Token: 0x06000292 RID: 658 RVA: 0x00007DBC File Offset: 0x00005FBC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterMoverParticleControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00007E08 File Offset: 0x00006008
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

		// Token: 0x06000294 RID: 660 RVA: 0x00007E52 File Offset: 0x00006052
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterMoverParticleControllerSpec left, WaterMoverParticleControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00007E5E File Offset: 0x0000605E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterMoverParticleControllerSpec left, WaterMoverParticleControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00007E72 File Offset: 0x00006072
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<AttachmentIds>k__BackingField);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00007E91 File Offset: 0x00006091
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterMoverParticleControllerSpec);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00002B9B File Offset: 0x00000D9B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00007E9F File Offset: 0x0000609F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterMoverParticleControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<AttachmentIds>k__BackingField, other.<AttachmentIds>k__BackingField));
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00007ED0 File Offset: 0x000060D0
		[CompilerGenerated]
		protected WaterMoverParticleControllerSpec([Nullable(1)] WaterMoverParticleControllerSpec original) : base(original)
		{
			this.AttachmentIds = original.<AttachmentIds>k__BackingField;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00002CBC File Offset: 0x00000EBC
		public WaterMoverParticleControllerSpec()
		{
		}
	}
}
