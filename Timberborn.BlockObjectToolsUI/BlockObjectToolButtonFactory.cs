using System;
using Timberborn.BlockObjectTools;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.ToolButtonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.BlockObjectToolsUI
{
	// Token: 0x02000005 RID: 5
	public class BlockObjectToolButtonFactory
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000238B File Offset: 0x0000058B
		public BlockObjectToolButtonFactory(ToolButtonFactory toolButtonFactory, BlockObjectToolFactory blockObjectToolFactory, BlockObjectToolDescriber blockObjectToolDescriber, BlockObjectPlacerService blockObjectPlacerService)
		{
			this._toolButtonFactory = toolButtonFactory;
			this._blockObjectToolFactory = blockObjectToolFactory;
			this._blockObjectToolDescriber = blockObjectToolDescriber;
			this._blockObjectPlacerService = blockObjectPlacerService;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023B0 File Offset: 0x000005B0
		public ToolButton Create(PlaceableBlockObjectSpec template, VisualElement buttonParent)
		{
			BlockObjectTool tool = this.CreateTool(template);
			Sprite toolImage = BlockObjectToolButtonFactory.GetToolImage(template);
			ToolShapes toolShape = template.ToolShape;
			ToolButton result;
			if (toolShape != ToolShapes.Square)
			{
				if (toolShape != ToolShapes.Hex)
				{
					throw new ArgumentOutOfRangeException(string.Format("Invalid tool shape: {0}", template.ToolShape));
				}
				result = this._toolButtonFactory.CreateHex(tool, toolImage, buttonParent);
			}
			else
			{
				result = this._toolButtonFactory.Create(tool, toolImage, buttonParent);
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000241A File Offset: 0x0000061A
		public ToolButton Create(PlaceableBlockObjectSpec template)
		{
			return this._toolButtonFactory.CreateGrouplessGreen(this.CreateTool(template), BlockObjectToolButtonFactory.GetToolImage(template));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002434 File Offset: 0x00000634
		public BlockObjectTool CreateTool(PlaceableBlockObjectSpec template)
		{
			BlockObjectSpec spec = template.GetSpec<BlockObjectSpec>();
			IBlockObjectPlacer matchingPlacer = this._blockObjectPlacerService.GetMatchingPlacer(spec);
			return this._blockObjectToolFactory.Create(template, matchingPlacer, this._blockObjectToolDescriber);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002468 File Offset: 0x00000668
		public static Sprite GetToolImage(PlaceableBlockObjectSpec template)
		{
			return template.GetSpec<LabeledEntitySpec>().Icon.Asset;
		}

		// Token: 0x04000017 RID: 23
		public readonly ToolButtonFactory _toolButtonFactory;

		// Token: 0x04000018 RID: 24
		public readonly BlockObjectToolFactory _blockObjectToolFactory;

		// Token: 0x04000019 RID: 25
		public readonly BlockObjectToolDescriber _blockObjectToolDescriber;

		// Token: 0x0400001A RID: 26
		public readonly BlockObjectPlacerService _blockObjectPlacerService;
	}
}
