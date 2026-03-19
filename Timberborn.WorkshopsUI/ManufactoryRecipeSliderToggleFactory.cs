using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using Timberborn.Workshops;
using UnityEngine.UIElements;

namespace Timberborn.WorkshopsUI
{
	// Token: 0x02000011 RID: 17
	public class ManufactoryRecipeSliderToggleFactory
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00002FA1 File Offset: 0x000011A1
		public ManufactoryRecipeSliderToggleFactory(SliderToggleFactory sliderToggleFactory, ILoc loc)
		{
			this._sliderToggleFactory = sliderToggleFactory;
			this._loc = loc;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002FB7 File Offset: 0x000011B7
		public SliderToggle Create(VisualElement parent, Manufactory manufactory)
		{
			return this._sliderToggleFactory.Create(parent, this.CreateItems(manufactory).ToArray<SliderToggleItem>());
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002FD1 File Offset: 0x000011D1
		public IEnumerable<SliderToggleItem> CreateItems(Manufactory manufactory)
		{
			ImmutableArray<RecipeSpec>.Enumerator enumerator = manufactory.ProductionRecipes.GetEnumerator();
			while (enumerator.MoveNext())
			{
				RecipeSpec productionRecipe = enumerator.Current;
				yield return SliderToggleItem.Create(() => this._loc.T(productionRecipe.DisplayLocKey), productionRecipe.UIIcon.Value, delegate()
				{
					manufactory.SetRecipe(productionRecipe);
				}, () => manufactory.CurrentRecipe == productionRecipe);
			}
			enumerator = default(ImmutableArray<RecipeSpec>.Enumerator);
			yield break;
		}

		// Token: 0x0400004B RID: 75
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x0400004C RID: 76
		public readonly ILoc _loc;
	}
}
