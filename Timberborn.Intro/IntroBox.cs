using System;
using System.IO;
using Bindito.Unity;
using Timberborn.AssetSystem;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.IntroSettingsSystem;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.TitleScreenUI;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

namespace Timberborn.Intro
{
	// Token: 0x02000004 RID: 4
	public class IntroBox : IPanelController, IUnloadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020B8 File Offset: 0x000002B8
		public IntroBox(PanelStack panelStack, VisualElementLoader visualElementLoader, RootObjectProvider rootObjectProvider, IAssetLoader assetLoader, IInstantiator instantiator, TitleScreen titleScreen, IntroSettings introSettings, MouseController mouseController)
		{
			this._panelStack = panelStack;
			this._visualElementLoader = visualElementLoader;
			this._rootObjectProvider = rootObjectProvider;
			this._assetLoader = assetLoader;
			this._instantiator = instantiator;
			this._titleScreen = titleScreen;
			this._introSettings = introSettings;
			this._mouseController = mouseController;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002108 File Offset: 0x00000308
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002110 File Offset: 0x00000310
		public bool OnUIConfirmed()
		{
			this.Start();
			return false;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002119 File Offset: 0x00000319
		public void OnUICancelled()
		{
			this.Start();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002124 File Offset: 0x00000324
		public void Show(Action onStart)
		{
			if (this._introSettings.DisableIntro || !File.Exists(IntroBox.IntroPath))
			{
				if (onStart != null)
				{
					onStart();
					return;
				}
			}
			else
			{
				this._root = this._visualElementLoader.LoadVisualElement("MainMenu/IntroBox");
				this._rootObject = this._rootObjectProvider.CreateRootObject("IntroBox");
				this._titleScreen.HideBackground();
				this._mouseController.HideCursor();
				this.InitializeVideoPlayer();
				this._onStart = onStart;
				this._panelStack.Push(this);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021AF File Offset: 0x000003AF
		public void Unload()
		{
			this._assetLoader.Load<RenderTexture>("Intro/Intro").Release();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021C8 File Offset: 0x000003C8
		public void Start()
		{
			this._root = null;
			Object.Destroy(this._rootObject);
			this._panelStack.Pop(this);
			this._titleScreen.ShowBackground();
			this._mouseController.ShowCursor();
			Action onStart = this._onStart;
			if (onStart == null)
			{
				return;
			}
			onStart();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000221C File Offset: 0x0000041C
		public void InitializeVideoPlayer()
		{
			GameObject gameObject = this._assetLoader.Load<GameObject>("Intro/Intro");
			VideoPlayer component = this._instantiator.Instantiate(gameObject, this._rootObject.transform).GetComponent<VideoPlayer>();
			component.source = 1;
			component.url = IntroBox.IntroPath;
			component.Prepare();
			component.prepareCompleted += delegate(VideoPlayer player)
			{
				player.Play();
			};
			component.loopPointReached += delegate(VideoPlayer _)
			{
				this.Start();
			};
			IntroBox.ClearRenderTexture(component);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022AA File Offset: 0x000004AA
		public static void ClearRenderTexture(VideoPlayer videoPlayer)
		{
			RenderTexture active = RenderTexture.active;
			RenderTexture.active = videoPlayer.targetTexture;
			GL.Clear(true, true, Color.black);
			RenderTexture.active = active;
		}

		// Token: 0x04000006 RID: 6
		public static readonly string IntroPath = Path.Combine(Application.streamingAssetsPath, "Intro", "Timberborn_Intro.mp4");

		// Token: 0x04000007 RID: 7
		public readonly PanelStack _panelStack;

		// Token: 0x04000008 RID: 8
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000009 RID: 9
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x0400000A RID: 10
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400000B RID: 11
		public readonly IInstantiator _instantiator;

		// Token: 0x0400000C RID: 12
		public readonly TitleScreen _titleScreen;

		// Token: 0x0400000D RID: 13
		public readonly IntroSettings _introSettings;

		// Token: 0x0400000E RID: 14
		public readonly MouseController _mouseController;

		// Token: 0x0400000F RID: 15
		public VisualElement _root;

		// Token: 0x04000010 RID: 16
		public GameObject _rootObject;

		// Token: 0x04000011 RID: 17
		public Action _onStart;
	}
}
