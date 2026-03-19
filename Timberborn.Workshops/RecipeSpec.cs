using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.Goods;
using Timberborn.SpriteOperations;
using UnityEngine;

namespace Timberborn.Workshops
{
	// Token: 0x02000025 RID: 37
	public class RecipeSpec : ComponentSpec, IEquatable<RecipeSpec>
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004A68 File Offset: 0x00002C68
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(RecipeSpec);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004A74 File Offset: 0x00002C74
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00004A7C File Offset: 0x00002C7C
		[Serialize]
		public string Id { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004A85 File Offset: 0x00002C85
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00004A8D File Offset: 0x00002C8D
		[Serialize]
		public ImmutableArray<string> BackwardCompatibleIds { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004A96 File Offset: 0x00002C96
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00004A9E File Offset: 0x00002C9E
		[Serialize]
		public string DisplayLocKey { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00004AA7 File Offset: 0x00002CA7
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00004AAF File Offset: 0x00002CAF
		[Serialize]
		public float CycleDurationInHours { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00004AB8 File Offset: 0x00002CB8
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00004AC0 File Offset: 0x00002CC0
		[Serialize]
		public ImmutableArray<GoodAmountSpec> Ingredients { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00004AC9 File Offset: 0x00002CC9
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00004AD1 File Offset: 0x00002CD1
		[Serialize]
		public ImmutableArray<GoodAmountSpec> Products { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00004ADA File Offset: 0x00002CDA
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00004AE2 File Offset: 0x00002CE2
		[Serialize]
		public int ProducedSciencePoints { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00004AEB File Offset: 0x00002CEB
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00004AF3 File Offset: 0x00002CF3
		[Serialize]
		public string Fuel { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00004AFC File Offset: 0x00002CFC
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00004B04 File Offset: 0x00002D04
		[Serialize]
		public int CyclesFuelLasts { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00004B0D File Offset: 0x00002D0D
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00004B15 File Offset: 0x00002D15
		[Serialize]
		public int FuelCapacity { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00004B1E File Offset: 0x00002D1E
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00004B26 File Offset: 0x00002D26
		[Serialize("Icon")]
		public UISprite UIIcon { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00004B2F File Offset: 0x00002D2F
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00004B37 File Offset: 0x00002D37
		[Serialize]
		private int CyclesCapacity { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00004B40 File Offset: 0x00002D40
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00004B48 File Offset: 0x00002D48
		[Serialize]
		private AssetRef<Sprite> Icon { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004B51 File Offset: 0x00002D51
		public bool ProducesProducts
		{
			get
			{
				return !this.Products.IsEmpty<GoodAmountSpec>();
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00004B66 File Offset: 0x00002D66
		public bool ProducesSciencePoints
		{
			get
			{
				return this.ProducedSciencePoints > 0;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00004B71 File Offset: 0x00002D71
		public bool ConsumesIngredients
		{
			get
			{
				return !this.Ingredients.IsEmpty<GoodAmountSpec>();
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00004B86 File Offset: 0x00002D86
		public bool ConsumesFuel
		{
			get
			{
				return this.FuelCapacity > 0;
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004B91 File Offset: 0x00002D91
		public int GetCapacity(GoodAmount goodAmount)
		{
			return goodAmount.Amount * this.CyclesCapacity;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004BA4 File Offset: 0x00002DA4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RecipeSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004BF0 File Offset: 0x00002DF0
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
			builder.Append(", DisplayLocKey = ");
			builder.Append(this.DisplayLocKey);
			builder.Append(", CycleDurationInHours = ");
			builder.Append(this.CycleDurationInHours.ToString());
			builder.Append(", Ingredients = ");
			builder.Append(this.Ingredients.ToString());
			builder.Append(", Products = ");
			builder.Append(this.Products.ToString());
			builder.Append(", ProducedSciencePoints = ");
			builder.Append(this.ProducedSciencePoints.ToString());
			builder.Append(", Fuel = ");
			builder.Append(this.Fuel);
			builder.Append(", CyclesFuelLasts = ");
			builder.Append(this.CyclesFuelLasts.ToString());
			builder.Append(", FuelCapacity = ");
			builder.Append(this.FuelCapacity.ToString());
			builder.Append(", UIIcon = ");
			builder.Append(this.UIIcon);
			builder.Append(", ProducesProducts = ");
			builder.Append(this.ProducesProducts.ToString());
			builder.Append(", ProducesSciencePoints = ");
			builder.Append(this.ProducesSciencePoints.ToString());
			builder.Append(", ConsumesIngredients = ");
			builder.Append(this.ConsumesIngredients.ToString());
			builder.Append(", ConsumesFuel = ");
			builder.Append(this.ConsumesFuel.ToString());
			return true;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004E28 File Offset: 0x00003028
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RecipeSpec left, RecipeSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004E34 File Offset: 0x00003034
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RecipeSpec left, RecipeSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004E48 File Offset: 0x00003048
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((((((((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<BackwardCompatibleIds>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayLocKey>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<CycleDurationInHours>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<GoodAmountSpec>>.Default.GetHashCode(this.<Ingredients>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<GoodAmountSpec>>.Default.GetHashCode(this.<Products>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ProducedSciencePoints>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Fuel>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<CyclesFuelLasts>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<FuelCapacity>k__BackingField)) * -1521134295 + EqualityComparer<UISprite>.Default.GetHashCode(this.<UIIcon>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<CyclesCapacity>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Sprite>>.Default.GetHashCode(this.<Icon>k__BackingField);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004F86 File Offset: 0x00003186
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RecipeSpec);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00003857 File Offset: 0x00001A57
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004F94 File Offset: 0x00003194
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RecipeSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<BackwardCompatibleIds>k__BackingField, other.<BackwardCompatibleIds>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayLocKey>k__BackingField, other.<DisplayLocKey>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<CycleDurationInHours>k__BackingField, other.<CycleDurationInHours>k__BackingField) && EqualityComparer<ImmutableArray<GoodAmountSpec>>.Default.Equals(this.<Ingredients>k__BackingField, other.<Ingredients>k__BackingField) && EqualityComparer<ImmutableArray<GoodAmountSpec>>.Default.Equals(this.<Products>k__BackingField, other.<Products>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ProducedSciencePoints>k__BackingField, other.<ProducedSciencePoints>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Fuel>k__BackingField, other.<Fuel>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<CyclesFuelLasts>k__BackingField, other.<CyclesFuelLasts>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<FuelCapacity>k__BackingField, other.<FuelCapacity>k__BackingField) && EqualityComparer<UISprite>.Default.Equals(this.<UIIcon>k__BackingField, other.<UIIcon>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<CyclesCapacity>k__BackingField, other.<CyclesCapacity>k__BackingField) && EqualityComparer<AssetRef<Sprite>>.Default.Equals(this.<Icon>k__BackingField, other.<Icon>k__BackingField));
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000510C File Offset: 0x0000330C
		[CompilerGenerated]
		protected RecipeSpec([Nullable(1)] RecipeSpec original) : base(original)
		{
			this.Id = original.<Id>k__BackingField;
			this.BackwardCompatibleIds = original.<BackwardCompatibleIds>k__BackingField;
			this.DisplayLocKey = original.<DisplayLocKey>k__BackingField;
			this.CycleDurationInHours = original.<CycleDurationInHours>k__BackingField;
			this.Ingredients = original.<Ingredients>k__BackingField;
			this.Products = original.<Products>k__BackingField;
			this.ProducedSciencePoints = original.<ProducedSciencePoints>k__BackingField;
			this.Fuel = original.<Fuel>k__BackingField;
			this.CyclesFuelLasts = original.<CyclesFuelLasts>k__BackingField;
			this.FuelCapacity = original.<FuelCapacity>k__BackingField;
			this.UIIcon = original.<UIIcon>k__BackingField;
			this.CyclesCapacity = original.<CyclesCapacity>k__BackingField;
			this.Icon = original.<Icon>k__BackingField;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000038A6 File Offset: 0x00001AA6
		public RecipeSpec()
		{
		}
	}
}
