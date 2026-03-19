using System;
using Timberborn.ToolSystemUI;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x0200001E RID: 30
	public interface IBlockObjectToolDescriber
	{
		// Token: 0x06000090 RID: 144
		ToolDescription Describe(BlockObjectTool blockObjectTool, IBlockObjectPlacer blockObjectPlacer);
	}
}
