using System;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.PathSystem
{
	// Token: 0x02000012 RID: 18
	public interface IConnectionService
	{
		// Token: 0x06000075 RID: 117
		bool CanConnectInDirection(Vector3Int origin, Direction2D direction2D);
	}
}
