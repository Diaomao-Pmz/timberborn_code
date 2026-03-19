using System;
using UnityEngine;

namespace Bindito.Unity
{
	// Token: 0x0200006C RID: 108
	public interface IInstantiator
	{
		// Token: 0x060000DC RID: 220
		T Instantiate<T>(T prefab, Transform parent) where T : Component;

		// Token: 0x060000DD RID: 221
		T Instantiate<T>(T prefab) where T : Component;

		// Token: 0x060000DE RID: 222
		GameObject Instantiate(GameObject prefab, Transform parent);

		// Token: 0x060000DF RID: 223
		GameObject InstantiateInactive(GameObject prefab, Transform parent, out bool wasActive);

		// Token: 0x060000E0 RID: 224
		GameObject InstantiateEmpty(string name);

		// Token: 0x060000E1 RID: 225
		GameObject InstantiateEmpty(string name, Transform parent);

		// Token: 0x060000E2 RID: 226
		T AddComponent<T>(GameObject gameObject) where T : Component;

		// Token: 0x060000E3 RID: 227
		Component AddComponent(GameObject gameObject, Type componentType);
	}
}
