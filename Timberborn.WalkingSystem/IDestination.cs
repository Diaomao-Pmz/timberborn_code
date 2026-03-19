using System;
using System.Collections.Generic;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.WalkingSystem
{
	// Token: 0x02000009 RID: 9
	public interface IDestination
	{
		// Token: 0x06000017 RID: 23
		bool FindPath(Vector3 start, List<PathCorner> pathCorners, out float distance);
	}
}
