using System;
using Timberborn.Debugging;

namespace Timberborn.TerrainSystemRendering
{
	// Token: 0x02000019 RID: 25
	public class TerrainSystemRenderingDevModule : IDevModule
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00004FC2 File Offset: 0x000031C2
		public TerrainSystemRenderingDevModule(TerrainMeshManager terrainMeshManager)
		{
			this._terrainMeshManager = terrainMeshManager;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004FD1 File Offset: 0x000031D1
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Toggle models: Terrain", new Action(this._terrainMeshManager.ToggleVisibilityForDebugging))).Build();
		}

		// Token: 0x0400007E RID: 126
		public readonly TerrainMeshManager _terrainMeshManager;
	}
}
