using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Demolishing
{
	// Token: 0x0200000B RID: 11
	public class DemolishableParticle : IEquatable<DemolishableParticle>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000025EF File Offset: 0x000007EF
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DemolishableParticle);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000025FB File Offset: 0x000007FB
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002603 File Offset: 0x00000803
		[Serialize]
		public string AttachmentId { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000260C File Offset: 0x0000080C
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002614 File Offset: 0x00000814
		[Serialize]
		public ImmutableArray<string> TemplateNames { get; set; }

		// Token: 0x06000034 RID: 52 RVA: 0x00002620 File Offset: 0x00000820
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DemolishableParticle");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000266C File Offset: 0x0000086C
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("AttachmentId = ");
			builder.Append(this.AttachmentId);
			builder.Append(", TemplateNames = ");
			builder.Append(this.TemplateNames.ToString());
			return true;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000026BF File Offset: 0x000008BF
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DemolishableParticle left, DemolishableParticle right)
		{
			return !(left == right);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000026CB File Offset: 0x000008CB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DemolishableParticle left, DemolishableParticle right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000026DF File Offset: 0x000008DF
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AttachmentId>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<TemplateNames>k__BackingField);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000271F File Offset: 0x0000091F
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DemolishableParticle);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002730 File Offset: 0x00000930
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DemolishableParticle other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<AttachmentId>k__BackingField, other.<AttachmentId>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<TemplateNames>k__BackingField, other.<TemplateNames>k__BackingField));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002791 File Offset: 0x00000991
		[CompilerGenerated]
		protected DemolishableParticle([Nullable(1)] DemolishableParticle original)
		{
			this.AttachmentId = original.<AttachmentId>k__BackingField;
			this.TemplateNames = original.<TemplateNames>k__BackingField;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000020F8 File Offset: 0x000002F8
		public DemolishableParticle()
		{
		}
	}
}
