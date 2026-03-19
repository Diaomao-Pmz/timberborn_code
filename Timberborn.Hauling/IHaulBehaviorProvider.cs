using System;
using System.Collections.Generic;

namespace Timberborn.Hauling
{
	// Token: 0x02000011 RID: 17
	public interface IHaulBehaviorProvider
	{
		// Token: 0x0600004B RID: 75
		void GetWeightedBehaviors(IList<WeightedBehavior> weightedBehaviors);
	}
}
