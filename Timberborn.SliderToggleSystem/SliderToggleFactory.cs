using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using UnityEngine.UIElements;

namespace Timberborn.SliderToggleSystem
{
	// Token: 0x02000009 RID: 9
	public class SliderToggleFactory
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002521 File Offset: 0x00000721
		public SliderToggleFactory(VisualElementLoader visualElementLoader, SliderToggleButtonFactory sliderToggleButtonFactory, InputService inputService)
		{
			this._visualElementLoader = visualElementLoader;
			this._sliderToggleButtonFactory = sliderToggleButtonFactory;
			this._inputService = inputService;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000253E File Offset: 0x0000073E
		public SliderToggle Create(VisualElement parent, params SliderToggleItem[] items)
		{
			return this.CreateBindable(parent, null, items);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000254C File Offset: 0x0000074C
		public SliderToggle CreateBindable(VisualElement parent, string toggleBindingKey, params SliderToggleItem[] items)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/SliderToggle");
			parent.Add(visualElement);
			return new SliderToggle(this._inputService, visualElement, toggleBindingKey, this.CreateItems(UQueryExtensions.Q<VisualElement>(visualElement, "Content", null), items));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002591 File Offset: 0x00000791
		public IEnumerable<SliderToggleButton> CreateItems(VisualElement parent, IReadOnlyList<SliderToggleItem> items)
		{
			int num;
			for (int i = 0; i < items.Count; i = num + 1)
			{
				yield return this._sliderToggleButtonFactory.Create(parent, items[i]);
				num = i;
			}
			yield break;
		}

		// Token: 0x04000018 RID: 24
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000019 RID: 25
		public readonly SliderToggleButtonFactory _sliderToggleButtonFactory;

		// Token: 0x0400001A RID: 26
		public readonly InputService _inputService;
	}
}
