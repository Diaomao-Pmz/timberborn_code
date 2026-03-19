using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Buildings
{
	// Token: 0x02000010 RID: 16
	public class BuildingModelSpec : ComponentSpec, IEquatable<BuildingModelSpec>
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002CDD File Offset: 0x00000EDD
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BuildingModelSpec);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002CE9 File Offset: 0x00000EE9
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002CF1 File Offset: 0x00000EF1
		[Serialize]
		public string FinishedModelName { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002CFA File Offset: 0x00000EFA
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002D02 File Offset: 0x00000F02
		[Serialize]
		public string UnfinishedModelName { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002D0B File Offset: 0x00000F0B
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002D13 File Offset: 0x00000F13
		[Serialize]
		public string FinishedUncoveredModelName { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002D1C File Offset: 0x00000F1C
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002D24 File Offset: 0x00000F24
		[Serialize]
		public string UndergroundModelName { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002D2D File Offset: 0x00000F2D
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00002D35 File Offset: 0x00000F35
		[Serialize]
		public ConstructionModeModel ConstructionModeModel { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002D3E File Offset: 0x00000F3E
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002D46 File Offset: 0x00000F46
		[Serialize]
		public int UndergroundModelDepth { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002D4F File Offset: 0x00000F4F
		public bool UnfinishedConstructionModeModel
		{
			get
			{
				return this.ConstructionModeModel == ConstructionModeModel.Unfinished;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002D5C File Offset: 0x00000F5C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BuildingModelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002DA8 File Offset: 0x00000FA8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("FinishedModelName = ");
			builder.Append(this.FinishedModelName);
			builder.Append(", UnfinishedModelName = ");
			builder.Append(this.UnfinishedModelName);
			builder.Append(", FinishedUncoveredModelName = ");
			builder.Append(this.FinishedUncoveredModelName);
			builder.Append(", UndergroundModelName = ");
			builder.Append(this.UndergroundModelName);
			builder.Append(", ConstructionModeModel = ");
			builder.Append(this.ConstructionModeModel.ToString());
			builder.Append(", UndergroundModelDepth = ");
			builder.Append(this.UndergroundModelDepth.ToString());
			builder.Append(", UnfinishedConstructionModeModel = ");
			builder.Append(this.UnfinishedConstructionModeModel.ToString());
			return true;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002EA4 File Offset: 0x000010A4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BuildingModelSpec left, BuildingModelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002EB0 File Offset: 0x000010B0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BuildingModelSpec left, BuildingModelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002EC4 File Offset: 0x000010C4
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FinishedModelName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UnfinishedModelName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FinishedUncoveredModelName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UndergroundModelName>k__BackingField)) * -1521134295 + EqualityComparer<ConstructionModeModel>.Default.GetHashCode(this.<ConstructionModeModel>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<UndergroundModelDepth>k__BackingField);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002F61 File Offset: 0x00001161
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BuildingModelSpec);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000023ED File Offset: 0x000005ED
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002F70 File Offset: 0x00001170
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BuildingModelSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<FinishedModelName>k__BackingField, other.<FinishedModelName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UnfinishedModelName>k__BackingField, other.<UnfinishedModelName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FinishedUncoveredModelName>k__BackingField, other.<FinishedUncoveredModelName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UndergroundModelName>k__BackingField, other.<UndergroundModelName>k__BackingField) && EqualityComparer<ConstructionModeModel>.Default.Equals(this.<ConstructionModeModel>k__BackingField, other.<ConstructionModeModel>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<UndergroundModelDepth>k__BackingField, other.<UndergroundModelDepth>k__BackingField));
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000302C File Offset: 0x0000122C
		[CompilerGenerated]
		protected BuildingModelSpec([Nullable(1)] BuildingModelSpec original) : base(original)
		{
			this.FinishedModelName = original.<FinishedModelName>k__BackingField;
			this.UnfinishedModelName = original.<UnfinishedModelName>k__BackingField;
			this.FinishedUncoveredModelName = original.<FinishedUncoveredModelName>k__BackingField;
			this.UndergroundModelName = original.<UndergroundModelName>k__BackingField;
			this.ConstructionModeModel = original.<ConstructionModeModel>k__BackingField;
			this.UndergroundModelDepth = original.<UndergroundModelDepth>k__BackingField;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000246D File Offset: 0x0000066D
		public BuildingModelSpec()
		{
		}
	}
}
