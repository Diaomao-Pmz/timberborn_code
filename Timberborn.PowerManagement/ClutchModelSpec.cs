using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.PowerManagement
{
	// Token: 0x0200000A RID: 10
	public class ClutchModelSpec : ComponentSpec, IEquatable<ClutchModelSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002546 File Offset: 0x00000746
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ClutchModelSpec);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002552 File Offset: 0x00000752
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000255A File Offset: 0x0000075A
		[Serialize]
		public string EngagedModelName { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002563 File Offset: 0x00000763
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000256B File Offset: 0x0000076B
		[Serialize]
		public string DisengagedModelName { get; set; }

		// Token: 0x06000029 RID: 41 RVA: 0x00002574 File Offset: 0x00000774
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ClutchModelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025C0 File Offset: 0x000007C0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("EngagedModelName = ");
			builder.Append(this.EngagedModelName);
			builder.Append(", DisengagedModelName = ");
			builder.Append(this.DisengagedModelName);
			return true;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002615 File Offset: 0x00000815
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ClutchModelSpec left, ClutchModelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002621 File Offset: 0x00000821
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ClutchModelSpec left, ClutchModelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002635 File Offset: 0x00000835
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<EngagedModelName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisengagedModelName>k__BackingField);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000266B File Offset: 0x0000086B
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ClutchModelSpec);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002679 File Offset: 0x00000879
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002684 File Offset: 0x00000884
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ClutchModelSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<EngagedModelName>k__BackingField, other.<EngagedModelName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisengagedModelName>k__BackingField, other.<DisengagedModelName>k__BackingField));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000026D8 File Offset: 0x000008D8
		[CompilerGenerated]
		protected ClutchModelSpec([Nullable(1)] ClutchModelSpec original) : base(original)
		{
			this.EngagedModelName = original.<EngagedModelName>k__BackingField;
			this.DisengagedModelName = original.<DisengagedModelName>k__BackingField;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000026F9 File Offset: 0x000008F9
		public ClutchModelSpec()
		{
		}
	}
}
