using System;
using System.Collections;
using System.Diagnostics;
using Timberborn.AssetSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Timberborn.SceneLoading
{
	// Token: 0x02000009 RID: 9
	public class SceneLoader : ISceneLoader
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002361 File Offset: 0x00000561
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002369 File Offset: 0x00000569
		public long LastLoadTimeMs { get; private set; }

		// Token: 0x0600001C RID: 28 RVA: 0x00002372 File Offset: 0x00000572
		public SceneLoader(LoadingScreen loadingScreen, IAssetLoader assetLoader, CoroutineStarter coroutineStarter)
		{
			this._loadingScreen = loadingScreen;
			this._assetLoader = assetLoader;
			this._coroutineStarter = coroutineStarter;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000238F File Offset: 0x0000058F
		public void LoadScene(ISceneParameters sceneParameters, string tip)
		{
			this.LoadSceneInternal(sceneParameters, false, tip);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000239A File Offset: 0x0000059A
		public void LoadSceneInstantly(ISceneParameters sceneParameters, string tip)
		{
			this.LoadSceneInternal(sceneParameters, true, tip);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023A5 File Offset: 0x000005A5
		public void LoadSceneInstantly(ISceneParameters sceneParameters)
		{
			this.LoadSceneInternal(sceneParameters, true, null);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023B0 File Offset: 0x000005B0
		public bool HasAnySceneParameters()
		{
			return this._sceneParameters != null;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023BC File Offset: 0x000005BC
		public bool TryGetSceneParameters<T>(out T sceneParameters) where T : ISceneParameters
		{
			ISceneParameters sceneParameters2 = this._sceneParameters;
			if (sceneParameters2 is T)
			{
				T t = (T)((object)sceneParameters2);
				sceneParameters = t;
				return true;
			}
			sceneParameters = default(T);
			return false;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023F0 File Offset: 0x000005F0
		public T GetSceneParameters<T>() where T : ISceneParameters
		{
			return (T)((object)this._sceneParameters);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023FD File Offset: 0x000005FD
		public void LoadSceneInternal(ISceneParameters sceneParameters, bool instantly, string tip)
		{
			this._coroutineStarter.StartCoroutine(this.LoadSceneCoroutine(sceneParameters, instantly, tip));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002413 File Offset: 0x00000613
		public IEnumerator LoadSceneCoroutine(ISceneParameters sceneParameters, bool instantly, string tip)
		{
			while (this._isLoading)
			{
				yield return null;
			}
			this._isLoading = true;
			this._sceneParameters = sceneParameters;
			this._loadingScreen.Enable(tip);
			Time.timeScale = 0f;
			Stopwatch stopwatch = Stopwatch.StartNew();
			if (!instantly)
			{
				yield return null;
			}
			this._assetLoader.Reset();
			SceneManager.LoadScene(this._sceneParameters.SceneIndex);
			yield return Resources.UnloadUnusedAssets();
			GC.Collect();
			this._loadingScreen.Disable();
			yield return new WaitForEndOfFrame();
			stopwatch.Stop();
			long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
			this.LastLoadTimeMs = elapsedMilliseconds;
			Debug.Log(string.Format("Load time: {0}ms (scene index: {1})", elapsedMilliseconds, this._sceneParameters.SceneIndex));
			this._isLoading = false;
			yield break;
		}

		// Token: 0x04000010 RID: 16
		public readonly LoadingScreen _loadingScreen;

		// Token: 0x04000011 RID: 17
		public readonly IAssetLoader _assetLoader;

		// Token: 0x04000012 RID: 18
		public readonly CoroutineStarter _coroutineStarter;

		// Token: 0x04000013 RID: 19
		public ISceneParameters _sceneParameters;

		// Token: 0x04000014 RID: 20
		public bool _isLoading;
	}
}
