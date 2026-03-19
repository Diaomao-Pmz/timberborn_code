using System;
using Timberborn.UISound;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000056 RID: 86
	public class UISoundInitializer : IVisualElementInitializer
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00005D18 File Offset: 0x00003F18
		public UISoundInitializer(UISoundController uiSoundController)
		{
			this._uiSoundController = uiSoundController;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005D27 File Offset: 0x00003F27
		public void InitializeVisualElement(VisualElement visualElement)
		{
			visualElement.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.PlayUISound), 0);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005D3C File Offset: 0x00003F3C
		public void PlayUISound(EventBase clickEvent)
		{
			VisualElement visualElement = clickEvent.currentTarget as VisualElement;
			if (visualElement != null && visualElement.enabledSelf)
			{
				VisualElement visualElement2 = clickEvent.target as VisualElement;
				string text;
				if (visualElement2 != null && visualElement2.enabledSelf && visualElement == visualElement2 && visualElement.customStyle.TryGetValue(UISoundInitializer.ClickSoundProperty, ref text) && text != UISoundInitializer.NoSoundValue)
				{
					this._uiSoundController.PlaySound(text);
				}
			}
		}

		// Token: 0x040000C5 RID: 197
		public static readonly CustomStyleProperty<string> ClickSoundProperty = new CustomStyleProperty<string>("--click-sound");

		// Token: 0x040000C6 RID: 198
		public static readonly string NoSoundValue = "none";

		// Token: 0x040000C7 RID: 199
		public readonly UISoundController _uiSoundController;
	}
}
