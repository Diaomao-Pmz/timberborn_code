using System;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.DropdownSystem
{
	// Token: 0x02000009 RID: 9
	public class DropdownInitializer : IVisualElementInitializer
	{
		// Token: 0x06000026 RID: 38 RVA: 0x0000280C File Offset: 0x00000A0C
		public DropdownInitializer(DropdownListDrawer dropdownListDrawer)
		{
			this._dropdownListDrawer = dropdownListDrawer;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000281C File Offset: 0x00000A1C
		public void InitializeVisualElement(VisualElement visualElement)
		{
			Dropdown dropdown = visualElement as Dropdown;
			if (dropdown != null)
			{
				dropdown.Initialize(this._dropdownListDrawer);
			}
		}

		// Token: 0x0400001F RID: 31
		public readonly DropdownListDrawer _dropdownListDrawer;
	}
}
