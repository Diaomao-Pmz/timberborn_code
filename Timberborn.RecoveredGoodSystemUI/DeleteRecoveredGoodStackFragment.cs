using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.EntitySystem;
using Timberborn.InputSystem;
using Timberborn.InputSystemUI;
using Timberborn.RecoveredGoodSystem;
using UnityEngine.UIElements;

namespace Timberborn.RecoveredGoodSystemUI
{
	// Token: 0x02000004 RID: 4
	public class DeleteRecoveredGoodStackFragment : IEntityPanelFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public DeleteRecoveredGoodStackFragment(VisualElementLoader visualElementLoader, DialogBoxShower dialogBoxShower, InputService inputService, EntityService entityService, BindableButtonFactory bindableButtonFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._dialogBoxShower = dialogBoxShower;
			this._inputService = inputService;
			this._entityService = entityService;
			this._bindableButtonFactory = bindableButtonFactory;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020EC File Offset: 0x000002EC
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/EntityPanel/DeleteObjectFragment");
			this._deleteButton = this._bindableButtonFactory.Create(UQueryExtensions.Q<Button>(this._root, "Button", null), DeleteRecoveredGoodStackFragment.DeleteObjectKey, new Action(this.DeleteCallback), true);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002155 File Offset: 0x00000355
		public void ShowFragment(BaseComponent entity)
		{
			this._recoveredGoodStack = entity.GetComponent<RecoveredGoodStack>();
			if (this._recoveredGoodStack)
			{
				this._root.ToggleDisplayStyle(true);
				this._deleteButton.Bind();
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002187 File Offset: 0x00000387
		public void ClearFragment()
		{
			if (this._recoveredGoodStack)
			{
				this._recoveredGoodStack = null;
				this._deleteButton.Unbind();
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021B4 File Offset: 0x000003B4
		public void UpdateFragment()
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021B6 File Offset: 0x000003B6
		public void DeleteCallback()
		{
			if (this._inputService.IsKeyHeld(DeleteRecoveredGoodStackFragment.SkipDeleteConfirmationKey))
			{
				this.DeleteRecoveredGoodStack();
				return;
			}
			this.ShowDialogBox();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021D7 File Offset: 0x000003D7
		public void ShowDialogBox()
		{
			this._dialogBoxShower.Create().SetLocalizedMessage(DeleteRecoveredGoodStackFragment.DeletePromptKey).SetConfirmButton(new Action(this.DeleteRecoveredGoodStack)).SetDefaultCancelButton().Show();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000220A File Offset: 0x0000040A
		public void DeleteRecoveredGoodStack()
		{
			if (this._recoveredGoodStack)
			{
				this._entityService.Delete(this._recoveredGoodStack);
			}
		}

		// Token: 0x04000006 RID: 6
		public static readonly string DeleteObjectKey = "DeleteObject";

		// Token: 0x04000007 RID: 7
		public static readonly string DeletePromptKey = "RecoveredGoodStack.DeletePrompt";

		// Token: 0x04000008 RID: 8
		public static readonly string SkipDeleteConfirmationKey = "SkipDeleteConfirmation";

		// Token: 0x04000009 RID: 9
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000A RID: 10
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x0400000B RID: 11
		public readonly InputService _inputService;

		// Token: 0x0400000C RID: 12
		public readonly EntityService _entityService;

		// Token: 0x0400000D RID: 13
		public readonly BindableButtonFactory _bindableButtonFactory;

		// Token: 0x0400000E RID: 14
		public BindableButton _deleteButton;

		// Token: 0x0400000F RID: 15
		public VisualElement _root;

		// Token: 0x04000010 RID: 16
		public RecoveredGoodStack _recoveredGoodStack;
	}
}
