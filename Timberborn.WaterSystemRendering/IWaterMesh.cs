using System;
using UnityEngine;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x0200000B RID: 11
	public interface IWaterMesh
	{
		// Token: 0x06000024 RID: 36
		void Show();

		// Token: 0x06000025 RID: 37
		void Hide();

		// Token: 0x06000026 RID: 38
		void EnableTile(Vector3Int tileIndex);

		// Token: 0x06000027 RID: 39
		void DisableAllTiles();
	}
}
