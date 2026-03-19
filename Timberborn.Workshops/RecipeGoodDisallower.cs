using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;

namespace Timberborn.Workshops
{
	// Token: 0x02000023 RID: 35
	public class RecipeGoodDisallower : BaseComponent, IAwakableComponent, IGoodDisallower, IFinishedStateListener
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060000F2 RID: 242 RVA: 0x000045B4 File Offset: 0x000027B4
		// (remove) Token: 0x060000F3 RID: 243 RVA: 0x000045EC File Offset: 0x000027EC
		public event EventHandler<DisallowedGoodsChangedEventArgs> DisallowedGoodsChanged;

		// Token: 0x060000F4 RID: 244 RVA: 0x00002711 File Offset: 0x00000911
		public void Awake()
		{
			base.DisableComponent();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004624 File Offset: 0x00002824
		public int AllowedAmount(string goodId)
		{
			int result;
			if (!this._limits.TryGetValue(goodId, out result))
			{
				return 0;
			}
			return result;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004644 File Offset: 0x00002844
		public void UpdateAllowedAmounts(RecipeSpec recipeSpec)
		{
			this.ResetLimits();
			if (recipeSpec != null)
			{
				this.SetLimitsForRecipe(recipeSpec);
			}
			foreach (string goodId in this._limits.Keys)
			{
				EventHandler<DisallowedGoodsChangedEventArgs> disallowedGoodsChanged = this.DisallowedGoodsChanged;
				if (disallowedGoodsChanged != null)
				{
					disallowedGoodsChanged(this, new DisallowedGoodsChangedEventArgs(goodId));
				}
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003F91 File Offset: 0x00002191
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00002711 File Offset: 0x00000911
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000046C4 File Offset: 0x000028C4
		public void ResetLimits()
		{
			foreach (string key in this._limits.Keys.ToList<string>())
			{
				this._limits[key] = 0;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004728 File Offset: 0x00002928
		public void SetLimitsForRecipe(RecipeSpec recipeSpec)
		{
			foreach (GoodAmountSpec goodAmountSpec in recipeSpec.Ingredients)
			{
				this._limits[goodAmountSpec.Id] = recipeSpec.GetCapacity(goodAmountSpec.ToGoodAmount());
			}
			foreach (GoodAmountSpec goodAmountSpec2 in recipeSpec.Products)
			{
				this._limits[goodAmountSpec2.Id] = recipeSpec.GetCapacity(goodAmountSpec2.ToGoodAmount());
			}
			if (recipeSpec.ConsumesFuel)
			{
				this._limits[recipeSpec.Fuel] = recipeSpec.FuelCapacity;
			}
		}

		// Token: 0x04000078 RID: 120
		public readonly Dictionary<string, int> _limits = new Dictionary<string, int>();
	}
}
