using System;
using Timberborn.BlockObjectTools;
using Timberborn.BlockSystem;

namespace Timberborn.GameStartup
{
	// Token: 0x02000014 RID: 20
	public class StartingBuildingToolFactory
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002951 File Offset: 0x00000B51
		public StartingBuildingToolFactory(BlockObjectToolFactory blockObjectToolFactory, StartingBuildingToolDescriber startingBuildingToolDescriber, StartingBuildingPlacer startingBuildingPlacer)
		{
			this._blockObjectToolFactory = blockObjectToolFactory;
			this._startingBuildingToolDescriber = startingBuildingToolDescriber;
			this._startingBuildingPlacer = startingBuildingPlacer;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000296E File Offset: 0x00000B6E
		public BlockObjectTool Create(PlaceableBlockObjectSpec template)
		{
			return this._blockObjectToolFactory.Create(template, this._startingBuildingPlacer, this._startingBuildingToolDescriber);
		}

		// Token: 0x0400003A RID: 58
		public readonly BlockObjectToolFactory _blockObjectToolFactory;

		// Token: 0x0400003B RID: 59
		public readonly StartingBuildingToolDescriber _startingBuildingToolDescriber;

		// Token: 0x0400003C RID: 60
		public readonly StartingBuildingPlacer _startingBuildingPlacer;
	}
}
