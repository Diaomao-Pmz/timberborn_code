using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000033 RID: 51
	public class PlaceDynamiteAtBottomTracker : BaseComponent, IAwakableComponent, IPostPlacementChangeListener
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00004028 File Offset: 0x00002228
		public PlaceDynamiteAtBottomTracker(PlaceDynamiteAtBottomAchievement placeDynamiteAtBottomAchievement)
		{
			this._placeDynamiteAtBottomAchievement = placeDynamiteAtBottomAchievement;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004037 File Offset: 0x00002237
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004048 File Offset: 0x00002248
		public void OnPostPlacementChanged()
		{
			if (this._blockObject.IsPreview && this._placeDynamiteAtBottomAchievement.IsEnabled && this._blockObject.CoordinatesAtBaseZ.z == 0 && base.GameObject.activeInHierarchy)
			{
				this._placeDynamiteAtBottomAchievement.Unlock();
			}
		}

		// Token: 0x0400007C RID: 124
		public readonly PlaceDynamiteAtBottomAchievement _placeDynamiteAtBottomAchievement;

		// Token: 0x0400007D RID: 125
		public BlockObject _blockObject;
	}
}
