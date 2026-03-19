using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Rendering
{
	// Token: 0x02000014 RID: 20
	public class MapBottomGroundCutoffSpec : ComponentSpec, IEquatable<MapBottomGroundCutoffSpec>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002D8C File Offset: 0x00000F8C
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(MapBottomGroundCutoffSpec);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002D98 File Offset: 0x00000F98
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002DA0 File Offset: 0x00000FA0
		[Serialize]
		public ImmutableArray<string> Targets { get; set; }

		// Token: 0x0600005E RID: 94 RVA: 0x00002DAC File Offset: 0x00000FAC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MapBottomGroundCutoffSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002DF8 File Offset: 0x00000FF8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Targets = ");
			builder.Append(this.Targets.ToString());
			return true;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002E42 File Offset: 0x00001042
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MapBottomGroundCutoffSpec left, MapBottomGroundCutoffSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002E4E File Offset: 0x0000104E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MapBottomGroundCutoffSpec left, MapBottomGroundCutoffSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002E62 File Offset: 0x00001062
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<Targets>k__BackingField);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002E81 File Offset: 0x00001081
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MapBottomGroundCutoffSpec);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002675 File Offset: 0x00000875
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002E8F File Offset: 0x0000108F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MapBottomGroundCutoffSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<Targets>k__BackingField, other.<Targets>k__BackingField));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002EC0 File Offset: 0x000010C0
		[CompilerGenerated]
		protected MapBottomGroundCutoffSpec([Nullable(1)] MapBottomGroundCutoffSpec original) : base(original)
		{
			this.Targets = original.<Targets>k__BackingField;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000026F5 File Offset: 0x000008F5
		public MapBottomGroundCutoffSpec()
		{
		}
	}
}
