using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000026 RID: 38
	public class ConnectBuildingsTutorialStepSpec : ComponentSpec, IEquatable<ConnectBuildingsTutorialStepSpec>
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004427 File Offset: 0x00002627
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ConnectBuildingsTutorialStepSpec);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004433 File Offset: 0x00002633
		// (set) Token: 0x06000105 RID: 261 RVA: 0x0000443B File Offset: 0x0000263B
		[Serialize]
		public string TemplateName { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004444 File Offset: 0x00002644
		// (set) Token: 0x06000107 RID: 263 RVA: 0x0000444C File Offset: 0x0000264C
		[Serialize]
		public int RequiredAmount { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004455 File Offset: 0x00002655
		// (set) Token: 0x06000109 RID: 265 RVA: 0x0000445D File Offset: 0x0000265D
		[Serialize]
		public bool CountUnfinishedBuildings { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00004466 File Offset: 0x00002666
		// (set) Token: 0x0600010B RID: 267 RVA: 0x0000446E File Offset: 0x0000266E
		[Serialize]
		public ImmutableArray<string> HighlightableBuildingIds { get; set; }

		// Token: 0x0600010C RID: 268 RVA: 0x00004478 File Offset: 0x00002678
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ConnectBuildingsTutorialStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000044C4 File Offset: 0x000026C4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("TemplateName = ");
			builder.Append(this.TemplateName);
			builder.Append(", RequiredAmount = ");
			builder.Append(this.RequiredAmount.ToString());
			builder.Append(", CountUnfinishedBuildings = ");
			builder.Append(this.CountUnfinishedBuildings.ToString());
			builder.Append(", HighlightableBuildingIds = ");
			builder.Append(this.HighlightableBuildingIds.ToString());
			return true;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004575 File Offset: 0x00002775
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ConnectBuildingsTutorialStepSpec left, ConnectBuildingsTutorialStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004581 File Offset: 0x00002781
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ConnectBuildingsTutorialStepSpec left, ConnectBuildingsTutorialStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004598 File Offset: 0x00002798
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TemplateName>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<RequiredAmount>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<CountUnfinishedBuildings>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<HighlightableBuildingIds>k__BackingField);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004607 File Offset: 0x00002807
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ConnectBuildingsTutorialStepSpec);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004618 File Offset: 0x00002818
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ConnectBuildingsTutorialStepSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<TemplateName>k__BackingField, other.<TemplateName>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<RequiredAmount>k__BackingField, other.<RequiredAmount>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<CountUnfinishedBuildings>k__BackingField, other.<CountUnfinishedBuildings>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<HighlightableBuildingIds>k__BackingField, other.<HighlightableBuildingIds>k__BackingField));
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000469C File Offset: 0x0000289C
		[CompilerGenerated]
		protected ConnectBuildingsTutorialStepSpec([Nullable(1)] ConnectBuildingsTutorialStepSpec original) : base(original)
		{
			this.TemplateName = original.<TemplateName>k__BackingField;
			this.RequiredAmount = original.<RequiredAmount>k__BackingField;
			this.CountUnfinishedBuildings = original.<CountUnfinishedBuildings>k__BackingField;
			this.HighlightableBuildingIds = original.<HighlightableBuildingIds>k__BackingField;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000239D File Offset: 0x0000059D
		public ConnectBuildingsTutorialStepSpec()
		{
		}
	}
}
