using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.ModularShafts
{
	// Token: 0x0200000B RID: 11
	public class ModularShaftCover : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000025B5 File Offset: 0x000007B5
		public ModularShaftCover(IBlockService blockService, PreviewBlockService previewBlockService)
		{
			this._blockService = blockService;
			this._previewBlockService = previewBlockService;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000025CC File Offset: 0x000007CC
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			ModularShaftCoverSpec component = base.GetComponent<ModularShaftCoverSpec>();
			this._cover = base.GameObject.FindChild(component.CoverModelName);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002604 File Offset: 0x00000804
		public void UpdateModel()
		{
			Vector3Int coordinates = this._blockObject.Coordinates.Above();
			ModularShaft bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<ModularShaft>(coordinates);
			ModularShaft bottomObjectComponentAt2 = this._previewBlockService.GetBottomObjectComponentAt<ModularShaft>(coordinates);
			this._cover.SetActive(!bottomObjectComponentAt && !bottomObjectComponentAt2);
		}

		// Token: 0x04000019 RID: 25
		public readonly IBlockService _blockService;

		// Token: 0x0400001A RID: 26
		public readonly PreviewBlockService _previewBlockService;

		// Token: 0x0400001B RID: 27
		public BlockObject _blockObject;

		// Token: 0x0400001C RID: 28
		public GameObject _cover;
	}
}
