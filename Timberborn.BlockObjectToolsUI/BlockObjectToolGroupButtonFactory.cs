using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BlockObjectTools;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.BottomBarSystem;
using Timberborn.ConstructionMode;
using Timberborn.ToolButtonSystem;
using Timberborn.ToolSystem;

namespace Timberborn.BlockObjectToolsUI
{
	// Token: 0x02000006 RID: 6
	public class BlockObjectToolGroupButtonFactory
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000247A File Offset: 0x0000067A
		public BlockObjectToolGroupButtonFactory(BlockObjectToolButtonFactory blockObjectToolButtonFactory, ToolGroupButtonFactory toolGroupButtonFactory, ToolGroupService toolGroupService)
		{
			this._blockObjectToolButtonFactory = blockObjectToolButtonFactory;
			this._toolGroupButtonFactory = toolGroupButtonFactory;
			this._toolGroupService = toolGroupService;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002498 File Offset: 0x00000698
		public BottomBarElement Create(BlockObjectToolGroupSpec blockObjectToolGroupSpec, IEnumerable<PlaceableBlockObjectSpec> blockObjects)
		{
			ToolGroupSpec spec = BlockObjectToolGroupButtonFactory.CreateBlueprint(blockObjectToolGroupSpec).GetSpec<ToolGroupSpec>();
			ToolGroupButton toolGroupButton = this._toolGroupButtonFactory.CreateGreen(spec);
			foreach (PlaceableBlockObjectSpec placeableBlockObjectSpec in blockObjects)
			{
				if (placeableBlockObjectSpec.UsableWithCurrentFeatureToggles)
				{
					ToolButton toolButton = this._blockObjectToolButtonFactory.Create(placeableBlockObjectSpec, toolGroupButton.ToolButtonsElement);
					this._toolGroupService.AssignToGroup(spec, toolButton.Tool);
					toolGroupButton.AddTool(toolButton);
				}
			}
			this._toolGroupService.RegisterGroup(spec);
			return BottomBarElement.CreateMultiLevel(toolGroupButton.Root, toolGroupButton.ToolButtonsElement);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002548 File Offset: 0x00000748
		public static Blueprint CreateBlueprint(BlockObjectToolGroupSpec blockObjectToolGroupSpec)
		{
			ToolGroupSpec toolGroupSpec = new ToolGroupSpec
			{
				Id = "BlockObjectToolGroupSpec." + blockObjectToolGroupSpec.Id,
				DisplayNameLocKey = blockObjectToolGroupSpec.NameLocKey,
				Icon = blockObjectToolGroupSpec.Icon
			};
			return new Blueprint("Blueprint." + blockObjectToolGroupSpec.Id, new ComponentSpec[]
			{
				toolGroupSpec,
				new ConstructionModeToolGroupSpec()
			}, ImmutableArray<Blueprint>.Empty);
		}

		// Token: 0x0400001B RID: 27
		public readonly BlockObjectToolButtonFactory _blockObjectToolButtonFactory;

		// Token: 0x0400001C RID: 28
		public readonly ToolGroupButtonFactory _toolGroupButtonFactory;

		// Token: 0x0400001D RID: 29
		public readonly ToolGroupService _toolGroupService;
	}
}
