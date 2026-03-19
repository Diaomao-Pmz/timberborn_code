using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using Timberborn.Workshops;
using UnityEngine.UIElements;

namespace Timberborn.WorkshopsUI
{
	// Token: 0x02000017 RID: 23
	public class ManufactoryTogglableRecipesFragment : IEntityPanelFragment
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003257 File Offset: 0x00001457
		public ManufactoryTogglableRecipesFragment(VisualElementLoader visualElementLoader, ManufactoryRecipeSliderToggleFactory manufactoryRecipeSliderToggleFactory, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._manufactoryRecipeSliderToggleFactory = manufactoryRecipeSliderToggleFactory;
			this._loc = loc;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003274 File Offset: 0x00001474
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/ManufactoryTogglableRecipesFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._toggleLabel = UQueryExtensions.Q<Label>(this._root, "ToggleLabel", null);
			this._toggleWrapper = UQueryExtensions.Q<VisualElement>(this._root, "ToggleWrapper", null);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000032DC File Offset: 0x000014DC
		public void ShowFragment(BaseComponent entity)
		{
			ManufactoryTogglableRecipes component = entity.GetComponent<ManufactoryTogglableRecipes>();
			if (component)
			{
				Manufactory component2 = entity.GetComponent<Manufactory>();
				this._sliderToggle = this._manufactoryRecipeSliderToggleFactory.Create(this._toggleWrapper, component2);
				this._toggleLabel.text = this._loc.T(component.LabelLocKey);
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000333F File Offset: 0x0000153F
		public void ClearFragment()
		{
			this._sliderToggle = null;
			this._toggleWrapper.Clear();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000335F File Offset: 0x0000155F
		public void UpdateFragment()
		{
			SliderToggle sliderToggle = this._sliderToggle;
			if (sliderToggle == null)
			{
				return;
			}
			sliderToggle.Update();
		}

		// Token: 0x0400005D RID: 93
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400005E RID: 94
		public readonly ManufactoryRecipeSliderToggleFactory _manufactoryRecipeSliderToggleFactory;

		// Token: 0x0400005F RID: 95
		public readonly ILoc _loc;

		// Token: 0x04000060 RID: 96
		public VisualElement _root;

		// Token: 0x04000061 RID: 97
		public Label _toggleLabel;

		// Token: 0x04000062 RID: 98
		public VisualElement _toggleWrapper;

		// Token: 0x04000063 RID: 99
		public SliderToggle _sliderToggle;
	}
}
