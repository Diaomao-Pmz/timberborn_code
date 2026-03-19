using System;
using System.Collections.Generic;
using Timberborn.BlockObjectTools;
using Timberborn.BlockObjectToolsUI;
using Timberborn.BlockSystem;
using Timberborn.BottomBarSystem;
using Timberborn.ToolButtonSystem;

namespace Timberborn.MapEditorUI
{
	// Token: 0x02000005 RID: 5
	internal class MapEditorBlockObjectButtons : IBottomBarElementsProvider
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002502 File Offset: 0x00000702
		public MapEditorBlockObjectButtons(BlockObjectToolGroupSpecService blockObjectToolGroupSpecService, PlaceableBlockObjectSpecService placeableBlockObjectSpecService, BlockObjectToolGroupButtonFactory blockObjectToolGroupButtonFactory, BlockObjectToolButtonFactory blockObjectToolButtonFactory)
		{
			this._blockObjectToolGroupSpecService = blockObjectToolGroupSpecService;
			this._placeableBlockObjectSpecService = placeableBlockObjectSpecService;
			this._blockObjectToolGroupButtonFactory = blockObjectToolGroupButtonFactory;
			this._blockObjectToolButtonFactory = blockObjectToolButtonFactory;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002527 File Offset: 0x00000727
		public IEnumerable<BottomBarElement> GetElements()
		{
			BlockObjectToolGroupSpec spec = this._blockObjectToolGroupSpecService.GetSpec(MapEditorBlockObjectButtons.SingleLevelGroup);
			IEnumerable<PlaceableBlockObjectSpec> blockObjects = this._placeableBlockObjectSpecService.GetBlockObjects(spec);
			foreach (PlaceableBlockObjectSpec template in blockObjects)
			{
				ToolButton toolButton = this._blockObjectToolButtonFactory.Create(template);
				yield return BottomBarElement.CreateSingleLevel(toolButton.Root);
			}
			IEnumerator<PlaceableBlockObjectSpec> enumerator = null;
			foreach (string groupId in MapEditorBlockObjectButtons.NestedGroups)
			{
				BlockObjectToolGroupSpec spec2 = this._blockObjectToolGroupSpecService.GetSpec(groupId);
				IEnumerable<PlaceableBlockObjectSpec> blockObjects2 = this._placeableBlockObjectSpecService.GetBlockObjects(spec2);
				yield return this._blockObjectToolGroupButtonFactory.Create(spec2, blockObjects2);
			}
			string[] array = null;
			yield break;
			yield break;
		}

		// Token: 0x04000018 RID: 24
		private static readonly string SingleLevelGroup = "MapEditor";

		// Token: 0x04000019 RID: 25
		private static readonly string[] NestedGroups = new string[]
		{
			"MapEditorWater",
			"MapEditorObjects",
			"Ruins"
		};

		// Token: 0x0400001A RID: 26
		private readonly BlockObjectToolGroupSpecService _blockObjectToolGroupSpecService;

		// Token: 0x0400001B RID: 27
		private readonly PlaceableBlockObjectSpecService _placeableBlockObjectSpecService;

		// Token: 0x0400001C RID: 28
		private readonly BlockObjectToolGroupButtonFactory _blockObjectToolGroupButtonFactory;

		// Token: 0x0400001D RID: 29
		private readonly BlockObjectToolButtonFactory _blockObjectToolButtonFactory;
	}
}
