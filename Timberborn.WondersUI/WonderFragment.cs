using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.GameSound;
using Timberborn.InputSystem;
using Timberborn.InventorySystemUI;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using Timberborn.Wonders;
using UnityEngine.UIElements;

namespace Timberborn.WondersUI
{
	// Token: 0x02000007 RID: 7
	public class WonderFragment : IEntityPanelFragment, IInputProcessor
	{
		// Token: 0x06000015 RID: 21 RVA: 0x0000227F File Offset: 0x0000047F
		public WonderFragment(VisualElementLoader visualElementLoader, InventoryFragmentBuilderFactory inventoryFragmentBuilderFactory, GameUISoundController gameUISoundController, InputService inputService, ITooltipRegistrar tooltipRegistrar, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._inventoryFragmentBuilderFactory = inventoryFragmentBuilderFactory;
			this._gameUISoundController = gameUISoundController;
			this._inputService = inputService;
			this._tooltipRegistrar = tooltipRegistrar;
			this._loc = loc;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022B4 File Offset: 0x000004B4
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/WonderFragment");
			VisualElement root = UQueryExtensions.Q<VisualElement>(this._root, "InventoryRoot", null);
			this._inventoryFragment = this._inventoryFragmentBuilderFactory.CreateBuilder(root).ShowEmptyRows().ShowRowLimit().Build();
			this._root.ToggleDisplayStyle(false);
			this._button = UQueryExtensions.Q<Button>(this._root, "ActivateButton", null);
			this._button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.ActivateWonder();
			}, 0);
			this._tooltipRegistrar.RegisterWithKeyBinding(this._button, this._loc.T(WonderFragment.WonderActivateLocKey), WonderFragment.UniqueBuildingActionKey);
			return this._root;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002374 File Offset: 0x00000574
		public void ShowFragment(BaseComponent entity)
		{
			this._wonder = entity.GetComponent<Wonder>();
			this._wonderInventory = entity.GetComponent<WonderInventory>();
			if (this._wonderInventory)
			{
				this._inventoryFragment.ShowFragment(this._wonderInventory.Inventory);
				this._inputService.AddInputProcessor(this);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023C8 File Offset: 0x000005C8
		public void UpdateFragment()
		{
			if (this._wonder && this._wonder.Enabled)
			{
				this._button.SetEnabled(this._wonder.CanBeActivated());
				this._inventoryFragment.UpdateFragment();
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002429 File Offset: 0x00000629
		public void ClearFragment()
		{
			this._wonder = null;
			this._root.ToggleDisplayStyle(false);
			this._inventoryFragment.ClearFragment();
			this._inputService.RemoveInputProcessor(this);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002455 File Offset: 0x00000655
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(WonderFragment.UniqueBuildingActionKey))
			{
				this.ActivateWonder();
				return true;
			}
			return false;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002472 File Offset: 0x00000672
		public void ActivateWonder()
		{
			this._wonder.Activate();
			this._gameUISoundController.PlayWonderLaunchSound();
		}

		// Token: 0x04000011 RID: 17
		public static readonly string WonderActivateLocKey = "Wonder.Activate";

		// Token: 0x04000012 RID: 18
		public static readonly string UniqueBuildingActionKey = "UniqueBuildingAction";

		// Token: 0x04000013 RID: 19
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000014 RID: 20
		public readonly InventoryFragmentBuilderFactory _inventoryFragmentBuilderFactory;

		// Token: 0x04000015 RID: 21
		public readonly GameUISoundController _gameUISoundController;

		// Token: 0x04000016 RID: 22
		public readonly InputService _inputService;

		// Token: 0x04000017 RID: 23
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000018 RID: 24
		public readonly ILoc _loc;

		// Token: 0x04000019 RID: 25
		public VisualElement _root;

		// Token: 0x0400001A RID: 26
		public Wonder _wonder;

		// Token: 0x0400001B RID: 27
		public WonderInventory _wonderInventory;

		// Token: 0x0400001C RID: 28
		public InventoryFragment _inventoryFragment;

		// Token: 0x0400001D RID: 29
		public Button _button;
	}
}
