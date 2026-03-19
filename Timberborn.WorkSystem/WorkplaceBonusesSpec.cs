using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.BonusSystem;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000025 RID: 37
	public class WorkplaceBonusesSpec : ComponentSpec, IEquatable<WorkplaceBonusesSpec>
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000041E9 File Offset: 0x000023E9
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WorkplaceBonusesSpec);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000041F5 File Offset: 0x000023F5
		// (set) Token: 0x060000FF RID: 255 RVA: 0x000041FD File Offset: 0x000023FD
		[Serialize]
		public ImmutableArray<BonusSpec> WorkerBonuses { get; set; }

		// Token: 0x06000100 RID: 256 RVA: 0x00004208 File Offset: 0x00002408
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WorkplaceBonusesSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004254 File Offset: 0x00002454
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("WorkerBonuses = ");
			builder.Append(this.WorkerBonuses.ToString());
			return true;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000429E File Offset: 0x0000249E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WorkplaceBonusesSpec left, WorkplaceBonusesSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000042AA File Offset: 0x000024AA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WorkplaceBonusesSpec left, WorkplaceBonusesSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000042BE File Offset: 0x000024BE
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<BonusSpec>>.Default.GetHashCode(this.<WorkerBonuses>k__BackingField);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000042DD File Offset: 0x000024DD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WorkplaceBonusesSpec);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00002511 File Offset: 0x00000711
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000042EB File Offset: 0x000024EB
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WorkplaceBonusesSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<BonusSpec>>.Default.Equals(this.<WorkerBonuses>k__BackingField, other.<WorkerBonuses>k__BackingField));
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000431C File Offset: 0x0000251C
		[CompilerGenerated]
		protected WorkplaceBonusesSpec([Nullable(1)] WorkplaceBonusesSpec original) : base(original)
		{
			this.WorkerBonuses = original.<WorkerBonuses>k__BackingField;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00002591 File Offset: 0x00000791
		public WorkplaceBonusesSpec()
		{
		}
	}
}
