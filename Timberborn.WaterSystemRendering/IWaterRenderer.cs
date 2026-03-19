using System;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x0200000C RID: 12
	public interface IWaterRenderer
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000028 RID: 40
		long UpdateMeshTime { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000029 RID: 41
		long UpdateTexturesTime { get; }

		// Token: 0x0600002A RID: 42
		void EnableMeshUpdate();

		// Token: 0x0600002B RID: 43
		void DisableMeshUpdate();

		// Token: 0x0600002C RID: 44
		void DisableTextureUpdate();

		// Token: 0x0600002D RID: 45
		void EnableTextureUpdate();

		// Token: 0x0600002E RID: 46
		void DisablePostprocessing();

		// Token: 0x0600002F RID: 47
		void EnablePostprocessing();
	}
}
