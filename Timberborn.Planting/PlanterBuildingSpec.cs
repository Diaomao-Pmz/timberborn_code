using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Planting
{
	// Token: 0x02000015 RID: 21
	public class PlanterBuildingSpec : ComponentSpec, IEquatable<PlanterBuildingSpec>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002B6B File Offset: 0x00000D6B
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(PlanterBuildingSpec);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002B77 File Offset: 0x00000D77
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002B7F File Offset: 0x00000D7F
		[Serialize]
		public string PlantableResourceGroup { get; set; }

		// Token: 0x06000061 RID: 97 RVA: 0x00002B88 File Offset: 0x00000D88
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PlanterBuildingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002BD4 File Offset: 0x00000DD4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("PlantableResourceGroup = ");
			builder.Append(this.PlantableResourceGroup);
			return true;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002C05 File Offset: 0x00000E05
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PlanterBuildingSpec left, PlanterBuildingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002C11 File Offset: 0x00000E11
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PlanterBuildingSpec left, PlanterBuildingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002C25 File Offset: 0x00000E25
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PlantableResourceGroup>k__BackingField);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002C44 File Offset: 0x00000E44
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlanterBuildingSpec);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000026F0 File Offset: 0x000008F0
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002C52 File Offset: 0x00000E52
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PlanterBuildingSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<PlantableResourceGroup>k__BackingField, other.<PlantableResourceGroup>k__BackingField));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002C83 File Offset: 0x00000E83
		[CompilerGenerated]
		protected PlanterBuildingSpec([Nullable(1)] PlanterBuildingSpec original) : base(original)
		{
			this.PlantableResourceGroup = original.<PlantableResourceGroup>k__BackingField;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002771 File Offset: 0x00000971
		public PlanterBuildingSpec()
		{
		}
	}
}
