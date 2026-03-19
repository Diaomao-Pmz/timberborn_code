using System;
using Timberborn.CoreUI;
using Timberborn.TooltipSystem;
using Timberborn.TutorialSettingsSystem;
using UnityEngine.UIElements;

namespace Timberborn.TutorialSystemUI
{
	// Token: 0x02000006 RID: 6
	public class DisableTutorialButtonInitializer
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000217D File Offset: 0x0000037D
		public DisableTutorialButtonInitializer(DialogBoxShower dialogBoxShower, TutorialSettings tutorialSettings, ITooltipRegistrar tooltipRegistrar)
		{
			this._dialogBoxShower = dialogBoxShower;
			this._tutorialSettings = tutorialSettings;
			this._tooltipRegistrar = tooltipRegistrar;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000219C File Offset: 0x0000039C
		public void Initialize(VisualElement root)
		{
			VisualElement header = UQueryExtensions.Q<VisualElement>(root, "TutorialHeader", null);
			Button button = UQueryExtensions.Q<Button>(root, "Disable", null);
			button.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnSkipTutorialClicked), 0);
			header.AddToClassList(DisableTutorialButtonInitializer.EnableHoverClass);
			button.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
			{
				header.RemoveFromClassList(DisableTutorialButtonInitializer.EnableHoverClass);
			}, 0);
			button.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
			{
				header.AddToClassList(DisableTutorialButtonInitializer.EnableHoverClass);
			}, 0);
			this._tooltipRegistrar.RegisterLocalizable(button, DisableTutorialButtonInitializer.DisableLocKey);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002228 File Offset: 0x00000428
		public void OnSkipTutorialClicked(ClickEvent evt)
		{
			this._dialogBoxShower.Create().SetLocalizedMessage(DisableTutorialButtonInitializer.DisablePromptLocKey).SetConfirmButton(new Action(this.DisableTutorial)).SetDefaultCancelButton().Show();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000225B File Offset: 0x0000045B
		public void DisableTutorial()
		{
			this._tutorialSettings.DisableTutorial = true;
		}

		// Token: 0x0400000D RID: 13
		public static readonly string EnableHoverClass = "hover-enabled";

		// Token: 0x0400000E RID: 14
		public static readonly string DisableLocKey = "Tutorial.Disable";

		// Token: 0x0400000F RID: 15
		public static readonly string DisablePromptLocKey = "Tutorial.DisablePrompt";

		// Token: 0x04000010 RID: 16
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x04000011 RID: 17
		public readonly TutorialSettings _tutorialSettings;

		// Token: 0x04000012 RID: 18
		public readonly ITooltipRegistrar _tooltipRegistrar;
	}
}
