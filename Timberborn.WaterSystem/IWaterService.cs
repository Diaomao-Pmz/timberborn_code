using System;
using UnityEngine;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000017 RID: 23
	public interface IWaterService
	{
		// Token: 0x0600005B RID: 91
		void AddFullObstacle(Vector3Int coordinates);

		// Token: 0x0600005C RID: 92
		void RemoveFullObstacle(Vector3Int coordinates);

		// Token: 0x0600005D RID: 93
		void AddHorizontalObstacle(Vector3Int coordinatesToAdd);

		// Token: 0x0600005E RID: 94
		void RemoveHorizontalObstacle(Vector3Int coordinatesToRemove);

		// Token: 0x0600005F RID: 95
		void RegisterWaterSource(IWaterSource waterSource);

		// Token: 0x06000060 RID: 96
		void UnregisterWaterSource(IWaterSource waterSource);

		// Token: 0x06000061 RID: 97
		void AddCleanWater(Vector3Int coordinates, float depth);

		// Token: 0x06000062 RID: 98
		void RemoveCleanWater(Vector3Int coordinates, float depth);

		// Token: 0x06000063 RID: 99
		void AddContaminatedWater(Vector3Int coordinates, float depth);

		// Token: 0x06000064 RID: 100
		void RemoveContaminatedWater(Vector3Int coordinates, float depth);

		// Token: 0x06000065 RID: 101
		void UpdateInflowLimiter(Vector3Int coordinates, float flowLimit);

		// Token: 0x06000066 RID: 102
		void RemoveInflowLimiter(Vector3Int coordinates);

		// Token: 0x06000067 RID: 103
		void SetOutflowLimit(Vector3Int coordinates, float outflowLimit);

		// Token: 0x06000068 RID: 104
		void RemoveOutflowLimit(Vector3Int coordinates);

		// Token: 0x06000069 RID: 105
		void AddDirectionLimiter(Vector3Int coordinates, FlowDirection flowDirection);

		// Token: 0x0600006A RID: 106
		void RemoveDirectionLimiter(Vector3Int coordinates);

		// Token: 0x0600006B RID: 107
		void SetControllerToDecreaseFlow(Vector3Int coordinates);

		// Token: 0x0600006C RID: 108
		void SetControllerToIncreaseFlow(Vector3Int coordinates);

		// Token: 0x0600006D RID: 109
		void RemoveFlowController(Vector3Int coordinates);
	}
}
