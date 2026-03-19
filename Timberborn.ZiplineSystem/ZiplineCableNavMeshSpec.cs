using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x02000010 RID: 16
	[NullableContext(1)]
	[Nullable(0)]
	public class ZiplineCableNavMeshSpec : ComponentSpec, IEquatable<ZiplineCableNavMeshSpec>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002D57 File Offset: 0x00000F57
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ZiplineCableNavMeshSpec);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002D63 File Offset: 0x00000F63
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002D6B File Offset: 0x00000F6B
		[Serialize]
		public float CableUnitCost { get; set; }

		// Token: 0x0600005F RID: 95 RVA: 0x00002D74 File Offset: 0x00000F74
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ZiplineCableNavMeshSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002DC0 File Offset: 0x00000FC0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("CableUnitCost = ");
			builder.Append(this.CableUnitCost.ToString());
			return true;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002E0A File Offset: 0x0000100A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ZiplineCableNavMeshSpec left, ZiplineCableNavMeshSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002E16 File Offset: 0x00001016
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ZiplineCableNavMeshSpec left, ZiplineCableNavMeshSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002E2A File Offset: 0x0000102A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<CableUnitCost>k__BackingField);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002E49 File Offset: 0x00001049
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ZiplineCableNavMeshSpec);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002363 File Offset: 0x00000563
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002E57 File Offset: 0x00001057
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ZiplineCableNavMeshSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<CableUnitCost>k__BackingField, other.<CableUnitCost>k__BackingField));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E88 File Offset: 0x00001088
		[CompilerGenerated]
		protected ZiplineCableNavMeshSpec(ZiplineCableNavMeshSpec original) : base(original)
		{
			this.CableUnitCost = original.<CableUnitCost>k__BackingField;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000238C File Offset: 0x0000058C
		public ZiplineCableNavMeshSpec()
		{
		}
	}
}
