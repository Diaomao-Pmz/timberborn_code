using System;
using Bindito.Core;
using UnityEngine;

namespace Bindito.Unity.Internal
{
	// Token: 0x02000075 RID: 117
	public class ProjectContainerProvider
	{
		// Token: 0x0600010A RID: 266 RVA: 0x00002F37 File Offset: 0x00001137
		public IContainer GetProjectContainer(ProjectConfigurator projectConfiguratorPrefab)
		{
			return (Object.FindObjectOfType<ProjectConfigurator>() ?? ProjectContainerProvider.InstantiateProjectConfigurator(projectConfiguratorPrefab)).ProjectContainer;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00002F4D File Offset: 0x0000114D
		private static ProjectConfigurator InstantiateProjectConfigurator(ProjectConfigurator projectConfiguratorPrefab)
		{
			ProjectConfigurator projectConfigurator = Object.Instantiate<ProjectConfigurator>(projectConfiguratorPrefab);
			projectConfigurator.name = projectConfiguratorPrefab.name;
			return projectConfigurator;
		}
	}
}
