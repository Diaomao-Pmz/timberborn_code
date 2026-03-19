using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.WaterObjects
{
	// Token: 0x0200000A RID: 10
	public class FinishableHorizontalWaterObstacleSpec : ComponentSpec, IEquatable<FinishableHorizontalWaterObstacleSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000022DC File Offset: 0x000004DC
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(FinishableHorizontalWaterObstacleSpec);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000022E8 File Offset: 0x000004E8
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000022F0 File Offset: 0x000004F0
		[Serialize]
		public ImmutableArray<Vector3Int> Obstacles { get; set; }

		// Token: 0x06000022 RID: 34 RVA: 0x000022FC File Offset: 0x000004FC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FinishableHorizontalWaterObstacleSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002348 File Offset: 0x00000548
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Obstacles = ");
			builder.Append(this.Obstacles.ToString());
			return true;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002392 File Offset: 0x00000592
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FinishableHorizontalWaterObstacleSpec left, FinishableHorizontalWaterObstacleSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000239E File Offset: 0x0000059E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FinishableHorizontalWaterObstacleSpec left, FinishableHorizontalWaterObstacleSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023B2 File Offset: 0x000005B2
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<Vector3Int>>.Default.GetHashCode(this.<Obstacles>k__BackingField);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000023D1 File Offset: 0x000005D1
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FinishableHorizontalWaterObstacleSpec);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002267 File Offset: 0x00000467
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000023DF File Offset: 0x000005DF
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FinishableHorizontalWaterObstacleSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<Vector3Int>>.Default.Equals(this.<Obstacles>k__BackingField, other.<Obstacles>k__BackingField));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002410 File Offset: 0x00000610
		[CompilerGenerated]
		protected FinishableHorizontalWaterObstacleSpec([Nullable(1)] FinishableHorizontalWaterObstacleSpec original) : base(original)
		{
			this.Obstacles = original.<Obstacles>k__BackingField;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002290 File Offset: 0x00000490
		public FinishableHorizontalWaterObstacleSpec()
		{
		}
	}
}
