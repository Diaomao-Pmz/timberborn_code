using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Autosaving
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	public class AutosaverSpec : ComponentSpec, IEquatable<AutosaverSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000024C3 File Offset: 0x000006C3
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(AutosaverSpec);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000024CF File Offset: 0x000006CF
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000024D7 File Offset: 0x000006D7
		[Serialize]
		public int AutosavesPerSettlement { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000024E0 File Offset: 0x000006E0
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000024E8 File Offset: 0x000006E8
		[Serialize]
		public float FrequencyInMinutes { get; set; }

		// Token: 0x06000024 RID: 36 RVA: 0x000024F4 File Offset: 0x000006F4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AutosaverSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002540 File Offset: 0x00000740
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("AutosavesPerSettlement = ");
			builder.Append(this.AutosavesPerSettlement.ToString());
			builder.Append(", FrequencyInMinutes = ");
			builder.Append(this.FrequencyInMinutes.ToString());
			return true;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025B1 File Offset: 0x000007B1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(AutosaverSpec left, AutosaverSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025BD File Offset: 0x000007BD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(AutosaverSpec left, AutosaverSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025D1 File Offset: 0x000007D1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<AutosavesPerSettlement>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<FrequencyInMinutes>k__BackingField);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002607 File Offset: 0x00000807
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AutosaverSpec);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002615 File Offset: 0x00000815
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002620 File Offset: 0x00000820
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(AutosaverSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<AutosavesPerSettlement>k__BackingField, other.<AutosavesPerSettlement>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<FrequencyInMinutes>k__BackingField, other.<FrequencyInMinutes>k__BackingField));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002674 File Offset: 0x00000874
		[CompilerGenerated]
		protected AutosaverSpec(AutosaverSpec original) : base(original)
		{
			this.AutosavesPerSettlement = original.<AutosavesPerSettlement>k__BackingField;
			this.FrequencyInMinutes = original.<FrequencyInMinutes>k__BackingField;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002695 File Offset: 0x00000895
		public AutosaverSpec()
		{
		}
	}
}
