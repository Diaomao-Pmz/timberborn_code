using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BlockingSystem
{
	// Token: 0x0200000B RID: 11
	public class BlockableObjectParticleControllerSpec : ComponentSpec, IEquatable<BlockableObjectParticleControllerSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000250D File Offset: 0x0000070D
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BlockableObjectParticleControllerSpec);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002519 File Offset: 0x00000719
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002521 File Offset: 0x00000721
		[Serialize]
		public ImmutableArray<string> AttachmentIds { get; set; }

		// Token: 0x0600002E RID: 46 RVA: 0x0000252C File Offset: 0x0000072C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockableObjectParticleControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002578 File Offset: 0x00000778
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

		// Token: 0x06000030 RID: 48 RVA: 0x000025C2 File Offset: 0x000007C2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockableObjectParticleControllerSpec left, BlockableObjectParticleControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000025CE File Offset: 0x000007CE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockableObjectParticleControllerSpec left, BlockableObjectParticleControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000025E2 File Offset: 0x000007E2
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<AttachmentIds>k__BackingField);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002601 File Offset: 0x00000801
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockableObjectParticleControllerSpec);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000023F7 File Offset: 0x000005F7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000260F File Offset: 0x0000080F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockableObjectParticleControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<AttachmentIds>k__BackingField, other.<AttachmentIds>k__BackingField));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002640 File Offset: 0x00000840
		[CompilerGenerated]
		protected BlockableObjectParticleControllerSpec([Nullable(1)] BlockableObjectParticleControllerSpec original) : base(original)
		{
			this.AttachmentIds = original.<AttachmentIds>k__BackingField;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002420 File Offset: 0x00000620
		public BlockableObjectParticleControllerSpec()
		{
		}
	}
}
