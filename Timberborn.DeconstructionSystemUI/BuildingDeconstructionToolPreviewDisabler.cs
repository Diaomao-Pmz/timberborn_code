using System;
using Timberborn.Debugging;

namespace Timberborn.DeconstructionSystemUI
{
	// Token: 0x02000006 RID: 6
	public class BuildingDeconstructionToolPreviewDisabler : IDevModule
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000022D0 File Offset: 0x000004D0
		public BuildingDeconstructionToolPreviewDisabler(BuildingDeconstructionTool buildingDeconstructionTool)
		{
			this._buildingDeconstructionTool = buildingDeconstructionTool;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022DF File Offset: 0x000004DF
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Toggle deconstruction tool preview", new Action(this.ToggleDeconstructionToolPreview))).Build();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002306 File Offset: 0x00000506
		public void ToggleDeconstructionToolPreview()
		{
			this._buildingDeconstructionTool.TogglePreview();
		}

		// Token: 0x04000011 RID: 17
		public readonly BuildingDeconstructionTool _buildingDeconstructionTool;
	}
}
