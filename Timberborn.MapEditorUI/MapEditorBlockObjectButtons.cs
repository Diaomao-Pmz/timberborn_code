using System;
using System.Collections.Generic;
using Timberborn.BlockObjectTools;
using Timberborn.BlockObjectToolsUI;
using Timberborn.BlockSystem;
using Timberborn.BottomBarSystem;
using Timberborn.ToolButtonSystem;

namespace Timberborn.MapEditorUI
{
	// Token: 0x02000007 RID: 7
	public class MapEditorBlockObjectButtons : IBottomBarElementsProvider
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000250F File Offset: 0x0000070F
		public MapEditorBlockObjectButtons(BlockObjectToolGroupSpecService blockObjectToolGroupSpecService, PlaceableBlockObjectSpecService placeableBlockObjectSpecService, BlockObjectToolGroupButtonFactory blockObjectToolGroupButtonFactory, BlockObjectToolButtonFactory blockObjectToolButtonFactory)
		{
			this._blockObjectToolGroupSpecService = blockObjectToolGroupSpecService;
			this._placeableBlockObjectSpecService = placeableBlockObjectSpecService;
			this._blockObjectToolGroupButtonFactory = blockObjectToolGroupButtonFactory;
			this._blockObjectToolButtonFactory = blockObjectToolButtonFactory;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002534 File Offset: 0x00000734
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

		// Token: 0x0400001E RID: 30
		public static readonly string SingleLevelGroup = "MapEditor";

		// Token: 0x0400001F RID: 31
		public static readonly string[] NestedGroups = new string[]
		{
			"MapEditorWater",
			"MapEditorObjects",
			"Ruins"
		};

		// Token: 0x04000020 RID: 32
		public readonly BlockObjectToolGroupSpecService _blockObjectToolGroupSpecService;

		// Token: 0x04000021 RID: 33
		public readonly PlaceableBlockObjectSpecService _placeableBlockObjectSpecService;

		// Token: 0x04000022 RID: 34
		public readonly BlockObjectToolGroupButtonFactory _blockObjectToolGroupButtonFactory;

		// Token: 0x04000023 RID: 35
		public readonly BlockObjectToolButtonFactory _blockObjectToolButtonFactory;
	}
}
