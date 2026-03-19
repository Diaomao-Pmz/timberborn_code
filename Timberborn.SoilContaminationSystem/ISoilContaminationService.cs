using System;
using UnityEngine;

namespace Timberborn.SoilContaminationSystem
{
	// Token: 0x0200000B RID: 11
	public interface ISoilContaminationService
	{
		// Token: 0x06000020 RID: 32
		bool SoilIsContaminated(Vector3Int coordinates);

		// Token: 0x06000021 RID: 33
		float Contamination(int index);
	}
}
