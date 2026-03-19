using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.Ruins
{
	// Token: 0x0200000C RID: 12
	public class RuinModelFactorySpec : ComponentSpec, IEquatable<RuinModelFactorySpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002501 File Offset: 0x00000701
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(RuinModelFactorySpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000250D File Offset: 0x0000070D
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002515 File Offset: 0x00000715
		[Serialize]
		public AssetRef<GameObject> IvyDryModel { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000251E File Offset: 0x0000071E
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002526 File Offset: 0x00000726
		[Serialize]
		public AssetRef<GameObject> IvyWetModel { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000252F File Offset: 0x0000072F
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002537 File Offset: 0x00000737
		[Serialize]
		public ImmutableArray<RuinModelVariantSpec> RuinModelVariants { get; set; }

		// Token: 0x06000029 RID: 41 RVA: 0x00002540 File Offset: 0x00000740
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RuinModelFactorySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000258C File Offset: 0x0000078C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("IvyDryModel = ");
			builder.Append(this.IvyDryModel);
			builder.Append(", IvyWetModel = ");
			builder.Append(this.IvyWetModel);
			builder.Append(", RuinModelVariants = ");
			builder.Append(this.RuinModelVariants.ToString());
			return true;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002608 File Offset: 0x00000808
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RuinModelFactorySpec left, RuinModelFactorySpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002614 File Offset: 0x00000814
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RuinModelFactorySpec left, RuinModelFactorySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002628 File Offset: 0x00000828
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<IvyDryModel>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<IvyWetModel>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<RuinModelVariantSpec>>.Default.GetHashCode(this.<RuinModelVariants>k__BackingField);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002680 File Offset: 0x00000880
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RuinModelFactorySpec);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000268E File Offset: 0x0000088E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002698 File Offset: 0x00000898
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RuinModelFactorySpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<IvyDryModel>k__BackingField, other.<IvyDryModel>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<IvyWetModel>k__BackingField, other.<IvyWetModel>k__BackingField) && EqualityComparer<ImmutableArray<RuinModelVariantSpec>>.Default.Equals(this.<RuinModelVariants>k__BackingField, other.<RuinModelVariants>k__BackingField));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002704 File Offset: 0x00000904
		[CompilerGenerated]
		protected RuinModelFactorySpec([Nullable(1)] RuinModelFactorySpec original) : base(original)
		{
			this.IvyDryModel = original.<IvyDryModel>k__BackingField;
			this.IvyWetModel = original.<IvyWetModel>k__BackingField;
			this.RuinModelVariants = original.<RuinModelVariants>k__BackingField;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002731 File Offset: 0x00000931
		public RuinModelFactorySpec()
		{
		}
	}
}
