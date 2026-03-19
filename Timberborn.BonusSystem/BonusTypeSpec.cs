using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.LocalizationSerialization;
using UnityEngine;

namespace Timberborn.BonusSystem
{
	// Token: 0x0200000D RID: 13
	public class BonusTypeSpec : ComponentSpec, IEquatable<BonusTypeSpec>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000028B4 File Offset: 0x00000AB4
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BonusTypeSpec);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000028C0 File Offset: 0x00000AC0
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000028C8 File Offset: 0x00000AC8
		[Serialize]
		public string Id { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000028D1 File Offset: 0x00000AD1
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000028D9 File Offset: 0x00000AD9
		[Serialize("LocKey")]
		public LocalizedText DisplayName { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000028E2 File Offset: 0x00000AE2
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000028EA File Offset: 0x00000AEA
		[Serialize]
		public float MinimumValue { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000028F3 File Offset: 0x00000AF3
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000028FB File Offset: 0x00000AFB
		[Serialize]
		public float MaximumValue { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002904 File Offset: 0x00000B04
		// (set) Token: 0x06000049 RID: 73 RVA: 0x0000290C File Offset: 0x00000B0C
		[Serialize]
		public AssetRef<Sprite> Icon { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002915 File Offset: 0x00000B15
		// (set) Token: 0x0600004B RID: 75 RVA: 0x0000291D File Offset: 0x00000B1D
		[Serialize]
		private string LocKey { get; set; }

		// Token: 0x0600004C RID: 76 RVA: 0x00002928 File Offset: 0x00000B28
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BonusTypeSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002974 File Offset: 0x00000B74
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", DisplayName = ");
			builder.Append(this.DisplayName);
			builder.Append(", MinimumValue = ");
			builder.Append(this.MinimumValue.ToString());
			builder.Append(", MaximumValue = ");
			builder.Append(this.MaximumValue.ToString());
			builder.Append(", Icon = ");
			builder.Append(this.Icon);
			return true;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002A30 File Offset: 0x00000C30
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BonusTypeSpec left, BonusTypeSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002A3C File Offset: 0x00000C3C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BonusTypeSpec left, BonusTypeSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A50 File Offset: 0x00000C50
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinimumValue>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaximumValue>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Sprite>>.Default.GetHashCode(this.<Icon>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LocKey>k__BackingField);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002AED File Offset: 0x00000CED
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BonusTypeSpec);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002399 File Offset: 0x00000599
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002AFC File Offset: 0x00000CFC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BonusTypeSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MinimumValue>k__BackingField, other.<MinimumValue>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaximumValue>k__BackingField, other.<MaximumValue>k__BackingField) && EqualityComparer<AssetRef<Sprite>>.Default.Equals(this.<Icon>k__BackingField, other.<Icon>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LocKey>k__BackingField, other.<LocKey>k__BackingField));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002BB8 File Offset: 0x00000DB8
		[CompilerGenerated]
		protected BonusTypeSpec([Nullable(1)] BonusTypeSpec original) : base(original)
		{
			this.Id = original.<Id>k__BackingField;
			this.DisplayName = original.<DisplayName>k__BackingField;
			this.MinimumValue = original.<MinimumValue>k__BackingField;
			this.MaximumValue = original.<MaximumValue>k__BackingField;
			this.Icon = original.<Icon>k__BackingField;
			this.LocKey = original.<LocKey>k__BackingField;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002419 File Offset: 0x00000619
		public BonusTypeSpec()
		{
		}
	}
}
