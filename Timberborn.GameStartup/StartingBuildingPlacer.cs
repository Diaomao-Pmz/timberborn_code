using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectTools;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystemUI;

namespace Timberborn.GameStartup
{
	// Token: 0x02000011 RID: 17
	public class StartingBuildingPlacer : IBlockObjectPlacer
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000026EC File Offset: 0x000008EC
		public StartingBuildingPlacer(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000026FB File Offset: 0x000008FB
		public void Place(BlockObjectSpec template, Placement placement, Action<BaseComponent> placedCallback)
		{
			this._eventBus.Post(new StartingBuildingPlacedEvent(placement));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000270E File Offset: 0x0000090E
		public void Describe(BlockObjectTool tool, ToolDescription.Builder builder, Preview preview)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002710 File Offset: 0x00000910
		public bool CanHandle(BlockObjectSpec template)
		{
			return true;
		}

		// Token: 0x0400002D RID: 45
		public readonly EventBus _eventBus;
	}
}
