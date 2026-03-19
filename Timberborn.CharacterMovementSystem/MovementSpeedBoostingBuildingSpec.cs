using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.CharacterMovementSystem
{
	// Token: 0x02000011 RID: 17
	[NullableContext(1)]
	[Nullable(0)]
	public class MovementSpeedBoostingBuildingSpec : ComponentSpec, IEquatable<MovementSpeedBoostingBuildingSpec>
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002FC5 File Offset: 0x000011C5
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(MovementSpeedBoostingBuildingSpec);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002FD1 File Offset: 0x000011D1
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002FD9 File Offset: 0x000011D9
		[Serialize]
		public int BoostPercentage { get; set; }

		// Token: 0x0600006B RID: 107 RVA: 0x00002FE4 File Offset: 0x000011E4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MovementSpeedBoostingBuildingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003030 File Offset: 0x00001230
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("BoostPercentage = ");
			builder.Append(this.BoostPercentage.ToString());
			return true;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000307A File Offset: 0x0000127A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MovementSpeedBoostingBuildingSpec left, MovementSpeedBoostingBuildingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003086 File Offset: 0x00001286
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MovementSpeedBoostingBuildingSpec left, MovementSpeedBoostingBuildingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000309A File Offset: 0x0000129A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<BoostPercentage>k__BackingField);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000030B9 File Offset: 0x000012B9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MovementSpeedBoostingBuildingSpec);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002F3F File Offset: 0x0000113F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000030C7 File Offset: 0x000012C7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MovementSpeedBoostingBuildingSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<BoostPercentage>k__BackingField, other.<BoostPercentage>k__BackingField));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000030F8 File Offset: 0x000012F8
		[CompilerGenerated]
		protected MovementSpeedBoostingBuildingSpec(MovementSpeedBoostingBuildingSpec original) : base(original)
		{
			this.BoostPercentage = original.<BoostPercentage>k__BackingField;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002F8E File Offset: 0x0000118E
		public MovementSpeedBoostingBuildingSpec()
		{
		}
	}
}
