using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.Fields;
using UnityEngine.UIElements;

namespace Timberborn.FieldsUI
{
	// Token: 0x02000005 RID: 5
	public class FarmHouseBatchControlRowItemFactory
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020EA File Offset: 0x000002EA
		public FarmHouseBatchControlRowItemFactory(VisualElementLoader visualElementLoader, FarmHouseToggleFactory farmHouseToggleFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._farmHouseToggleFactory = farmHouseToggleFactory;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			FarmHouse component = entity.GetComponent<FarmHouse>();
			if (component != null)
			{
				string elementName = "Game/BatchControl/SelectionToggleBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				FarmHouseToggle farmHouseToggle = this._farmHouseToggleFactory.Create(visualElement);
				farmHouseToggle.Show(component);
				return new FarmHouseBatchControlRowItem(visualElement, farmHouseToggle);
			}
			return null;
		}

		// Token: 0x04000008 RID: 8
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000009 RID: 9
		public readonly FarmHouseToggleFactory _farmHouseToggleFactory;
	}
}
