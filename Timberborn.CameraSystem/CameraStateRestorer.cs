using System;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.MapStateSystem;
using Timberborn.Persistence;
using Timberborn.QuickNotificationSystem;
using Timberborn.SerializationSystem;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.CameraSystem
{
	// Token: 0x0200000F RID: 15
	public class CameraStateRestorer : ISaveableSingleton, ILoadableSingleton, IInputProcessor
	{
		// Token: 0x06000089 RID: 137 RVA: 0x000037A4 File Offset: 0x000019A4
		public CameraStateRestorer(ISingletonLoader singletonLoader, InputService inputService, CameraService cameraService, QuickNotificationService quickNotificationService, ILoc loc, CameraStateSerializer cameraStateSerializer, SerializedObjectReaderWriter serializedObjectReaderWriter, MapEditorMode mapEditorMode)
		{
			this._singletonLoader = singletonLoader;
			this._inputService = inputService;
			this._cameraService = cameraService;
			this._quickNotificationService = quickNotificationService;
			this._loc = loc;
			this._cameraStateSerializer = cameraStateSerializer;
			this._serializedObjectReaderWriter = serializedObjectReaderWriter;
			this._mapEditorMode = mapEditorMode;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000037F4 File Offset: 0x000019F4
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(CameraStateRestorer.CameraStateRestorerKey, out objectLoader))
			{
				this._savedCameraState = new CameraState?(objectLoader.Get<CameraState>(CameraStateRestorer.SavedCameraStateKey, this._cameraStateSerializer));
			}
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003840 File Offset: 0x00001A40
		public void Save(ISingletonSaver singletonSaver)
		{
			if (this._savedCameraState != null && !this._mapEditorMode.IsMapEditor)
			{
				singletonSaver.GetSingleton(CameraStateRestorer.CameraStateRestorerKey).Set<CameraState>(CameraStateRestorer.SavedCameraStateKey, this._savedCameraState.Value, this._cameraStateSerializer);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003890 File Offset: 0x00001A90
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(CameraStateRestorer.SaveCameraKey))
			{
				this.SaveCameraState();
				return true;
			}
			if (this._inputService.IsKeyDown(CameraStateRestorer.SaveCameraToClipboardKey))
			{
				this.SaveCameraStateToClipboard();
				return true;
			}
			if (this._inputService.IsKeyDown(CameraStateRestorer.RestoreCameraKey))
			{
				this.LoadCameraState();
				return true;
			}
			if (this._inputService.IsKeyDown(CameraStateRestorer.RestoreCameraFromClipboardKey))
			{
				this.LoadCameraStateFromClipboard();
				return true;
			}
			return false;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003906 File Offset: 0x00001B06
		public void SaveCameraState()
		{
			this._savedCameraState = new CameraState?(this._cameraService.GetCurrentState());
			this._quickNotificationService.SendNotification(this._loc.T(CameraStateRestorer.CameraStateSavedLocKey));
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003939 File Offset: 0x00001B39
		public void LoadCameraState()
		{
			if (this._savedCameraState != null)
			{
				this._cameraService.RestoreState(this._savedCameraState.Value);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003960 File Offset: 0x00001B60
		public void SaveCameraStateToClipboard()
		{
			CameraState currentState = this._cameraService.GetCurrentState();
			ValueSaver valueSaver = new ValueSaver();
			this._cameraStateSerializer.Serialize(currentState, valueSaver);
			SerializedObject serializedObject = new SerializedObject();
			serializedObject.Set<object>(CameraStateRestorer.ClipboardStateKey, valueSaver.Value);
			GUIUtility.systemCopyBuffer = this._serializedObjectReaderWriter.WriteJson(serializedObject);
			this._quickNotificationService.SendNotification(this._loc.T(CameraStateRestorer.CameraStateSavedLocKey));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000039D0 File Offset: 0x00001BD0
		public void LoadCameraStateFromClipboard()
		{
			try
			{
				ObjectLoader objectLoader = new ObjectLoader(this._serializedObjectReaderWriter.ReadJson(GUIUtility.systemCopyBuffer));
				PropertyKey<CameraState> key = new PropertyKey<CameraState>(CameraStateRestorer.ClipboardStateKey);
				CameraState cameraState = objectLoader.Get<CameraState>(key, this._cameraStateSerializer);
				this._cameraService.RestoreState(cameraState);
			}
			catch (Exception)
			{
				this._quickNotificationService.SendNotification("Clipboard does not contain a valid camera state.");
			}
		}

		// Token: 0x04000046 RID: 70
		public static readonly string SaveCameraKey = "SaveCamera";

		// Token: 0x04000047 RID: 71
		public static readonly string RestoreCameraKey = "RestoreCamera";

		// Token: 0x04000048 RID: 72
		public static readonly string SaveCameraToClipboardKey = "SaveCameraToClipboard";

		// Token: 0x04000049 RID: 73
		public static readonly string RestoreCameraFromClipboardKey = "RestoreCameraFromClipboard";

		// Token: 0x0400004A RID: 74
		public static readonly string ClipboardStateKey = "CameraState";

		// Token: 0x0400004B RID: 75
		public static readonly SingletonKey CameraStateRestorerKey = new SingletonKey("CameraStateRestorer");

		// Token: 0x0400004C RID: 76
		public static readonly PropertyKey<CameraState> SavedCameraStateKey = new PropertyKey<CameraState>("SavedCameraState");

		// Token: 0x0400004D RID: 77
		public static readonly string CameraStateSavedLocKey = "Camera.StateSaved";

		// Token: 0x0400004E RID: 78
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400004F RID: 79
		public readonly InputService _inputService;

		// Token: 0x04000050 RID: 80
		public readonly CameraService _cameraService;

		// Token: 0x04000051 RID: 81
		public readonly QuickNotificationService _quickNotificationService;

		// Token: 0x04000052 RID: 82
		public readonly ILoc _loc;

		// Token: 0x04000053 RID: 83
		public readonly CameraStateSerializer _cameraStateSerializer;

		// Token: 0x04000054 RID: 84
		public readonly SerializedObjectReaderWriter _serializedObjectReaderWriter;

		// Token: 0x04000055 RID: 85
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x04000056 RID: 86
		public CameraState? _savedCameraState;
	}
}
