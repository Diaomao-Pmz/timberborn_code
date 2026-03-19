using System;
using Timberborn.BaseComponentSystem;

namespace Timberborn.Yielding
{
	// Token: 0x0200000C RID: 12
	public interface IYielderRetriever
	{
		// Token: 0x0600002F RID: 47
		bool TryGetYielder(BaseComponent component, out Yielder yielder);
	}
}
