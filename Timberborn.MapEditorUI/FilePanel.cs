using System;
using JetBrains.Annotations;
using Timberborn.CoreUI;
using Timberborn.InputSystemUI;
using Timberborn.Localization;
using Timberborn.MapEditorPersistenceUI;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using Timberborn.UndoSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorUI
{
	// Token: 0x02000004 RID: 4
	public class FilePanel : ILoadableSingleton
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002261 File Offset: 0x00000461
		public FilePanel(MapSaverLoader mapSaverLoader, VisualElementLoader visualElementLoader, UILayout uiLayout, BindableButtonFactory bindableButtonFactory, ILoc loc, IUndoRegistry undoRegistry, EventBus eventBus)
		{
			this._mapSaverLoader = mapSaverLoader;
			this._visualElementLoader = visualElementLoader;
			this._uiLayout = uiLayout;
			this._bindableButtonFactory = bindableButtonFactory;
			this._loc = loc;
			this._undoRegistry = undoRegistry;
			this._eventBus = eventBus;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022A0 File Offset: 0x000004A0
		public void Load()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("MapEditor/FilePanel");
			this._mapFileButtons = visualElement.Q("MapFileButtons", null);
			this._bindableButtonFactory.CreateAndBind(visualElement.Q("SaveButton", null), FilePanel.SaveMapKey, delegate
			{
				this._mapSaverLoader.Save(null);
			});
			visualElement.Q("SaveAsButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._mapSaverLoader.SaveAs(null);
			}, TrickleDown.NoTrickleDown);
			visualElement.Q("LoadButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._mapSaverLoader.LoadMap();
			}, TrickleDown.NoTrickleDown);
			visualElement.Q("NewMapButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._mapSaverLoader.NewMap();
			}, TrickleDown.NoTrickleDown);
			this._undoButton = this._bindableButtonFactory.CreateAndBind(visualElement.Q("UndoButton", null), FilePanel.UndoKey, delegate
			{
				this._undoRegistry.Undo();
			});
			this._redoButton = this._bindableButtonFactory.CreateAndBind(visualElement.Q("RedoButton", null), FilePanel.RedoKey, delegate
			{
				this._undoRegistry.Redo();
			});
			this.UpdateUndoButtons();
			this._uiLayout.AddTopLeft(visualElement, 1);
			this._eventBus.Register(this);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023D0 File Offset: 0x000005D0
		[UsedImplicitly]
		public void AddMapFileButton(Action action, string locKey)
		{
			Button button = (Button)this._visualElementLoader.LoadVisualElement("MapEditor/FilePanelButton");
			button.text = this._loc.T(locKey);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				action();
			}, TrickleDown.NoTrickleDown);
			this._mapFileButtons.Add(button);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002431 File Offset: 0x00000631
		[OnEvent]
		public void OnUndoStateChanged(UndoStateChangedEvent undoStateChangedEvent)
		{
			this.UpdateUndoButtons();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000243C File Offset: 0x0000063C
		private void UpdateUndoButtons()
		{
			if (this._undoRegistry.CanUndo)
			{
				this._undoButton.Enable();
			}
			else
			{
				this._undoButton.Disable();
			}
			if (this._undoRegistry.CanRedo)
			{
				this._redoButton.Enable();
				return;
			}
			this._redoButton.Disable();
		}

		// Token: 0x0400000B RID: 11
		private static readonly string SaveMapKey = "SaveMap";

		// Token: 0x0400000C RID: 12
		private static readonly string UndoKey = "Undo";

		// Token: 0x0400000D RID: 13
		private static readonly string RedoKey = "Redo";

		// Token: 0x0400000E RID: 14
		private readonly MapSaverLoader _mapSaverLoader;

		// Token: 0x0400000F RID: 15
		private readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000010 RID: 16
		private readonly UILayout _uiLayout;

		// Token: 0x04000011 RID: 17
		private readonly BindableButtonFactory _bindableButtonFactory;

		// Token: 0x04000012 RID: 18
		private readonly ILoc _loc;

		// Token: 0x04000013 RID: 19
		private readonly IUndoRegistry _undoRegistry;

		// Token: 0x04000014 RID: 20
		private readonly EventBus _eventBus;

		// Token: 0x04000015 RID: 21
		private VisualElement _mapFileButtons;

		// Token: 0x04000016 RID: 22
		private BindableButton _undoButton;

		// Token: 0x04000017 RID: 23
		private BindableButton _redoButton;
	}
}
