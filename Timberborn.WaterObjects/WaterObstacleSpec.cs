using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.WaterObjects
{
	// Token: 0x02000017 RID: 23
	public class WaterObstacleSpec : ComponentSpec, IEquatable<WaterObstacleSpec>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000030A6 File Offset: 0x000012A6
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WaterObstacleSpec);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000030B2 File Offset: 0x000012B2
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000030BA File Offset: 0x000012BA
		[Serialize]
		public ImmutableArray<Vector2Int> Coordinates { get; set; }

		// Token: 0x06000094 RID: 148 RVA: 0x000030C4 File Offset: 0x000012C4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterObstacleSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003110 File Offset: 0x00001310
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Coordinates = ");
			builder.Append(this.Coordinates.ToString());
			return true;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000315A File Offset: 0x0000135A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterObstacleSpec left, WaterObstacleSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003166 File Offset: 0x00001366
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterObstacleSpec left, WaterObstacleSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000317A File Offset: 0x0000137A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<Vector2Int>>.Default.GetHashCode(this.<Coordinates>k__BackingField);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003199 File Offset: 0x00001399
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterObstacleSpec);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002267 File Offset: 0x00000467
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000031A7 File Offset: 0x000013A7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterObstacleSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<Vector2Int>>.Default.Equals(this.<Coordinates>k__BackingField, other.<Coordinates>k__BackingField));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000031D8 File Offset: 0x000013D8
		[CompilerGenerated]
		protected WaterObstacleSpec([Nullable(1)] WaterObstacleSpec original) : base(original)
		{
			this.Coordinates = original.<Coordinates>k__BackingField;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00002290 File Offset: 0x00000490
		public WaterObstacleSpec()
		{
		}
	}
}
