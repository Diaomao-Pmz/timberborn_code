using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ZiplineMovementSystem
{
	// Token: 0x02000009 RID: 9
	public class ZiplineHarnessModelSpec : ComponentSpec, IEquatable<ZiplineHarnessModelSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002417 File Offset: 0x00000617
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ZiplineHarnessModelSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002423 File Offset: 0x00000623
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000242B File Offset: 0x0000062B
		[Serialize]
		public string AttachmentId { get; set; }

		// Token: 0x06000016 RID: 22 RVA: 0x00002434 File Offset: 0x00000634
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ZiplineHarnessModelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002480 File Offset: 0x00000680
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

		// Token: 0x06000018 RID: 24 RVA: 0x000024B1 File Offset: 0x000006B1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ZiplineHarnessModelSpec left, ZiplineHarnessModelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024BD File Offset: 0x000006BD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ZiplineHarnessModelSpec left, ZiplineHarnessModelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024D1 File Offset: 0x000006D1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AttachmentId>k__BackingField);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024F0 File Offset: 0x000006F0
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ZiplineHarnessModelSpec);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024FE File Offset: 0x000006FE
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002507 File Offset: 0x00000707
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ZiplineHarnessModelSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<AttachmentId>k__BackingField, other.<AttachmentId>k__BackingField));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002538 File Offset: 0x00000738
		[CompilerGenerated]
		protected ZiplineHarnessModelSpec([Nullable(1)] ZiplineHarnessModelSpec original) : base(original)
		{
			this.AttachmentId = original.<AttachmentId>k__BackingField;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000254D File Offset: 0x0000074D
		public ZiplineHarnessModelSpec()
		{
		}
	}
}
