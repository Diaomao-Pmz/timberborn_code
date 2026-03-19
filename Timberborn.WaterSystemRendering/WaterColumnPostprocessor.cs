using System;
using Timberborn.AssetSystem;
using Timberborn.GraphicsQualitySystem;
using Timberborn.MapIndexSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x02000012 RID: 18
	public class WaterColumnPostprocessor : ILoadableSingleton, IPostLoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002D26 File Offset: 0x00000F26
		public WaterColumnPostprocessor(MapIndexService mapIndexService, IAssetLoader assetLoader, WaterQualitySetting waterQualitySetting)
		{
			this._mapIndexService = mapIndexService;
			this._assetLoader = assetLoader;
			this._waterQualitySetting = waterQualitySetting;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002D44 File Offset: 0x00000F44
		public void Load()
		{
			this._shader = Object.Instantiate<ComputeShader>(this._assetLoader.Load<ComputeShader>(WaterColumnPostprocessor.PostprocessingShaderPath));
			this._processColumnsKernel = this._shader.FindKernel(WaterColumnPostprocessor.ProcessColumnsKernel);
			this._findBestPropertiesKernel = this._shader.FindKernel(WaterColumnPostprocessor.FindBestPropertiesKernel);
			this._breakCornerLinksKernel = this._shader.FindKernel(WaterColumnPostprocessor.BreakCornerLinksKernel);
			this._breakCornerLinksReversedKernel = this._shader.FindKernel(WaterColumnPostprocessor.BreakCornerLinksReversedKernel);
			this._calculateHeightsKernel = this._shader.FindKernel(WaterColumnPostprocessor.CalculateHeightsKernel);
			this._findWaterfallsKernel = this._shader.FindKernel(WaterColumnPostprocessor.FindWaterfallsKernel);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public void PostLoad()
		{
			this._waterQualitySetting.WaterQualityChanged += this.OnWaterQualityChanged;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002E09 File Offset: 0x00001009
		public void Unload()
		{
			this.UnloadComputeShader();
			this.UnloadRenderTextures();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002E17 File Offset: 0x00001017
		public void Resize(int arraySize)
		{
			this._arraySize = arraySize + 1;
			this.UnloadRenderTextures();
			this.InitializeTextures();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002E2E File Offset: 0x0000102E
		public void Postprocess(int maxIndex, IDataTextureArray depths, IDataTextureArray columns, IDataTextureArray outflows, IDataTextureArray contaminations, IDataTextureArray linkBarriers, IDataTextureArray flowLimits)
		{
			this._shader.SetFloat(WaterColumnPostprocessor.MaxIndexProperty, (float)maxIndex);
			this.DispatchAll(depths, columns, outflows, contaminations, linkBarriers, flowLimits);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002E54 File Offset: 0x00001054
		public void InitializeTextures()
		{
			Vector3Int tileMapTextureSize = this.GetTileMapTextureSize();
			this._oldBrokenCornerLinks = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 16, null);
			this._newBrokenCornerLinks = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 16, null);
			this._oldWaterData = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 11, WaterTextureNames.OldWaterData);
			this._newWaterData = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 11, WaterTextureNames.NewWaterData);
			this._oldEdgeLinks = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 11, WaterTextureNames.OldEdgeLinks);
			this._newEdgeLinks = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 11, WaterTextureNames.NewEdgeLinks);
			this._oldCornerLinks = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 11, WaterTextureNames.OldCornerLinks);
			this._newCornerLinks = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 11, WaterTextureNames.NewCornerLinks);
			this._oldBaseCornerLinks = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 11, WaterTextureNames.OldBaseCornerLinks);
			this._newBaseCornerLinks = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 11, WaterTextureNames.NewBaseCornerLinks);
			this._oldSkirts = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 0, WaterTextureNames.OldSkirts);
			this._newSkirts = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 0, WaterTextureNames.NewSkirts);
			this._oldOutflows = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 12, WaterTextureNames.OldOutflows);
			this._newOutflows = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 12, WaterTextureNames.NewOutflows);
			this._oldContaminations = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 16, WaterTextureNames.OldContaminations);
			this._newContaminations = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 16, WaterTextureNames.NewContaminations);
			this._oldCornerLinksBuffer = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 11, null);
			this._newCornerLinksBuffer = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 11, null);
			this.CreateWaterfallTextures();
			Vector3Int vertexMapTextureSize = this.GetVertexMapTextureSize();
			this._oldWaterHeights = WaterColumnPostprocessor.CreateTexture(vertexMapTextureSize, 14, WaterTextureNames.OldWaterHeights);
			this._newWaterHeights = WaterColumnPostprocessor.CreateTexture(vertexMapTextureSize, 14, WaterTextureNames.NewWaterHeights);
			uint num;
			uint num2;
			uint num3;
			this._shader.GetKernelThreadGroupSizes(this._processColumnsKernel, ref num, ref num2, ref num3);
			this._tileThreadGroupCount = new Vector2Int(Mathf.CeilToInt((float)tileMapTextureSize.x / num), Mathf.CeilToInt((float)tileMapTextureSize.y / num2));
			this._vertexThreadGroupCount = new Vector2Int(Mathf.CeilToInt((float)vertexMapTextureSize.x / num), Mathf.CeilToInt((float)vertexMapTextureSize.y / num2));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003050 File Offset: 0x00001250
		public static RenderTexture CreateTexture(Vector3Int size, RenderTextureFormat textureFormat, string propertyName = null)
		{
			RenderTexture renderTexture = new RenderTexture(size.x, size.y, 0, textureFormat)
			{
				dimension = 5,
				enableRandomWrite = true,
				useMipMap = false,
				autoGenerateMips = false,
				anisoLevel = 0,
				antiAliasing = 1,
				useDynamicScale = false,
				volumeDepth = size.z,
				wrapMode = 1,
				filterMode = 0
			};
			if (!string.IsNullOrWhiteSpace(propertyName))
			{
				renderTexture.name = propertyName;
				Shader.SetGlobalTexture(WaterColumnPostprocessor.GetId(propertyName), renderTexture);
			}
			return renderTexture;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000030DC File Offset: 0x000012DC
		public void CreateWaterfallTextures()
		{
			if (this._waterQualitySetting.HighQualityWaterEnabled)
			{
				Vector3Int tileMapTextureSize = this.GetTileMapTextureSize();
				this._oldWaterfalls = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 14, WaterTextureNames.OldWaterfalls);
				this._newWaterfalls = WaterColumnPostprocessor.CreateTexture(tileMapTextureSize, 14, WaterTextureNames.NewWaterfalls);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003123 File Offset: 0x00001323
		public void UnloadComputeShader()
		{
			if (this._shader != null)
			{
				Object.Destroy(this._shader);
				this._shader = null;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003148 File Offset: 0x00001348
		public void UnloadRenderTextures()
		{
			WaterColumnPostprocessor.UnloadRenderTexture(this._oldBrokenCornerLinks);
			WaterColumnPostprocessor.UnloadRenderTexture(this._newBrokenCornerLinks);
			WaterColumnPostprocessor.UnloadRenderTexture(this._oldWaterData);
			WaterColumnPostprocessor.UnloadRenderTexture(this._newWaterData);
			WaterColumnPostprocessor.UnloadRenderTexture(this._oldEdgeLinks);
			WaterColumnPostprocessor.UnloadRenderTexture(this._newEdgeLinks);
			WaterColumnPostprocessor.UnloadRenderTexture(this._oldCornerLinks);
			WaterColumnPostprocessor.UnloadRenderTexture(this._newCornerLinks);
			WaterColumnPostprocessor.UnloadRenderTexture(this._oldBaseCornerLinks);
			WaterColumnPostprocessor.UnloadRenderTexture(this._newBaseCornerLinks);
			WaterColumnPostprocessor.UnloadRenderTexture(this._oldSkirts);
			WaterColumnPostprocessor.UnloadRenderTexture(this._newSkirts);
			WaterColumnPostprocessor.UnloadRenderTexture(this._oldWaterHeights);
			WaterColumnPostprocessor.UnloadRenderTexture(this._newWaterHeights);
			WaterColumnPostprocessor.UnloadRenderTexture(this._oldOutflows);
			WaterColumnPostprocessor.UnloadRenderTexture(this._newOutflows);
			WaterColumnPostprocessor.UnloadRenderTexture(this._oldContaminations);
			WaterColumnPostprocessor.UnloadRenderTexture(this._newContaminations);
			WaterColumnPostprocessor.UnloadRenderTexture(this._oldCornerLinksBuffer);
			WaterColumnPostprocessor.UnloadRenderTexture(this._newCornerLinksBuffer);
			this.UnloadWaterfallTextures();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003237 File Offset: 0x00001437
		public void UnloadWaterfallTextures()
		{
			WaterColumnPostprocessor.UnloadRenderTexture(this._oldWaterfalls);
			WaterColumnPostprocessor.UnloadRenderTexture(this._newWaterfalls);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000324F File Offset: 0x0000144F
		public void OnWaterQualityChanged(object sender, EventArgs e)
		{
			this.UnloadWaterfallTextures();
			this.CreateWaterfallTextures();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000325D File Offset: 0x0000145D
		public static void UnloadRenderTexture(RenderTexture texture)
		{
			if (texture != null)
			{
				texture.Release();
				Object.Destroy(texture);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003274 File Offset: 0x00001474
		public void DispatchAll(IDataTextureArray depths, IDataTextureArray columns, IDataTextureArray outflows, IDataTextureArray contaminations, IDataTextureArray linkBarriers, IDataTextureArray flowLimits)
		{
			this.DispatchProcessColumns(depths.OldArray, columns.OldArray, linkBarriers.OldArray, outflows.OldArray, flowLimits.OldArray, this._oldWaterData, this._oldEdgeLinks, this._oldCornerLinks, this._oldBaseCornerLinks, this._oldSkirts);
			this.DispatchProcessColumns(depths.NewArray, columns.NewArray, linkBarriers.NewArray, outflows.NewArray, flowLimits.NewArray, this._newWaterData, this._newEdgeLinks, this._newCornerLinks, this._newBaseCornerLinks, this._newSkirts);
			this.DispatchFindBestProperties(outflows.OldArray, contaminations.OldArray, this._oldWaterData, this._oldEdgeLinks, this._oldCornerLinksBuffer, this._oldOutflows, this._oldContaminations);
			this.DispatchFindBestProperties(outflows.NewArray, contaminations.NewArray, this._newWaterData, this._newEdgeLinks, this._newCornerLinksBuffer, this._newOutflows, this._newContaminations);
			this.DispatchBreakCornerLinks(linkBarriers.NewArray, this._oldEdgeLinks, this._oldCornerLinks, this._oldCornerLinksBuffer, this._oldBrokenCornerLinks);
			this.DispatchBreakCornerLinks(linkBarriers.OldArray, this._newEdgeLinks, this._newCornerLinks, this._newCornerLinksBuffer, this._newBrokenCornerLinks);
			this.DispatchBreakCornerLinksReversed(this._oldCornerLinksBuffer, this._oldBrokenCornerLinks, this._oldCornerLinks);
			this.DispatchBreakCornerLinksReversed(this._newCornerLinksBuffer, this._newBrokenCornerLinks, this._newCornerLinks);
			this.DispatchVertexHeights(this._oldWaterData, this._oldEdgeLinks, this._oldCornerLinks, this._oldWaterHeights);
			this.DispatchVertexHeights(this._newWaterData, this._newEdgeLinks, this._newCornerLinks, this._newWaterHeights);
			if (this._waterQualitySetting.HighQualityWaterEnabled)
			{
				this.DispatchFindWaterfalls(this._oldWaterData, this._oldWaterHeights, this._oldWaterfalls);
				this.DispatchFindWaterfalls(this._newWaterData, this._newWaterHeights, this._newWaterfalls);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003460 File Offset: 0x00001660
		public void DispatchProcessColumns(Texture inDepths, Texture inColumns, Texture inLinkBarriers, Texture inOutflows, Texture inFlowLimits, Texture outWaterData, Texture outEdgeLinks, Texture outCornerLinks, Texture outBaseCornerLinks, Texture outSkirtVisibility)
		{
			this.SetTexture(this._processColumnsKernel, WaterColumnPostprocessor.InDepthsId, inDepths);
			this.SetTexture(this._processColumnsKernel, WaterColumnPostprocessor.InColumnsId, inColumns);
			this.SetTexture(this._processColumnsKernel, WaterColumnPostprocessor.InLinkBarriersId, inLinkBarriers);
			this.SetTexture(this._processColumnsKernel, WaterColumnPostprocessor.InOutflowsId, inOutflows);
			this.SetTexture(this._processColumnsKernel, WaterColumnPostprocessor.InFlowLimitsId, inFlowLimits);
			this.SetTexture(this._processColumnsKernel, WaterColumnPostprocessor.OutWaterDataId, outWaterData);
			this.SetTexture(this._processColumnsKernel, WaterColumnPostprocessor.OutEdgeLinksId, outEdgeLinks);
			this.SetTexture(this._processColumnsKernel, WaterColumnPostprocessor.OutCornerLinksId, outCornerLinks);
			this.SetTexture(this._processColumnsKernel, WaterColumnPostprocessor.OutBaseCornerLinksId, outBaseCornerLinks);
			this.SetTexture(this._processColumnsKernel, WaterColumnPostprocessor.OutSkirtsId, outSkirtVisibility);
			this.Dispatch(this._processColumnsKernel, true);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003538 File Offset: 0x00001738
		public void DispatchFindBestProperties(Texture inOutflows, Texture inContaminations, Texture outWaterDataBuffer, Texture outEdgeLinksBuffer, Texture outCornerLinksBuffer, Texture outOutflows, Texture outContaminations)
		{
			this.SetTexture(this._findBestPropertiesKernel, WaterColumnPostprocessor.InOutflowsId, inOutflows);
			this.SetTexture(this._findBestPropertiesKernel, WaterColumnPostprocessor.InContaminationsId, inContaminations);
			this.SetTexture(this._findBestPropertiesKernel, WaterColumnPostprocessor.OutWaterDataBufferId, outWaterDataBuffer);
			this.SetTexture(this._findBestPropertiesKernel, WaterColumnPostprocessor.OutEdgeLinksBufferId, outEdgeLinksBuffer);
			this.SetTexture(this._findBestPropertiesKernel, WaterColumnPostprocessor.OutCornerLinksBufferId, outCornerLinksBuffer);
			this.SetTexture(this._findBestPropertiesKernel, WaterColumnPostprocessor.OutOutflowsId, outOutflows);
			this.SetTexture(this._findBestPropertiesKernel, WaterColumnPostprocessor.OutContaminationsId, outContaminations);
			this.Dispatch(this._findBestPropertiesKernel, true);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000035D4 File Offset: 0x000017D4
		public void DispatchBreakCornerLinks(Texture inLinkBarriers, Texture outEdgeLinksBuffer, Texture outCornerLinksBuffer, Texture outCornerLinks, Texture brokenCornerLinks)
		{
			this.SetTexture(this._breakCornerLinksKernel, WaterColumnPostprocessor.InLinkBarriersId, inLinkBarriers);
			this.SetTexture(this._breakCornerLinksKernel, WaterColumnPostprocessor.OutEdgeLinksBufferId, outEdgeLinksBuffer);
			this.SetTexture(this._breakCornerLinksKernel, WaterColumnPostprocessor.OutCornerLinksBufferId, outCornerLinksBuffer);
			this.SetTexture(this._breakCornerLinksKernel, WaterColumnPostprocessor.OutCornerLinksId, outCornerLinks);
			this.SetTexture(this._breakCornerLinksKernel, WaterColumnPostprocessor.BrokenCornerLinksId, brokenCornerLinks);
			this.Dispatch(this._breakCornerLinksKernel, true);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000364C File Offset: 0x0000184C
		public void DispatchBreakCornerLinksReversed(Texture outCornerLinksBuffer, Texture brokenCornerLinksBuffer, Texture outCornerLinks)
		{
			this.SetTexture(this._breakCornerLinksReversedKernel, WaterColumnPostprocessor.OutCornerLinksBufferId, outCornerLinksBuffer);
			this.SetTexture(this._breakCornerLinksReversedKernel, WaterColumnPostprocessor.BrokenCornerLinksBufferId, brokenCornerLinksBuffer);
			this.SetTexture(this._breakCornerLinksReversedKernel, WaterColumnPostprocessor.OutCornerLinksId, outCornerLinks);
			this.Dispatch(this._breakCornerLinksReversedKernel, true);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000369C File Offset: 0x0000189C
		public void DispatchVertexHeights(Texture outWaterDataBuffer, Texture outEdgeLinksBuffer, Texture outCornerLinksBuffer, Texture outHeights)
		{
			this.SetTexture(this._calculateHeightsKernel, WaterColumnPostprocessor.OutWaterDataBufferId, outWaterDataBuffer);
			this.SetTexture(this._calculateHeightsKernel, WaterColumnPostprocessor.OutEdgeLinksBufferId, outEdgeLinksBuffer);
			this.SetTexture(this._calculateHeightsKernel, WaterColumnPostprocessor.OutCornerLinksBufferId, outCornerLinksBuffer);
			this.SetTexture(this._calculateHeightsKernel, WaterColumnPostprocessor.OutHeightsId, outHeights);
			this.Dispatch(this._calculateHeightsKernel, false);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003700 File Offset: 0x00001900
		public void DispatchFindWaterfalls(Texture outWaterDataBuffer, Texture outHeights, Texture outWaterfalls)
		{
			this.SetTexture(this._findWaterfallsKernel, WaterColumnPostprocessor.OutWaterDataBufferId, outWaterDataBuffer);
			this.SetTexture(this._findWaterfallsKernel, WaterColumnPostprocessor.OutHeightsBufferId, outHeights);
			this.SetTexture(this._findWaterfallsKernel, WaterColumnPostprocessor.OutWaterfallsId, outWaterfalls);
			this.Dispatch(this._findWaterfallsKernel, true);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003750 File Offset: 0x00001950
		public void SetTexture(int kernel, int key, Texture texture)
		{
			this._shader.SetTexture(kernel, key, texture);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003760 File Offset: 0x00001960
		public void Dispatch(int kernel, bool isTile)
		{
			if (isTile)
			{
				this._shader.Dispatch(kernel, this._tileThreadGroupCount.x, this._tileThreadGroupCount.y, 1);
				return;
			}
			this._shader.Dispatch(kernel, this._vertexThreadGroupCount.x, this._vertexThreadGroupCount.y, 1);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000037B7 File Offset: 0x000019B7
		public static int GetId(string name)
		{
			return Shader.PropertyToID(name);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000037C0 File Offset: 0x000019C0
		public Vector3Int GetTileMapTextureSize()
		{
			return new Vector3Int(this._mapIndexService.TerrainSize.x, this._mapIndexService.TerrainSize.y, this._arraySize);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003800 File Offset: 0x00001A00
		public Vector3Int GetVertexMapTextureSize()
		{
			return new Vector3Int(4 * this._mapIndexService.TerrainSize.x, 4 * this._mapIndexService.TerrainSize.y, this._arraySize);
		}

		// Token: 0x0400003B RID: 59
		public static readonly string PostprocessingShaderPath = "Rendering/WaterColumnPostprocessor";

		// Token: 0x0400003C RID: 60
		public static readonly int MaxIndexProperty = WaterColumnPostprocessor.GetId("MaxIndex");

		// Token: 0x0400003D RID: 61
		public static readonly string ProcessColumnsKernel = "ProcessColumns";

		// Token: 0x0400003E RID: 62
		public static readonly string FindBestPropertiesKernel = "FindBestProperties";

		// Token: 0x0400003F RID: 63
		public static readonly string BreakCornerLinksKernel = "BreakCornerLinks";

		// Token: 0x04000040 RID: 64
		public static readonly string BreakCornerLinksReversedKernel = "BreakCornerLinksReversed";

		// Token: 0x04000041 RID: 65
		public static readonly string CalculateHeightsKernel = "CalculateHeights";

		// Token: 0x04000042 RID: 66
		public static readonly string FindWaterfallsKernel = "FindWaterfalls";

		// Token: 0x04000043 RID: 67
		public static readonly int InDepthsId = WaterColumnPostprocessor.GetId("InDepths");

		// Token: 0x04000044 RID: 68
		public static readonly int InColumnsId = WaterColumnPostprocessor.GetId("InColumns");

		// Token: 0x04000045 RID: 69
		public static readonly int InOutflowsId = WaterColumnPostprocessor.GetId("InOutflows");

		// Token: 0x04000046 RID: 70
		public static readonly int InContaminationsId = WaterColumnPostprocessor.GetId("InContaminations");

		// Token: 0x04000047 RID: 71
		public static readonly int InLinkBarriersId = WaterColumnPostprocessor.GetId("InLinkBarriers");

		// Token: 0x04000048 RID: 72
		public static readonly int InFlowLimitsId = WaterColumnPostprocessor.GetId("InFlowLimits");

		// Token: 0x04000049 RID: 73
		public static readonly int BrokenCornerLinksId = WaterColumnPostprocessor.GetId("BrokenCornerLinks");

		// Token: 0x0400004A RID: 74
		public static readonly int BrokenCornerLinksBufferId = WaterColumnPostprocessor.GetId("BrokenCornerLinksBuffer");

		// Token: 0x0400004B RID: 75
		public static readonly int OutWaterDataId = WaterColumnPostprocessor.GetId("OutWaterData");

		// Token: 0x0400004C RID: 76
		public static readonly int OutWaterDataBufferId = WaterColumnPostprocessor.GetId("OutWaterDataBuffer");

		// Token: 0x0400004D RID: 77
		public static readonly int OutEdgeLinksId = WaterColumnPostprocessor.GetId("OutEdgeLinks");

		// Token: 0x0400004E RID: 78
		public static readonly int OutEdgeLinksBufferId = WaterColumnPostprocessor.GetId("OutEdgeLinksBuffer");

		// Token: 0x0400004F RID: 79
		public static readonly int OutCornerLinksId = WaterColumnPostprocessor.GetId("OutCornerLinks");

		// Token: 0x04000050 RID: 80
		public static readonly int OutCornerLinksBufferId = WaterColumnPostprocessor.GetId("OutCornerLinksBuffer");

		// Token: 0x04000051 RID: 81
		public static readonly int OutOutflowsId = WaterColumnPostprocessor.GetId("OutOutflows");

		// Token: 0x04000052 RID: 82
		public static readonly int OutContaminationsId = WaterColumnPostprocessor.GetId("OutContaminations");

		// Token: 0x04000053 RID: 83
		public static readonly int OutBaseCornerLinksId = WaterColumnPostprocessor.GetId("OutBaseCornerLinks");

		// Token: 0x04000054 RID: 84
		public static readonly int OutSkirtsId = WaterColumnPostprocessor.GetId("OutSkirts");

		// Token: 0x04000055 RID: 85
		public static readonly int OutWaterfallsId = WaterColumnPostprocessor.GetId("OutWaterfalls");

		// Token: 0x04000056 RID: 86
		public static readonly int OutHeightsId = WaterColumnPostprocessor.GetId("OutHeights");

		// Token: 0x04000057 RID: 87
		public static readonly int OutHeightsBufferId = WaterColumnPostprocessor.GetId("OutHeightsBuffer");

		// Token: 0x04000058 RID: 88
		public readonly MapIndexService _mapIndexService;

		// Token: 0x04000059 RID: 89
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400005A RID: 90
		public readonly WaterQualitySetting _waterQualitySetting;

		// Token: 0x0400005B RID: 91
		public ComputeShader _shader;

		// Token: 0x0400005C RID: 92
		public int _processColumnsKernel;

		// Token: 0x0400005D RID: 93
		public int _findBestPropertiesKernel;

		// Token: 0x0400005E RID: 94
		public int _breakCornerLinksKernel;

		// Token: 0x0400005F RID: 95
		public int _breakCornerLinksReversedKernel;

		// Token: 0x04000060 RID: 96
		public int _calculateHeightsKernel;

		// Token: 0x04000061 RID: 97
		public int _findWaterfallsKernel;

		// Token: 0x04000062 RID: 98
		public RenderTexture _oldBrokenCornerLinks;

		// Token: 0x04000063 RID: 99
		public RenderTexture _newBrokenCornerLinks;

		// Token: 0x04000064 RID: 100
		public RenderTexture _oldWaterData;

		// Token: 0x04000065 RID: 101
		public RenderTexture _newWaterData;

		// Token: 0x04000066 RID: 102
		public RenderTexture _oldEdgeLinks;

		// Token: 0x04000067 RID: 103
		public RenderTexture _newEdgeLinks;

		// Token: 0x04000068 RID: 104
		public RenderTexture _oldCornerLinks;

		// Token: 0x04000069 RID: 105
		public RenderTexture _newCornerLinks;

		// Token: 0x0400006A RID: 106
		public RenderTexture _oldBaseCornerLinks;

		// Token: 0x0400006B RID: 107
		public RenderTexture _newBaseCornerLinks;

		// Token: 0x0400006C RID: 108
		public RenderTexture _oldSkirts;

		// Token: 0x0400006D RID: 109
		public RenderTexture _newSkirts;

		// Token: 0x0400006E RID: 110
		public RenderTexture _oldWaterHeights;

		// Token: 0x0400006F RID: 111
		public RenderTexture _newWaterHeights;

		// Token: 0x04000070 RID: 112
		public RenderTexture _oldWaterfalls;

		// Token: 0x04000071 RID: 113
		public RenderTexture _newWaterfalls;

		// Token: 0x04000072 RID: 114
		public RenderTexture _oldOutflows;

		// Token: 0x04000073 RID: 115
		public RenderTexture _newOutflows;

		// Token: 0x04000074 RID: 116
		public RenderTexture _oldContaminations;

		// Token: 0x04000075 RID: 117
		public RenderTexture _newContaminations;

		// Token: 0x04000076 RID: 118
		public RenderTexture _oldCornerLinksBuffer;

		// Token: 0x04000077 RID: 119
		public RenderTexture _newCornerLinksBuffer;

		// Token: 0x04000078 RID: 120
		public Vector2Int _tileThreadGroupCount;

		// Token: 0x04000079 RID: 121
		public Vector2Int _vertexThreadGroupCount;

		// Token: 0x0400007A RID: 122
		public int _arraySize;
	}
}
