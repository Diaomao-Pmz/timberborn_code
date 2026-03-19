using System;
using Timberborn.CoreUI;
using Timberborn.FactionSystem;
using Timberborn.TutorialSettingsSystem;
using UnityEngine.UIElements;

namespace Timberborn.MainMenuPanels
{
	// Token: 0x02000012 RID: 18
	public class TutorialToggleController
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00004045 File Offset: 0x00002245
		public TutorialToggleController(TutorialSettings tutorialSettings)
		{
			this._tutorialSettings = tutorialSettings;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004054 File Offset: 0x00002254
		public void Initialize(VisualElement root)
		{
			this._tutorialToggleWrapper = UQueryExtensions.Q<VisualElement>(root, "TutorialToggleWrapper", null);
			this._tutorialToggle = UQueryExtensions.Q<Toggle>(root, "TutorialToggle", null);
			this._tutorialToggleCustomWrapper = UQueryExtensions.Q<VisualElement>(root, "TutorialToggleCustomWrapper", null);
			this._tutorialToggleCustom = UQueryExtensions.Q<Toggle>(root, "TutorialToggleCustom", null);
			this.UpdateToggles();
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._tutorialToggle, new EventCallback<ChangeEvent<bool>>(this.OnToggleChanged));
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._tutorialToggleCustom, new EventCallback<ChangeEvent<bool>>(this.OnToggleChanged));
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000040DF File Offset: 0x000022DF
		public void SetFaction(FactionSpec factionSpec)
		{
			this._showToggles = factionSpec.HasSpec<StartingFactionSpec>();
			this.UpdateTogglesVisibility();
			this.UpdateToggles();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000040F9 File Offset: 0x000022F9
		public void ShowMainToggle()
		{
			this._showMainToggle = true;
			this.UpdateTogglesVisibility();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004108 File Offset: 0x00002308
		public void HideMainToggle()
		{
			this._showMainToggle = false;
			this.UpdateTogglesVisibility();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004117 File Offset: 0x00002317
		public void OnToggleChanged(ChangeEvent<bool> evt)
		{
			this._tutorialSettings.DisableTutorial = !evt.newValue;
			this.UpdateToggles();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004133 File Offset: 0x00002333
		public void UpdateTogglesVisibility()
		{
			this._tutorialToggleWrapper.ToggleDisplayStyle(this._showToggles && this._showMainToggle);
			this._tutorialToggleCustomWrapper.ToggleDisplayStyle(this._showToggles && !this._showMainToggle);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004170 File Offset: 0x00002370
		public void UpdateToggles()
		{
			this._tutorialToggle.SetValueWithoutNotify(!this._tutorialSettings.DisableTutorial);
			this._tutorialToggleCustom.SetValueWithoutNotify(!this._tutorialSettings.DisableTutorial);
		}

		// Token: 0x0400008A RID: 138
		public readonly TutorialSettings _tutorialSettings;

		// Token: 0x0400008B RID: 139
		public Toggle _tutorialToggle;

		// Token: 0x0400008C RID: 140
		public VisualElement _tutorialToggleWrapper;

		// Token: 0x0400008D RID: 141
		public Toggle _tutorialToggleCustom;

		// Token: 0x0400008E RID: 142
		public VisualElement _tutorialToggleCustomWrapper;

		// Token: 0x0400008F RID: 143
		public bool _showToggles;

		// Token: 0x04000090 RID: 144
		public bool _showMainToggle;
	}
}
