using System;
using Timberborn.InputSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000059 RID: 89
	public static class VisualElementExtensions
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00005E97 File Offset: 0x00004097
		public static void ToggleDisplayStyle(this VisualElement visualElement, bool visible)
		{
			visualElement.style.display = (visible ? 0 : 1);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005EB0 File Offset: 0x000040B0
		public static bool IsDisplayed(this VisualElement visualElement)
		{
			return visualElement.resolvedStyle.display == 0;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005EC0 File Offset: 0x000040C0
		public static void SetHeightAsPercent(this VisualElement visualElement, float value01)
		{
			float num = Mathf.Clamp01(value01) * 100f;
			visualElement.style.height = new StyleLength(new Length(num, 1));
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005EF1 File Offset: 0x000040F1
		public static bool IsFocused(this VisualElement visualElement)
		{
			FocusController focusController = visualElement.focusController;
			return ((focusController != null) ? focusController.focusedElement : null) == visualElement;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005F08 File Offset: 0x00004108
		public static void SetConfirmCancelActions(this VisualElement visualElement, InputService inputService, Action confirmAction, Action cancelAction)
		{
			visualElement.RegisterCallback<FocusOutEvent>(delegate(FocusOutEvent _)
			{
				if (inputService.WasConfirmPressedLastFrame)
				{
					confirmAction();
				}
				if (inputService.WasCancelPressedLastFrame)
				{
					cancelAction();
				}
			}, 0);
		}
	}
}
