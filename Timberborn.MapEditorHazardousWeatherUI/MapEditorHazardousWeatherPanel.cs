using System;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;
using Timberborn.SliderToggleSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorHazardousWeatherUI
{
	// Token: 0x02000005 RID: 5
	public class MapEditorHazardousWeatherPanel : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002262 File Offset: 0x00000462
		public MapEditorHazardousWeatherPanel(VisualElementLoader visualElementLoader, UILayout uiLayout, HazardousWeatherToggleFactory hazardousWeatherToggleFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._uiLayout = uiLayout;
			this._hazardousWeatherToggleFactory = hazardousWeatherToggleFactory;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002280 File Offset: 0x00000480
		public void Load()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("MapEditor/MapEditorHazardousWeatherPanel");
			this._sliderToggle = this._hazardousWeatherToggleFactory.Create(visualElement);
			this._uiLayout.AddTopRight(visualElement, 3);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022BD File Offset: 0x000004BD
		public void UpdateSingleton()
		{
			this._sliderToggle.Update();
		}

		// Token: 0x0400000F RID: 15
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000010 RID: 16
		public readonly UILayout _uiLayout;

		// Token: 0x04000011 RID: 17
		public readonly HazardousWeatherToggleFactory _hazardousWeatherToggleFactory;

		// Token: 0x04000012 RID: 18
		public SliderToggle _sliderToggle;
	}
}
