using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BlockSystemUI;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.EntitySystem;
using Timberborn.InputSystemUI;
using Timberborn.TooltipSystem;
using Timberborn.UndoSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorUI
{
	// Token: 0x02000004 RID: 4
	public class DeleteBlockObjectFragment : IEntityPanelFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BF File Offset: 0x000002BF
		public DeleteBlockObjectFragment(VisualElementLoader visualElementLoader, EntityService entityService, BindableButtonFactory bindableButtonFactory, ITooltipRegistrar tooltipRegistrar, IUndoRegistry undoRegistry)
		{
			this._visualElementLoader = visualElementLoader;
			this._entityService = entityService;
			this._bindableButtonFactory = bindableButtonFactory;
			this._tooltipRegistrar = tooltipRegistrar;
			this._undoRegistry = undoRegistry;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020EC File Offset: 0x000002EC
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/EntityPanel/DeleteObjectFragment");
			Button button = UQueryExtensions.Q<Button>(this._root, "Button", null);
			this._deleteButton = this._bindableButtonFactory.Create(button, DeleteBlockObjectFragment.DeleteObjectKey, new Action(this.DeleteObject), true);
			this._tooltipRegistrar.Register(button, new Func<string>(this.GetTooltipText));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002170 File Offset: 0x00000370
		public void ShowFragment(BaseComponent entity)
		{
			this._selectedBlockObject = entity.GetComponent<BlockObject>();
			if (this._selectedBlockObject)
			{
				this._deletionDescriber = this._selectedBlockObject.GetComponent<BlockObjectDeletionDescriber>();
				this._root.ToggleDisplayStyle(true);
				this._deleteButton.Bind();
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021BE File Offset: 0x000003BE
		public void ClearFragment()
		{
			this._selectedBlockObject = null;
			this._deletionDescriber = null;
			this._deleteButton.Unbind();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021E5 File Offset: 0x000003E5
		public void UpdateFragment()
		{
			if (this.IsSelectedObjectDeletable())
			{
				this._deleteButton.Enable();
				return;
			}
			this._deleteButton.Disable();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002206 File Offset: 0x00000406
		public void DeleteObject()
		{
			if (this.IsSelectedObjectDeletable())
			{
				this._entityService.Delete(this._selectedBlockObject);
				this._undoRegistry.CommitStack();
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000222C File Offset: 0x0000042C
		public string GetTooltipText()
		{
			return this._deletionDescriber.GetDescription();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002239 File Offset: 0x00000439
		public bool IsSelectedObjectDeletable()
		{
			return this._selectedBlockObject && this._selectedBlockObject.CanDelete();
		}

		// Token: 0x04000006 RID: 6
		public static readonly string DeleteObjectKey = "DeleteObject";

		// Token: 0x04000007 RID: 7
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000008 RID: 8
		public readonly EntityService _entityService;

		// Token: 0x04000009 RID: 9
		public readonly BindableButtonFactory _bindableButtonFactory;

		// Token: 0x0400000A RID: 10
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400000B RID: 11
		public readonly IUndoRegistry _undoRegistry;

		// Token: 0x0400000C RID: 12
		public BlockObject _selectedBlockObject;

		// Token: 0x0400000D RID: 13
		public BlockObjectDeletionDescriber _deletionDescriber;

		// Token: 0x0400000E RID: 14
		public VisualElement _root;

		// Token: 0x0400000F RID: 15
		public BindableButton _deleteButton;
	}
}
