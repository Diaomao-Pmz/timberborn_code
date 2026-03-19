using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.WaterObjects
{
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	[Nullable(0)]
	public class WaterObjectSpec : ComponentSpec, IWaterObjectSpecification, IEquatable<WaterObjectSpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002D67 File Offset: 0x00000F67
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WaterObjectSpec);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002D73 File Offset: 0x00000F73
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002D7B File Offset: 0x00000F7B
		[Serialize]
		public Vector3Int WaterCoordinates { get; set; }

		// Token: 0x06000082 RID: 130 RVA: 0x00002D84 File Offset: 0x00000F84
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterObjectSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002DD0 File Offset: 0x00000FD0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("WaterCoordinates = ");
			builder.Append(this.WaterCoordinates.ToString());
			return true;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002E1A File Offset: 0x0000101A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterObjectSpec left, WaterObjectSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002E26 File Offset: 0x00001026
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterObjectSpec left, WaterObjectSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002E3A File Offset: 0x0000103A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<WaterCoordinates>k__BackingField);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002E59 File Offset: 0x00001059
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterObjectSpec);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002267 File Offset: 0x00000467
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002E67 File Offset: 0x00001067
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterObjectSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Vector3Int>.Default.Equals(this.<WaterCoordinates>k__BackingField, other.<WaterCoordinates>k__BackingField));
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002E98 File Offset: 0x00001098
		[CompilerGenerated]
		protected WaterObjectSpec(WaterObjectSpec original) : base(original)
		{
			this.WaterCoordinates = original.<WaterCoordinates>k__BackingField;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002290 File Offset: 0x00000490
		public WaterObjectSpec()
		{
		}
	}
}
