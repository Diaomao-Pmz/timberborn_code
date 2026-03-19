using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.GameDistrictsMigration
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	public class MigrationCoordinatorSpec : ComponentSpec, IEquatable<MigrationCoordinatorSpec>
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002C60 File Offset: 0x00000E60
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(MigrationCoordinatorSpec);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002C6C File Offset: 0x00000E6C
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002C74 File Offset: 0x00000E74
		[Serialize]
		public int MaxAutomaticMigration { get; set; }

		// Token: 0x06000059 RID: 89 RVA: 0x00002C80 File Offset: 0x00000E80
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MigrationCoordinatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002CCC File Offset: 0x00000ECC
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MaxAutomaticMigration = ");
			builder.Append(this.MaxAutomaticMigration.ToString());
			return true;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002D16 File Offset: 0x00000F16
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MigrationCoordinatorSpec left, MigrationCoordinatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002D22 File Offset: 0x00000F22
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MigrationCoordinatorSpec left, MigrationCoordinatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002D36 File Offset: 0x00000F36
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxAutomaticMigration>k__BackingField);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002D55 File Offset: 0x00000F55
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MigrationCoordinatorSpec);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002D63 File Offset: 0x00000F63
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002D6C File Offset: 0x00000F6C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MigrationCoordinatorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<MaxAutomaticMigration>k__BackingField, other.<MaxAutomaticMigration>k__BackingField));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002D9D File Offset: 0x00000F9D
		[CompilerGenerated]
		protected MigrationCoordinatorSpec(MigrationCoordinatorSpec original) : base(original)
		{
			this.MaxAutomaticMigration = original.<MaxAutomaticMigration>k__BackingField;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002DB2 File Offset: 0x00000FB2
		public MigrationCoordinatorSpec()
		{
		}
	}
}
