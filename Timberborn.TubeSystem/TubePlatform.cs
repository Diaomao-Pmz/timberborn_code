using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.TubeSystem
{
	// Token: 0x02000010 RID: 16
	public class TubePlatform : BaseComponent, IAwakableComponent, IInitializableEntity, IModelUpdater
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002A95 File Offset: 0x00000C95
		public TubePlatform(IBlockService blockService, PreviewBlockService previewBlockService, StackableBlockService stackableBlockService)
		{
			this._blockService = blockService;
			this._previewBlockService = previewBlockService;
			this._stackableBlockService = stackableBlockService;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			TubePlatformSpec component = base.GetComponent<TubePlatformSpec>();
			this._tubePlatform = base.GameObject.FindChild(component.PlatformModelName);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002AEB File Offset: 0x00000CEB
		public void InitializeEntity()
		{
			this.UpdateVisibility();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002AEB File Offset: 0x00000CEB
		public void UpdateModel()
		{
			this.UpdateVisibility();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public void UpdateVisibility()
		{
			Vector3Int vector3Int = this._blockObject.Coordinates.Above();
			Tube pathObjectComponentAt = this._blockService.GetPathObjectComponentAt<Tube>(vector3Int);
			Tube pathObjectComponentAt2 = this._previewBlockService.GetPathObjectComponentAt<Tube>(vector3Int);
			bool flag = this._stackableBlockService.IsStackableBlockAt(vector3Int, false);
			this._tubePlatform.SetActive((!pathObjectComponentAt && !pathObjectComponentAt2) || !flag);
		}

		// Token: 0x04000029 RID: 41
		public readonly IBlockService _blockService;

		// Token: 0x0400002A RID: 42
		public readonly PreviewBlockService _previewBlockService;

		// Token: 0x0400002B RID: 43
		public readonly StackableBlockService _stackableBlockService;

		// Token: 0x0400002C RID: 44
		public BlockObject _blockObject;

		// Token: 0x0400002D RID: 45
		public GameObject _tubePlatform;
	}
}
