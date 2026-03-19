using System;
using System.Collections.Generic;
using Timberborn.AssetSystem;
using Timberborn.PlatformUtilities;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x0200000F RID: 15
	public class WaterBackfacesRenderer : ScriptableRenderPass, ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002AD7 File Offset: 0x00000CD7
		public WaterBackfacesRenderer(IAssetLoader assetLoader)
		{
			this._assetLoader = assetLoader;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public void Load()
		{
			this._filteringSettings = new FilteringSettings(new RenderQueueRange?(RenderQueueRange.all), 1 << WaterBackfacesRenderer.WaterLayerId, uint.MaxValue, 0);
			this._backfacesMaterial = this._assetLoader.Load<Material>(WaterBackfacesRenderer.MaterialPath);
			base.renderPassEvent = 301;
			RenderPipelineManager.beginCameraRendering += this.BeginCameraRendering;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002B48 File Offset: 0x00000D48
		public void Unload()
		{
			RenderPipelineManager.beginCameraRendering -= this.BeginCameraRendering;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002B5C File Offset: 0x00000D5C
		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer contextContainer)
		{
			WaterBackfacesRenderer.PassData passData;
			using (IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<WaterBackfacesRenderer.PassData>(WaterBackfacesRenderer.RendererPassName, ref passData, "F:\\workspace\\p\\Assets\\Scripts\\Timberborn\\WaterSystemRendering\\WaterBackfacesRenderer.cs", 46))
			{
				passData.RendererList = this.CreateRendererList(renderGraph, contextContainer);
				UniversalResourceData universalResourceData = contextContainer.Get<UniversalResourceData>();
				rasterRenderGraphBuilder.UseRendererList(ref passData.RendererList);
				if (ApplicationPlatform.IsMacOS())
				{
					RenderTextureDescriptor cameraTargetDescriptor = contextContainer.Get<UniversalCameraData>().cameraTargetDescriptor;
					cameraTargetDescriptor.depthBufferBits = 0;
					TextureHandle textureHandle = UniversalRenderer.CreateRenderGraphTexture(renderGraph, cameraTargetDescriptor, "DummyColorTarget", true, 0, 1);
					rasterRenderGraphBuilder.SetRenderAttachment(textureHandle, 0, 2);
				}
				rasterRenderGraphBuilder.SetRenderAttachmentDepth(universalResourceData.activeDepthTexture, 3);
				rasterRenderGraphBuilder.SetRenderFunc<WaterBackfacesRenderer.PassData>(delegate(WaterBackfacesRenderer.PassData data, RasterGraphContext context)
				{
					context.cmd.DrawRendererList(data.RendererList);
				});
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002C24 File Offset: 0x00000E24
		public RendererListHandle CreateRendererList(RenderGraph renderGraph, ContextContainer contextContainer)
		{
			UniversalCameraData universalCameraData = contextContainer.Get<UniversalCameraData>();
			UniversalRenderingData universalRenderingData = contextContainer.Get<UniversalRenderingData>();
			UniversalLightData universalLightData = contextContainer.Get<UniversalLightData>();
			DrawingSettings drawingSettings = RenderingUtils.CreateDrawingSettings(WaterBackfacesRenderer.ShaderTags, universalRenderingData, universalCameraData, universalLightData, universalCameraData.defaultOpaqueSortFlags);
			drawingSettings.overrideMaterial = this._backfacesMaterial;
			RendererListParams rendererListParams;
			rendererListParams..ctor(universalRenderingData.cullResults, drawingSettings, this._filteringSettings);
			return renderGraph.CreateRendererList(ref rendererListParams);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002C83 File Offset: 0x00000E83
		public void BeginCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			CameraExtensions.GetUniversalAdditionalCameraData(camera).scriptableRenderer.EnqueuePass(this);
		}

		// Token: 0x04000031 RID: 49
		public static readonly List<ShaderTagId> ShaderTags = new List<ShaderTagId>
		{
			new ShaderTagId("UniversalForwardOnly"),
			new ShaderTagId("UniversalForward"),
			new ShaderTagId("SRPDefaultUnlit")
		};

		// Token: 0x04000032 RID: 50
		public static readonly string MaterialPath = "Environment/Water/Materials/PhysicalWater_Backfaces";

		// Token: 0x04000033 RID: 51
		public static readonly string RendererPassName = "RenderWaterBackfaces";

		// Token: 0x04000034 RID: 52
		public static readonly int WaterLayerId = LayerMask.NameToLayer("Water");

		// Token: 0x04000035 RID: 53
		public readonly IAssetLoader _assetLoader;

		// Token: 0x04000036 RID: 54
		public FilteringSettings _filteringSettings;

		// Token: 0x04000037 RID: 55
		public Material _backfacesMaterial;

		// Token: 0x02000010 RID: 16
		public class PassData
		{
			// Token: 0x04000038 RID: 56
			public RendererListHandle RendererList;
		}
	}
}
