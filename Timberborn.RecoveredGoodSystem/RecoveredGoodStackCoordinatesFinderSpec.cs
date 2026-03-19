using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.RecoveredGoodSystem
{
	// Token: 0x02000011 RID: 17
	[NullableContext(1)]
	[Nullable(0)]
	public class RecoveredGoodStackCoordinatesFinderSpec : ComponentSpec, IEquatable<RecoveredGoodStackCoordinatesFinderSpec>
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002E17 File Offset: 0x00001017
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(RecoveredGoodStackCoordinatesFinderSpec);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002E23 File Offset: 0x00001023
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002E2B File Offset: 0x0000102B
		[Serialize]
		public int NeighboursRange { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002E34 File Offset: 0x00001034
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002E3C File Offset: 0x0000103C
		[Serialize]
		public int MaxUpperSearch { get; set; }

		// Token: 0x0600005C RID: 92 RVA: 0x00002E48 File Offset: 0x00001048
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RecoveredGoodStackCoordinatesFinderSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E94 File Offset: 0x00001094
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("NeighboursRange = ");
			builder.Append(this.NeighboursRange.ToString());
			builder.Append(", MaxUpperSearch = ");
			builder.Append(this.MaxUpperSearch.ToString());
			return true;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F05 File Offset: 0x00001105
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RecoveredGoodStackCoordinatesFinderSpec left, RecoveredGoodStackCoordinatesFinderSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F11 File Offset: 0x00001111
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RecoveredGoodStackCoordinatesFinderSpec left, RecoveredGoodStackCoordinatesFinderSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002F25 File Offset: 0x00001125
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<NeighboursRange>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxUpperSearch>k__BackingField);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002F5B File Offset: 0x0000115B
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RecoveredGoodStackCoordinatesFinderSpec);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002F69 File Offset: 0x00001169
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002F74 File Offset: 0x00001174
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RecoveredGoodStackCoordinatesFinderSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<NeighboursRange>k__BackingField, other.<NeighboursRange>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MaxUpperSearch>k__BackingField, other.<MaxUpperSearch>k__BackingField));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002FC8 File Offset: 0x000011C8
		[CompilerGenerated]
		protected RecoveredGoodStackCoordinatesFinderSpec(RecoveredGoodStackCoordinatesFinderSpec original) : base(original)
		{
			this.NeighboursRange = original.<NeighboursRange>k__BackingField;
			this.MaxUpperSearch = original.<MaxUpperSearch>k__BackingField;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002FE9 File Offset: 0x000011E9
		public RecoveredGoodStackCoordinatesFinderSpec()
		{
		}
	}
}
