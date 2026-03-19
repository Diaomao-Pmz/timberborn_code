using System;
using Bindito.Core;
using Bindito.Unity.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bindito.Unity
{
	// Token: 0x0200006E RID: 110
	public class Instantiator : IInstantiator
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00002965 File Offset: 0x00000B65
		public Instantiator(IContainer container, ISceneProvider sceneProvider)
		{
			this._container = container;
			this._sceneProvider = sceneProvider;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000297B File Offset: 0x00000B7B
		public T Instantiate<T>(T prefab, Transform parent) where T : Component
		{
			return this.Instantiate(prefab.gameObject, parent).GetComponent<T>();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00002994 File Offset: 0x00000B94
		public T Instantiate<T>(T prefab) where T : Component
		{
			return this.Instantiate<T>(prefab, null);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000029A0 File Offset: 0x00000BA0
		public GameObject Instantiate(GameObject prefab, Transform parent)
		{
			bool active;
			GameObject gameObject = this.InstantiateInactive(prefab, parent, out active);
			gameObject.SetActive(active);
			return gameObject;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000029C0 File Offset: 0x00000BC0
		public GameObject InstantiateInactive(GameObject prefab, Transform parent, out bool wasActive)
		{
			GameObject gameObject = InactiveParent.InstantiatePrefabUnderInactiveParent(prefab);
			wasActive = gameObject.activeSelf;
			gameObject.SetActive(false);
			gameObject.transform.SetParent(parent, false);
			if (parent == null)
			{
				SceneManager.MoveGameObjectToScene(gameObject, this._sceneProvider.Scene);
			}
			this.InjectIntoObjectAndChildren(gameObject);
			return gameObject;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00002A0C File Offset: 0x00000C0C
		public GameObject InstantiateEmpty(string name)
		{
			return this.InstantiateEmpty(name, null);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00002A16 File Offset: 0x00000C16
		public GameObject InstantiateEmpty(string name, Transform parent)
		{
			GameObject gameObject = new GameObject(name, Array.Empty<Type>());
			SceneManager.MoveGameObjectToScene(gameObject, this._sceneProvider.Scene);
			return gameObject;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00002A34 File Offset: 0x00000C34
		public T AddComponent<T>(GameObject gameObject) where T : Component
		{
			T t = gameObject.AddComponent<T>();
			this._container.Inject(t);
			return t;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00002A5C File Offset: 0x00000C5C
		public Component AddComponent(GameObject gameObject, Type componentType)
		{
			Component component = gameObject.AddComponent(componentType);
			this._container.Inject(component);
			return component;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00002A80 File Offset: 0x00000C80
		private void InjectIntoObjectAndChildren(GameObject gameObject)
		{
			foreach (GameObject gameObject2 in gameObject.GetSelfAndChildren())
			{
				foreach (MonoBehaviour instance in gameObject2.GetComponents<MonoBehaviour>())
				{
					this._container.Inject(instance);
				}
			}
		}

		// Token: 0x04000069 RID: 105
		private readonly IContainer _container;

		// Token: 0x0400006A RID: 106
		private readonly ISceneProvider _sceneProvider;
	}
}
