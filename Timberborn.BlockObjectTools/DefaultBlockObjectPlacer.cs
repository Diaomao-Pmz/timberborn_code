using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.ToolSystemUI;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x0200001A RID: 26
	public class DefaultBlockObjectPlacer : IBlockObjectPlacer
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00003469 File Offset: 0x00001669
		public DefaultBlockObjectPlacer(BlockObjectFactory blockObjectFactory)
		{
			this._blockObjectFactory = blockObjectFactory;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003478 File Offset: 0x00001678
		public void Place(BlockObjectSpec template, Placement placement, Action<BaseComponent> placedCallback)
		{
			BlockObject obj = this._blockObjectFactory.CreateFinished(template, placement);
			placedCallback(obj);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000022AE File Offset: 0x000004AE
		public void Describe(BlockObjectTool tool, ToolDescription.Builder builder, Preview preview)
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002302 File Offset: 0x00000502
		public bool CanHandle(BlockObjectSpec template)
		{
			return true;
		}

		// Token: 0x04000057 RID: 87
		public readonly BlockObjectFactory _blockObjectFactory;
	}
}
