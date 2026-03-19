using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.ToolSystemUI;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x0200001D RID: 29
	public interface IBlockObjectPlacer
	{
		// Token: 0x0600008D RID: 141
		void Place(BlockObjectSpec template, Placement placement, Action<BaseComponent> placedCallback);

		// Token: 0x0600008E RID: 142
		void Describe(BlockObjectTool tool, ToolDescription.Builder builder, Preview preview);

		// Token: 0x0600008F RID: 143
		bool CanHandle(BlockObjectSpec template);
	}
}
