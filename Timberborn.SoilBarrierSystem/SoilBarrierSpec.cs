using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.SoilBarrierSystem
{
	// Token: 0x0200000C RID: 12
	[NullableContext(1)]
	[Nullable(0)]
	public class SoilBarrierSpec : ComponentSpec, IEquatable<SoilBarrierSpec>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000025EC File Offset: 0x000007EC
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SoilBarrierSpec);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000025F8 File Offset: 0x000007F8
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002600 File Offset: 0x00000800
		[Serialize]
		public bool BlockAboveMoisture { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002609 File Offset: 0x00000809
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002611 File Offset: 0x00000811
		[Serialize]
		public bool BlockFullMoisture { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000261A File Offset: 0x0000081A
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002622 File Offset: 0x00000822
		[Serialize]
		public bool BlockContamination { get; set; }

		// Token: 0x0600002E RID: 46 RVA: 0x0000262C File Offset: 0x0000082C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SoilBarrierSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002678 File Offset: 0x00000878
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("BlockAboveMoisture = ");
			builder.Append(this.BlockAboveMoisture.ToString());
			builder.Append(", BlockFullMoisture = ");
			builder.Append(this.BlockFullMoisture.ToString());
			builder.Append(", BlockContamination = ");
			builder.Append(this.BlockContamination.ToString());
			return true;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002710 File Offset: 0x00000910
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SoilBarrierSpec left, SoilBarrierSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000271C File Offset: 0x0000091C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SoilBarrierSpec left, SoilBarrierSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002730 File Offset: 0x00000930
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<BlockAboveMoisture>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<BlockFullMoisture>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<BlockContamination>k__BackingField);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002788 File Offset: 0x00000988
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SoilBarrierSpec);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002796 File Offset: 0x00000996
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027A0 File Offset: 0x000009A0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SoilBarrierSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<bool>.Default.Equals(this.<BlockAboveMoisture>k__BackingField, other.<BlockAboveMoisture>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<BlockFullMoisture>k__BackingField, other.<BlockFullMoisture>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<BlockContamination>k__BackingField, other.<BlockContamination>k__BackingField));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000280C File Offset: 0x00000A0C
		[CompilerGenerated]
		protected SoilBarrierSpec(SoilBarrierSpec original) : base(original)
		{
			this.BlockAboveMoisture = original.<BlockAboveMoisture>k__BackingField;
			this.BlockFullMoisture = original.<BlockFullMoisture>k__BackingField;
			this.BlockContamination = original.<BlockContamination>k__BackingField;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002839 File Offset: 0x00000A39
		public SoilBarrierSpec()
		{
		}
	}
}
