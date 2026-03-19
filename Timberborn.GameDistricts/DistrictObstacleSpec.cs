using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000020 RID: 32
	[NullableContext(1)]
	[Nullable(0)]
	public class DistrictObstacleSpec : ComponentSpec, IEquatable<DistrictObstacleSpec>
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004208 File Offset: 0x00002408
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DistrictObstacleSpec);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004214 File Offset: 0x00002414
		// (set) Token: 0x060000EE RID: 238 RVA: 0x0000421C File Offset: 0x0000241C
		[Serialize]
		public Vector3Int CoordinateOffset { get; set; }

		// Token: 0x060000EF RID: 239 RVA: 0x00004228 File Offset: 0x00002428
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DistrictObstacleSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004274 File Offset: 0x00002474
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("CoordinateOffset = ");
			builder.Append(this.CoordinateOffset.ToString());
			return true;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000042BE File Offset: 0x000024BE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DistrictObstacleSpec left, DistrictObstacleSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000042CA File Offset: 0x000024CA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DistrictObstacleSpec left, DistrictObstacleSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000042DE File Offset: 0x000024DE
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<CoordinateOffset>k__BackingField);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000042FD File Offset: 0x000024FD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DistrictObstacleSpec);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000030CB File Offset: 0x000012CB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000430B File Offset: 0x0000250B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DistrictObstacleSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Vector3Int>.Default.Equals(this.<CoordinateOffset>k__BackingField, other.<CoordinateOffset>k__BackingField));
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000433C File Offset: 0x0000253C
		[CompilerGenerated]
		protected DistrictObstacleSpec(DistrictObstacleSpec original) : base(original)
		{
			this.CoordinateOffset = original.<CoordinateOffset>k__BackingField;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000030F4 File Offset: 0x000012F4
		public DistrictObstacleSpec()
		{
		}
	}
}
