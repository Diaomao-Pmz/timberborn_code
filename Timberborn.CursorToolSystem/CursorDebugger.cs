using System;
using Bindito.Unity;
using Timberborn.AssetSystem;
using Timberborn.Coordinates;
using Timberborn.Debugging;
using Timberborn.InputSystem;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.CursorToolSystem
{
	// Token: 0x02000009 RID: 9
	public class CursorDebugger : ILoadableSingleton, IInputProcessor
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000025A9 File Offset: 0x000007A9
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000025B1 File Offset: 0x000007B1
		public Vector3 Position { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000025BA File Offset: 0x000007BA
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000025C2 File Offset: 0x000007C2
		public Vector3Int Coordinates { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000025CB File Offset: 0x000007CB
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000025D3 File Offset: 0x000007D3
		public bool Active { get; private set; }

		// Token: 0x06000027 RID: 39 RVA: 0x000025DC File Offset: 0x000007DC
		public CursorDebugger(CursorCoordinatesPicker cursorCoordinatesPicker, InputService inputService, DebugModeManager debugModeManager, IAssetLoader assetLoader, RootObjectProvider rootObjectProvider, IInstantiator instantiator)
		{
			this._cursorCoordinatesPicker = cursorCoordinatesPicker;
			this._inputService = inputService;
			this._debugModeManager = debugModeManager;
			this._assetLoader = assetLoader;
			this._rootObjectProvider = rootObjectProvider;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000260C File Offset: 0x0000080C
		public void Load()
		{
			Transform transform = this._rootObjectProvider.CreateRootObject("CursorDebugger").transform;
			GameObject gameObject = this._assetLoader.Load<GameObject>(CursorDebugger.CrosshairMarkerPrefabPath);
			GameObject gameObject2 = this._assetLoader.Load<GameObject>(CursorDebugger.TileMarkerPrefabPath);
			this._crosshairMarker = Object.Instantiate<GameObject>(gameObject, transform).transform;
			this._tileMarker = Object.Instantiate<GameObject>(gameObject2, transform).transform;
			this._inputService.AddInputProcessor(this);
			this.Hide();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002688 File Offset: 0x00000888
		public bool ProcessInput()
		{
			if (this._debugModeManager.Enabled)
			{
				CursorCoordinates? cursorCoordinates = this._cursorCoordinatesPicker.Pick();
				if (cursorCoordinates != null)
				{
					CursorCoordinates valueOrDefault = cursorCoordinates.GetValueOrDefault();
					this.Show(valueOrDefault);
					return false;
				}
			}
			this.Hide();
			return false;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026D0 File Offset: 0x000008D0
		public void Show(CursorCoordinates cursorCoordinates)
		{
			this.Position = CoordinateSystem.GridToWorld(cursorCoordinates.Coordinates);
			this.Coordinates = cursorCoordinates.TileCoordinates;
			this._crosshairMarker.position = this.Position;
			this._crosshairMarker.gameObject.SetActive(true);
			this._tileMarker.position = CoordinateSystem.GridToWorldCentered(this.Coordinates);
			this._tileMarker.gameObject.SetActive(true);
			this.Active = true;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000274C File Offset: 0x0000094C
		public void Hide()
		{
			this._crosshairMarker.gameObject.SetActive(false);
			this._tileMarker.gameObject.SetActive(false);
			this.Active = false;
		}

		// Token: 0x0400001C RID: 28
		public static readonly string CrosshairMarkerPrefabPath = "UI/Markers/Debug/Crosshair";

		// Token: 0x0400001D RID: 29
		public static readonly string TileMarkerPrefabPath = "UI/Markers/Debug/Tile";

		// Token: 0x04000021 RID: 33
		public readonly CursorCoordinatesPicker _cursorCoordinatesPicker;

		// Token: 0x04000022 RID: 34
		public readonly InputService _inputService;

		// Token: 0x04000023 RID: 35
		public readonly DebugModeManager _debugModeManager;

		// Token: 0x04000024 RID: 36
		public readonly IAssetLoader _assetLoader;

		// Token: 0x04000025 RID: 37
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x04000026 RID: 38
		public Transform _crosshairMarker;

		// Token: 0x04000027 RID: 39
		public Transform _tileMarker;
	}
}
