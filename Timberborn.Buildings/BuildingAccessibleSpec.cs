using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.Buildings
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class BuildingAccessibleSpec : ComponentSpec, IEquatable<BuildingAccessibleSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002274 File Offset: 0x00000474
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BuildingAccessibleSpec);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002280 File Offset: 0x00000480
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002288 File Offset: 0x00000488
		[Serialize]
		public Vector3 LocalAccess { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002291 File Offset: 0x00000491
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002299 File Offset: 0x00000499
		[Serialize]
		public bool ForceOneFinalAccess { get; set; }

		// Token: 0x06000020 RID: 32 RVA: 0x000022A4 File Offset: 0x000004A4
		[NullableContext(0)]
		public Vector3 CalculateAccessFromLocalAccess(BlockObject blockObject)
		{
			Vector3 coordinates = CoordinateSystem.WorldToGrid(this.LocalAccess);
			return CoordinateSystem.GridToWorld(blockObject.TransformCoordinates(coordinates));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000022CC File Offset: 0x000004CC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BuildingAccessibleSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002318 File Offset: 0x00000518
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("LocalAccess = ");
			builder.Append(this.LocalAccess.ToString());
			builder.Append(", ForceOneFinalAccess = ");
			builder.Append(this.ForceOneFinalAccess.ToString());
			return true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002389 File Offset: 0x00000589
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BuildingAccessibleSpec left, BuildingAccessibleSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002395 File Offset: 0x00000595
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BuildingAccessibleSpec left, BuildingAccessibleSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023A9 File Offset: 0x000005A9
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<LocalAccess>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ForceOneFinalAccess>k__BackingField);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023DF File Offset: 0x000005DF
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BuildingAccessibleSpec);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000023ED File Offset: 0x000005ED
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000023F8 File Offset: 0x000005F8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BuildingAccessibleSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Vector3>.Default.Equals(this.<LocalAccess>k__BackingField, other.<LocalAccess>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<ForceOneFinalAccess>k__BackingField, other.<ForceOneFinalAccess>k__BackingField));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000244C File Offset: 0x0000064C
		[CompilerGenerated]
		protected BuildingAccessibleSpec(BuildingAccessibleSpec original) : base(original)
		{
			this.LocalAccess = original.<LocalAccess>k__BackingField;
			this.ForceOneFinalAccess = original.<ForceOneFinalAccess>k__BackingField;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000246D File Offset: 0x0000066D
		public BuildingAccessibleSpec()
		{
		}
	}
}
