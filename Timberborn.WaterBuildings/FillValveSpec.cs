using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class FillValveSpec : ComponentSpec, IEquatable<FillValveSpec>
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002901 File Offset: 0x00000B01
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(FillValveSpec);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000290D File Offset: 0x00000B0D
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002915 File Offset: 0x00000B15
		[Serialize]
		public bool DefaultTargetHeightEnabled { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000291E File Offset: 0x00000B1E
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002926 File Offset: 0x00000B26
		[Serialize]
		public float DefaultTargetHeightOffset { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000292F File Offset: 0x00000B2F
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002937 File Offset: 0x00000B37
		[Serialize]
		public bool DefaultAutomationTargetHeightEnabled { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002940 File Offset: 0x00000B40
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002948 File Offset: 0x00000B48
		[Serialize]
		public float DefaultAutomationTargetHeightOffset { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002951 File Offset: 0x00000B51
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002959 File Offset: 0x00000B59
		[Serialize]
		public float OverflowLimit { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002962 File Offset: 0x00000B62
		// (set) Token: 0x06000049 RID: 73 RVA: 0x0000296A File Offset: 0x00000B6A
		[Serialize]
		public Vector3Int OutputCoordinates { get; set; }

		// Token: 0x0600004A RID: 74 RVA: 0x00002974 File Offset: 0x00000B74
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FillValveSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000029C0 File Offset: 0x00000BC0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DefaultTargetHeightEnabled = ");
			builder.Append(this.DefaultTargetHeightEnabled.ToString());
			builder.Append(", DefaultTargetHeightOffset = ");
			builder.Append(this.DefaultTargetHeightOffset.ToString());
			builder.Append(", DefaultAutomationTargetHeightEnabled = ");
			builder.Append(this.DefaultAutomationTargetHeightEnabled.ToString());
			builder.Append(", DefaultAutomationTargetHeightOffset = ");
			builder.Append(this.DefaultAutomationTargetHeightOffset.ToString());
			builder.Append(", OverflowLimit = ");
			builder.Append(this.OverflowLimit.ToString());
			builder.Append(", OutputCoordinates = ");
			builder.Append(this.OutputCoordinates.ToString());
			return true;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002ACD File Offset: 0x00000CCD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FillValveSpec left, FillValveSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002AD9 File Offset: 0x00000CD9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FillValveSpec left, FillValveSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002AF0 File Offset: 0x00000CF0
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((base.GetHashCode() * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<DefaultTargetHeightEnabled>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DefaultTargetHeightOffset>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<DefaultAutomationTargetHeightEnabled>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DefaultAutomationTargetHeightOffset>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<OverflowLimit>k__BackingField)) * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<OutputCoordinates>k__BackingField);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B8D File Offset: 0x00000D8D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FillValveSpec);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B9B File Offset: 0x00000D9B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002BA4 File Offset: 0x00000DA4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FillValveSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<bool>.Default.Equals(this.<DefaultTargetHeightEnabled>k__BackingField, other.<DefaultTargetHeightEnabled>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DefaultTargetHeightOffset>k__BackingField, other.<DefaultTargetHeightOffset>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<DefaultAutomationTargetHeightEnabled>k__BackingField, other.<DefaultAutomationTargetHeightEnabled>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DefaultAutomationTargetHeightOffset>k__BackingField, other.<DefaultAutomationTargetHeightOffset>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<OverflowLimit>k__BackingField, other.<OverflowLimit>k__BackingField) && EqualityComparer<Vector3Int>.Default.Equals(this.<OutputCoordinates>k__BackingField, other.<OutputCoordinates>k__BackingField));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002C60 File Offset: 0x00000E60
		[CompilerGenerated]
		protected FillValveSpec(FillValveSpec original) : base(original)
		{
			this.DefaultTargetHeightEnabled = original.<DefaultTargetHeightEnabled>k__BackingField;
			this.DefaultTargetHeightOffset = original.<DefaultTargetHeightOffset>k__BackingField;
			this.DefaultAutomationTargetHeightEnabled = original.<DefaultAutomationTargetHeightEnabled>k__BackingField;
			this.DefaultAutomationTargetHeightOffset = original.<DefaultAutomationTargetHeightOffset>k__BackingField;
			this.OverflowLimit = original.<OverflowLimit>k__BackingField;
			this.OutputCoordinates = original.<OutputCoordinates>k__BackingField;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002CBC File Offset: 0x00000EBC
		public FillValveSpec()
		{
		}
	}
}
