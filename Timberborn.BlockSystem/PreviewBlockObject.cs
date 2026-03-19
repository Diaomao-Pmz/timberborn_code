using System;
using Timberborn.BaseComponentSystem;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000061 RID: 97
	public class PreviewBlockObject : BaseComponent, IAwakableComponent, IPreviewServiceMember
	{
		// Token: 0x0600027E RID: 638 RVA: 0x00007A2C File Offset: 0x00005C2C
		public PreviewBlockObject(PreviewBlockService previewBlockService)
		{
			this._previewBlockService = previewBlockService;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00007A3B File Offset: 0x00005C3B
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00007A49 File Offset: 0x00005C49
		public void AddToPreviewService()
		{
			if (!this._setInService)
			{
				this._previewBlockService.SetPreview(this._blockObject);
				this._setInService = true;
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00007A6B File Offset: 0x00005C6B
		public void RemoveFromPreviewService()
		{
			if (this._setInService)
			{
				this._previewBlockService.UnsetPreview(this._blockObject);
				this._setInService = false;
			}
		}

		// Token: 0x0400012E RID: 302
		public readonly PreviewBlockService _previewBlockService;

		// Token: 0x0400012F RID: 303
		public BlockObject _blockObject;

		// Token: 0x04000130 RID: 304
		public bool _setInService;
	}
}
