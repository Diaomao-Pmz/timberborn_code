using System;
using Steamworks;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.SteamStoreSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.SteamOnScreenKeyboard
{
	// Token: 0x02000005 RID: 5
	public class SteamOnScreenKeyboardController : IVisualElementInitializer
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D9 File Offset: 0x000002D9
		public SteamOnScreenKeyboardController(InputSettings inputSettings, SteamManager steamManager)
		{
			this._inputSettings = inputSettings;
			this._steamManager = steamManager;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020F0 File Offset: 0x000002F0
		public void InitializeVisualElement(VisualElement visualElement)
		{
			if (!Application.isEditor && this._steamManager.Initialized && (visualElement is TextField || visualElement is IntegerField || visualElement is FloatField))
			{
				TextElement textElement = UQueryExtensions.Q<TextElement>(visualElement, null, null);
				textElement.RegisterCallback<FocusInEvent>(new EventCallback<FocusInEvent>(this.OnFocusIn), 0);
				textElement.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnFocusOut), 0);
				textElement.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnTextfieldGeometryChange), 0);
				textElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnTextfieldGeometryChange), 0);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000217C File Offset: 0x0000037C
		public void OnFocusIn(FocusInEvent focusInEvent)
		{
			TextElement textElement = focusInEvent.currentTarget as TextElement;
			if (textElement != null)
			{
				this._lastFocusedElement = textElement;
				if (SteamOnScreenKeyboardController.ElementHasValidGeometry(textElement))
				{
					this.TryOpenOnScreenKeyboard(textElement);
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021B0 File Offset: 0x000003B0
		public void OnFocusOut(FocusOutEvent focusOutEvent)
		{
			TextElement textElement = focusOutEvent.currentTarget as TextElement;
			if (textElement != null && textElement == this._lastFocusedElement)
			{
				SteamOnScreenKeyboardController.HideKeyboard();
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021DC File Offset: 0x000003DC
		public void OnTextfieldGeometryChange(EventBase eventBase)
		{
			TextElement textElement = eventBase.currentTarget as TextElement;
			if (textElement != null && textElement == this._lastFocusedElement && SteamOnScreenKeyboardController.ElementHasValidGeometry(textElement))
			{
				this.TryOpenOnScreenKeyboard(textElement);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002210 File Offset: 0x00000410
		public void TryOpenOnScreenKeyboard(TextElement textElement)
		{
			if (this.ShouldOpenKeyboard())
			{
				Rect worldBound = textElement.worldBound;
				SteamUtils.ShowFloatingGamepadTextInput(EFloatingGamepadTextInputMode.k_EFloatingGamepadTextInputModeModeSingleLine, (int)worldBound.min.x, (int)worldBound.min.y, (int)worldBound.width, (int)worldBound.height);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000225D File Offset: 0x0000045D
		public static void HideKeyboard()
		{
			SteamUtils.DismissFloatingGamepadTextInput();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002265 File Offset: 0x00000465
		public bool ShouldOpenKeyboard()
		{
			return this.SteamDeckCheck() || this.SteamBigPictureCheck();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002278 File Offset: 0x00000478
		public static bool ElementHasValidGeometry(VisualElement element)
		{
			Rect worldBound = element.worldBound;
			return worldBound.x != 0f || worldBound.y != 0f || !float.IsNaN(worldBound.width) || !float.IsNaN(worldBound.height);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022C8 File Offset: 0x000004C8
		public bool SteamDeckCheck()
		{
			if (this._steamManager.Initialized)
			{
				string onScreenKeyboard = this._inputSettings.OnScreenKeyboard;
				if (onScreenKeyboard == "Auto" || onScreenKeyboard == "Enabled")
				{
					return SteamUtils.IsSteamRunningOnSteamDeck();
				}
			}
			return false;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000230F File Offset: 0x0000050F
		public bool SteamBigPictureCheck()
		{
			return this._steamManager.Initialized && this._inputSettings.OnScreenKeyboard == "Enabled" && SteamUtils.IsSteamInBigPictureMode();
		}

		// Token: 0x04000006 RID: 6
		public readonly InputSettings _inputSettings;

		// Token: 0x04000007 RID: 7
		public readonly SteamManager _steamManager;

		// Token: 0x04000008 RID: 8
		public TextElement _lastFocusedElement;
	}
}
