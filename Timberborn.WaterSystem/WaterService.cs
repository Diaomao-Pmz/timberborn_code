using System;
using UnityEngine;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000034 RID: 52
	public class WaterService : IWaterService
	{
		// Token: 0x060000ED RID: 237 RVA: 0x0000557A File Offset: 0x0000377A
		public WaterService(WaterSourceRegistry waterSourceRegistry, WaterChangeService waterChangeService, WaterSimulator waterSimulator, FlowLimiterService flowLimiterService)
		{
			this._waterSourceRegistry = waterSourceRegistry;
			this._waterChangeService = waterChangeService;
			this._waterSimulator = waterSimulator;
			this._flowLimiterService = flowLimiterService;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000559F File Offset: 0x0000379F
		public void AddFullObstacle(Vector3Int coordinates)
		{
			this._waterSimulator.AddFullObstacle(coordinates);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000055AD File Offset: 0x000037AD
		public void RemoveFullObstacle(Vector3Int coordinates)
		{
			this._waterSimulator.RemoveFullObstacle(coordinates);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000055BB File Offset: 0x000037BB
		public void AddHorizontalObstacle(Vector3Int coordinates)
		{
			this._waterSimulator.AddHorizontalObstacle(coordinates);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000055C9 File Offset: 0x000037C9
		public void RemoveHorizontalObstacle(Vector3Int coordinates)
		{
			this._waterSimulator.RemoveHorizontalObstacle(coordinates);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000055D7 File Offset: 0x000037D7
		public void RegisterWaterSource(IWaterSource waterSource)
		{
			this._waterSourceRegistry.RegisterWaterSource(waterSource);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000055E5 File Offset: 0x000037E5
		public void UnregisterWaterSource(IWaterSource waterSource)
		{
			this._waterSourceRegistry.UnregisterWaterSource(waterSource);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000055F3 File Offset: 0x000037F3
		public void AddCleanWater(Vector3Int coordinates, float depth)
		{
			this._waterChangeService.EnqueueWaterChange(coordinates, depth, 0f);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005607 File Offset: 0x00003807
		public void RemoveCleanWater(Vector3Int coordinates, float depth)
		{
			this._waterChangeService.EnqueueWaterChange(coordinates, -depth, 0f);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000561C File Offset: 0x0000381C
		public void AddContaminatedWater(Vector3Int coordinates, float depth)
		{
			this._waterChangeService.EnqueueWaterChange(coordinates, depth, 1f);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005630 File Offset: 0x00003830
		public void RemoveContaminatedWater(Vector3Int coordinates, float depth)
		{
			this._waterChangeService.EnqueueWaterChange(coordinates, -depth, 1f);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005645 File Offset: 0x00003845
		public void UpdateInflowLimiter(Vector3Int coordinates, float flowLimit)
		{
			this._flowLimiterService.UpdateInflowLimiter(coordinates, flowLimit);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005654 File Offset: 0x00003854
		public void RemoveInflowLimiter(Vector3Int coordinates)
		{
			this._flowLimiterService.RemoveInflowLimiter(coordinates);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005662 File Offset: 0x00003862
		public void SetOutflowLimit(Vector3Int coordinates, float outflowLimit)
		{
			this._flowLimiterService.SetOutflowLimit(coordinates, outflowLimit);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005671 File Offset: 0x00003871
		public void RemoveOutflowLimit(Vector3Int coordinates)
		{
			this._flowLimiterService.RemoveOutflowLimit(coordinates);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000567F File Offset: 0x0000387F
		public void AddDirectionLimiter(Vector3Int coordinates, FlowDirection flowDirection)
		{
			this._flowLimiterService.AddDirectionLimiter(coordinates, flowDirection);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000568E File Offset: 0x0000388E
		public void RemoveDirectionLimiter(Vector3Int coordinates)
		{
			this._flowLimiterService.RemoveDirectionLimiter(coordinates);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000569C File Offset: 0x0000389C
		public void SetControllerToDecreaseFlow(Vector3Int coordinates)
		{
			this._flowLimiterService.SetControllerToDecreaseFlow(coordinates);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000056AA File Offset: 0x000038AA
		public void SetControllerToIncreaseFlow(Vector3Int coordinates)
		{
			this._flowLimiterService.SetControllerToIncreaseFlow(coordinates);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000056B8 File Offset: 0x000038B8
		public void RemoveFlowController(Vector3Int coordinates)
		{
			this._flowLimiterService.RemoveFlowController(coordinates);
		}

		// Token: 0x040000D0 RID: 208
		public readonly WaterSourceRegistry _waterSourceRegistry;

		// Token: 0x040000D1 RID: 209
		public readonly WaterChangeService _waterChangeService;

		// Token: 0x040000D2 RID: 210
		public readonly WaterSimulator _waterSimulator;

		// Token: 0x040000D3 RID: 211
		public readonly FlowLimiterService _flowLimiterService;
	}
}
