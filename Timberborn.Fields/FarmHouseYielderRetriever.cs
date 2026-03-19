using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Yielding;

namespace Timberborn.Fields
{
	// Token: 0x0200000E RID: 14
	public class FarmHouseYielderRetriever : BaseComponent, IYielderRetriever
	{
		// Token: 0x06000043 RID: 67 RVA: 0x000026E8 File Offset: 0x000008E8
		public bool TryGetYielder(BaseComponent component, out Yielder yielder)
		{
			Crop component2 = component.GetComponent<Crop>();
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
