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
	// Token: 0x02000005 RID: 5
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
			this._mapFileButtons = UQueryExtensions.Q<VisualElement>(visualElement, "MapFileButtons", null);
			this._bindableButtonFactory.CreateAndBind(UQueryExtensions.Q<Button>(visualElement, "SaveButton", null), FilePanel.SaveMapKey, delegate
			{
				this._mapSaverLoader.Save(null);
			});
			UQueryExtensions.Q<Button>(visualElement, "SaveAsButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._mapSaverLoader.SaveAs(null);
			}, 0);
			UQueryExtensions.Q<Button>(visualElement, "LoadButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._mapSaverLoader.LoadMap();
			}, 0);
			UQueryExtensions.Q<Button>(visualElement, "NewMapButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._mapSaverLoader.NewMap();
			}, 0);
			this._undoButton = this._bindableButtonFactory.CreateAndBind(UQueryExtensions.Q<Button>(visualElement, "UndoButton", null), FilePanel.UndoKey, delegate
			{
				this._undoRegistry.Undo();
			});
			this._redoButton = this._bindableButtonFactory.CreateAndBind(UQueryExtensions.Q<Button>(visualElement, "RedoButton", null), FilePanel.RedoKey, delegate
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
			}, 0);
			this._mapFileButtons.Add(button);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002431 File Offset: 0x00000631
		[OnEvent]
		public void OnUndoStateChanged(UndoStateChangedEvent undoStateChangedEvent)
		{
			this.UpdateUndoButtons();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000243C File Offset: 0x0000063C
		public void UpdateUndoButtons()
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

		// Token: 0x04000010 RID: 16
		public static readonly string SaveMapKey = "SaveMap";

		// Token: 0x04000011 RID: 17
		public static readonly string UndoKey = "Undo";

		// Token: 0x04000012 RID: 18
		public static readonly string RedoKey = "Redo";

		// Token: 0x04000013 RID: 19
		public readonly MapSaverLoader _mapSaverLoader;

		// Token: 0x04000014 RID: 20
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000015 RID: 21
		public readonly UILayout _uiLayout;

		// Token: 0x04000016 RID: 22
		public readonly BindableButtonFactory _bindableButtonFactory;

		// Token: 0x04000017 RID: 23
		public readonly ILoc _loc;

		// Token: 0x04000018 RID: 24
		public readonly IUndoRegistry _undoRegistry;

		// Token: 0x04000019 RID: 25
		public readonly EventBus _eventBus;

		// Token: 0x0400001A RID: 26
		public VisualElement _mapFileButtons;

		// Token: 0x0400001B RID: 27
		public BindableButton _undoButton;

		// Token: 0x0400001C RID: 28
		public BindableButton _redoButton;
	}
}
