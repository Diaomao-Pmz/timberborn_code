using System;
using System.Collections.Generic;
using System.Linq;
using Bindito.Core;
using Bindito.Unity.Internal;
using JetBrains.Annotations;
using UnityEngine;

namespace Bindito.Unity
{
	// Token: 0x02000072 RID: 114
	public class SceneConfigurator : MonoBehaviour, IConfigurator
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00002C84 File Offset: 0x00000E84
		public void Configure(IContainerDefinition containerDefinition)
		{
			foreach (string contextName in this.Contexts)
			{
				containerDefinition.InstallAll(contextName);
			}
			containerDefinition.Bind<ISceneProvider>().ToInstance(new SceneProvider(base.gameObject.scene));
			containerDefinition.Bind<IInstantiator>().To<Instantiator>().AsSingleton();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00002CE0 File Offset: 0x00000EE0
		[UsedImplicitly]
		protected T GetInstanceFromScene<T>()
		{
			return base.gameObject.scene.GetRootGameObjects().SelectMany(delegate(GameObject rootGameObject)
			{
				IEnumerable<T> componentsInChildren = rootGameObject.GetComponentsInChildren<T>(true);
				return componentsInChildren ?? Enumerable.Empty<T>();
			}).Single<T>();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00002D29 File Offset: 0x00000F29
		[UsedImplicitly]
		private void Awake()
		{
			if (!this.ProjectConfiguratorPrefab)
			{
				throw new InvalidOperationException("ProjectConfiguratorPrefab has to be set");
			}
			this.CreateSceneContainer();
			this.InjectIntoEveryObjectInScene();
			this.RunSceneInitializers();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00002D58 File Offset: 0x00000F58
		private void CreateSceneContainer()
		{
			IContainer projectContainer = new ProjectContainerProvider().GetProjectContainer(this.ProjectConfiguratorPrefab);
			this._sceneContainer = projectContainer.CreateChildContainer(new IConfigurator[]
			{
				this
			});
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00002D8C File Offset: 0x00000F8C
		private void InjectIntoEveryObjectInScene()
		{
			GameObject[] rootGameObjects = base.gameObject.scene.GetRootGameObjects();
			HashSet<object> hashSet = new HashSet<object>(this._sceneContainer.GetBoundInstances());
			GameObject[] array = rootGameObjects;
			for (int i = 0; i < array.Length; i++)
			{
				foreach (GameObject gameObject in array[i].GetSelfAndChildren())
				{
					foreach (MonoBehaviour monoBehaviour in gameObject.GetComponents<MonoBehaviour>())
					{
						if (!hashSet.Contains(monoBehaviour))
						{
							this._sceneContainer.Inject(monoBehaviour);
						}
					}
				}
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00002E44 File Offset: 0x00001044
		private void RunSceneInitializers()
		{
			foreach (ISceneInitializer sceneInitializer in this._sceneContainer.GetInstances<ISceneInitializer>())
			{
				sceneInitializer.InitializeScene();
			}
		}

		// Token: 0x0400006D RID: 109
		[SerializeField]
		public ProjectConfigurator ProjectConfiguratorPrefab;

		// Token: 0x0400006E RID: 110
		[SerializeField]
		public string[] Contexts = new string[0];

		// Token: 0x0400006F RID: 111
		private IContainer _sceneContainer;
	}
}
