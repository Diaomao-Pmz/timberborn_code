using System;
using UnityEngine;

namespace Timberborn.SoilMoistureSystem
{
	// Token: 0x0200000C RID: 12
	public interface ISoilMoistureService
	{
		// Token: 0x0600003B RID: 59
		bool SoilIsMoist(Vector3Int coordinates);

		// Token: 0x0600003C RID: 60
		float SoilMoisture(int index);
	}
}
