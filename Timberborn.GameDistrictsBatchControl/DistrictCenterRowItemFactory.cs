using System;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.SelectionSystem;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsBatchControl
{
	// Token: 0x02000005 RID: 5
	public class DistrictCenterRowItemFactory
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020FB File Offset: 0x000002FB
		public DistrictCenterRowItemFactory(EntitySelectionService entitySelectionService, VisualElementLoader visualElementLoader)
		{
			this._entitySelectionService = entitySelectionService;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002114 File Offset: 0x00000314
		public IBatchControlRowItem Create(DistrictCenter districtCenter)
		{
			string elementName = "Game/BatchControl/DistrictCenterRowItem";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			LabeledEntity component = districtCenter.GetComponent<LabeledEntity>();
			UQueryExtensions.Q<Image>(visualElement, "Image", null).sprite = component.Image;
			UQueryExtensions.Q<Button>(visualElement, "Select", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._entitySelectionService.SelectAndFocusOn(districtCenter);
			}, 0);
			return new DistrictCenterRowItem(visualElement, districtCenter, UQueryExtensions.Q<Label>(visualElement, "Text", null));
		}

		// Token: 0x04000009 RID: 9
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x0400000A RID: 10
		public readonly VisualElementLoader _visualElementLoader;
	}
}
