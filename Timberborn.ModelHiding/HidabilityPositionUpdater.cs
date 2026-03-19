using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;

namespace Timberborn.ModelHiding
{
	// Token: 0x02000009 RID: 9
	public class HidabilityPositionUpdater : BaseComponent, IAwakableComponent, IPrePlacementChangeListener, IPostPlacementChangeListener
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000023CB File Offset: 0x000005CB
		public HidabilityPositionUpdater(IModelAdder modelAdder)
		{
			this._modelAdder = modelAdder;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023DA File Offset: 0x000005DA
		public void Awake()
		{
			this._blockObjectModelController = base.GetComponent<BlockObjectModelController>();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023E8 File Offset: 0x000005E8
		public void OnPrePlacementChanged()
		{
			if (!this._blockObjectModelController.BlockObject.IsPreview)
			{
				this._modelAdder.RemoveModel(this._blockObjectModelController);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000240D File Offset: 0x0000060D
		public void OnPostPlacementChanged()
		{
			if (!this._blockObjectModelController.BlockObject.IsPreview)
			{
				this._modelAdder.AddModel(this._blockObjectModelController);
			}
		}

		// Token: 0x0400000D RID: 13
		public readonly IModelAdder _modelAdder;

		// Token: 0x0400000E RID: 14
		public BlockObjectModelController _blockObjectModelController;
	}
}
