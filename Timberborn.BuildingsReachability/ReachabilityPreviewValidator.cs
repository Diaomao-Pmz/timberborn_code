using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Localization;

namespace Timberborn.BuildingsReachability
{
	// Token: 0x0200000F RID: 15
	public class ReachabilityPreviewValidator : BaseComponent, IAwakableComponent, IPreviewValidator
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002625 File Offset: 0x00000825
		public ReachabilityPreviewValidator(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002634 File Offset: 0x00000834
		public void Awake()
		{
			this._reachableConstructionSite = base.GetComponent<ReachableConstructionSite>();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002642 File Offset: 0x00000842
		public bool IsValid(out string warningMessage)
		{
			warningMessage = this._loc.T(ReachabilityPreviewValidator.UnreachableObjectLocKey);
			return !this._reachableConstructionSite.IsUnreachable();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002664 File Offset: 0x00000864
		public ReadOnlyHashSet<BaseComponent> InvalidatedObjects(out string warningMessage)
		{
			warningMessage = null;
			return ReachabilityPreviewValidator.EmptyObjects;
		}

		// Token: 0x0400001C RID: 28
		public static readonly ReadOnlyHashSet<BaseComponent> EmptyObjects = new HashSet<BaseComponent>().AsReadOnlyHashSet<BaseComponent>();

		// Token: 0x0400001D RID: 29
		public static readonly string UnreachableObjectLocKey = "Status.Object.UnreachableObject";

		// Token: 0x0400001E RID: 30
		public readonly ILoc _loc;

		// Token: 0x0400001F RID: 31
		public ReachableConstructionSite _reachableConstructionSite;
	}
}
