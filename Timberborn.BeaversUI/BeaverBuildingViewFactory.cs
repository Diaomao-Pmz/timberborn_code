using System;
using Timberborn.SelectionSystem;
using UnityEngine.UIElements;

namespace Timberborn.BeaversUI
{
	// Token: 0x0200000C RID: 12
	public class BeaverBuildingViewFactory
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002AF2 File Offset: 0x00000CF2
		public BeaverBuildingViewFactory(EntitySelectionService entitySelectionService)
		{
			this._entitySelectionService = entitySelectionService;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002B04 File Offset: 0x00000D04
		public BeaverBuildingView Create(Button root)
		{
			Image buildingImage = UQueryExtensions.Q<Image>(root, "Icon", null);
			Label description = UQueryExtensions.Q<Label>(root, "Name", null);
			return new BeaverBuildingView(this._entitySelectionService, root, buildingImage, description);
		}

		// Token: 0x04000039 RID: 57
		public readonly EntitySelectionService _entitySelectionService;
	}
}
