using System;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.MapEditorPersistence;
using Timberborn.MapMetadataSystem;
using Timberborn.MapRepositorySystem;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;
using Timberborn.ToolPanelSystem;
using Timberborn.ToolSystem;
using Timberborn.UndoSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapMetadataSystemUI
{
	// Token: 0x02000004 RID: 4
	public class MapMetadataPanel : IToolFragment, IPostLoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public MapMetadataPanel(DevModeManager devModeManager, EventBus eventBus, MapDeserializer mapDeserializer, MapEditorMapLoader mapEditorMapLoader, MapMetadataSerializer mapMetadataSerializer, VisualElementLoader visualElementLoader, IUndoRegistry undoRegistry, MapSize mapSize, ToolService toolService, MapMetadataSaveEntryWriter mapMetadataSaveEntryWriter)
		{
			this._devModeManager = devModeManager;
			this._eventBus = eventBus;
			this._mapDeserializer = mapDeserializer;
			this._mapEditorMapLoader = mapEditorMapLoader;
			this._mapMetadataSerializer = mapMetadataSerializer;
			this._visualElementLoader = visualElementLoader;
			this._undoRegistry = undoRegistry;
			this._mapSize = mapSize;
			this._toolService = toolService;
			this._mapMetadataSaveEntryWriter = mapMetadataSaveEntryWriter;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002120 File Offset: 0x00000320
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("MapEditor/ToolPanel/MapMetadataPanel");
			this._mapDescription = UQueryExtensions.Q<TextField>(this._root, "MapDescription", null);
			this._mapDescriptionLocKey = UQueryExtensions.Q<TextField>(this._root, "MapDescriptionLocKey", null);
			this._mapNameLocKey = UQueryExtensions.Q<TextField>(this._root, "MapNameLocKey", null);
			this._isRecommendedToggle = UQueryExtensions.Q<Toggle>(this._root, "IsRecommended", null);
			this._isUnconventionalToggle = UQueryExtensions.Q<Toggle>(this._root, "IsUnconventional", null);
			this._isDevToggle = UQueryExtensions.Q<Toggle>(this._root, "IsDev", null);
			this._devControls = UQueryExtensions.Q<VisualElement>(this._root, "DevControls", null);
			this._root.ToggleDisplayStyle(false);
			this._eventBus.Register(this);
			this.UpdateDevControlsVisibility();
			this.RegisterOnEvents();
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000220E File Offset: 0x0000040E
		public void PostLoad()
		{
			this.SetMapMetadata(this.GetMapMetadata() ?? this.GetCurrentMapMetadata(), false);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002228 File Offset: 0x00000428
		[OnEvent]
		public void OnToolEntered(ToolEnteredEvent toolEnteredEvent)
		{
			MapMetadataTool mapMetadataTool = toolEnteredEvent.Tool as MapMetadataTool;
			if (mapMetadataTool != null)
			{
				this._mapMetadataTool = mapMetadataTool;
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002257 File Offset: 0x00000457
		[OnEvent]
		public void OnToolExited(ToolExitedEvent toolExitedEvent)
		{
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002265 File Offset: 0x00000465
		[OnEvent]
		public void OnDevModeToggled(DevModeToggledEvent devModeToggledEvent)
		{
			this.UpdateDevControlsVisibility();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002270 File Offset: 0x00000470
		public void RegisterOnEvents()
		{
			INotifyValueChangedExtensions.RegisterValueChangedCallback<string>(this._mapDescription, delegate(ChangeEvent<string> _)
			{
				this.OnValueChanged();
			});
			this._mapDescription.isDelayed = true;
			INotifyValueChangedExtensions.RegisterValueChangedCallback<string>(this._mapDescriptionLocKey, delegate(ChangeEvent<string> _)
			{
				this.OnValueChanged();
			});
			this._mapDescriptionLocKey.isDelayed = true;
			INotifyValueChangedExtensions.RegisterValueChangedCallback<string>(this._mapNameLocKey, delegate(ChangeEvent<string> _)
			{
				this.OnValueChanged();
			});
			this._mapNameLocKey.isDelayed = true;
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._isRecommendedToggle, delegate(ChangeEvent<bool> _)
			{
				this.OnValueChanged();
			});
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._isUnconventionalToggle, delegate(ChangeEvent<bool> _)
			{
				this.OnValueChanged();
			});
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._isDevToggle, delegate(ChangeEvent<bool> _)
			{
				this.OnValueChanged();
			});
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002331 File Offset: 0x00000531
		public void OnValueChanged()
		{
			this.SetMapMetadata(this.GetCurrentMapMetadata(), true);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002340 File Offset: 0x00000540
		public MapMetadata GetCurrentMapMetadata()
		{
			return new MapMetadata(this._mapSize.TerrainSize.x, this._mapSize.TerrainSize.y, this._mapNameLocKey.text, this._mapDescriptionLocKey.text, this._mapDescription.text, this._isRecommendedToggle.value, this._isUnconventionalToggle.value, this._isDevToggle.value);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023BA File Offset: 0x000005BA
		public void UpdateDevControlsVisibility()
		{
			this._devControls.ToggleDisplayStyle(this._devModeManager.Enabled);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023D4 File Offset: 0x000005D4
		public void SetMapMetadata(MapMetadata mapMetadata, bool registerChange = false)
		{
			MapMetadata currentMapMetadata = this._mapMetadataSaveEntryWriter.CurrentMapMetadata;
			this._mapMetadataSaveEntryWriter.SetCurrentMapMetadata(mapMetadata);
			if (registerChange)
			{
				this._undoRegistry.RegisterSingleUndoable(new MapMetadataPanel.MapMetadataUndoable(this, currentMapMetadata, mapMetadata));
			}
			this.FillMetadataElements(mapMetadata);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002418 File Offset: 0x00000618
		public void FillMetadataElements(MapMetadata mapMetadata)
		{
			if (mapMetadata != null)
			{
				this._mapDescription.SetValueWithoutNotify(mapMetadata.MapDescription);
				this._mapDescriptionLocKey.SetValueWithoutNotify(mapMetadata.MapDescriptionLocKey);
				this._mapNameLocKey.SetValueWithoutNotify(mapMetadata.MapNameLocKey);
				this._isRecommendedToggle.SetValueWithoutNotify(mapMetadata.IsRecommended);
				this._isUnconventionalToggle.SetValueWithoutNotify(mapMetadata.IsUnconventional);
				this._isDevToggle.SetValueWithoutNotify(mapMetadata.IsDev);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000248E File Offset: 0x0000068E
		public void OpenToolPanel()
		{
			this._toolService.SwitchTool(this._mapMetadataTool);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000024A4 File Offset: 0x000006A4
		public MapMetadata GetMapMetadata()
		{
			if (this._mapEditorMapLoader.LoadedMap != null)
			{
				MapFileReference value = this._mapEditorMapLoader.LoadedMap.Value;
				return this._mapDeserializer.ReadFromMapFileUnsafe<MapMetadata>(value, this._mapMetadataSerializer);
			}
			return null;
		}

		// Token: 0x04000006 RID: 6
		public readonly DevModeManager _devModeManager;

		// Token: 0x04000007 RID: 7
		public readonly EventBus _eventBus;

		// Token: 0x04000008 RID: 8
		public readonly MapDeserializer _mapDeserializer;

		// Token: 0x04000009 RID: 9
		public readonly MapEditorMapLoader _mapEditorMapLoader;

		// Token: 0x0400000A RID: 10
		public readonly MapMetadataSerializer _mapMetadataSerializer;

		// Token: 0x0400000B RID: 11
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000C RID: 12
		public readonly IUndoRegistry _undoRegistry;

		// Token: 0x0400000D RID: 13
		public readonly MapSize _mapSize;

		// Token: 0x0400000E RID: 14
		public readonly ToolService _toolService;

		// Token: 0x0400000F RID: 15
		public readonly MapMetadataSaveEntryWriter _mapMetadataSaveEntryWriter;

		// Token: 0x04000010 RID: 16
		public VisualElement _root;

		// Token: 0x04000011 RID: 17
		public MapMetadataTool _mapMetadataTool;

		// Token: 0x04000012 RID: 18
		public TextField _mapDescription;

		// Token: 0x04000013 RID: 19
		public TextField _mapDescriptionLocKey;

		// Token: 0x04000014 RID: 20
		public TextField _mapNameLocKey;

		// Token: 0x04000015 RID: 21
		public Toggle _isRecommendedToggle;

		// Token: 0x04000016 RID: 22
		public Toggle _isUnconventionalToggle;

		// Token: 0x04000017 RID: 23
		public Toggle _isDevToggle;

		// Token: 0x04000018 RID: 24
		public VisualElement _devControls;

		// Token: 0x02000005 RID: 5
		public class MapMetadataUndoable : IUndoable
		{
			// Token: 0x06000017 RID: 23 RVA: 0x000024F6 File Offset: 0x000006F6
			public MapMetadataUndoable(MapMetadataPanel metadataPanel, MapMetadata oldValue, MapMetadata newValue)
			{
				this._metadataPanel = metadataPanel;
				this._oldValue = oldValue;
				this._newValue = newValue;
			}

			// Token: 0x06000018 RID: 24 RVA: 0x00002513 File Offset: 0x00000713
			public void Undo()
			{
				this._metadataPanel.SetMapMetadata(this._oldValue, false);
				this._metadataPanel.OpenToolPanel();
			}

			// Token: 0x06000019 RID: 25 RVA: 0x00002532 File Offset: 0x00000732
			public void Redo()
			{
				this._metadataPanel.SetMapMetadata(this._newValue, false);
				this._metadataPanel.OpenToolPanel();
			}

			// Token: 0x04000019 RID: 25
			public readonly MapMetadataPanel _metadataPanel;

			// Token: 0x0400001A RID: 26
			public readonly MapMetadata _oldValue;

			// Token: 0x0400001B RID: 27
			public readonly MapMetadata _newValue;
		}
	}
}
