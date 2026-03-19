using System;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.WalkingSystem
{
	// Token: 0x0200001C RID: 28
	public class WalkerService
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00003A5F File Offset: 0x00001C5F
		public WalkerService(INavigationService navigationService)
		{
			this._navigationService = navigationService;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003A70 File Offset: 0x00001C70
		public Vector3 ClosestPositionOnNavMesh(Vector3 position)
		{
			Vector3? vector = this._navigationService.ClosestPositionOnNavMesh(position, 50f);
			if (vector == null)
			{
				throw new InvalidOperationException(string.Format("Couldn't find valid position on NavMesh in {0} radius around {1}", 50f, position));
			}
			return vector.Value;
		}

		// Token: 0x04000059 RID: 89
		public readonly INavigationService _navigationService;
	}
}
