using System;
using Timberborn.CameraSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.ThumbnailCapturing
{
	// Token: 0x02000006 RID: 6
	public class ThumbnailCamera : ILoadableSingleton, IPostLoadableSingleton
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020BE File Offset: 0x000002BE
		public ThumbnailCamera(CameraFactory cameraFactory, CameraService mainCamera, IThumbnailRenderTextureProvider thumbnailRenderTextureProvider)
		{
			this._cameraFactory = cameraFactory;
			this._mainCamera = mainCamera;
			this._thumbnailRenderTextureProvider = thumbnailRenderTextureProvider;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020DB File Offset: 0x000002DB
		public Transform Transform
		{
			get
			{
				return this._thumbnailCamera.transform;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020E8 File Offset: 0x000002E8
		public void Load()
		{
			this._thumbnailCamera = this._cameraFactory.Create("ThumbnailCamera");
			this._thumbnailCamera.enabled = false;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000210C File Offset: 0x0000030C
		public void PostLoad()
		{
			this._thumbnailCamera.targetTexture = this._thumbnailRenderTextureProvider.RenderTexture;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002124 File Offset: 0x00000324
		public void MoveToMainCameraPosition()
		{
			Transform transform = this._mainCamera.Transform;
			this.SetPositionAndRotation(transform.position, transform.rotation);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000214F File Offset: 0x0000034F
		public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
		{
			this._thumbnailCamera.transform.SetPositionAndRotation(position, rotation);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002163 File Offset: 0x00000363
		public void Render()
		{
			this._thumbnailCamera.Render();
		}

		// Token: 0x04000006 RID: 6
		public readonly CameraFactory _cameraFactory;

		// Token: 0x04000007 RID: 7
		public readonly CameraService _mainCamera;

		// Token: 0x04000008 RID: 8
		public readonly IThumbnailRenderTextureProvider _thumbnailRenderTextureProvider;

		// Token: 0x04000009 RID: 9
		public Camera _thumbnailCamera;
	}
}
