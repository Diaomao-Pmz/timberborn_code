using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BuildingsReachability;

namespace Timberborn.LinkedBuildingSystem
{
	// Token: 0x0200000B RID: 11
	public class LinkedConstructionSiteReachability : BaseComponent, IAwakableComponent, IExpandedConstructionSiteReachability
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002853 File Offset: 0x00000A53
		public LinkedConstructionSiteReachability(PreviewBlockService previewBlockService)
		{
			this._previewBlockService = previewBlockService;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000286D File Offset: 0x00000A6D
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._reachableConstructionSite = base.GetComponent<ReachableConstructionSite>();
			base.GetComponent<LinkedBuilding>().BuildingLinked += this.OnBuildingLinked;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000289E File Offset: 0x00000A9E
		public bool IsReachable()
		{
			if (!this._mirrorOperationLock.IsUnlocked)
			{
				return false;
			}
			if (!this._blockObject.IsPreview)
			{
				return this._linked.IsReachableWithoutMirroring();
			}
			return this.IsPreviewReachable();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000028CE File Offset: 0x00000ACE
		public void OnBuildingLinked(object sender, LinkedBuilding e)
		{
			this._linked = e.GetComponent<LinkedConstructionSiteReachability>();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028DC File Offset: 0x00000ADC
		public bool IsPreviewReachable()
		{
			return this._previewBlockService.GetBottomObjectComponentAt<LinkedConstructionSiteReachability>(this._blockObject.CoordinatesBehind()).IsReachableWithoutMirroring();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000028FC File Offset: 0x00000AFC
		public bool IsReachableWithoutMirroring()
		{
			bool result;
			using (this._mirrorOperationLock.Lock())
			{
				result = this._reachableConstructionSite.IsReachableByBuilders();
			}
			return result;
		}

		// Token: 0x04000014 RID: 20
		public readonly PreviewBlockService _previewBlockService;

		// Token: 0x04000015 RID: 21
		public BlockObject _blockObject;

		// Token: 0x04000016 RID: 22
		public ReachableConstructionSite _reachableConstructionSite;

		// Token: 0x04000017 RID: 23
		public LinkedConstructionSiteReachability _linked;

		// Token: 0x04000018 RID: 24
		public readonly MirrorOperationLock _mirrorOperationLock = new MirrorOperationLock();
	}
}
