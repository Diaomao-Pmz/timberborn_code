using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000011 RID: 17
	public class BuildingTutorialStepSpec : ComponentSpec, IEquatable<BuildingTutorialStepSpec>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000028CB File Offset: 0x00000ACB
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BuildingTutorialStepSpec);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000028D7 File Offset: 0x00000AD7
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000028DF File Offset: 0x00000ADF
		[Serialize]
		public ImmutableArray<string> TemplateNames { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000028E8 File Offset: 0x00000AE8
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000028F0 File Offset: 0x00000AF0
		[Serialize]
		public bool OnlyFinishedBuildings { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000028F9 File Offset: 0x00000AF9
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002901 File Offset: 0x00000B01
		[Serialize]
		public int RequiredAmount { get; set; }

		// Token: 0x0600004B RID: 75 RVA: 0x0000290C File Offset: 0x00000B0C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BuildingTutorialStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002958 File Offset: 0x00000B58
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("TemplateNames = ");
			builder.Append(this.TemplateNames.ToString());
			builder.Append(", OnlyFinishedBuildings = ");
			builder.Append(this.OnlyFinishedBuildings.ToString());
			builder.Append(", RequiredAmount = ");
			builder.Append(this.RequiredAmount.ToString());
			return true;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000029F0 File Offset: 0x00000BF0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BuildingTutorialStepSpec left, BuildingTutorialStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000029FC File Offset: 0x00000BFC
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BuildingTutorialStepSpec left, BuildingTutorialStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002A10 File Offset: 0x00000C10
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<TemplateNames>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<OnlyFinishedBuildings>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<RequiredAmount>k__BackingField);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A68 File Offset: 0x00000C68
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BuildingTutorialStepSpec);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A78 File Offset: 0x00000C78
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BuildingTutorialStepSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<TemplateNames>k__BackingField, other.<TemplateNames>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<OnlyFinishedBuildings>k__BackingField, other.<OnlyFinishedBuildings>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<RequiredAmount>k__BackingField, other.<RequiredAmount>k__BackingField));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002AE4 File Offset: 0x00000CE4
		[CompilerGenerated]
		protected BuildingTutorialStepSpec([Nullable(1)] BuildingTutorialStepSpec original) : base(original)
		{
			this.TemplateNames = original.<TemplateNames>k__BackingField;
			this.OnlyFinishedBuildings = original.<OnlyFinishedBuildings>k__BackingField;
			this.RequiredAmount = original.<RequiredAmount>k__BackingField;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000239D File Offset: 0x0000059D
		public BuildingTutorialStepSpec()
		{
		}
	}
}
