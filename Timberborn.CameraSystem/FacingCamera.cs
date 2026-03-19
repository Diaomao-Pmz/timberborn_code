using System;
using Timberborn.BaseComponentSystem;
using Timberborn.MortalComponents;
using UnityEngine;

namespace Timberborn.CameraSystem
{
	// Token: 0x02000018 RID: 24
	public class FacingCamera : BaseComponent, IAwakableComponent, ILateUpdatableComponent, IDeadNeededComponent
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00004671 File Offset: 0x00002871
		public FacingCamera(CameraService cameraService)
		{
			this._cameraService = cameraService;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004680 File Offset: 0x00002880
		public void Awake()
		{
			base.DisableComponent();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004688 File Offset: 0x00002888
		public void LateUpdate()
		{
			this.SetRotation();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004690 File Offset: 0x00002890
		public void Enable(Transform transformToRotate)
		{
			this._transform = transformToRotate;
			this.SetRotation();
			base.EnableComponent();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000046A5 File Offset: 0x000028A5
		public void Disable()
		{
			this._transform = null;
			base.DisableComponent();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000046B4 File Offset: 0x000028B4
		public void SetRotation()
		{
			this._transform.rotation = this._cameraService.FacingCamera;
		}

		// Token: 0x04000077 RID: 119
		public readonly CameraService _cameraService;

		// Token: 0x04000078 RID: 120
		public Transform _transform;
	}
}
