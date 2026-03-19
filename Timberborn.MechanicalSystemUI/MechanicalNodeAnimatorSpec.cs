using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x02000018 RID: 24
	[NullableContext(1)]
	[Nullable(0)]
	public class MechanicalNodeAnimatorSpec : ComponentSpec, IEquatable<MechanicalNodeAnimatorSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002F26 File Offset: 0x00001126
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(MechanicalNodeAnimatorSpec);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002F32 File Offset: 0x00001132
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002F3A File Offset: 0x0000113A
		[Serialize]
		public float MinSpeedMultiplier { get; set; }

		// Token: 0x06000059 RID: 89 RVA: 0x00002F44 File Offset: 0x00001144
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MechanicalNodeAnimatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002F90 File Offset: 0x00001190
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MinSpeedMultiplier = ");
			builder.Append(this.MinSpeedMultiplier.ToString());
			return true;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002FDA File Offset: 0x000011DA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MechanicalNodeAnimatorSpec left, MechanicalNodeAnimatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002FE6 File Offset: 0x000011E6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MechanicalNodeAnimatorSpec left, MechanicalNodeAnimatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002FFA File Offset: 0x000011FA
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinSpeedMultiplier>k__BackingField);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003019 File Offset: 0x00001219
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MechanicalNodeAnimatorSpec);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003027 File Offset: 0x00001227
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003030 File Offset: 0x00001230
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MechanicalNodeAnimatorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<MinSpeedMultiplier>k__BackingField, other.<MinSpeedMultiplier>k__BackingField));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003061 File Offset: 0x00001261
		[CompilerGenerated]
		protected MechanicalNodeAnimatorSpec(MechanicalNodeAnimatorSpec original) : base(original)
		{
			this.MinSpeedMultiplier = original.<MinSpeedMultiplier>k__BackingField;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003076 File Offset: 0x00001276
		public MechanicalNodeAnimatorSpec()
		{
		}
	}
}
