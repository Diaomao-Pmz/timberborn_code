using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WindSystem
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	public class WindServiceSpec : ComponentSpec, IEquatable<WindServiceSpec>
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002E28 File Offset: 0x00001028
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WindServiceSpec);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002E34 File Offset: 0x00001034
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002E3C File Offset: 0x0000103C
		[Serialize]
		public float MinWindTimeInHours { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002E45 File Offset: 0x00001045
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002E4D File Offset: 0x0000104D
		[Serialize]
		public float MaxWindTimeInHours { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002E56 File Offset: 0x00001056
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002E5E File Offset: 0x0000105E
		[Serialize]
		public float MinWindStrength { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002E67 File Offset: 0x00001067
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002E6F File Offset: 0x0000106F
		[Serialize]
		public float MaxWindStrength { get; set; }

		// Token: 0x06000085 RID: 133 RVA: 0x00002E78 File Offset: 0x00001078
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WindServiceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002EC4 File Offset: 0x000010C4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MinWindTimeInHours = ");
			builder.Append(this.MinWindTimeInHours.ToString());
			builder.Append(", MaxWindTimeInHours = ");
			builder.Append(this.MaxWindTimeInHours.ToString());
			builder.Append(", MinWindStrength = ");
			builder.Append(this.MinWindStrength.ToString());
			builder.Append(", MaxWindStrength = ");
			builder.Append(this.MaxWindStrength.ToString());
			return true;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002F83 File Offset: 0x00001183
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WindServiceSpec left, WindServiceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002F8F File Offset: 0x0000118F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WindServiceSpec left, WindServiceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002FA4 File Offset: 0x000011A4
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinWindTimeInHours>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxWindTimeInHours>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinWindStrength>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxWindStrength>k__BackingField);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003013 File Offset: 0x00001213
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WindServiceSpec);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002263 File Offset: 0x00000463
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003024 File Offset: 0x00001224
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WindServiceSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<MinWindTimeInHours>k__BackingField, other.<MinWindTimeInHours>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxWindTimeInHours>k__BackingField, other.<MaxWindTimeInHours>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MinWindStrength>k__BackingField, other.<MinWindStrength>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxWindStrength>k__BackingField, other.<MaxWindStrength>k__BackingField));
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000030A8 File Offset: 0x000012A8
		[CompilerGenerated]
		protected WindServiceSpec(WindServiceSpec original) : base(original)
		{
			this.MinWindTimeInHours = original.<MinWindTimeInHours>k__BackingField;
			this.MaxWindTimeInHours = original.<MaxWindTimeInHours>k__BackingField;
			this.MinWindStrength = original.<MinWindStrength>k__BackingField;
			this.MaxWindStrength = original.<MaxWindStrength>k__BackingField;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000228C File Offset: 0x0000048C
		public WindServiceSpec()
		{
		}
	}
}
