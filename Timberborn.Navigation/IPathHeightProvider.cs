using System;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000041 RID: 65
	public interface IPathHeightProvider
	{
		// Token: 0x0600015B RID: 347
		bool TryGetHeight(Vector3 worldPosition, out float pathHeight);
	}
}
