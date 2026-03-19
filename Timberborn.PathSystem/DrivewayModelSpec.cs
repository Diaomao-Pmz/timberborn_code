using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.PathSystem
{
	// Token: 0x0200000D RID: 13
	[NullableContext(1)]
	[Nullable(0)]
	public class DrivewayModelSpec : ComponentSpec, IEquatable<DrivewayModelSpec>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002AAC File Offset: 0x00000CAC
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DrivewayModelSpec);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002AB8 File Offset: 0x00000CB8
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002AC0 File Offset: 0x00000CC0
		[Serialize]
		public Driveway Driveway { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002AC9 File Offset: 0x00000CC9
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002AD1 File Offset: 0x00000CD1
		[Serialize]
		public bool HasCustomCoordinates { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002ADA File Offset: 0x00000CDA
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002AE2 File Offset: 0x00000CE2
		[Serialize]
		public Vector3Int CustomCoordinates { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002AEB File Offset: 0x00000CEB
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002AF3 File Offset: 0x00000CF3
		[Serialize]
		public Direction2D CustomDirection { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002AFC File Offset: 0x00000CFC
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002B04 File Offset: 0x00000D04
		[Serialize]
		public DrivewayMode DrivewayMode { get; set; }

		// Token: 0x06000042 RID: 66 RVA: 0x00002B10 File Offset: 0x00000D10
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DrivewayModelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B5C File Offset: 0x00000D5C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Driveway = ");
			builder.Append(this.Driveway.ToString());
			builder.Append(", HasCustomCoordinates = ");
			builder.Append(this.HasCustomCoordinates.ToString());
			builder.Append(", CustomCoordinates = ");
			builder.Append(this.CustomCoordinates.ToString());
			builder.Append(", CustomDirection = ");
			builder.Append(this.CustomDirection.ToString());
			builder.Append(", DrivewayMode = ");
			builder.Append(this.DrivewayMode.ToString());
			return true;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002C43 File Offset: 0x00000E43
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DrivewayModelSpec left, DrivewayModelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002C4F File Offset: 0x00000E4F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DrivewayModelSpec left, DrivewayModelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002C64 File Offset: 0x00000E64
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((base.GetHashCode() * -1521134295 + EqualityComparer<Driveway>.Default.GetHashCode(this.<Driveway>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<HasCustomCoordinates>k__BackingField)) * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<CustomCoordinates>k__BackingField)) * -1521134295 + EqualityComparer<Direction2D>.Default.GetHashCode(this.<CustomDirection>k__BackingField)) * -1521134295 + EqualityComparer<DrivewayMode>.Default.GetHashCode(this.<DrivewayMode>k__BackingField);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002CEA File Offset: 0x00000EEA
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DrivewayModelSpec);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002983 File Offset: 0x00000B83
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002CF8 File Offset: 0x00000EF8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DrivewayModelSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Driveway>.Default.Equals(this.<Driveway>k__BackingField, other.<Driveway>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<HasCustomCoordinates>k__BackingField, other.<HasCustomCoordinates>k__BackingField) && EqualityComparer<Vector3Int>.Default.Equals(this.<CustomCoordinates>k__BackingField, other.<CustomCoordinates>k__BackingField) && EqualityComparer<Direction2D>.Default.Equals(this.<CustomDirection>k__BackingField, other.<CustomDirection>k__BackingField) && EqualityComparer<DrivewayMode>.Default.Equals(this.<DrivewayMode>k__BackingField, other.<DrivewayMode>k__BackingField));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002D98 File Offset: 0x00000F98
		[CompilerGenerated]
		protected DrivewayModelSpec(DrivewayModelSpec original) : base(original)
		{
			this.Driveway = original.<Driveway>k__BackingField;
			this.HasCustomCoordinates = original.<HasCustomCoordinates>k__BackingField;
			this.CustomCoordinates = original.<CustomCoordinates>k__BackingField;
			this.CustomDirection = original.<CustomDirection>k__BackingField;
			this.DrivewayMode = original.<DrivewayMode>k__BackingField;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public DrivewayModelSpec()
		{
		}
	}
}
