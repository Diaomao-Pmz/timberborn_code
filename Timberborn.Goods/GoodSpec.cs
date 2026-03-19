using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.LocalizationSerialization;
using Timberborn.NeedSpecs;
using Timberborn.SpriteOperations;
using UnityEngine;

namespace Timberborn.Goods
{
	// Token: 0x02000018 RID: 24
	public class GoodSpec : ComponentSpec, IEquatable<GoodSpec>
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003226 File Offset: 0x00001426
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(GoodSpec);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003232 File Offset: 0x00001432
		// (set) Token: 0x06000083 RID: 131 RVA: 0x0000323A File Offset: 0x0000143A
		[Serialize]
		public string Id { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003243 File Offset: 0x00001443
		// (set) Token: 0x06000085 RID: 133 RVA: 0x0000324B File Offset: 0x0000144B
		[Serialize]
		public ImmutableArray<string> BackwardCompatibleIds { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003254 File Offset: 0x00001454
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000325C File Offset: 0x0000145C
		[Serialize("DisplayNameLocKey")]
		public LocalizedText DisplayName { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003265 File Offset: 0x00001465
		// (set) Token: 0x06000089 RID: 137 RVA: 0x0000326D File Offset: 0x0000146D
		[Serialize("PluralDisplayNameLocKey")]
		public LocalizedText PluralDisplayName { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003276 File Offset: 0x00001476
		// (set) Token: 0x0600008B RID: 139 RVA: 0x0000327E File Offset: 0x0000147E
		[Serialize]
		public ImmutableArray<InstantEffectSpec> ConsumptionEffects { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003287 File Offset: 0x00001487
		// (set) Token: 0x0600008D RID: 141 RVA: 0x0000328F File Offset: 0x0000148F
		[Serialize]
		public string GoodType { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003298 File Offset: 0x00001498
		// (set) Token: 0x0600008F RID: 143 RVA: 0x000032A0 File Offset: 0x000014A0
		[Serialize]
		public string StockpileVisualization { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000032A9 File Offset: 0x000014A9
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000032B1 File Offset: 0x000014B1
		[Serialize]
		public VisibleContainer VisibleContainer { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000032BA File Offset: 0x000014BA
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000032C2 File Offset: 0x000014C2
		[Serialize]
		public Color ContainerColor { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000032CB File Offset: 0x000014CB
		// (set) Token: 0x06000095 RID: 149 RVA: 0x000032D3 File Offset: 0x000014D3
		[Serialize]
		public AssetRef<Material> ContainerMaterial { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000032DC File Offset: 0x000014DC
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000032E4 File Offset: 0x000014E4
		[Serialize]
		public string CarryingAnimation { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000032ED File Offset: 0x000014ED
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000032F5 File Offset: 0x000014F5
		[Serialize]
		public int Weight { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000032FE File Offset: 0x000014FE
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00003306 File Offset: 0x00001506
		[Serialize]
		public string GoodGroupId { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000330F File Offset: 0x0000150F
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00003317 File Offset: 0x00001517
		[Serialize]
		public int GoodOrder { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003320 File Offset: 0x00001520
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00003328 File Offset: 0x00001528
		[Serialize]
		public AssetRef<Sprite> Icon { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003331 File Offset: 0x00001531
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003339 File Offset: 0x00001539
		[Serialize("Icon")]
		public FlippedSprite IconFlipped { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003342 File Offset: 0x00001542
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x0000334A File Offset: 0x0000154A
		[Serialize("Icon")]
		public UISprite IconSmall { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003353 File Offset: 0x00001553
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x0000335B File Offset: 0x0000155B
		[Serialize]
		public bool ForceImport { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003364 File Offset: 0x00001564
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x0000336C File Offset: 0x0000156C
		[Serialize]
		private string DisplayNameLocKey { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003375 File Offset: 0x00001575
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x0000337D File Offset: 0x0000157D
		[Serialize]
		private string PluralDisplayNameLocKey { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003386 File Offset: 0x00001586
		public bool HasConsumptionEffects
		{
			get
			{
				return !this.ConsumptionEffects.IsEmpty<InstantEffectSpec>();
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000339B File Offset: 0x0000159B
		public override string ToString()
		{
			return this.Id;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000033A4 File Offset: 0x000015A4
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
			builder.Append(", BackwardCompatibleIds = ");
			builder.Append(this.BackwardCompatibleIds.ToString());
			builder.Append(", DisplayName = ");
			builder.Append(this.DisplayName);
			builder.Append(", PluralDisplayName = ");
			builder.Append(this.PluralDisplayName);
			builder.Append(", ConsumptionEffects = ");
			builder.Append(this.ConsumptionEffects.ToString());
			builder.Append(", GoodType = ");
			builder.Append(this.GoodType);
			builder.Append(", StockpileVisualization = ");
			builder.Append(this.StockpileVisualization);
			builder.Append(", VisibleContainer = ");
			builder.Append(this.VisibleContainer.ToString());
			builder.Append(", ContainerColor = ");
			builder.Append(this.ContainerColor.ToString());
			builder.Append(", ContainerMaterial = ");
			builder.Append(this.ContainerMaterial);
			builder.Append(", CarryingAnimation = ");
			builder.Append(this.CarryingAnimation);
			builder.Append(", Weight = ");
			builder.Append(this.Weight.ToString());
			builder.Append(", GoodGroupId = ");
			builder.Append(this.GoodGroupId);
			builder.Append(", GoodOrder = ");
			builder.Append(this.GoodOrder.ToString());
			builder.Append(", Icon = ");
			builder.Append(this.Icon);
			builder.Append(", IconFlipped = ");
			builder.Append(this.IconFlipped);
			builder.Append(", IconSmall = ");
			builder.Append(this.IconSmall);
			builder.Append(", ForceImport = ");
			builder.Append(this.ForceImport.ToString());
			builder.Append(", HasConsumptionEffects = ");
			builder.Append(this.HasConsumptionEffects.ToString());
			return true;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003616 File Offset: 0x00001816
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GoodSpec left, GoodSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003622 File Offset: 0x00001822
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GoodSpec left, GoodSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003638 File Offset: 0x00001838
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((((((((((((((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<BackwardCompatibleIds>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<PluralDisplayName>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<InstantEffectSpec>>.Default.GetHashCode(this.<ConsumptionEffects>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GoodType>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<StockpileVisualization>k__BackingField)) * -1521134295 + EqualityComparer<VisibleContainer>.Default.GetHashCode(this.<VisibleContainer>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ContainerColor>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Material>>.Default.GetHashCode(this.<ContainerMaterial>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CarryingAnimation>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Weight>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GoodGroupId>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<GoodOrder>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Sprite>>.Default.GetHashCode(this.<Icon>k__BackingField)) * -1521134295 + EqualityComparer<FlippedSprite>.Default.GetHashCode(this.<IconFlipped>k__BackingField)) * -1521134295 + EqualityComparer<UISprite>.Default.GetHashCode(this.<IconSmall>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ForceImport>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayNameLocKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PluralDisplayNameLocKey>k__BackingField);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003817 File Offset: 0x00001A17
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GoodSpec);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000261B File Offset: 0x0000081B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003828 File Offset: 0x00001A28
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GoodSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<BackwardCompatibleIds>k__BackingField, other.<BackwardCompatibleIds>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<PluralDisplayName>k__BackingField, other.<PluralDisplayName>k__BackingField) && EqualityComparer<ImmutableArray<InstantEffectSpec>>.Default.Equals(this.<ConsumptionEffects>k__BackingField, other.<ConsumptionEffects>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<GoodType>k__BackingField, other.<GoodType>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<StockpileVisualization>k__BackingField, other.<StockpileVisualization>k__BackingField) && EqualityComparer<VisibleContainer>.Default.Equals(this.<VisibleContainer>k__BackingField, other.<VisibleContainer>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ContainerColor>k__BackingField, other.<ContainerColor>k__BackingField) && EqualityComparer<AssetRef<Material>>.Default.Equals(this.<ContainerMaterial>k__BackingField, other.<ContainerMaterial>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CarryingAnimation>k__BackingField, other.<CarryingAnimation>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Weight>k__BackingField, other.<Weight>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<GoodGroupId>k__BackingField, other.<GoodGroupId>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<GoodOrder>k__BackingField, other.<GoodOrder>k__BackingField) && EqualityComparer<AssetRef<Sprite>>.Default.Equals(this.<Icon>k__BackingField, other.<Icon>k__BackingField) && EqualityComparer<FlippedSprite>.Default.Equals(this.<IconFlipped>k__BackingField, other.<IconFlipped>k__BackingField) && EqualityComparer<UISprite>.Default.Equals(this.<IconSmall>k__BackingField, other.<IconSmall>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<ForceImport>k__BackingField, other.<ForceImport>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayNameLocKey>k__BackingField, other.<DisplayNameLocKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<PluralDisplayNameLocKey>k__BackingField, other.<PluralDisplayNameLocKey>k__BackingField));
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003A5C File Offset: 0x00001C5C
		[CompilerGenerated]
		protected GoodSpec([Nullable(1)] GoodSpec original) : base(original)
		{
			this.Id = original.<Id>k__BackingField;
			this.BackwardCompatibleIds = original.<BackwardCompatibleIds>k__BackingField;
			this.DisplayName = original.<DisplayName>k__BackingField;
			this.PluralDisplayName = original.<PluralDisplayName>k__BackingField;
			this.ConsumptionEffects = original.<ConsumptionEffects>k__BackingField;
			this.GoodType = original.<GoodType>k__BackingField;
			this.StockpileVisualization = original.<StockpileVisualization>k__BackingField;
			this.VisibleContainer = original.<VisibleContainer>k__BackingField;
			this.ContainerColor = original.<ContainerColor>k__BackingField;
			this.ContainerMaterial = original.<ContainerMaterial>k__BackingField;
			this.CarryingAnimation = original.<CarryingAnimation>k__BackingField;
			this.Weight = original.<Weight>k__BackingField;
			this.GoodGroupId = original.<GoodGroupId>k__BackingField;
			this.GoodOrder = original.<GoodOrder>k__BackingField;
			this.Icon = original.<Icon>k__BackingField;
			this.IconFlipped = original.<IconFlipped>k__BackingField;
			this.IconSmall = original.<IconSmall>k__BackingField;
			this.ForceImport = original.<ForceImport>k__BackingField;
			this.DisplayNameLocKey = original.<DisplayNameLocKey>k__BackingField;
			this.PluralDisplayNameLocKey = original.<PluralDisplayNameLocKey>k__BackingField;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000273C File Offset: 0x0000093C
		public GoodSpec()
		{
		}
	}
}
