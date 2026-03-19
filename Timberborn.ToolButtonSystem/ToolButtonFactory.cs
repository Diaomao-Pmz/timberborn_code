using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using Timberborn.AssetSystem;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.ToolButtonSystem
{
	// Token: 0x02000008 RID: 8
	public class ToolButtonFactory
	{
		// Token: 0x06000025 RID: 37 RVA: 0x0000250C File Offset: 0x0000070C
		public ToolButtonFactory(EventBus eventBus, ToolService toolService, DevModeManager devModeManager, VisualElementLoader visualElementLoader, ToolButtonService toolButtonService, IAssetLoader assetLoader, ToolGroupService toolGroupService, MapEditorMode mapEditorMode, IEnumerable<IToolDisabler> toolDisablers)
		{
			this._eventBus = eventBus;
			this._toolService = toolService;
			this._devModeManager = devModeManager;
			this._visualElementLoader = visualElementLoader;
			this._toolButtonService = toolButtonService;
			this._assetLoader = assetLoader;
			this._toolGroupService = toolGroupService;
			this._mapEditorMode = mapEditorMode;
			this._toolDisablers = toolDisablers.ToImmutableArray<IToolDisabler>();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000256C File Offset: 0x0000076C
		public ToolButton Create(ITool tool, string imageName, VisualElement parent)
		{
			Sprite toolImage = this._assetLoader.Load<Sprite>(Path.Combine(ToolButtonFactory.ImageDirectory, imageName));
			return this.Create(tool, toolImage, parent);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000259C File Offset: 0x0000079C
		public ToolButton Create(ITool tool, Sprite toolImage, VisualElement parent)
		{
			VisualElement content = this._visualElementLoader.LoadVisualElement("Common/BottomBar/ToolButton");
			return this.Create(tool, toolImage, parent, content);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025C4 File Offset: 0x000007C4
		public ToolButton CreateHex(ITool tool, Sprite toolImage, VisualElement parent)
		{
			VisualElement content = this._visualElementLoader.LoadVisualElement("Common/BottomBar/ToolButtonHex");
			return this.Create(tool, toolImage, parent, content);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025EC File Offset: 0x000007EC
		public ToolButton CreateGrouplessRed(ITool tool, string imageName)
		{
			Sprite toolImage = this._assetLoader.Load<Sprite>(Path.Combine(ToolButtonFactory.ImageDirectory, imageName));
			return this.CreateGroupless(tool, toolImage, "bottom-bar-button--red");
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002620 File Offset: 0x00000820
		public ToolButton CreateGrouplessBlue(ITool tool, string imageName)
		{
			Sprite toolImage = this._assetLoader.Load<Sprite>(Path.Combine(ToolButtonFactory.ImageDirectory, imageName));
			return this.CreateGroupless(tool, toolImage, "bottom-bar-button--blue");
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002651 File Offset: 0x00000851
		public ToolButton CreateGrouplessGreen(ITool tool, Sprite toolImage)
		{
			return this.CreateGroupless(tool, toolImage, "bottom-bar-button--green");
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002660 File Offset: 0x00000860
		public ToolButton CreateGroupless(ITool tool, Sprite toolImage, string className)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/BottomBar/GrouplessToolButton");
			UQueryExtensions.Q(visualElement, "Tooltip", null).ToggleDisplayStyle(false);
			visualElement.AddToClassList(className);
			return this.CreateButton(tool, toolImage, visualElement);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026A0 File Offset: 0x000008A0
		public ToolButton Create(ITool tool, Sprite toolImage, VisualElement parent, VisualElement content)
		{
			ToolButton toolButton = this.CreateButton(tool, toolImage, content);
			parent.Add(toolButton.Root);
			return toolButton;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026C8 File Offset: 0x000008C8
		public ToolButton CreateButton(ITool tool, Sprite toolImage, VisualElement root)
		{
			UQueryExtensions.Q<VisualElement>(root, "ToolImage", null).style.backgroundImage = new StyleBackground(toolImage);
			ToolButton toolButton = new ToolButton(this._toolService, this._devModeManager, this._toolGroupService, this._mapEditorMode, this._toolDisablers, tool, root, UQueryExtensions.Q<Button>(root, null, null));
			this._eventBus.Register(toolButton);
			this._toolButtonService.Add(toolButton);
			return toolButton;
		}

		// Token: 0x04000015 RID: 21
		public static readonly string ImageDirectory = "Sprites/BottomBar";

		// Token: 0x04000016 RID: 22
		public readonly EventBus _eventBus;

		// Token: 0x04000017 RID: 23
		public readonly ToolService _toolService;

		// Token: 0x04000018 RID: 24
		public readonly DevModeManager _devModeManager;

		// Token: 0x04000019 RID: 25
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001A RID: 26
		public readonly ToolButtonService _toolButtonService;

		// Token: 0x0400001B RID: 27
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400001C RID: 28
		public readonly ToolGroupService _toolGroupService;

		// Token: 0x0400001D RID: 29
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x0400001E RID: 30
		public readonly ImmutableArray<IToolDisabler> _toolDisablers;
	}
}
