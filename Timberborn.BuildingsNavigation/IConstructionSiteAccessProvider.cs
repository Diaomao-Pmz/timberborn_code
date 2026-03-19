using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x02000019 RID: 25
	public interface IConstructionSiteAccessProvider
	{
		// Token: 0x060000A5 RID: 165
		IEnumerable<Vector3> GetAccesses();
	}
}
