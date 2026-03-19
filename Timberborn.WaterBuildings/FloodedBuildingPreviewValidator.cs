using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Localization;
using Timberborn.WaterObjects;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200000F RID: 15
	public class FloodedBuildingPreviewValidator : BaseComponent, IAwakableComponent, IPreviewValidator
	{
		// Token: 0x0600007E RID: 126 RVA: 0x0000311A File Offset: 0x0000131A
		public FloodedBuildingPreviewValidator(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003129 File Offset: 0x00001329
		public void Awake()
		{
			this._floodableObject = base.GetComponent<FloodableObject>();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003137 File Offset: 0x00001337
		public bool IsValid(out string warningMessage)
		{
			warningMessage = this._loc.T(FloodedBuildingPreviewValidator.BuildingPreviewFloodedLocKey);
			return !this._floodableObject.IsPreviewFlooded();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003159 File Offset: 0x00001359
		public ReadOnlyHashSet<BaseComponent> InvalidatedObjects(out string warningMessage)
		{
			warningMessage = string.Empty;
			return FloodedBuildingPreviewValidator.EmptyHashSet;
		}

		// Token: 0x0400002D RID: 45
		public static readonly string BuildingPreviewFloodedLocKey = "Buildings.PreviewFlooded";

		// Token: 0x0400002E RID: 46
		public static readonly ReadOnlyHashSet<BaseComponent> EmptyHashSet = new HashSet<BaseComponent>().AsReadOnlyHashSet<BaseComponent>();

		// Token: 0x0400002F RID: 47
		public readonly ILoc _loc;

		// Token: 0x04000030 RID: 48
		public FloodableObject _floodableObject;
	}
}
