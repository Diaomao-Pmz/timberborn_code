using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Buildings
{
	// Token: 0x0200001E RID: 30
	public class FireSpec : ComponentSpec, IEquatable<FireSpec>
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000040A0 File Offset: 0x000022A0
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(FireSpec);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000040AC File Offset: 0x000022AC
		// (set) Token: 0x060000FE RID: 254 RVA: 0x000040B4 File Offset: 0x000022B4
		[Serialize]
		public string AttachmentId { get; set; }

		// Token: 0x060000FF RID: 255 RVA: 0x000040C0 File Offset: 0x000022C0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FireSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000410C File Offset: 0x0000230C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("AttachmentId = ");
			builder.Append(this.AttachmentId);
			return true;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000413D File Offset: 0x0000233D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FireSpec left, FireSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004149 File Offset: 0x00002349
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FireSpec left, FireSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000415D File Offset: 0x0000235D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AttachmentId>k__BackingField);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000417C File Offset: 0x0000237C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FireSpec);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000023ED File Offset: 0x000005ED
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000418A File Offset: 0x0000238A
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FireSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<AttachmentId>k__BackingField, other.<AttachmentId>k__BackingField));
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000041BB File Offset: 0x000023BB
		[CompilerGenerated]
		protected FireSpec([Nullable(1)] FireSpec original) : base(original)
		{
			this.AttachmentId = original.<AttachmentId>k__BackingField;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000246D File Offset: 0x0000066D
		public FireSpec()
		{
		}
	}
}
