using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000042 RID: 66
	public interface IPathToAccessibleModifier
	{
		// Token: 0x0600015C RID: 348
		void ModifyPath(Vector3 start, List<PathCorner> pathCorners, ref float distance);
	}
}
