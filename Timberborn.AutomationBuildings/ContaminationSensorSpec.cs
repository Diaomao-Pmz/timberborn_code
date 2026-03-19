using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200000C RID: 12
	[NullableContext(1)]
	[Nullable(0)]
	public class ContaminationSensorSpec : ComponentSpec, IEquatable<ContaminationSensorSpec>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002A22 File Offset: 0x00000C22
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ContaminationSensorSpec);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002A2E File Offset: 0x00000C2E
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002A36 File Offset: 0x00000C36
		[Serialize]
		public Vector3Int SensorCoordinates { get; set; }

		// Token: 0x06000041 RID: 65 RVA: 0x00002A40 File Offset: 0x00000C40
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ContaminationSensorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002A8C File Offset: 0x00000C8C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("SensorCoordinates = ");
			builder.Append(this.SensorCoordinates.ToString());
			return true;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002AD6 File Offset: 0x00000CD6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ContaminationSensorSpec left, ContaminationSensorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002AE2 File Offset: 0x00000CE2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ContaminationSensorSpec left, ContaminationSensorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002AF6 File Offset: 0x00000CF6
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<SensorCoordinates>k__BackingField);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B15 File Offset: 0x00000D15
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ContaminationSensorSpec);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B23 File Offset: 0x00000D23
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ContaminationSensorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Vector3Int>.Default.Equals(this.<SensorCoordinates>k__BackingField, other.<SensorCoordinates>k__BackingField));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B54 File Offset: 0x00000D54
		[CompilerGenerated]
		protected ContaminationSensorSpec(ContaminationSensorSpec original) : base(original)
		{
			this.SensorCoordinates = original.<SensorCoordinates>k__BackingField;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002778 File Offset: 0x00000978
		public ContaminationSensorSpec()
		{
		}
	}
}
