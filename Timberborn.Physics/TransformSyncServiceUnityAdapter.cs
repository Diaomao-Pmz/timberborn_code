using System;
using UnityEngine;

namespace Timberborn.Physics
{
	// Token: 0x02000005 RID: 5
	public class TransformSyncServiceUnityAdapter : MonoBehaviour
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D6 File Offset: 0x000002D6
		public void LateUpdate()
		{
			Physics.SyncTransforms();
		}
	}
}
