using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Yielding;

namespace Timberborn.Ruins
{
	// Token: 0x02000016 RID: 22
	public class ScavengerYielderRetriever : BaseComponent, IYielderRetriever
	{
		// Token: 0x06000090 RID: 144 RVA: 0x0000337C File Offset: 0x0000157C
		public bool TryGetYielder(BaseComponent component, out Yielder yielder)
		{
			Ruin component2 = component.GetComponent<Ruin>();
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
