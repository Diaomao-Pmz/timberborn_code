using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Demolishing
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	public class DemolishableScienceRewardSpec : ComponentSpec, IEquatable<DemolishableScienceRewardSpec>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002B9D File Offset: 0x00000D9D
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DemolishableScienceRewardSpec);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002BA9 File Offset: 0x00000DA9
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002BB1 File Offset: 0x00000DB1
		[Serialize]
		public int SciencePoints { get; set; }

		// Token: 0x06000061 RID: 97 RVA: 0x00002BBC File Offset: 0x00000DBC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DemolishableScienceRewardSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002C08 File Offset: 0x00000E08
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("SciencePoints = ");
			builder.Append(this.SciencePoints.ToString());
			return true;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002C52 File Offset: 0x00000E52
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DemolishableScienceRewardSpec left, DemolishableScienceRewardSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002C5E File Offset: 0x00000E5E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DemolishableScienceRewardSpec left, DemolishableScienceRewardSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002C72 File Offset: 0x00000E72
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<SciencePoints>k__BackingField);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002C91 File Offset: 0x00000E91
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DemolishableScienceRewardSpec);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000025A7 File Offset: 0x000007A7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002C9F File Offset: 0x00000E9F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DemolishableScienceRewardSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<SciencePoints>k__BackingField, other.<SciencePoints>k__BackingField));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002CD0 File Offset: 0x00000ED0
		[CompilerGenerated]
		protected DemolishableScienceRewardSpec(DemolishableScienceRewardSpec original) : base(original)
		{
			this.SciencePoints = original.<SciencePoints>k__BackingField;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000025D0 File Offset: 0x000007D0
		public DemolishableScienceRewardSpec()
		{
		}
	}
}
