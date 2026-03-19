using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.LocalizationSerialization;
using UnityEngine;

namespace Timberborn.Goods
{
	// Token: 0x0200000B RID: 11
	public class GoodGroupSpec : ComponentSpec, IEquatable<GoodGroupSpec>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000023D5 File Offset: 0x000005D5
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(GoodGroupSpec);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000023E1 File Offset: 0x000005E1
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000023E9 File Offset: 0x000005E9
		[Serialize]
		public string Id { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000023F2 File Offset: 0x000005F2
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000023FA File Offset: 0x000005FA
		[Serialize]
		public int Order { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002403 File Offset: 0x00000603
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000240B File Offset: 0x0000060B
		[Serialize("DisplayNameLocKey")]
		public LocalizedText DisplayName { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002414 File Offset: 0x00000614
		// (set) Token: 0x06000029 RID: 41 RVA: 0x0000241C File Offset: 0x0000061C
		[Serialize]
		public AssetRef<Sprite> Icon { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002425 File Offset: 0x00000625
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000242D File Offset: 0x0000062D
		[Serialize]
		public bool SingleResourceGroup { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002436 File Offset: 0x00000636
		// (set) Token: 0x0600002D RID: 45 RVA: 0x0000243E File Offset: 0x0000063E
		[Serialize]
		private string DisplayNameLocKey { get; set; }

		// Token: 0x0600002E RID: 46 RVA: 0x00002448 File Offset: 0x00000648
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GoodGroupSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002494 File Offset: 0x00000694
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
			builder.Append(", Order = ");
			builder.Append(this.Order.ToString());
			builder.Append(", DisplayName = ");
			builder.Append(this.DisplayName);
			builder.Append(", Icon = ");
			builder.Append(this.Icon);
			builder.Append(", SingleResourceGroup = ");
			builder.Append(this.SingleResourceGroup.ToString());
			return true;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002550 File Offset: 0x00000750
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GoodGroupSpec left, GoodGroupSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000255C File Offset: 0x0000075C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GoodGroupSpec left, GoodGroupSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002570 File Offset: 0x00000770
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Order>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Sprite>>.Default.GetHashCode(this.<Icon>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<SingleResourceGroup>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayNameLocKey>k__BackingField);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000260D File Offset: 0x0000080D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GoodGroupSpec);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000261B File Offset: 0x0000081B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002624 File Offset: 0x00000824
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GoodGroupSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Order>k__BackingField, other.<Order>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<AssetRef<Sprite>>.Default.Equals(this.<Icon>k__BackingField, other.<Icon>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<SingleResourceGroup>k__BackingField, other.<SingleResourceGroup>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayNameLocKey>k__BackingField, other.<DisplayNameLocKey>k__BackingField));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000026E0 File Offset: 0x000008E0
		[CompilerGenerated]
		protected GoodGroupSpec([Nullable(1)] GoodGroupSpec original) : base(original)
		{
			this.Id = original.<Id>k__BackingField;
			this.Order = original.<Order>k__BackingField;
			this.DisplayName = original.<DisplayName>k__BackingField;
			this.Icon = original.<Icon>k__BackingField;
			this.SingleResourceGroup = original.<SingleResourceGroup>k__BackingField;
			this.DisplayNameLocKey = original.<DisplayNameLocKey>k__BackingField;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000273C File Offset: 0x0000093C
		public GoodGroupSpec()
		{
		}
	}
}
