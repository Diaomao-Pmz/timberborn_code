using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BuildingRange;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;

namespace Timberborn.RangedEffectBuildingUI
{
	// Token: 0x02000007 RID: 7
	public class BuildingWithRangePreviewUpdater : BaseComponent, IAwakableComponent, IPreviewSelectionListener, IPostPlacementChangeListener
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public BuildingWithRangePreviewUpdater(BuildingWithRangeUpdateService buildingWithRangeUpdateService, EventBus eventBus)
		{
			this._buildingWithRangeUpdateService = buildingWithRangeUpdateService;
			this._eventBus = eventBus;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002114 File Offset: 0x00000314
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._preview = base.GetComponent<Preview>();
			this._buildingWithRange = base.GetComponent<IBuildingWithRange>();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000213A File Offset: 0x0000033A
		public void OnPreviewSelect()
		{
			this.RegisterOnPreviewSelect();
			this.DrawArea();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002148 File Offset: 0x00000348
		public void OnPreviewUnselect()
		{
			this._buildingWithRangeUpdateService.DrawArea();
			this._drawArea = true;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000215C File Offset: 0x0000035C
		public void OnPostPlacementChanged()
		{
			if (this._blockObject.IsPreview)
			{
				this._drawArea = true;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002172 File Offset: 0x00000372
		[OnEvent]
		public void OnToolExited(ToolExitedEvent toolExitedEvent)
		{
			this.UnregisterOnToolExited();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000217A File Offset: 0x0000037A
		public void RegisterOnPreviewSelect()
		{
			if (!this._isRegistered)
			{
				this._eventBus.Register(this);
				this._buildingWithRangeUpdateService.AddPreview(this._buildingWithRange, this._preview);
				this._isRegistered = true;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021AE File Offset: 0x000003AE
		public void UnregisterOnToolExited()
		{
			if (this._isRegistered)
			{
				this._eventBus.Unregister(this);
				this._buildingWithRangeUpdateService.RemovePreview();
				this._isRegistered = false;
				this._drawArea = false;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021DD File Offset: 0x000003DD
		public void DrawArea()
		{
			if (this._drawArea)
			{
				this._buildingWithRangeUpdateService.DrawArea();
				this._drawArea = false;
			}
		}

		// Token: 0x04000008 RID: 8
		public readonly BuildingWithRangeUpdateService _buildingWithRangeUpdateService;

		// Token: 0x04000009 RID: 9
		public readonly EventBus _eventBus;

		// Token: 0x0400000A RID: 10
		public BlockObject _blockObject;

		// Token: 0x0400000B RID: 11
		public Preview _preview;

		// Token: 0x0400000C RID: 12
		public IBuildingWithRange _buildingWithRange;

		// Token: 0x0400000D RID: 13
		public bool _isRegistered;

		// Token: 0x0400000E RID: 14
		public bool _drawArea;
	}
}
