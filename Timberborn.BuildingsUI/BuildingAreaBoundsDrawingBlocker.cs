using System;
using Timberborn.AreaSelectionSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.Buildings;

namespace Timberborn.BuildingsUI
{
	// Token: 0x02000005 RID: 5
	public class BuildingAreaBoundsDrawingBlocker : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000022E8 File Offset: 0x000004E8
		public void Awake()
		{
			BuildingSpec component = base.GetComponent<BuildingSpec>();
			if (component != null && component.DrawRangeBoundsOnIt)
			{
				base.GetComponent<AreaBoundsDrawingBlocker>().DisableBlocking();
			}
		}
	}
}
