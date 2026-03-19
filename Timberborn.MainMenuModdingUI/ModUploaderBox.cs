using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.Modding;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.MainMenuModdingUI
{
	// Token: 0x02000010 RID: 16
	public class ModUploaderBox : ILoadableSingleton, IPanelController
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00002F8C File Offset: 0x0000118C
		public ModUploaderBox(VisualElementLoader visualElementLoader, PanelStack panelStack, ModRepository modRepository)
		{
			this._visualElementLoader = visualElementLoader;
			this._panelStack = panelStack;
			this._modRepository = modRepository;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002FBF File Offset: 0x000011BF
		public bool HasUploader
		{
			get
			{
				return this._uploadButtons.Count > 0;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002FD0 File Offset: 0x000011D0
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Modding/ModUploaderBox");
			UQueryExtensions.Q<Button>(this._root, "CloseButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnUICancelled();
			}, 0);
			this._buttonsContainer = UQueryExtensions.Q<VisualElement>(this._root, "Buttons", null);
			this._mods.AddRange(this._modRepository.UserMods);
			this._modList = UQueryExtensions.Q<ListView>(this._root, "Items", null);
			this._modList.makeItem = (() => this._visualElementLoader.LoadVisualElement("Modding/UploadableModItem"));
			this._modList.bindItem = new Action<VisualElement, int>(this.BindItem);
			this._modList.itemsSource = this._mods;
			this._modList.selectionChanged += this.ModSelectionChanged;
			this._modList.virtualizationMethod = 1;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000030BC File Offset: 0x000012BC
		public void AddUploader(string text, Action<Mod> onUpload)
		{
			Button button = (Button)this._visualElementLoader.LoadVisualElement("Modding/UploadModButton");
			button.text = text;
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				onUpload((Mod)this._modList.selectedItem);
			}, 0);
			button.SetEnabled(false);
			this._buttonsContainer.Add(button);
			this._uploadButtons.Add(button);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000312C File Offset: 0x0000132C
		public void Show()
		{
			Asserts.IsTrue<ModUploaderBox>(this, this.HasUploader, "HasUploader");
			this._panelStack.HideAndPushOverlay(this);
			this._modList.ClearSelection();
			this._modList.ScrollToItem(0);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003162 File Offset: 0x00001362
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B28 File Offset: 0x00000D28
		public bool OnUIConfirmed()
		{
			return false;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000316A File Offset: 0x0000136A
		public void OnUICancelled()
		{
			this._panelStack.Pop(this);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003178 File Offset: 0x00001378
		public void BindItem(VisualElement visualElement, int index)
		{
			UQueryExtensions.Q<Label>(visualElement, "ModName", null).text = this._mods[index].DisplayName;
			UQueryExtensions.Q<Label>(visualElement, "ModVersion", null).text = this._mods[index].Manifest.Version.Formatted;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000031D8 File Offset: 0x000013D8
		public void ModSelectionChanged(IEnumerable<object> obj)
		{
			bool enabled = obj.Any<object>();
			foreach (Button button in this._uploadButtons)
			{
				button.SetEnabled(enabled);
			}
		}

		// Token: 0x0400004C RID: 76
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400004D RID: 77
		public readonly PanelStack _panelStack;

		// Token: 0x0400004E RID: 78
		public readonly ModRepository _modRepository;

		// Token: 0x0400004F RID: 79
		public readonly List<Mod> _mods = new List<Mod>();

		// Token: 0x04000050 RID: 80
		public readonly List<Button> _uploadButtons = new List<Button>();

		// Token: 0x04000051 RID: 81
		public VisualElement _root;

		// Token: 0x04000052 RID: 82
		public VisualElement _buttonsContainer;

		// Token: 0x04000053 RID: 83
		public ListView _modList;
	}
}
