using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.SoilMoistureSystem
{
	// Token: 0x0200000A RID: 10
	public class DryObjectModelSpec : ComponentSpec, IEquatable<DryObjectModelSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002594 File Offset: 0x00000794
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DryObjectModelSpec);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000025A0 File Offset: 0x000007A0
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000025A8 File Offset: 0x000007A8
		[Serialize]
		public string WetModelName { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000025B1 File Offset: 0x000007B1
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000025B9 File Offset: 0x000007B9
		[Serialize]
		public string DryModelName { get; set; }

		// Token: 0x06000024 RID: 36 RVA: 0x000025C4 File Offset: 0x000007C4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DryObjectModelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002610 File Offset: 0x00000810
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("WetModelName = ");
			builder.Append(this.WetModelName);
			builder.Append(", DryModelName = ");
			builder.Append(this.DryModelName);
			return true;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002665 File Offset: 0x00000865
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DryObjectModelSpec left, DryObjectModelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002671 File Offset: 0x00000871
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DryObjectModelSpec left, DryObjectModelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002685 File Offset: 0x00000885
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WetModelName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DryModelName>k__BackingField);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026BB File Offset: 0x000008BB
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DryObjectModelSpec);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026C9 File Offset: 0x000008C9
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026D4 File Offset: 0x000008D4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DryObjectModelSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<WetModelName>k__BackingField, other.<WetModelName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DryModelName>k__BackingField, other.<DryModelName>k__BackingField));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002728 File Offset: 0x00000928
		[CompilerGenerated]
		protected DryObjectModelSpec([Nullable(1)] DryObjectModelSpec original) : base(original)
		{
			this.WetModelName = original.<WetModelName>k__BackingField;
			this.DryModelName = original.<DryModelName>k__BackingField;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002749 File Offset: 0x00000949
		public DryObjectModelSpec()
		{
		}
	}
}
