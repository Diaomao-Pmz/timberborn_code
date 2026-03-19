using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BlockSystemUI;
using Timberborn.Buildings;
using Timberborn.ConstructionSites;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.EntityPanelSystem;
using Timberborn.EntitySystem;
using Timberborn.InputSystem;
using Timberborn.InputSystemUI;
using Timberborn.RecoverableGoodSystemUI;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.BuildingsUI
{
	// Token: 0x0200000E RID: 14
	public class DeleteBuildingFragment : IEntityPanelFragment
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002A2F File Offset: 0x00000C2F
		public DeleteBuildingFragment(VisualElementLoader visualElementLoader, RecoverableGoodDialogBoxShower recoverableGoodDialogBoxShower, InputService inputService, EntityService entityService, BindableButtonFactory bindableButtonFactory, DevModeManager devModeManager, ITooltipRegistrar tooltipRegistrar)
		{
			this._visualElementLoader = visualElementLoader;
			this._recoverableGoodDialogBoxShower = recoverableGoodDialogBoxShower;
			this._inputService = inputService;
			this._entityService = entityService;
			this._bindableButtonFactory = bindableButtonFactory;
			this._devModeManager = devModeManager;
			this._tooltipRegistrar = tooltipRegistrar;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002A6C File Offset: 0x00000C6C
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/EntityPanel/DeleteObjectFragment");
			Button button = UQueryExtensions.Q<Button>(this._root, "Button", null);
			this._deleteButton = this._bindableButtonFactory.Create(button, DeleteBuildingFragment.DeleteObjectKey, new Action(this.DeleteCallback), true);
			this._tooltipRegistrar.Register(button, new Func<string>(this.GetTooltipText));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public void ShowFragment(BaseComponent entity)
		{
			BaseComponent component = entity.GetComponent<Building>();
			ConstructionSite component2 = entity.GetComponent<ConstructionSite>();
			BlockObject component3 = entity.GetComponent<BlockObject>();
			if (component || component2 || (this._devModeManager.Enabled && component3))
			{
				this._selectedBlockObject = component3;
				this._deletionDescriber = entity.GetComponent<BlockObjectDeletionDescriber>();
				this._root.ToggleDisplayStyle(true);
				this._deleteButton.Bind();
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002B5F File Offset: 0x00000D5F
		public void ClearFragment()
		{
			this._selectedBlockObject = null;
			this._deletionDescriber = null;
			this._deleteButton.Unbind();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B86 File Offset: 0x00000D86
		public void UpdateFragment()
		{
			if (this._selectedBlockObject)
			{
				if (this.SelectedBuildingIsDeletable())
				{
					this._deleteButton.Enable();
					return;
				}
				this._deleteButton.Disable();
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002BB4 File Offset: 0x00000DB4
		public void DeleteCallback()
		{
			if (this._inputService.IsKeyHeld(DeleteBuildingFragment.SkipDeleteConfirmationKey))
			{
				this.DeleteBuilding();
				return;
			}
			this.ShowDialogBox();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002BD5 File Offset: 0x00000DD5
		public string GetTooltipText()
		{
			return this._deletionDescriber.GetDescription();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002BE2 File Offset: 0x00000DE2
		public void ShowDialogBox()
		{
			if (this.SelectedBuildingIsDeletable())
			{
				this._recoverableGoodDialogBoxShower.Show(this._selectedBlockObject, new Action(this.DeleteBuilding), DeleteBuildingFragment.DeletePromptLocKey);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002C0E File Offset: 0x00000E0E
		public void DeleteBuilding()
		{
			if (this.SelectedBuildingIsDeletable())
			{
				this._entityService.Delete(this._selectedBlockObject);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002C29 File Offset: 0x00000E29
		public bool SelectedBuildingIsDeletable()
		{
			return this._selectedBlockObject && this._selectedBlockObject.CanDelete();
		}

		// Token: 0x0400002F RID: 47
		public static readonly string DeleteObjectKey = "DeleteObject";

		// Token: 0x04000030 RID: 48
		public static readonly string SkipDeleteConfirmationKey = "SkipDeleteConfirmation";

		// Token: 0x04000031 RID: 49
		public static readonly string DeletePromptLocKey = "Buildings.DeletePrompt";

		// Token: 0x04000032 RID: 50
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000033 RID: 51
		public readonly RecoverableGoodDialogBoxShower _recoverableGoodDialogBoxShower;

		// Token: 0x04000034 RID: 52
		public readonly InputService _inputService;

		// Token: 0x04000035 RID: 53
		public readonly EntityService _entityService;

		// Token: 0x04000036 RID: 54
		public readonly BindableButtonFactory _bindableButtonFactory;

		// Token: 0x04000037 RID: 55
		public readonly DevModeManager _devModeManager;

		// Token: 0x04000038 RID: 56
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000039 RID: 57
		public BlockObject _selectedBlockObject;

		// Token: 0x0400003A RID: 58
		public BlockObjectDeletionDescriber _deletionDescriber;

		// Token: 0x0400003B RID: 59
		public BindableButton _deleteButton;

		// Token: 0x0400003C RID: 60
		public VisualElement _root;
	}
}
