using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TemplateAttachmentSystem
{
	// Token: 0x0200000C RID: 12
	public class TemplateAttachmentsSpec : ComponentSpec, IEquatable<TemplateAttachmentsSpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002876 File Offset: 0x00000A76
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(TemplateAttachmentsSpec);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002882 File Offset: 0x00000A82
		// (set) Token: 0x06000037 RID: 55 RVA: 0x0000288A File Offset: 0x00000A8A
		[Serialize]
		public ImmutableArray<AttachmentDefinition> Attachments { get; set; }

		// Token: 0x06000038 RID: 56 RVA: 0x00002894 File Offset: 0x00000A94
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TemplateAttachmentsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028E0 File Offset: 0x00000AE0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Attachments = ");
			builder.Append(this.Attachments.ToString());
			return true;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000292A File Offset: 0x00000B2A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TemplateAttachmentsSpec left, TemplateAttachmentsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002936 File Offset: 0x00000B36
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TemplateAttachmentsSpec left, TemplateAttachmentsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000294A File Offset: 0x00000B4A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<AttachmentDefinition>>.Default.GetHashCode(this.<Attachments>k__BackingField);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002969 File Offset: 0x00000B69
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TemplateAttachmentsSpec);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002977 File Offset: 0x00000B77
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002980 File Offset: 0x00000B80
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TemplateAttachmentsSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<AttachmentDefinition>>.Default.Equals(this.<Attachments>k__BackingField, other.<Attachments>k__BackingField));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000029B1 File Offset: 0x00000BB1
		[CompilerGenerated]
		protected TemplateAttachmentsSpec([Nullable(1)] TemplateAttachmentsSpec original) : base(original)
		{
			this.Attachments = original.<Attachments>k__BackingField;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029C6 File Offset: 0x00000BC6
		public TemplateAttachmentsSpec()
		{
		}
	}
}
