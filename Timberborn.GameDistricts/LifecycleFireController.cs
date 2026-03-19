using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Buildings;

namespace Timberborn.GameDistricts
{
	// Token: 0x0200002A RID: 42
	public class LifecycleFireController : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00004790 File Offset: 0x00002990
		public void Awake()
		{
			FireIntensityController fireIntensityController = base.GetComponent<FireIntensityController>();
			DistrictCitizenLifecycleNotifier component = base.GetComponent<DistrictCitizenLifecycleNotifier>();
			component.BeaverBorn += delegate(object _, Citizen _)
			{
				fireIntensityController.Strengthen();
			};
			component.BeaverDied += delegate(object _, Citizen _)
			{
				fireIntensityController.Dampen();
			};
		}
	}
}
