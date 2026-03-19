using System;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Timberborn.CameraSystem
{
	// Token: 0x02000020 RID: 32
	public class ShadowDistanceUpdater : ILoadableSingleton, IUnloadableSingleton, ILateUpdatableSingleton
	{
		// Token: 0x06000134 RID: 308 RVA: 0x000054D9 File Offset: 0x000036D9
		public ShadowDistanceUpdater(CameraService cameraService)
		{
			this._cameraService = cameraService;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000054E8 File Offset: 0x000036E8
		public void Load()
		{
			this._originalPipelineAsset = (UniversalRenderPipelineAsset)QualitySettings.renderPipeline;
			this._originalShadowDistance = this._originalPipelineAsset.shadowDistance;
			this.UpdateShadowDistance();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005514 File Offset: 0x00003714
		public void Unload()
		{
			UniversalRenderPipelineAsset universalRenderPipelineAsset = (UniversalRenderPipelineAsset)QualitySettings.renderPipeline;
			if (this._originalPipelineAsset == universalRenderPipelineAsset)
			{
				universalRenderPipelineAsset.shadowDistance = this._originalShadowDistance;
				QualitySettings.shadowDistance = this._originalShadowDistance;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005551 File Offset: 0x00003751
		public void LateUpdateSingleton()
		{
			this.UpdateShadowDistance();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005559 File Offset: 0x00003759
		public void SetShadowDistance(float shadowDistance)
		{
			QualitySettings.shadowDistance = shadowDistance;
			((UniversalRenderPipelineAsset)QualitySettings.renderPipeline).shadowDistance = shadowDistance;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005571 File Offset: 0x00003771
		public float GetShadowDistance()
		{
			return QualitySettings.shadowDistance;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005578 File Offset: 0x00003778
		public void UpdateShadowDistance()
		{
			float num = this.DistanceAtNormalizedScreenPoint(new Vector2(0f, 0f));
			float num2 = this.DistanceAtNormalizedScreenPoint(new Vector2(0f, 1f));
			float num3 = this.DistanceAtNormalizedScreenPoint(new Vector2(1f, 0f));
			float num4 = this.DistanceAtNormalizedScreenPoint(new Vector2(1f, 1f));
			float num5 = Mathf.Clamp(Mathf.Max(Mathf.Max(num, num2), Mathf.Max(num3, num4)), 0f, (float)ShadowDistanceUpdater.MaxDistance);
			if (Math.Abs(num5 - this.GetShadowDistance()) > 0.1f)
			{
				this.SetShadowDistance(num5);
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000561C File Offset: 0x0000381C
		public float DistanceAtNormalizedScreenPoint(Vector2 point)
		{
			Ray ray = this.NormalizedScreenPointToRay(point);
			Plane plane;
			plane..ctor(Vector3.up, 0f);
			float result;
			if (plane.Raycast(ray, ref result))
			{
				return result;
			}
			return float.MaxValue;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005658 File Offset: 0x00003858
		public Ray NormalizedScreenPointToRay(Vector2 point)
		{
			Vector2 vector;
			vector..ctor((float)Screen.width, (float)Screen.height);
			return this._cameraService.ScreenPointToRayInWorldSpace(vector * point);
		}

		// Token: 0x0400009F RID: 159
		public static readonly int MaxDistance = 150;

		// Token: 0x040000A0 RID: 160
		public readonly CameraService _cameraService;

		// Token: 0x040000A1 RID: 161
		public UniversalRenderPipelineAsset _originalPipelineAsset;

		// Token: 0x040000A2 RID: 162
		public float _originalShadowDistance;
	}
}
