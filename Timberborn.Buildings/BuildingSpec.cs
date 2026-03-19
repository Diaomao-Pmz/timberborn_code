using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.Goods;

namespace Timberborn.Buildings
{
	// Token: 0x02000019 RID: 25
	public class BuildingSpec : ComponentSpec, IEquatable<BuildingSpec>
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003793 File Offset: 0x00001993
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BuildingSpec);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000379F File Offset: 0x0000199F
		// (set) Token: 0x060000BB RID: 187 RVA: 0x000037A7 File Offset: 0x000019A7
		[Serialize]
		public string SelectionSoundName { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000037B0 File Offset: 0x000019B0
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000037B8 File Offset: 0x000019B8
		[Serialize]
		public string LoopingSoundName { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000037C1 File Offset: 0x000019C1
		// (set) Token: 0x060000BF RID: 191 RVA: 0x000037C9 File Offset: 0x000019C9
		[Serialize]
		public ImmutableArray<GoodAmountSpec> BuildingCost { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000037D2 File Offset: 0x000019D2
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000037DA File Offset: 0x000019DA
		[Serialize]
		public int ScienceCost { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000037E3 File Offset: 0x000019E3
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000037EB File Offset: 0x000019EB
		[Serialize]
		public bool PlaceFinished { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000037F4 File Offset: 0x000019F4
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000037FC File Offset: 0x000019FC
		[Serialize]
		public bool FinishableWithBeaversOnSite { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003805 File Offset: 0x00001A05
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x0000380D File Offset: 0x00001A0D
		[Serialize]
		public bool DrawRangeBoundsOnIt { get; set; }

		// Token: 0x060000C8 RID: 200 RVA: 0x00003818 File Offset: 0x00001A18
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BuildingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003864 File Offset: 0x00001A64
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("SelectionSoundName = ");
			builder.Append(this.SelectionSoundName);
			builder.Append(", LoopingSoundName = ");
			builder.Append(this.LoopingSoundName);
			builder.Append(", BuildingCost = ");
			builder.Append(this.BuildingCost.ToString());
			builder.Append(", ScienceCost = ");
			builder.Append(this.ScienceCost.ToString());
			builder.Append(", PlaceFinished = ");
			builder.Append(this.PlaceFinished.ToString());
			builder.Append(", FinishableWithBeaversOnSite = ");
			builder.Append(this.FinishableWithBeaversOnSite.ToString());
			builder.Append(", DrawRangeBoundsOnIt = ");
			builder.Append(this.DrawRangeBoundsOnIt.ToString());
			return true;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000397C File Offset: 0x00001B7C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BuildingSpec left, BuildingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003988 File Offset: 0x00001B88
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BuildingSpec left, BuildingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000399C File Offset: 0x00001B9C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SelectionSoundName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LoopingSoundName>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<GoodAmountSpec>>.Default.GetHashCode(this.<BuildingCost>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ScienceCost>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<PlaceFinished>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<FinishableWithBeaversOnSite>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<DrawRangeBoundsOnIt>k__BackingField);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003A50 File Offset: 0x00001C50
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BuildingSpec);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000023ED File Offset: 0x000005ED
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003A60 File Offset: 0x00001C60
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BuildingSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<SelectionSoundName>k__BackingField, other.<SelectionSoundName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LoopingSoundName>k__BackingField, other.<LoopingSoundName>k__BackingField) && EqualityComparer<ImmutableArray<GoodAmountSpec>>.Default.Equals(this.<BuildingCost>k__BackingField, other.<BuildingCost>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ScienceCost>k__BackingField, other.<ScienceCost>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<PlaceFinished>k__BackingField, other.<PlaceFinished>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<FinishableWithBeaversOnSite>k__BackingField, other.<FinishableWithBeaversOnSite>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<DrawRangeBoundsOnIt>k__BackingField, other.<DrawRangeBoundsOnIt>k__BackingField));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003B38 File Offset: 0x00001D38
		[CompilerGenerated]
		protected BuildingSpec([Nullable(1)] BuildingSpec original) : base(original)
		{
			this.SelectionSoundName = original.<SelectionSoundName>k__BackingField;
			this.LoopingSoundName = original.<LoopingSoundName>k__BackingField;
			this.BuildingCost = original.<BuildingCost>k__BackingField;
			this.ScienceCost = original.<ScienceCost>k__BackingField;
			this.PlaceFinished = original.<PlaceFinished>k__BackingField;
			this.FinishableWithBeaversOnSite = original.<FinishableWithBeaversOnSite>k__BackingField;
			this.DrawRangeBoundsOnIt = original.<DrawRangeBoundsOnIt>k__BackingField;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003BA0 File Offset: 0x00001DA0
		public BuildingSpec()
		{
			this.SelectionSoundName = "Default";
			base..ctor();
		}
	}
}
