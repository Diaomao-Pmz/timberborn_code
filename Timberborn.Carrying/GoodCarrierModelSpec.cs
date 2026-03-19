using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Carrying
{
	// Token: 0x02000014 RID: 20
	public class GoodCarrierModelSpec : ComponentSpec, IEquatable<GoodCarrierModelSpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000342B File Offset: 0x0000162B
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(GoodCarrierModelSpec);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003437 File Offset: 0x00001637
		// (set) Token: 0x06000074 RID: 116 RVA: 0x0000343F File Offset: 0x0000163F
		[Serialize]
		public string CarriedInHandsAttachmentName { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003448 File Offset: 0x00001648
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003450 File Offset: 0x00001650
		[Serialize]
		public string BackpackAttachmentName { get; set; }

		// Token: 0x06000077 RID: 119 RVA: 0x0000345C File Offset: 0x0000165C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GoodCarrierModelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000034A8 File Offset: 0x000016A8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("CarriedInHandsAttachmentName = ");
			builder.Append(this.CarriedInHandsAttachmentName);
			builder.Append(", BackpackAttachmentName = ");
			builder.Append(this.BackpackAttachmentName);
			return true;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000034FD File Offset: 0x000016FD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GoodCarrierModelSpec left, GoodCarrierModelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003509 File Offset: 0x00001709
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GoodCarrierModelSpec left, GoodCarrierModelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000351D File Offset: 0x0000171D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CarriedInHandsAttachmentName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BackpackAttachmentName>k__BackingField);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003553 File Offset: 0x00001753
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GoodCarrierModelSpec);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003561 File Offset: 0x00001761
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000356C File Offset: 0x0000176C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GoodCarrierModelSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<CarriedInHandsAttachmentName>k__BackingField, other.<CarriedInHandsAttachmentName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<BackpackAttachmentName>k__BackingField, other.<BackpackAttachmentName>k__BackingField));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000035C0 File Offset: 0x000017C0
		[CompilerGenerated]
		protected GoodCarrierModelSpec([Nullable(1)] GoodCarrierModelSpec original) : base(original)
		{
			this.CarriedInHandsAttachmentName = original.<CarriedInHandsAttachmentName>k__BackingField;
			this.BackpackAttachmentName = original.<BackpackAttachmentName>k__BackingField;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000035E1 File Offset: 0x000017E1
		public GoodCarrierModelSpec()
		{
		}
	}
}
