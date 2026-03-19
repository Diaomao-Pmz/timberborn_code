using System;
using Timberborn.CoreUI;
using Timberborn.PrioritySystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.PrioritySystemUI
{
	// Token: 0x0200000C RID: 12
	public class PriorityToggleFactory
	{
		// Token: 0x06000036 RID: 54 RVA: 0x0000289C File Offset: 0x00000A9C
		public PriorityToggleFactory(VisualElementLoader visualElementLoader, PriorityColors priorityColors)
		{
			this._visualElementLoader = visualElementLoader;
			this._priorityColors = priorityColors;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000028B4 File Offset: 0x00000AB4
		public PriorityToggle Create(Priority priority, VisualElement parent, Sprite sprite)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/PriorityToggle");
			parent.Add(visualElement);
			Color buttonColor = this._priorityColors.GetButtonColor(priority);
			Toggle toggle = UQueryExtensions.Q<Toggle>(visualElement, "PriorityToggle", null);
			VisualElement visualElement2 = UQueryExtensions.Q<VisualElement>(toggle, "unity-checkmark", null);
			visualElement2.style.unityBackgroundImageTintColor = new StyleColor(buttonColor);
			visualElement2.style.backgroundImage = new StyleBackground(sprite);
			toggle.style.unityBackgroundImageTintColor = new StyleColor(buttonColor);
			PriorityToggle priorityToggle = new PriorityToggle(priority, toggle);
			priorityToggle.Initialize();
			return priorityToggle;
		}

		// Token: 0x04000018 RID: 24
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000019 RID: 25
		public readonly PriorityColors _priorityColors;
	}
}
