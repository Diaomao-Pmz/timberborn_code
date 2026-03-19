using System;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.InventorySystemUI
{
	// Token: 0x02000012 RID: 18
	public class InventoryFragmentBuilderFactory
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002E5F File Offset: 0x0000105F
		public InventoryFragmentBuilderFactory(VisualElementLoader visualElementLoader, InformationalRowsFactory informationalRowsFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._informationalRowsFactory = informationalRowsFactory;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E75 File Offset: 0x00001075
		public InventoryFragment.Builder CreateBuilder(VisualElement root)
		{
			return new InventoryFragment.Builder(this._visualElementLoader, this._informationalRowsFactory, root);
		}

		// Token: 0x04000046 RID: 70
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000047 RID: 71
		public readonly InformationalRowsFactory _informationalRowsFactory;
	}
}
