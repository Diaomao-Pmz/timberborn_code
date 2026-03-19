using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.SliderToggleSystem;
using Timberborn.Workshops;
using UnityEngine.UIElements;

namespace Timberborn.WorkshopsUI
{
	// Token: 0x02000016 RID: 22
	public class ManufactoryTogglableRecipesBatchControlRowItemFactory
	{
		// Token: 0x0600006D RID: 109 RVA: 0x000031F6 File Offset: 0x000013F6
		public ManufactoryTogglableRecipesBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ManufactoryRecipeSliderToggleFactory manufactoryRecipeSliderToggleFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._manufactoryRecipeSliderToggleFactory = manufactoryRecipeSliderToggleFactory;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000320C File Offset: 0x0000140C
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			if (entity.GetComponent<ManufactoryTogglableRecipes>())
			{
				string elementName = "Game/BatchControl/SelectionToggleBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				Manufactory component = entity.GetComponent<Manufactory>();
				SliderToggle sliderToggle = this._manufactoryRecipeSliderToggleFactory.Create(visualElement, component);
				return new ManufactoryTogglableRecipesBatchControlRowItem(visualElement, sliderToggle);
			}
			return null;
		}

		// Token: 0x0400005B RID: 91
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400005C RID: 92
		public readonly ManufactoryRecipeSliderToggleFactory _manufactoryRecipeSliderToggleFactory;
	}
}
