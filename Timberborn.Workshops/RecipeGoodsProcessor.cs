using System;
using System.Collections.Generic;
using Timberborn.Goods;
using UnityEngine;

namespace Timberborn.Workshops
{
	// Token: 0x02000024 RID: 36
	public class RecipeGoodsProcessor
	{
		// Token: 0x060000FC RID: 252 RVA: 0x000047E5 File Offset: 0x000029E5
		public RecipeGoodsProcessor(IGoodService goodService, RecipeSpecService recipeSpecService)
		{
			this._goodService = goodService;
			this._recipeSpecService = recipeSpecService;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000047FC File Offset: 0x000029FC
		public Dictionary<StorableGood, int> ProcessRecipes(IEnumerable<string> recipeIds, int capacityMultiplier = 1)
		{
			Dictionary<StorableGood, int> dictionary = new Dictionary<StorableGood, int>();
			foreach (string recipeId in recipeIds)
			{
				RecipeSpec recipe = this._recipeSpecService.GetRecipe(recipeId);
				if (recipe.Blueprint.IsAllowedByFeatureToggles())
				{
					this.ProcessRecipe(recipe, dictionary, capacityMultiplier);
				}
			}
			return dictionary;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004868 File Offset: 0x00002A68
		public void ProcessRecipe(RecipeSpec productionRecipe, Dictionary<StorableGood, int> goods, int capacityMultiplier = 1)
		{
			this.CheckRecipeGoods(productionRecipe);
			if (productionRecipe.ConsumesIngredients)
			{
				foreach (GoodAmountSpec goodAmountSpec in productionRecipe.Ingredients)
				{
					StorableGood storableGood = StorableGood.CreateAsGivable(goodAmountSpec.Id);
					int capacity = productionRecipe.GetCapacity(goodAmountSpec.ToGoodAmount());
					RecipeGoodsProcessor.IncreaseAmount(goods, storableGood, capacity * capacityMultiplier);
				}
			}
			if (productionRecipe.ConsumesFuel)
			{
				StorableGood storableGood2 = StorableGood.CreateAsGivable(productionRecipe.Fuel);
				RecipeGoodsProcessor.IncreaseAmount(goods, storableGood2, productionRecipe.FuelCapacity * capacityMultiplier);
			}
			if (productionRecipe.ProducesProducts)
			{
				foreach (GoodAmountSpec goodAmountSpec2 in productionRecipe.Products)
				{
					StorableGood storableGood3 = StorableGood.CreateAsTakeable(goodAmountSpec2.Id);
					int capacity2 = productionRecipe.GetCapacity(goodAmountSpec2.ToGoodAmount());
					RecipeGoodsProcessor.IncreaseAmount(goods, storableGood3, capacity2 * capacityMultiplier);
				}
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004944 File Offset: 0x00002B44
		public void CheckRecipeGoods(RecipeSpec recipeSpec)
		{
			string id = recipeSpec.Id;
			this.ValidateGoods(recipeSpec.Ingredients, id, "Ingredients");
			this.ValidateGoods(recipeSpec.Products, id, "Products");
			string fuel = recipeSpec.Fuel;
			if (!string.IsNullOrEmpty(fuel))
			{
				this.ValidateGood(fuel, id, "Fuel");
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000049A4 File Offset: 0x00002BA4
		public void ValidateGoods(IEnumerable<GoodAmountSpec> goods, string recipeId, string type)
		{
			foreach (GoodAmountSpec goodAmountSpec in goods)
			{
				this.ValidateGood(goodAmountSpec.Id, recipeId, type);
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000049F4 File Offset: 0x00002BF4
		public void ValidateGood(string goodId, string recipeId, string type)
		{
			if (!this._goodService.HasGood(goodId))
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Good ",
					goodId,
					" for ",
					type,
					" in recipe ",
					recipeId,
					" not found"
				}));
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004A4B File Offset: 0x00002C4B
		public static void IncreaseAmount(IDictionary<StorableGood, int> goods, StorableGood storableGood, int amount)
		{
			if (!goods.ContainsKey(storableGood) || goods[storableGood] < amount)
			{
				goods[storableGood] = amount;
			}
		}

		// Token: 0x04000079 RID: 121
		public readonly IGoodService _goodService;

		// Token: 0x0400007A RID: 122
		public readonly RecipeSpecService _recipeSpecService;
	}
}
