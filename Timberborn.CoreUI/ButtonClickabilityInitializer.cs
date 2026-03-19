using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000009 RID: 9
	public class ButtonClickabilityInitializer : IVisualElementInitializer
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000021D0 File Offset: 0x000003D0
		public void InitializeVisualElement(VisualElement visualElement)
		{
			Button button = visualElement as Button;
			if (button != null)
			{
				ButtonClickabilityInitializer.MakeButtonClickableWithAnyModifier(button);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021F0 File Offset: 0x000003F0
		public static void MakeButtonClickableWithAnyModifier(Button button)
		{
			List<ManipulatorActivationFilter> activators = button.clickable.activators;
			activators.Clear();
			foreach (EventModifiers modifiers in ButtonClickabilityInitializer.EventModifiers)
			{
				List<ManipulatorActivationFilter> list = activators;
				ManipulatorActivationFilter item = default(ManipulatorActivationFilter);
				item.button = 0;
				item.modifiers = modifiers;
				list.Add(item);
			}
		}

		// Token: 0x0400000F RID: 15
		public static readonly List<EventModifiers> EventModifiers = new List<EventModifiers>(Enum.GetValues(typeof(EventModifiers)).Cast<EventModifiers>());
	}
}
