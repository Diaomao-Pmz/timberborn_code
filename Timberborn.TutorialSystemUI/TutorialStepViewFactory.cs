using System;
using Timberborn.CoreUI;
using Timberborn.KeyBindingSystemUI;
using Timberborn.TutorialSystem;
using UnityEngine.UIElements;

namespace Timberborn.TutorialSystemUI
{
	// Token: 0x02000013 RID: 19
	public class TutorialStepViewFactory
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002EFA File Offset: 0x000010FA
		public TutorialStepViewFactory(VisualElementLoader visualElementLoader, KeyBindingShortcutService keyBindingShortcutService, FixedKeyBindingElementFactory fixedKeyBindingElementFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._keyBindingShortcutService = keyBindingShortcutService;
			this._fixedKeyBindingElementFactory = fixedKeyBindingElementFactory;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002F18 File Offset: 0x00001118
		public TutorialStepView Create(TutorialStep tutorialStep, TutorialPanel parent)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/Tutorial/TutorialStepView");
			Label description = UQueryExtensions.Q<Label>(visualElement, "Description", null);
			if (tutorialStep.KeyBinding != null)
			{
				Label textElement = UQueryExtensions.Q<Label>(visualElement, "KeyBinding", null);
				this._keyBindingShortcutService.CreateAny(textElement, tutorialStep.KeyBinding);
			}
			if (tutorialStep.FixedKeyBinding != null)
			{
				VisualElement visualElement2 = UQueryExtensions.Q<VisualElement>(visualElement, "FixedKeyBinding", null);
				visualElement2.ToggleDisplayStyle(true);
				visualElement2.Add(this._fixedKeyBindingElementFactory.Create(tutorialStep.FixedKeyBinding));
			}
			return new TutorialStepView(tutorialStep, parent, visualElement, description);
		}

		// Token: 0x04000047 RID: 71
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000048 RID: 72
		public readonly KeyBindingShortcutService _keyBindingShortcutService;

		// Token: 0x04000049 RID: 73
		public readonly FixedKeyBindingElementFactory _fixedKeyBindingElementFactory;
	}
}
