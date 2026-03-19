using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Yielding;

namespace Timberborn.Gathering
{
	// Token: 0x02000010 RID: 16
	public class GathererFlagYielderRetriever : BaseComponent, IYielderRetriever
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00002BFC File Offset: 0x00000DFC
		public bool TryGetYielder(BaseComponent component, out Yielder yielder)
		{
			Gatherable component2 = component.GetComponent<Gatherable>();
			if (component2 != null)
			{
				yielder = component2.Yielder;
				return true;
			}
			yielder = null;
			return false;
		}
	}
}
