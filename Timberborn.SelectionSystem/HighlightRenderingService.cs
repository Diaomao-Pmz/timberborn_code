using System;
using System.Collections.Generic;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace Timberborn.SelectionSystem
{
	// Token: 0x0200000F RID: 15
	public class HighlightRenderingService : ScriptableRenderPass, ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00002F37 File Offset: 0x00001137
		public void Load()
		{
			this._filteringSettings = new FilteringSettings(new RenderQueueRange?(RenderQueueRange.all), -1, 1U << HighlightRenderingService.LayerId, 0);
			base.renderPassEvent = 300;
			RenderPipelineManager.beginCameraRendering += this.BeginCameraRendering;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002F76 File Offset: 0x00001176
		public void Unload()
		{
			RenderPipelineManager.beginCameraRendering -= this.BeginCameraRendering;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002F8C File Offset: 0x0000118C
		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer contextContainer)
		{
			HighlightRenderingService.PassData passData;
			using (IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<HighlightRenderingService.PassData>(HighlightRenderingService.PassName, ref passData, "F:\\workspace\\p\\Assets\\Scripts\\Timberborn\\SelectionSystem\\HighlightRenderingService.cs", 37))
			{
				passData.RendererList = this.CreateRendererList(renderGraph, contextContainer);
				UniversalResourceData universalResourceData = contextContainer.Get<UniversalResourceData>();
				TextureHandle highlightMask = HighlightRenderingService.GetHighlightMask(renderGraph, contextContainer);
				rasterRenderGraphBuilder.UseRendererList(ref passData.RendererList);
				rasterRenderGraphBuilder.SetRenderAttachment(highlightMask, 0, 2);
				rasterRenderGraphBuilder.SetRenderAttachmentDepth(universalResourceData.activeDepthTexture, 1);
				rasterRenderGraphBuilder.SetGlobalTextureAfterPass(ref highlightMask, HighlightRenderingService.HighlightMaskId);
				rasterRenderGraphBuilder.AllowPassCulling(true);
				rasterRenderGraphBuilder.SetRenderFunc<HighlightRenderingService.PassData>(delegate(HighlightRenderingService.PassData data, RasterGraphContext context)
				{
					context.cmd.DrawRendererList(data.RendererList);
				});
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003044 File Offset: 0x00001244
		public void AddToHighlight(GameObject root)
		{
			this.UpdateSelectionLayer(root, true);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000304E File Offset: 0x0000124E
		public void RemoveFromHighlight(GameObject root)
		{
			this.UpdateSelectionLayer(root, false);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003058 File Offset: 0x00001258
		public void BeginCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			CameraExtensions.GetUniversalAdditionalCameraData(camera).scriptableRenderer.EnqueuePass(this);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000306C File Offset: 0x0000126C
		public RendererListHandle CreateRendererList(RenderGraph renderGraph, ContextContainer contextContainer)
		{
			UniversalCameraData universalCameraData = contextContainer.Get<UniversalCameraData>();
			UniversalRenderingData universalRenderingData = contextContainer.Get<UniversalRenderingData>();
			UniversalLightData universalLightData = contextContainer.Get<UniversalLightData>();
			DrawingSettings drawingSettings = RenderingUtils.CreateDrawingSettings(HighlightRenderingService.ShaderTags, universalRenderingData, universalCameraData, universalLightData, universalCameraData.defaultOpaqueSortFlags);
			RendererListParams rendererListParams;
			rendererListParams..ctor(universalRenderingData.cullResults, drawingSettings, this._filteringSettings);
			return renderGraph.CreateRendererList(ref rendererListParams);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000030C0 File Offset: 0x000012C0
		public static TextureHandle GetHighlightMask(RenderGraph renderGraph, ContextContainer contextContainer)
		{
			RenderTextureDescriptor cameraTargetDescriptor = contextContainer.Get<UniversalCameraData>().cameraTargetDescriptor;
			cameraTargetDescriptor.depthBufferBits = 0;
			return UniversalRenderer.CreateRenderGraphTexture(renderGraph, cameraTargetDescriptor, HighlightRenderingService.HighlightMaskName, true, 0, 1);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000030F0 File Offset: 0x000012F0
		public void UpdateSelectionLayer(GameObject root, bool layerState)
		{
			root.GetComponentsInChildren<MeshRenderer>(true, this._renderersCache);
			foreach (MeshRenderer meshRenderer in this._renderersCache)
			{
				if (layerState)
				{
					meshRenderer.renderingLayerMask |= 1U << HighlightRenderingService.LayerId;
				}
				else
				{
					meshRenderer.renderingLayerMask &= ~(1U << HighlightRenderingService.LayerId);
				}
			}
			this._renderersCache.Clear();
		}

		// Token: 0x0400002B RID: 43
		public static readonly List<ShaderTagId> ShaderTags = new List<ShaderTagId>
		{
			new ShaderTagId("UniversalForwardOnly"),
			new ShaderTagId("UniversalForward"),
			new ShaderTagId("SRPDefaultUnlit")
		};

		// Token: 0x0400002C RID: 44
		public static readonly int LayerId = RenderingLayerMask.NameToRenderingLayer("Selection");

		// Token: 0x0400002D RID: 45
		public static readonly string PassName = "RenderHighlightedObjects";

		// Token: 0x0400002E RID: 46
		public static readonly string HighlightMaskName = "_HighlightMask";

		// Token: 0x0400002F RID: 47
		public static readonly int HighlightMaskId = Shader.PropertyToID(HighlightRenderingService.HighlightMaskName);

		// Token: 0x04000030 RID: 48
		public readonly List<MeshRenderer> _renderersCache = new List<MeshRenderer>();

		// Token: 0x04000031 RID: 49
		public FilteringSettings _filteringSettings;

		// Token: 0x02000010 RID: 16
		public class PassData
		{
			// Token: 0x04000032 RID: 50
			public RendererListHandle RendererList;
		}
	}
}
