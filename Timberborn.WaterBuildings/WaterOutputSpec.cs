using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200003F RID: 63
	[NullableContext(1)]
	[Nullable(0)]
	public class WaterOutputSpec : ComponentSpec, IEquatable<WaterOutputSpec>
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002DA RID: 730 RVA: 0x000084CD File Offset: 0x000066CD
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WaterOutputSpec);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002DB RID: 731 RVA: 0x000084D9 File Offset: 0x000066D9
		// (set) Token: 0x060002DC RID: 732 RVA: 0x000084E1 File Offset: 0x000066E1
		[Serialize]
		public Vector3Int WaterCoordinates { get; set; }

		// Token: 0x060002DD RID: 733 RVA: 0x000084EC File Offset: 0x000066EC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterOutputSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00008538 File Offset: 0x00006738
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

		// Token: 0x060002DF RID: 735 RVA: 0x00008582 File Offset: 0x00006782
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterOutputSpec left, WaterOutputSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000858E File Offset: 0x0000678E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterOutputSpec left, WaterOutputSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000085A2 File Offset: 0x000067A2
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<WaterCoordinates>k__BackingField);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x000085C1 File Offset: 0x000067C1
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterOutputSpec);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00002B9B File Offset: 0x00000D9B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000085CF File Offset: 0x000067CF
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterOutputSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Vector3Int>.Default.Equals(this.<WaterCoordinates>k__BackingField, other.<WaterCoordinates>k__BackingField));
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00008600 File Offset: 0x00006800
		[CompilerGenerated]
		protected WaterOutputSpec(WaterOutputSpec original) : base(original)
		{
			this.WaterCoordinates = original.<WaterCoordinates>k__BackingField;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00002CBC File Offset: 0x00000EBC
		public WaterOutputSpec()
		{
		}
	}
}
