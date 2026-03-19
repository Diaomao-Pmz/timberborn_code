using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x0200005B RID: 91
	public class VisualElementInitializer
	{
		// Token: 0x0600017C RID: 380 RVA: 0x00005F75 File Offset: 0x00004175
		public VisualElementInitializer(IEnumerable<IVisualElementInitializer> visualElementInitializers)
		{
			this._visualElementInitializers = visualElementInitializers.ToList<IVisualElementInitializer>();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005F8C File Offset: 0x0000418C
		public void InitializeVisualElement(VisualElement visualElement)
		{
			this.InitializeWithEveryInitializer(visualElement);
			foreach (VisualElement visualElement2 in visualElement.hierarchy.Children())
			{
				this.InitializeVisualElement(visualElement2);
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005FE8 File Offset: 0x000041E8
		public void InitializeWithEveryInitializer(VisualElement visualElement)
		{
			for (int i = 0; i < this._visualElementInitializers.Count; i++)
			{
				this._visualElementInitializers[i].InitializeVisualElement(visualElement);
			}
		}

		// Token: 0x040000CE RID: 206
		public readonly List<IVisualElementInitializer> _visualElementInitializers;
	}
}
