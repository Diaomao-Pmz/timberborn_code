using System;
using UnityEngine;

namespace Timberborn.RootProviders
{
	// Token: 0x02000004 RID: 4
	public class RootObjectProvider
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public GameObject CreateRootObject(string name)
		{
			return new GameObject(name);
		}
	}
}
