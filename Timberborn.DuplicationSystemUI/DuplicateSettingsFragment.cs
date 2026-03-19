using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.InputSystemUI;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.DuplicationSystemUI
{
	// Token: 0x02000008 RID: 8
	public class DuplicateSettingsFragment : IEntityPanelFragment
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002223 File Offset: 0x00000423
		public DuplicateSettingsFragment(DuplicationValidator duplicationValidator, VisualElementLoader visualElementLoader, BindableButtonFactory bindableButtonFactory, DuplicateSettingsTool duplicateSettingsTool, ITooltipRegistrar tooltipRegistrar, ILoc loc)
		{
			this._duplicationValidator = duplicationValidator;
			this._visualElementLoader = visualElementLoader;
			this._bindableButtonFactory = bindableButtonFactory;
			this._duplicateSettingsTool = duplicateSettingsTool;
			this._tooltipRegistrar = tooltipRegistrar;
			this._loc = loc;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002258 File Offset: 0x00000458
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/EntityPanel/DuplicateSettingsFragment");
			Button button = UQueryExtensions.Q<Button>(this._root, "Button", null);
			this._button = this._bindableButtonFactory.Create(button, DuplicateSettingsFragment.DuplicateSettingsKey, new Action(this.Callback), true);
			this._tooltipRegistrar.RegisterWithKeyBinding(button, DuplicateSettingsFragment.DuplicateSettingsKey);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022D4 File Offset: 0x000004D4
		public void ShowFragment(BaseComponent entity)
		{
			if (this._duplicationValidator.CanDuplicateSettings(entity))
			{
				this._selectedDuplicable = entity;
				this._root.ToggleDisplayStyle(true);
				this._button.Bind();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002302 File Offset: 0x00000502
		public void ClearFragment()
		{
			this._selectedDuplicable = null;
			this._button.Unbind();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002203 File Offset: 0x00000403
		public void UpdateFragment()
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002322 File Offset: 0x00000522
		public void Callback()
		{
			this._duplicateSettingsTool.ActivateWithSource(this._selectedDuplicable);
		}

		// Token: 0x04000011 RID: 17
		public static readonly string DuplicateSettingsKey = "DuplicateSettings";

		// Token: 0x04000012 RID: 18
		public readonly DuplicationValidator _duplicationValidator;

		// Token: 0x04000013 RID: 19
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000014 RID: 20
		public readonly BindableButtonFactory _bindableButtonFactory;

		// Token: 0x04000015 RID: 21
		public readonly DuplicateSettingsTool _duplicateSettingsTool;

		// Token: 0x04000016 RID: 22
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000017 RID: 23
		public readonly ILoc _loc;

		// Token: 0x04000018 RID: 24
		public BaseComponent _selectedDuplicable;

		// Token: 0x04000019 RID: 25
		public BindableButton _button;

		// Token: 0x0400001A RID: 26
		public VisualElement _root;
	}
}
