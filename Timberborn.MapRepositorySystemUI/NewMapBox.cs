using System;
using Timberborn.BlueprintSystem;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.MapEditorSceneLoading;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.MapRepositorySystemUI
{
	// Token: 0x02000013 RID: 19
	public class NewMapBox : IPanelController, ILoadableSingleton
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002D19 File Offset: 0x00000F19
		public NewMapBox(MapEditorSceneLoader mapEditorSceneLoader, DialogBoxShower dialogBoxShower, ILoc loc, VisualElementLoader visualElementLoader, PanelStack panelStack, ISpecService specService)
		{
			this._mapEditorSceneLoader = mapEditorSceneLoader;
			this._dialogBoxShower = dialogBoxShower;
			this._loc = loc;
			this._visualElementLoader = visualElementLoader;
			this._panelStack = panelStack;
			this._specService = specService;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D50 File Offset: 0x00000F50
		public void Load()
		{
			this._mapSizeSpec = this._specService.GetSingleSpec<MapSizeSpec>();
			this._root = this._visualElementLoader.LoadVisualElement("Options/NewMapBox");
			UQueryExtensions.Q<Button>(this._root, "StartButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.StartNewMap();
			}, 0);
			UQueryExtensions.Q<Button>(this._root, "CloseButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnUICancelled();
			}, 0);
			this._sizeXField = UQueryExtensions.Q<TextField>(this._root, "SizeXField", null);
			this._sizeYField = UQueryExtensions.Q<TextField>(this._root, "SizeYField", null);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public VisualElement GetPanel()
		{
			this._sizeXField.value = this._mapSizeSpec.DefaultMapSize.x.ToString();
			this._sizeYField.value = this._mapSizeSpec.DefaultMapSize.y.ToString();
			return this._root;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002E57 File Offset: 0x00001057
		public bool OnUIConfirmed()
		{
			this.StartNewMap();
			return true;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002E60 File Offset: 0x00001060
		public void OnUICancelled()
		{
			this._panelStack.Pop(this);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002E70 File Offset: 0x00001070
		public void StartNewMap()
		{
			int num;
			int num2;
			if (this.TryParseSize(this._sizeXField.text, out num) && this.TryParseSize(this._sizeYField.text, out num2))
			{
				Vector2Int mapSize;
				mapSize..ctor(num, num2);
				this._mapEditorSceneLoader.StartNewMap(mapSize);
				return;
			}
			this._dialogBoxShower.Create().SetMessage(this._loc.T<int, int>(NewMapBox.SizePromptLocKey, this._mapSizeSpec.MinMapSize, this._mapSizeSpec.MaxMapSize)).Show();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002EF9 File Offset: 0x000010F9
		public bool TryParseSize(string text, out int size)
		{
			return int.TryParse(text, out size) && size >= this._mapSizeSpec.MinMapSize && size <= this._mapSizeSpec.MaxMapSize;
		}

		// Token: 0x0400003C RID: 60
		public static readonly string SizePromptLocKey = "MapEditor.SizePrompt";

		// Token: 0x0400003D RID: 61
		public readonly MapEditorSceneLoader _mapEditorSceneLoader;

		// Token: 0x0400003E RID: 62
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x0400003F RID: 63
		public readonly ILoc _loc;

		// Token: 0x04000040 RID: 64
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000041 RID: 65
		public readonly PanelStack _panelStack;

		// Token: 0x04000042 RID: 66
		public readonly ISpecService _specService;

		// Token: 0x04000043 RID: 67
		public MapSizeSpec _mapSizeSpec;

		// Token: 0x04000044 RID: 68
		public VisualElement _root;

		// Token: 0x04000045 RID: 69
		public TextField _sizeXField;

		// Token: 0x04000046 RID: 70
		public TextField _sizeYField;
	}
}
