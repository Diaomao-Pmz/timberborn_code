using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Demolishing
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	public class DemolishableSpec : ComponentSpec, IEquatable<DemolishableSpec>
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002CE5 File Offset: 0x00000EE5
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DemolishableSpec);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002CF1 File Offset: 0x00000EF1
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002CF9 File Offset: 0x00000EF9
		[Serialize]
		public float DemolishTimeInHours { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002D02 File Offset: 0x00000F02
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002D0A File Offset: 0x00000F0A
		[Serialize]
		public bool ShowDemolishButtonInEntityPanel { get; set; }

		// Token: 0x06000071 RID: 113 RVA: 0x00002D14 File Offset: 0x00000F14
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DemolishableSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002D60 File Offset: 0x00000F60
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DemolishTimeInHours = ");
			builder.Append(this.DemolishTimeInHours.ToString());
			builder.Append(", ShowDemolishButtonInEntityPanel = ");
			builder.Append(this.ShowDemolishButtonInEntityPanel.ToString());
			return true;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002DD1 File Offset: 0x00000FD1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DemolishableSpec left, DemolishableSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002DDD File Offset: 0x00000FDD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DemolishableSpec left, DemolishableSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002DF1 File Offset: 0x00000FF1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DemolishTimeInHours>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ShowDemolishButtonInEntityPanel>k__BackingField);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002E27 File Offset: 0x00001027
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DemolishableSpec);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000025A7 File Offset: 0x000007A7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002E38 File Offset: 0x00001038
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DemolishableSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<DemolishTimeInHours>k__BackingField, other.<DemolishTimeInHours>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<ShowDemolishButtonInEntityPanel>k__BackingField, other.<ShowDemolishButtonInEntityPanel>k__BackingField));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002E8C File Offset: 0x0000108C
		[CompilerGenerated]
		protected DemolishableSpec(DemolishableSpec original) : base(original)
		{
			this.DemolishTimeInHours = original.<DemolishTimeInHours>k__BackingField;
			this.ShowDemolishButtonInEntityPanel = original.<ShowDemolishButtonInEntityPanel>k__BackingField;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000025D0 File Offset: 0x000007D0
		public DemolishableSpec()
		{
		}
	}
}
