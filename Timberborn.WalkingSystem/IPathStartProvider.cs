using System;
using System.Collections.Generic;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.WalkingSystem
{
	// Token: 0x0200000B RID: 11
	public interface IPathStartProvider
	{
		// Token: 0x06000019 RID: 25
		bool TryGetPathStart(IDestination destination, List<PathCorner> pathCorners, out Vector3 start);
	}
}
