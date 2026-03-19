using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockObjectTools;
using Timberborn.BlockObjectToolsUI;
using Timberborn.BlockSystem;
using Timberborn.BottomBarSystem;
using Timberborn.Common;
using Timberborn.TemplateSystem;
using UnityEngine;

namespace Timberborn.GameBlockObjectButtonsSystem
{
	// Token: 0x02000004 RID: 4
	public class GameBlockObjectButtons : IBottomBarElementsProvider
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public GameBlockObjectButtons(BlockObjectToolGroupSpecService blockObjectToolGroupSpecService, PlaceableBlockObjectSpecService placeableBlockObjectSpecService, BlockObjectToolGroupButtonFactory blockObjectToolGroupButtonFactory)
		{
			this._blockObjectToolGroupSpecService = blockObjectToolGroupSpecService;
			this._placeableBlockObjectSpecService = placeableBlockObjectSpecService;
			this._blockObjectToolGroupButtonFactory = blockObjectToolGroupButtonFactory;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020DB File Offset: 0x000002DB
		public IEnumerable<BottomBarElement> GetElements()
		{
			return this.CreateRegularBlockObjectToolGroups().Concat(this.CreateFallbackBlockObjectToolGroup());
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020EE File Offset: 0x000002EE
		public IEnumerable<BottomBarElement> CreateRegularBlockObjectToolGroups()
		{
			IEnumerable<BlockObjectToolGroupSpec> enumerable = from toolGroupSpec in this._blockObjectToolGroupSpecService.AllSpecs
			where !toolGroupSpec.FallbackGroup
			select toolGroupSpec;
			foreach (BlockObjectToolGroupSpec blockObjectToolGroupSpec in enumerable)
			{
				BottomBarElement? bottomBarElement = this.CreateRegularBlockObjectToolGroup(blockObjectToolGroupSpec);
				if (bottomBarElement != null)
				{
					yield return bottomBarElement.Value;
				}
			}
			IEnumerator<BlockObjectToolGroupSpec> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002100 File Offset: 0x00000300
		public BottomBarElement? CreateRegularBlockObjectToolGroup(BlockObjectToolGroupSpec blockObjectToolGroupSpec)
		{
			List<PlaceableBlockObjectSpec> list = this._placeableBlockObjectSpecService.GetBlockObjects(blockObjectToolGroupSpec).ToList<PlaceableBlockObjectSpec>();
			if (list.Count == 0)
			{
				return null;
			}
			return new BottomBarElement?(this._blockObjectToolGroupButtonFactory.Create(blockObjectToolGroupSpec, list));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002143 File Offset: 0x00000343
		public IEnumerable<BottomBarElement> CreateFallbackBlockObjectToolGroup()
		{
			List<PlaceableBlockObjectSpec> list = this._placeableBlockObjectSpecService.GetBlockObjectsWithoutValidGroup().ToList<PlaceableBlockObjectSpec>();
			if (!list.IsEmpty<PlaceableBlockObjectSpec>())
			{
				GameBlockObjectButtons.LogBlockObjectsWithUnknownToolGroup(list);
				BlockObjectToolGroupSpec fallbackSpec = this._blockObjectToolGroupSpecService.GetFallbackSpec();
				yield return this._blockObjectToolGroupButtonFactory.Create(fallbackSpec, list);
			}
			yield break;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002154 File Offset: 0x00000354
		public static void LogBlockObjectsWithUnknownToolGroup(IEnumerable<PlaceableBlockObjectSpec> blockObjects)
		{
			foreach (PlaceableBlockObjectSpec placeableBlockObjectSpec in blockObjects)
			{
				string templateName = placeableBlockObjectSpec.GetSpec<TemplateSpec>().TemplateName;
				string toolGroupId = placeableBlockObjectSpec.ToolGroupId;
				Debug.LogWarning(string.Concat(new string[]
				{
					"Block object \"",
					templateName,
					"\" is associated with an unknown BlockObjectToolGroupSpec with ID \"",
					toolGroupId,
					"\""
				}));
			}
		}

		// Token: 0x04000006 RID: 6
		public readonly BlockObjectToolGroupSpecService _blockObjectToolGroupSpecService;

		// Token: 0x04000007 RID: 7
		public readonly PlaceableBlockObjectSpecService _placeableBlockObjectSpecService;

		// Token: 0x04000008 RID: 8
		public readonly BlockObjectToolGroupButtonFactory _blockObjectToolGroupButtonFactory;
	}
}
