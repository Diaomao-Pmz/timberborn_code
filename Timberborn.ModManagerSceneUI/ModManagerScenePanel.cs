using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.CommandLine;
using Timberborn.FileSystem;
using Timberborn.Modding;
using Timberborn.ModdingAssets;
using Timberborn.ModdingUI;
using Timberborn.ModManagerScene;
using Timberborn.PlatformUtilities;
using Timberborn.SerializationSystem;
using Timberborn.SteamStoreSystem;
using Timberborn.SteamWorkshopContent;
using Timberborn.SteamWorkshopModDownloading;
using Timberborn.Versioning;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Timberborn.ModManagerSceneUI
{
	// Token: 0x02000005 RID: 5
	public class ModManagerScenePanel : MonoBehaviour
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002130 File Offset: 0x00000330
		public void Awake()
		{
			if (this._fileService.HasDocumentsPermissions && !ModManagerScenePanel.ShouldIgnoreMods())
			{
				ModRepository modRepository = this.CreateModRepository();
				if (modRepository != null && modRepository.Mods.Any<Mod>())
				{
					if (this.AutoStartingInEditor || ModManagerScenePanel.ShouldSkipModManager())
					{
						this.LoadModsAndStartGame();
						return;
					}
					this.InitializeModManagerPanel(modRepository.Mods);
					return;
				}
			}
			this.StartGame();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002196 File Offset: 0x00000396
		public void Update()
		{
			if (ModManagerScenePanel.WasKeyReleased(2) || ModManagerScenePanel.WasKeyReleased(77))
			{
				this.LoadModsAndStartGame();
				return;
			}
			ModListView modListView = this._modListView;
			if (modListView == null)
			{
				return;
			}
			modListView.Update();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021C0 File Offset: 0x000003C0
		public bool AutoStartingInEditor
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021C3 File Offset: 0x000003C3
		public static bool ShouldIgnoreMods()
		{
			return CommandLineArguments.CreateWithCommandLineArgs().Has(ModManagerScenePanel.BenchmarkLengthKey);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021D4 File Offset: 0x000003D4
		public static bool ShouldSkipModManager()
		{
			return CommandLineArguments.CreateWithCommandLineArgs().Has(ModManagerScenePanel.SkipModManagerKey);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021E8 File Offset: 0x000003E8
		public ModRepository CreateModRepository()
		{
			ModLoader modLoader = new ModLoader(new ManifestLoader(new SerializedObjectReaderWriter(new JsonMerger())));
			ModRepository modRepository = new ModRepository(modLoader, new ModSorter(), this.GetModProviders(modLoader));
			modRepository.Load();
			return modRepository;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002222 File Offset: 0x00000422
		public IEnumerable<IModsProvider> GetModProviders(ModLoader modLoader)
		{
			yield return new UserFolderModsProvider(this._fileService);
			SteamManager steamManager = new SteamManager();
			steamManager.Load();
			if (steamManager.Initialized)
			{
				yield return new SteamWorkshopModsProvider(new SteamWorkshopContentProvider(steamManager), modLoader);
			}
			yield break;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000223C File Offset: 0x0000043C
		public void LoadModsAndStartGame()
		{
			ModRepository modRepository = this.CreateModRepository();
			List<Mod> list = (from mod in modRepository.Mods
			where mod.IsEnabled
			select mod).ToList<Mod>();
			if (list.Any<Mod>())
			{
				ModdedState.SetOfficialMods(list);
			}
			new ModCodeStarter(modRepository).Start();
			new ModAssetBundleLoader(modRepository).Load();
			this.StartGame();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022A8 File Offset: 0x000004A8
		public void StartGame()
		{
			if (this._modManagerBox != null)
			{
				this._modManagerBox.style.display = 1;
			}
			SceneManager.LoadScene(ModManagerScenePanel.MainMenuSceneName);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022D4 File Offset: 0x000004D4
		public void InitializeModManagerPanel(IEnumerable<Mod> mods)
		{
			this._modListView = new ModListView(base.GetComponent<ModManagerSceneItemFactory>(), new ModSorter());
			VisualElement rootVisualElement = this._uiDocument.rootVisualElement;
			this._modListView.Initialize(rootVisualElement, mods);
			UQueryExtensions.Q<ScrollView>(rootVisualElement, null, null).mouseWheelScrollSize = ScrollWheelSpeed.WheelScrollSize.Value;
			UQueryExtensions.Q<Button>(rootVisualElement, "StartButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.LoadModsAndStartGame();
			}, 0);
			this._modManagerBox = UQueryExtensions.Q<VisualElement>(rootVisualElement, "ModManagerBox", null);
			UQueryExtensions.Q<Label>(rootVisualElement, "GameVersion", null).text = GameVersions.CurrentVersion.Formatted;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002375 File Offset: 0x00000575
		public static bool WasKeyReleased(Key key)
		{
			return Keyboard.current[key].wasReleasedThisFrame;
		}

		// Token: 0x04000007 RID: 7
		[HideInInspector]
		public static readonly string MainMenuSceneName = "1-MainMenuScene";

		// Token: 0x04000008 RID: 8
		[HideInInspector]
		public static readonly string BenchmarkLengthKey = "benchmarkLength";

		// Token: 0x04000009 RID: 9
		[HideInInspector]
		public static readonly string SkipModManagerKey = "skipModManager";

		// Token: 0x0400000A RID: 10
		[SerializeField]
		public UIDocument _uiDocument;

		// Token: 0x0400000B RID: 11
		[HideInInspector]
		public readonly FileService _fileService = new FileService();

		// Token: 0x0400000C RID: 12
		[HideInInspector]
		public VisualElement _modManagerBox;

		// Token: 0x0400000D RID: 13
		[HideInInspector]
		public ModListView _modListView;
	}
}
