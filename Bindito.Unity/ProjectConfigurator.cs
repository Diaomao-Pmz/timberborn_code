using System;
using Bindito.Core;
using Bindito.Unity.Internal;
using JetBrains.Annotations;
using UnityEngine;

namespace Bindito.Unity
{
	// Token: 0x02000071 RID: 113
	public class ProjectConfigurator : MonoBehaviour, IConfigurator
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00002AEC File Offset: 0x00000CEC
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public IContainer ProjectContainer { get; private set; }

		// Token: 0x060000F4 RID: 244 RVA: 0x00002B00 File Offset: 0x00000D00
		public void Configure(IContainerDefinition containerDefinition)
		{
			foreach (string contextName in this.Contexts)
			{
				containerDefinition.InstallAll(contextName);
			}
			containerDefinition.Bind<ISceneProvider>().ToInstance(new SceneProvider(base.gameObject.scene));
			containerDefinition.Bind<IInstantiator>().To<Instantiator>().AsSingleton();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00002B5A File Offset: 0x00000D5A
		[UsedImplicitly]
		private void Awake()
		{
			this.MoveToDontDestroyOnLoadScene();
			this.CreateProjectContainer();
			this.InjectIntoSelfAndChildren();
			this.RunSceneInitializers();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00002B74 File Offset: 0x00000D74
		private void MoveToDontDestroyOnLoadScene()
		{
			Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00002B81 File Offset: 0x00000D81
		private void CreateProjectContainer()
		{
			this.ProjectContainer = Bindito.CreateContainer(new IConfigurator[]
			{
				this
			});
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00002B98 File Offset: 0x00000D98
		private void InjectIntoSelfAndChildren()
		{
			foreach (GameObject injectee in base.gameObject.GetSelfAndChildren())
			{
				this.InjectIntoAllComponents(injectee);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00002BEC File Offset: 0x00000DEC
		private void InjectIntoAllComponents(GameObject injectee)
		{
			foreach (MonoBehaviour instance in injectee.GetComponents<MonoBehaviour>())
			{
				this.ProjectContainer.Inject(instance);
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00002C20 File Offset: 0x00000E20
		private void RunSceneInitializers()
		{
			foreach (ISceneInitializer sceneInitializer in this.ProjectContainer.GetInstances<ISceneInitializer>())
			{
				sceneInitializer.InitializeScene();
			}
		}

		// Token: 0x0400006B RID: 107
		[SerializeField]
		public string[] Contexts = new string[0];
	}
}
