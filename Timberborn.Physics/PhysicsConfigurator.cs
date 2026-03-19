using System;
using Bindito.Core;
using Bindito.Unity;

namespace Timberborn.Physics
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	[Context("MapEditor")]
	public class PhysicsConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public override void Configure()
		{
			base.MultiBind<ISceneInitializer>().To<InstantiatingSceneInitializer<TransformSyncServiceUnityAdapter>>().AsSingleton();
		}
	}
}
