using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Hauling;

namespace Timberborn.Emptying
{
	// Token: 0x02000008 RID: 8
	public class EmptiableHaulBehaviorProvider : BaseComponent, IAwakableComponent, IHaulBehaviorProvider
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002619 File Offset: 0x00000819
		public void Awake()
		{
			this._emptiable = base.GetComponent<Emptiable>();
			this._emptyInventoriesWorkplaceBehavior = base.GetComponent<EmptyInventoriesWorkplaceBehavior>();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002633 File Offset: 0x00000833
		public void GetWeightedBehaviors(IList<WeightedBehavior> weightedBehaviors)
		{
			if (this._emptiable.IsMarkedForEmptying)
			{
				weightedBehaviors.Add(new WeightedBehavior(EmptiableHaulBehaviorProvider.EmptiableWeight, this._emptyInventoriesWorkplaceBehavior));
			}
		}

		// Token: 0x04000013 RID: 19
		public static readonly float EmptiableWeight = 0.51f;

		// Token: 0x04000014 RID: 20
		public Emptiable _emptiable;

		// Token: 0x04000015 RID: 21
		public EmptyInventoriesWorkplaceBehavior _emptyInventoriesWorkplaceBehavior;
	}
}
