using System;
using Timberborn.AutomationBuildings;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x02000015 RID: 21
	public class LeverFragment : IEntityPanelFragment, IInputProcessor
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00003862 File Offset: 0x00001A62
		public LeverFragment(VisualElementLoader visualElementLoader, InputService inputService, ILoc loc, ITooltipRegistrar tooltipRegistrar)
		{
			this._visualElementLoader = visualElementLoader;
			this._inputService = inputService;
			this._loc = loc;
			this._tooltipRegistrar = tooltipRegistrar;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003888 File Offset: 0x00001A88
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/LeverFragment");
			this._root.ToggleDisplayStyle(false);
			this._button = UQueryExtensions.Q<Button>(this._root, "Button", null);
			this._button.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), 1);
			this._button.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), 1);
			this._tooltipRegistrar.RegisterWithKeyBinding(this._button, this.GetButtonText(), LeverFragment.UniqueBuildingActionKey);
			this._springReturnToggle = UQueryExtensions.Q<Toggle>(this._root, "SpringReturn", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._springReturnToggle, new EventCallback<ChangeEvent<bool>>(this.OnSpringReturnChanged));
			this._pinnedToggle = UQueryExtensions.Q<Toggle>(this._root, "Pinned", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._pinnedToggle, new EventCallback<ChangeEvent<bool>>(this.OnPinnedChanged));
			return this._root;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000397E File Offset: 0x00001B7E
		public void ShowFragment(BaseComponent entity)
		{
			this._lever = entity.GetComponent<Lever>();
			if (this._lever)
			{
				this._inputService.AddInputProcessor(this);
				this.UpdateButtonTooltip();
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000039AB File Offset: 0x00001BAB
		public void ClearFragment()
		{
			this._lever = null;
			this._root.ToggleDisplayStyle(false);
			this._inputService.RemoveInputProcessor(this);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000039CC File Offset: 0x00001BCC
		public void UpdateFragment()
		{
			if (this._lever)
			{
				this._button.text = this.GetButtonText();
				this._springReturnToggle.SetValueWithoutNotify(this._lever.IsSpringReturn);
				this._pinnedToggle.SetValueWithoutNotify(this._lever.IsPinned);
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003A3C File Offset: 0x00001C3C
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(LeverFragment.UniqueBuildingActionKey))
			{
				if (this._lever.IsSpringReturn)
				{
					this._lever.Press();
					return true;
				}
				this._lever.Press();
				this._lever.Release();
				return true;
			}
			else
			{
				if (this._inputService.IsKeyUp(LeverFragment.UniqueBuildingActionKey) && this._lever.IsSpringReturn)
				{
					this._lever.Release();
					return true;
				}
				return false;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003ABA File Offset: 0x00001CBA
		public void UpdateButtonTooltip()
		{
			this._tooltipRegistrar.RegisterWithKeyBinding(this._button, this.GetButtonText(), LeverFragment.UniqueBuildingActionKey);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003AD8 File Offset: 0x00001CD8
		public string GetButtonText()
		{
			Lever lever = this._lever;
			if (lever == null || !lever.IsOn)
			{
				return this._loc.T(LeverFragment.SwitchOnLocKey);
			}
			return this._loc.T(LeverFragment.SwitchOffLocKey);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003B0F File Offset: 0x00001D0F
		public void OnPointerDown(PointerDownEvent pointerDownEvent)
		{
			this._lever.Press();
			this.UpdateButtonTooltip();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003B22 File Offset: 0x00001D22
		public void OnPointerUp(PointerUpEvent pointerUpEvent)
		{
			this._lever.Release();
			this.UpdateButtonTooltip();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003B35 File Offset: 0x00001D35
		public void OnSpringReturnChanged(ChangeEvent<bool> evt)
		{
			this._lever.SetSpringReturn(evt.newValue);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003B48 File Offset: 0x00001D48
		public void OnPinnedChanged(ChangeEvent<bool> evt)
		{
			this._lever.SetPinned(evt.newValue);
		}

		// Token: 0x0400007D RID: 125
		public static readonly string SwitchOnLocKey = "Building.Lever.SwitchOn";

		// Token: 0x0400007E RID: 126
		public static readonly string SwitchOffLocKey = "Building.Lever.SwitchOff";

		// Token: 0x0400007F RID: 127
		public static readonly string UniqueBuildingActionKey = "UniqueBuildingAction";

		// Token: 0x04000080 RID: 128
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000081 RID: 129
		public readonly InputService _inputService;

		// Token: 0x04000082 RID: 130
		public readonly ILoc _loc;

		// Token: 0x04000083 RID: 131
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000084 RID: 132
		public VisualElement _root;

		// Token: 0x04000085 RID: 133
		public Button _button;

		// Token: 0x04000086 RID: 134
		public Lever _lever;

		// Token: 0x04000087 RID: 135
		public Toggle _springReturnToggle;

		// Token: 0x04000088 RID: 136
		public Toggle _pinnedToggle;
	}
}
