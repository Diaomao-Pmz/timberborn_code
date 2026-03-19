using System;
using Timberborn.BaseComponentSystem;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x02000021 RID: 33
	public class StartableMarkerPositionUpdater : BaseComponent, IStartableComponent
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x0000457C File Offset: 0x0000277C
		public void Start()
		{
			base.GetComponent<MarkerPosition>().UpdatePosition(default(Vector3));
		}
	}
}
