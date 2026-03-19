using System;
using UnityEngine;

namespace Timberborn.PathSystem
{
	// Token: 0x02000013 RID: 19
	public interface IPathConnectionEnforcer
	{
		// Token: 0x06000076 RID: 118
		bool CanConnectPath(Vector3Int origin, Vector3Int target);
	}
}
