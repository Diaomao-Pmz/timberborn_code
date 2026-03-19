using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.AreaSelectionSystem
{
	// Token: 0x0200001D RID: 29
	[NullableContext(1)]
	[Nullable(0)]
	public class AreaPickersSpec : ComponentSpec, IEquatable<AreaPickersSpec>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000321B File Offset: 0x0000141B
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(AreaPickersSpec);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003227 File Offset: 0x00001427
		// (set) Token: 0x06000071 RID: 113 RVA: 0x0000322F File Offset: 0x0000142F
		[Serialize]
		public int AreaMaxBlocks { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003238 File Offset: 0x00001438
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00003240 File Offset: 0x00001440
		[Serialize]
		public int SculptingMaxBlocks { get; set; }

		// Token: 0x06000074 RID: 116 RVA: 0x0000324C File Offset: 0x0000144C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AreaPickersSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003298 File Offset: 0x00001498
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("AreaMaxBlocks = ");
			builder.Append(this.AreaMaxBlocks.ToString());
			builder.Append(", SculptingMaxBlocks = ");
			builder.Append(this.SculptingMaxBlocks.ToString());
			return true;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003309 File Offset: 0x00001509
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(AreaPickersSpec left, AreaPickersSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003315 File Offset: 0x00001515
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(AreaPickersSpec left, AreaPickersSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003329 File Offset: 0x00001529
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<AreaMaxBlocks>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<SculptingMaxBlocks>k__BackingField);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000335F File Offset: 0x0000155F
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AreaPickersSpec);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000336D File Offset: 0x0000156D
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003378 File Offset: 0x00001578
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(AreaPickersSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<AreaMaxBlocks>k__BackingField, other.<AreaMaxBlocks>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<SculptingMaxBlocks>k__BackingField, other.<SculptingMaxBlocks>k__BackingField));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000033CC File Offset: 0x000015CC
		[CompilerGenerated]
		protected AreaPickersSpec(AreaPickersSpec original) : base(original)
		{
			this.AreaMaxBlocks = original.<AreaMaxBlocks>k__BackingField;
			this.SculptingMaxBlocks = original.<SculptingMaxBlocks>k__BackingField;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000033ED File Offset: 0x000015ED
		public AreaPickersSpec()
		{
		}
	}
}
