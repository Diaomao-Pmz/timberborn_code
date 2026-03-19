using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.PathSystem
{
	// Token: 0x02000010 RID: 16
	public class DynamicPathModelSpec : ComponentSpec, IEquatable<DynamicPathModelSpec>
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000032D0 File Offset: 0x000014D0
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DynamicPathModelSpec);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000032DC File Offset: 0x000014DC
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000032E4 File Offset: 0x000014E4
		[Serialize]
		public string GroundModelPrefix { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000032ED File Offset: 0x000014ED
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000032F5 File Offset: 0x000014F5
		[Serialize]
		public string RoofModelPrefix { get; set; }

		// Token: 0x06000062 RID: 98 RVA: 0x00003300 File Offset: 0x00001500
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DynamicPathModelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000334C File Offset: 0x0000154C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("GroundModelPrefix = ");
			builder.Append(this.GroundModelPrefix);
			builder.Append(", RoofModelPrefix = ");
			builder.Append(this.RoofModelPrefix);
			return true;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000033A1 File Offset: 0x000015A1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DynamicPathModelSpec left, DynamicPathModelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000033AD File Offset: 0x000015AD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DynamicPathModelSpec left, DynamicPathModelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000033C1 File Offset: 0x000015C1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GroundModelPrefix>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RoofModelPrefix>k__BackingField);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000033F7 File Offset: 0x000015F7
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DynamicPathModelSpec);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002983 File Offset: 0x00000B83
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003408 File Offset: 0x00001608
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DynamicPathModelSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<GroundModelPrefix>k__BackingField, other.<GroundModelPrefix>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RoofModelPrefix>k__BackingField, other.<RoofModelPrefix>k__BackingField));
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000345C File Offset: 0x0000165C
		[CompilerGenerated]
		protected DynamicPathModelSpec([Nullable(1)] DynamicPathModelSpec original) : base(original)
		{
			this.GroundModelPrefix = original.<GroundModelPrefix>k__BackingField;
			this.RoofModelPrefix = original.<RoofModelPrefix>k__BackingField;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public DynamicPathModelSpec()
		{
		}
	}
}
