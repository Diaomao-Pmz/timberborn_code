using System;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000033 RID: 51
	public interface INavMeshDrawer
	{
		// Token: 0x0600013C RID: 316
		void DrawForOneFrameAroundCoordinates(Vector3Int coordinates);
	}
}
