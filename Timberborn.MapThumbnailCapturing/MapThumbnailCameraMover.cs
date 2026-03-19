using System;
using Timberborn.CameraSystem;
using Timberborn.MapThumbnail;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.ThumbnailCapturing;
using Timberborn.UndoSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.MapThumbnailCapturing
{
	// Token: 0x02000006 RID: 6
	public class MapThumbnailCameraMover : ILoadableSingleton, ISaveableSingleton
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000219A File Offset: 0x0000039A
		public MapThumbnailCameraMover(CameraConfigurationSerializer cameraConfigurationSerializer, ShadowDistanceUpdater shadowDistanceUpdater, ISingletonLoader singletonLoader, ThumbnailCamera thumbnailCamera, ThumbnailCameraDefaultPositionProvider thumbnailCameraDefaultPositionProvider, EventBus eventBus, IUndoRegistry undoRegistry)
		{
			this._cameraConfigurationSerializer = cameraConfigurationSerializer;
			this._shadowDistanceUpdater = shadowDistanceUpdater;
			this._singletonLoader = singletonLoader;
			this._thumbnailCamera = thumbnailCamera;
			this._thumbnailCameraDefaultPositionProvider = thumbnailCameraDefaultPositionProvider;
			this._eventBus = eventBus;
			this._undoRegistry = undoRegistry;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021D7 File Offset: 0x000003D7
		public CameraConfiguration CurrentConfiguration
		{
			get
			{
				return this._currentConfiguration;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021DF File Offset: 0x000003DF
		public void Save(ISingletonSaver singletonSaver)
		{
			singletonSaver.GetSingleton(MapThumbnailCameraMover.MapThumbnailCameraMoverKey).Set<CameraConfiguration>(MapThumbnailCameraMover.CurrentConfigurationKey, this._currentConfiguration, this._cameraConfigurationSerializer);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002204 File Offset: 0x00000404
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(MapThumbnailCameraMover.MapThumbnailCameraMoverKey, out objectLoader))
			{
				this.SetNewConfiguration(objectLoader.Get<CameraConfiguration>(MapThumbnailCameraMover.CurrentConfigurationKey, this._cameraConfigurationSerializer), false);
			}
			else
			{
				this.SetNewConfiguration(this._thumbnailCameraDefaultPositionProvider.GetDefaultPosition(), false);
			}
			this.MoveToConfiguredPosition();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002258 File Offset: 0x00000458
		public void MoveToMainCameraPosition()
		{
			this._thumbnailCamera.MoveToMainCameraPosition();
			Transform transform = this._thumbnailCamera.Transform;
			this.SetNewConfiguration(new CameraConfiguration(transform.position, transform.rotation, this._shadowDistanceUpdater.GetShadowDistance()), true);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000229F File Offset: 0x0000049F
		public void MoveToDefaultPosition()
		{
			this.SetNewConfiguration(this._thumbnailCameraDefaultPositionProvider.GetDefaultPosition(), true);
			this.MoveToConfiguredPosition();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022B9 File Offset: 0x000004B9
		public void SetNewConfiguration(CameraConfiguration cameraConfiguration, bool registerUndo)
		{
			if (registerUndo)
			{
				this._undoRegistry.RegisterSingleUndoable(new MapThumbnailCameraMover.CameraConfigurationUndoable(this, this._currentConfiguration, cameraConfiguration));
			}
			this._currentConfiguration = cameraConfiguration;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022DD File Offset: 0x000004DD
		public void MoveToPositionAndNotify(CameraConfiguration newConfiguration)
		{
			this.SetNewConfiguration(newConfiguration, false);
			this.MoveToConfiguredPosition();
			this._eventBus.Post(new MapThumbnailChangedEvent());
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022FD File Offset: 0x000004FD
		public void MoveToConfiguredPosition()
		{
			this._thumbnailCamera.SetPositionAndRotation(this._currentConfiguration.Position, this._currentConfiguration.Rotation);
		}

		// Token: 0x0400000C RID: 12
		public static readonly SingletonKey MapThumbnailCameraMoverKey = new SingletonKey("MapThumbnailCameraMover");

		// Token: 0x0400000D RID: 13
		public static readonly PropertyKey<CameraConfiguration> CurrentConfigurationKey = new PropertyKey<CameraConfiguration>("CurrentConfiguration");

		// Token: 0x0400000E RID: 14
		public readonly CameraConfigurationSerializer _cameraConfigurationSerializer;

		// Token: 0x0400000F RID: 15
		public readonly ShadowDistanceUpdater _shadowDistanceUpdater;

		// Token: 0x04000010 RID: 16
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000011 RID: 17
		public readonly ThumbnailCamera _thumbnailCamera;

		// Token: 0x04000012 RID: 18
		public readonly ThumbnailCameraDefaultPositionProvider _thumbnailCameraDefaultPositionProvider;

		// Token: 0x04000013 RID: 19
		public readonly EventBus _eventBus;

		// Token: 0x04000014 RID: 20
		public readonly IUndoRegistry _undoRegistry;

		// Token: 0x04000015 RID: 21
		public CameraConfiguration _currentConfiguration;

		// Token: 0x02000007 RID: 7
		public class CameraConfigurationUndoable : IUndoable
		{
			// Token: 0x06000015 RID: 21 RVA: 0x00002340 File Offset: 0x00000540
			public CameraConfigurationUndoable(MapThumbnailCameraMover mapThumbnailCameraMover, CameraConfiguration oldConfiguration, CameraConfiguration newConfiguration)
			{
				this._mapThumbnailCameraMover = mapThumbnailCameraMover;
				this._oldConfiguration = oldConfiguration;
				this._newConfiguration = newConfiguration;
			}

			// Token: 0x06000016 RID: 22 RVA: 0x0000235D File Offset: 0x0000055D
			public void Undo()
			{
				this._mapThumbnailCameraMover.MoveToPositionAndNotify(this._oldConfiguration);
			}

			// Token: 0x06000017 RID: 23 RVA: 0x00002370 File Offset: 0x00000570
			public void Redo()
			{
				this._mapThumbnailCameraMover.MoveToPositionAndNotify(this._newConfiguration);
			}

			// Token: 0x04000016 RID: 22
			public readonly MapThumbnailCameraMover _mapThumbnailCameraMover;

			// Token: 0x04000017 RID: 23
			public readonly CameraConfiguration _oldConfiguration;

			// Token: 0x04000018 RID: 24
			public readonly CameraConfiguration _newConfiguration;
		}
	}
}
